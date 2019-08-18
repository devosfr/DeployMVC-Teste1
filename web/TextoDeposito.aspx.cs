using System;
using System.Linq;
using System.Web.UI;
using Modelos;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DadoVO texto = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Compra Finalizada - Depósito"));
            if (texto != null)
            {
                litTitulo.Text = texto.nome;
                litTexto.Text = texto.descricao;
                Page.Title = texto.nome + " - " + Configuracoes.getSetting("NomeSite");
            }
            if (RouteData.Values["Chave"] != null)
            {
                int chave = Convert.ToInt32(RouteData.Values["Chave"].ToString());


                texto = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Bancos para Depósito") && x.Id == chave);
                if (texto != null)
                {
                    litTexto.Text = litTexto.Text + "<br/><br/>"+ texto.nome + "<br/>" + texto.descricao;

                }

            }

            ControleCarrinho.limparCookieCompra();


        }
    }
}
