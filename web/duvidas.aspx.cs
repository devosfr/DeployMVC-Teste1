using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelos;

public partial class _Dicas : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DadoVO dado = null;
            dado = MetodosFE.getTela("Como Comprar");
            if (dado != null)
            {
                litTitulo.Text = dado.nome;
                litTexto.Text = dado.descricao;
            }   
        }
    }
    public string pagina()
    {
        if (!String.IsNullOrEmpty(Request.QueryString["pagina"]))
            return "?pagina=" + Request.QueryString["pagina"];
        return "";
    }
}
