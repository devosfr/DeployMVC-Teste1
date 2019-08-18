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
public class ControleImagensDestaque : System.Web.Services.WebService
{

    private Repository<Destaque> RepositorioDestaque
    {
        get
        {
            return new Repository<Destaque>(NHibernateHelper.CurrentSession);
        }
    }

    [WebMethod(EnableSession = true)]
    public string getImagensDados(int idDado)
    {
        try
        {
            ControleLogin.statusLoginGerenciador();

            string listaImagens = "";
            Destaque foto = RepositorioDestaque.FindBy(x => x.Id == idDado);
            if (!String.IsNullOrEmpty(foto.Imagem))
                listaImagens += "<li id=\"" + foto.Id + "\"><img src=\"" + MetodosFE.BaseURL + "/ImagensLQ/" + foto.Imagem + "\" > <img src=\"" + MetodosFE.BaseURL + "/Controle/comum/img/close-button.png\" class=\"botaoExcluirImagem\" rel=\"" + foto.Id + "\" ></li>";

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
                Destaque foto = RepositorioDestaque.FindBy(idImagem);
                if (foto != null)
                {
                    foto.ExcluirArquivos();
                    foto.Imagem = null;

                    RepositorioDestaque.Update(foto);

                    return getImagensDados(foto.Id);
                }

            }
        }
        catch (Exception ex)
        {
            //
        }

        return "";
    }



}
