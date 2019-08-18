using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Linq.Dynamic;
using Modelos;

public partial class Controle_Cadastro_produtos : System.Web.UI.Page
{
    private Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                //Carregar();

                Pesquisar();
            }
            else
            {
                try
                {
                    Pesquisar();
                }
                catch (Exception er)
                {
                    MetodosFE.mostraMensagem(er.Message);
                }
            }
        }
        else
        {
        }

    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    //Metodo para colocar a informação do Pedido em edição na página


    protected void Pesquisar()
    {
        try
        {

            IList<Produto> produtos = RepositorioProduto.All().OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();

            if (!String.IsNullOrEmpty(txtBuscaID.Text))
            {
                int id = Convert.ToInt32(txtBuscaID.Text);
                produtos = produtos.Where(x => x.Id == id).ToList();
            }

            if (!String.IsNullOrEmpty(txtReferencia.Text)) 
            {
                string referencia = txtReferencia.Text;

                produtos = produtos.Where(x => x.Referencia.ToLower().Contains(referencia.ToLower())).ToList();
            }
                
            //if (colecaoPedidos.Count > 0)

            gvPrecos.DataSourceID = String.Empty;
            gvPrecos.DataSource = produtos;
            gvPrecos.DataBind();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    //Método para mudar os dados de um registro Pedido já existente

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Limpar();

            btnPesquisar.Visible = true;
            //btnSalvar.Visible = true;
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void Limpar()
    {

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Remove("Codigo");
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "";
        if (nameValues.Count > 0)
            updatedQueryString = "?" + nameValues.ToString();
        MetodosFE.recuperaMensagem();
        string urlFinal = url + updatedQueryString;
        Response.Redirect(urlFinal, false);

    }

    #region Guardamos o Código no ViewState
    private int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }
    private bool asc
    {
        get
        {
            if (ViewState["asc"] == null) ViewState["asc"] = true;
            return (bool)ViewState["asc"];
        }
        set { ViewState["asc"] = value; }
    }
    private string Ordenacao
    {
        get
        {
            if ((string)ViewState["Ordenacao"] == null)
                Ordenacao = "Nome";
            return (string)ViewState["Ordenacao"];
            
        }
        set { ViewState["Ordenacao"] = value; }
    }
    #endregion

    protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPrecos.PageIndex = e.NewPageIndex;
            Pesquisar();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }



    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
        //MetodosFE.limparCampos(this);
        //int i = 1;
    }


    #region Edicao da lista de pedidos

    //Acionar edição da linha
    protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Set the edit index.
        gvPrecos.EditIndex = e.NewEditIndex;
        Pesquisar();


        //Bind data to the GridView control.

    }

    //Cancelar edição da linha
    protected void gvDados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Reset the edit index.
        gvPrecos.EditIndex = -1;
        //Bind data to the GridView control.
        Pesquisar();
    }

    //Confirmar edição da linha
    protected void gvDados_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //Update the values.
            GridViewRow row = gvPrecos.Rows[e.RowIndex];
            //dt.Rows[gvItensPedido.EditIndex]["nome"] = ((string)(row.Cells[4].Text)).ToString();

            bool importar = ((CheckBox)(row.Cells[4].FindControl("chkImportar"))).Checked;

            int id = Convert.ToInt32(gvPrecos.Rows[e.RowIndex].Cells[0].Text);

            decimal valor = Convert.ToDecimal(((TextBox)(row.Cells[3].FindControl("txtValor"))).Text);
            

            //dt.Rows[gvItensPedido.EditIndex]["valor_unitario"] = ((TextBox)(row.Cells[4].Controls[0])).Text;
            //dt.Rows[gvItensPedido.EditIndex]["qtde_produto"] = ((TextBox)(row.Cells[6].Controls[0])).Text;
            //dt.Rows[gvItensPedido.EditIndex]["valor_total"] = ((TextBox)(row.Cells[7].Controls[0])).Text;

            //Update the values.        

            Produto produto = RepositorioProduto.FindBy(id);
            if (produto != null)
            {
                produto.Preco.Valor = valor;
                produto.Preco.DataVigencia = DateTime.Now;
                produto.ImportarPreco = importar;

                RepositorioProduto.Update(produto);

                MetodosFE.mostraMensagem("Preço do produto ref." + produto.Referencia + " alterado com sucesso.", "sucesso");
            }

            gvPrecos.EditIndex = -1;

            //Bind data to the GridView control.
            //Pesquisar("");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }
    #endregion







    protected void gvPrecos_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        if (ordenacao == Ordenacao)
            asc = !asc;
        Ordenacao = ordenacao;
        Pesquisar();
    }
}
