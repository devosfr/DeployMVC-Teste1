using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Cidade : System.Web.UI.Page
{

    private Repository<Cidade> repoCidade
    {
        get
        {
            return new Repository<Cidade>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Estado> repoEstado
    {
        get
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                Pesquisar("nome");
            }
            else
            {
                Pesquisar("nome");
                CarregarDropEstado();
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }
        }
        else
        {
            Pesquisar("nome");
            btnAlterar.Visible = false;
            btnPesquisar.Visible = true;
            btnSalvar.Visible = true;
        }
    }

    protected void Pesquisar(string ordenacao)
    {
        try
        {
            var pesquisa = repoCidade.All();// (x => (id > 0 ? x.Id == id : true) && (!String.IsNullOrEmpty(nome) ? x.Nome.Contains(nome) : true)).ToList();

            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x => x.Nome != null && x.Nome.ToLower().Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }

            IList<Cidade> colecaoCidade = pesquisa.OrderBy(ordenacao).ToList();


            gvCidade.DataSourceID = String.Empty;
            gvCidade.DataSource = colecaoCidade;
            gvCidade.DataBind();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    protected void Carregar()
    {
        try
        {
            Cidade cidade = repoCidade.FindBy(Codigo);

            if (cidade != null)
            {

                txtCidade.Text = cidade.Nome;
                DropEstado.Text = cidade.Estado.Id.ToString();
                txtId.Text = cidade.Id.ToString();


                btnSalvar.Visible = false;
                btnPesquisar.Visible = false;
                btnAlterar.Visible = true;
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void CarregarDropEstado()
    {
        try
        {
            //Prenche o drop estado.

            IList<Estado> colecaoEstado = repoEstado.All().OrderBy("id").ToList();

            if (colecaoEstado.Count > 0)
            {
                DropEstado.DataSourceID = String.Empty;
                DropEstado.DataSource = colecaoEstado;
                DropEstado.DataTextField = "nome";
                DropEstado.DataValueField = "id";
                DropEstado.DataBind();
                DropEstado.Items.Insert(0, new ListItem("--Selecione--", "-1"));
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void gvCidade_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        Pesquisar(ordenacao);
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar("nome");
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Cidade cidade = new Cidade();

            if ((txtCidade.Text != ""))
            {
                cidade.Nome = txtCidade.Text.Trim();
                cidade.Estado = repoEstado.FindBy(Convert.ToInt32(DropEstado.SelectedValue));

                repoCidade.Add(cidade);
                MetodosFE.mostraMensagem("Cidade " + cidade.Nome + " cadastrada com sucesso.", "sucesso");
                this.Limpar();

            }
            else
            {
                MetodosFE.mostraMensagem("Campos Obrigatórios.");
            }

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }


    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            Cidade cidade = new Cidade();

            if ((txtCidade.Text != ""))
            {
                cidade.Id = Convert.ToInt32(txtId.Text);
                cidade.Nome = txtCidade.Text.Trim();
                cidade.Estado = repoEstado.FindBy(Convert.ToInt32(DropEstado.SelectedValue));
                repoCidade.Update(cidade);
                MetodosFE.mostraMensagem("Dados alterados com sucesso.", "sucesso");
                Limpar();
            }
            else
            {
                MetodosFE.mostraMensagem(" Campos Obrigatórios.");
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarDropEstado();
            Pesquisar("nome");
            Limpar();
        }
        catch { throw; }
    }

    protected void Limpar()
    {

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        MetodosFE.recuperaMensagem();
        nameValues.Remove("Codigo");
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "";
        if (nameValues.Count > 0)
            updatedQueryString = "?" + nameValues.ToString();

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

    private int Deleta
    {
        get
        {
            if (ViewState["Deleta"] == null) ViewState["Deleta"] = 0;
            return (Int32)ViewState["Deleta"];
        }
        set { ViewState["Deleta"] = value; }
    }

    #endregion

    protected void gvCidade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvCidade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCidade.PageIndex = e.NewPageIndex;

        Pesquisar("nome");
    }

    protected void gvCidade_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Cidade cor = repoCidade.FindBy(Convert.ToInt32(gvCidade.DataKeys[e.RowIndex].Value));
            repoCidade.Delete(cor);
            MetodosFE.mostraMensagem("Cidade " + cor.Nome + " alterado com sucesso.", "sucesso");
            Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }
    protected void gvCidade_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect(this.AppRelativeVirtualPath + "?Codigo=" + Convert.ToInt32(gvCidade.DataKeys[e.NewEditIndex].Value), false);
    }
}
