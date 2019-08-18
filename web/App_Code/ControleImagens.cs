using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for ControleImagens
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ControleImagens : System.Web.Services.WebService
{

    private Repository<ImagemDadoVO> repFotos
    {
        get
        {
            return new Repository<ImagemDadoVO>(NHibernateHelper.CurrentSession);
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
    public string getImagensDados(int idDado)
    {
        try
        {
            ControleLogin.statusLoginGerenciador();

            string listaImagens = "";
            IList<ImagemDadoVO> fotos = repFotos.All().Where(x => x.dado.Id == idDado).OrderBy(x => x.Ordem).ToList();
            foreach (ImagemDadoVO foto in fotos)
            {
                listaImagens += "<li id=\"" + foto.Id + "\"><img src=\"" + MetodosFE.BaseURL + "/ImagensLQ/" + foto.Nome + "\" > <img src=\"" + MetodosFE.BaseURL + "/Controle/comum/img/close-button.png\" class=\"botaoExcluirImagem\" rel=\"" + foto.Id + "\" ></li>";
            }
            return listaImagens;
        }
        catch (Exception)
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
                ImagemDadoVO foto = repFotos.FindBy(idImagem);
                if (foto != null)
                {
                    foto.ExcluirArquivos();
                    repFotos.Delete(foto);

                    return getImagensDados(foto.dado.Id);
                }

                Album album = RepositorioAlbum.FindBy(x => x.Imagens.Any(y => y.Id == idImagem));

                album.Primeira = album.GetImagensOrdenadas().FirstOrDefault();

                RepositorioAlbum.Update(album);

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
            ImagemDadoVO imagem = null;
            foreach (string id in ids)
            {
                int idInt = Convert.ToInt32(id);
                ImagemDadoVO foto = repFotos.FindBy(idInt);
                foto.Ordem = count++;
                repFotos.Update(foto);
            }

            //Album album = RepositorioAlbum.FindBy(x => x.Imagens.Any(y=>y.Id == imagem.Id));

            //album.Primeira = album.GetImagensOrdenadas().FirstOrDefault();

            //RepositorioAlbum.Update(album);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "Ordem de imagens salva.";
    }

}
