using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelos;

public partial class detalhe_noticia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        Page.Title = "Detalhe do Blog " + Configuracoes.getSetting("NomeSite");
        string chave = string.Empty;
        if (!IsPostBack)
        {

            if (RouteData.Values.Count > 0)
            {
                chave = RouteData.Values["Chave"].ToString();

                DadoVO Noticia = MetodosFE.documentos.Where(n => n.tela.nome.Equals("Publicações") && n.chave.Equals(chave) && n.visivel).FirstOrDefault();

                if (Noticia != null)
                {
                    liDetalheLancamentosNome.Text = Noticia.nome;
                    liDetalheLancamentosData.Text = Noticia.data.ToShortDateString().ToString();
                    //liDetalheLancamentosResumo.Text = Noticia.resumo;
                    liDetalheLancamentosDescricao.Text = Noticia.descricao;
                    imgNoticia.Src = MetodosFE.BaseURL + "/ImagensHQ/" + Noticia.listaFotos[0].Nome;
                }
            }


            if (chave != null)
            {
                string urlShareFace = String.Format("{0}/{1}/{2}", "http://institutotri.com.br", "detalhe-noticia", chave);
                //linkShareFace.HRef = "http://www.facebook.com/sharer/sharer.php?u=" + HttpUtility.UrlEncode(urlShareFace);
                linkFace.Visible = true;
                linkFace.HRef = "http://www.facebook.com/sharer/sharer.php?u=" + urlShareFace;

                string urlShareGoogle = String.Format("{0}/{1}/{2}", "http://ahortaorganicos.com", "detalhe-blog", chave);
                linkGoogle.Visible = true;
                linkGoogle.HRef = "https://plus.google.com/share?url=" + urlShareGoogle;

                string urlShareTwitter = String.Format("{0}/{1}/{2}", "http://ahortaorganicos.com", "detalhe-blog", chave);
                linkTwitter.Visible = true;
                linkTwitter.HRef = "https://twitter.com/intent/tweet?url=" + urlShareTwitter;
            }


        }


    }





    //public string compartilharFace(string chave)
    //{

    //    string urlShareFace = String.Format("{0}/{1}/{2}", "http://institutotri.com.br", "detalhe-noticia", chave);
    //    //linkShareFace.HRef = "http://www.facebook.com/sharer/sharer.php?u=" + HttpUtility.UrlEncode(urlShareFace);
    //    linkShareFace.HRef = "http://www.facebook.com/sharer/sharer.php?u=" + urlShareFace;

    //    return urlShareFace;
    //}


}