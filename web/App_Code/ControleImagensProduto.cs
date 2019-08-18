using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for ControleImagensProduto
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ControleImagensProduto : System.Web.Services.WebService
{

    private Repository<ImagemProduto> RepositorioFotos
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Album> RepositorioAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

   [WebMethod(EnableSession = true)]
   public string getImagensDados(int idAlbum)
   {
      try
      {
         ControleLogin.statusLoginGerenciador();

         string listaImagens = "";
         Album album = RepositorioAlbum.FindBy(idAlbum);

         if (album != null)
            foreach (ImagemProduto foto in album.Imagens)
            {
               listaImagens += "<li id=\"" + foto.Id + "\"><img src=\"" + MetodosFE.BaseURL + "/ImagensLQ/" + foto.Nome + "\" > <img src=\"" + MetodosFE.BaseURL + "/Controle/comum/img/close-button.png\" class=\"botaoExcluirImagem\" rel=\"" + foto.Id + "\" ></li>";
            }
         return listaImagens;
      }
      catch (Exception ex)
      {
         //
      }

      return "";
   }

   [WebMethod(EnableSession = true)]
   public string excluirImagemDados(int idImagem)
   {
      try
      {
         ControleLogin.statusLoginGerenciador();

         if (idImagem > 0)
         {
            ImagemProduto foto = RepositorioFotos.FindBy(idImagem);
            Album album = RepositorioAlbum.FindBy(x => x.Imagens.Any(y => y.Id == foto.Id));
            album.Imagens.Remove(foto);

            album.Primeira = album.GetImagensOrdenadas().FirstOrDefault();

            RepositorioAlbum.Update(album);

            foto.ExcluirArquivos();
            RepositorioFotos.Delete(foto);

            return getImagensDados(foto.Album.Id);

         }
      }
      catch (Exception ex)
      {
         //
      }

      return "";
   }
   [WebMethod(EnableSession = true)]
   public string salvarOrdem(string[] ids)
   {
      try
      {
         ControleLogin.statusLoginGerenciador();
         if (ids.Length > 300)
            throw new Exception("Quantidade acima do normal");
         int count = 0;
         ImagemProduto primeira = null;
         int idAlbum = 0;
         //Album album = RepositorioAlbum.FindBy(idAlbum);

         foreach (string id in ids)
         {

            int idInt = Convert.ToInt32(id);
            ImagemProduto foto = RepositorioFotos.FindBy(idInt);
            if (count == 0)
            {
               primeira = foto;
               idAlbum = foto.Album.Id;
            }
            foto.Ordem = count++;

            RepositorioFotos.Update(foto);



         }
         Album album = RepositorioAlbum.FindBy(primeira.Album.Id);
         album.Primeira = primeira;
         RepositorioAlbum.Update(album);
      }
      catch (Exception ex)
      {
         return ex.Message;
      }

      return "Ordem de imagens salva.";
   }



}
