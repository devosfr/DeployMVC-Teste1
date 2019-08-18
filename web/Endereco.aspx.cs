using System;
using Modelos;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Informações de Cobrança" + " - " + Configuracoes.getSetting("nomeSite");
        
        ControleLoginCliente.statusLogin();
        if (!IsPostBack)
        {

           Cliente cliente = ControleLoginCliente.GetClienteLogado();

            //EmailCliente.Text = cliente.Nome;

        }
    }
}
