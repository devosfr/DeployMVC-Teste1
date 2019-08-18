using System;
using System.Web.UI;
using System.Linq;
using Modelos;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }

    public string MetaTagsSociais { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        form1.Action = Request.RawUrl;
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";

        if (!IsPostBack)
        {
            string url = Request.Url.GetLeftPart(UriPartial.Path);

            DadoVO dados = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome == "MetaTags por Página" && !String.IsNullOrEmpty(x.nome) && x.nome.Equals(url));
            if (dados != null)
            {
                litMetaTags.Text = dados.descricao;
            }
            else
            {
                dados = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome == "MetaTags Principais");
                if (dados != null)
                {
                    litMetaTags.Text = dados.descricao;
                }
            }

            dados = MetodosFE.getTela("Analytics");
            if (dados != null)
                litAnalytics.Text = dados.descricao;

            dados = MetodosFE.getTela("Barra GetSocial");
            if (dados != null)
                litGetSocial.Text = dados.descricao;

            //if (ViewState["Metas"] != null)
            //    litSocialTags.Text = ViewState["Metas"].ToString();


        }
        if (String.IsNullOrEmpty(Page.Title))
        {
            Page.Title =  Configuracoes.getSetting("NomeSite");
        }
    }


    public void configuraTags(string tags)
    {
        MetaTagsSociais = tags;
    }
    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }


}
