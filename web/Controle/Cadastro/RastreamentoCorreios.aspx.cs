using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;
using RDev.Correios;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }
    
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        ltlDivDados.Text = "";
        if (!string.IsNullOrEmpty(CodRastreio.Text))
        {
            RastreamentoFreteCorreios freteCorreios = new RastreamentoFreteCorreios();

            RetornoRastreamento rastreamento = freteCorreios.ConsultaRastreioCorreios(CodRastreio.Text.Trim());

            if (rastreamento.Sucesso)
            {
                ltlDivDados.Text = rastreamento.Html;
                //GridViewTableRastreio.DataSource = rastreamento.DataSet;
                //GridViewTableRastreio.DataBind();

                //GridViewTableRastreio2.DataSource = rastreamento.ListObject;
                //GridViewTableRastreio2.DataBind();
            }
            else
            {
                ltlDivDados.Text = rastreamento.Html;
            }
        }
        else
        {
            ltlDivDados.Text = ("<br /><br /><font color='red' size='3'>Por Favor informar Codigo de rastreio.</font>");
        }
    }
}
