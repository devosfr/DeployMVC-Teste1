using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for ControleImagensSegFilho
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ControleImagensSegFilho : System.Web.Services.WebService {

    private Repository<ImagemSegFilhoVO> repFotos
    {
        get
        {
            return new Repository<ImagemSegFilhoVO>(NHibernateHelper.CurrentSession);
        }
    }

    [WebMethod(EnableSession = true)]
    public string getImagensDados(int idDado)
    {
        try
        {
            ControleLogin.statusLoginGerenciador();

            string listaImagens = "";
            IList<ImagemSegFilhoVO> fotos = repFotos.FilterBy(x => x.segFilho.Id == idDado).OrderBy(x => x.Ordem).ToList();
            foreach (ImagemSegFilhoVO foto in fotos)
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
                ImagemSegFilhoVO foto = repFotos.FindBy(idImagem);
                if (foto != null)
                {
                    foto.ExcluirArquivos();
                    repFotos.Delete(foto);

                    return getImagensDados(foto.segFilho.Id);
                }

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
            foreach (string id in ids)
            {
                int idInt = Convert.ToInt32(id);
                ImagemSegFilhoVO foto = repFotos.FindBy(idInt);
                foto.Ordem = count++;
                repFotos.Update(foto);
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "Ordem de imagens salva.";
    }
    
}
