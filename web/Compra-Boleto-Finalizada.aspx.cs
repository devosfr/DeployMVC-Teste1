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

            DadoVO texto = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Compra Finalizada - Boleto"));
            if (texto != null)
            {
                litTitulo.Text = texto.nome;
                litTexto.Text = texto.descricao;
                Page.Title = texto.nome + " - " + Configuracoes.getSetting("NomeSite");
            }

            ControleCarrinho.limparCookieCompra();


        }
    }
}
