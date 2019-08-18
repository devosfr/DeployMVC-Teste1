using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Modelos;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ControleLoginCliente.statusLogin();
        if (!IsPostBack)
        {
            IList<DadoVO> posts = MetodosFE.documentos.Where(x => x.tela.nome == "Bancos para Depósito" && x.visivel).ToList();

            repBancos.DataSourceID = String.Empty;
            repBancos.DataSource = posts;
            repBancos.DataBind();
        }
    }

    protected void linkSelecionar_Click(object sender, EventArgs e)
    {
        int idBanco = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        ControlePedido.FecharPedido(2,idBanco);
    }
}
