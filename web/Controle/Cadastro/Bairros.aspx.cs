using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }

    private Repository<Bairro> repoBairro
    {
        get
        {
            return new Repository<Bairro>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Estado> repoEstado
    {
        get
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Cidade> repoCidade
    {
        get
        {
            return new Repository<Cidade>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Bairros";
        nome2 = "Bairro";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            carregarEstados();
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                Pesquisar();
            }
            else
            {
                Pesquisar();
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }
        }
    }

    protected void Pesquisar()
    {
        try
        {
            var pesquisa = repoBairro.All();// (x => (id > 0 ? x.Id == id : true) && (!String.IsNullOrEmpty(nome) ? x.Nome.Contains(nome) : true)).ToList();

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
                pesquisa = pesquisa.Where(x => x.Id==id);
            }

            IList<Bairro> colecaoEstado = pesquisa.OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();


            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = colecaoEstado;
            gvObjeto.DataBind();

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


    protected void carregarEstados()
    {
        IList<Estado> estados = repoEstado.All().OrderBy(x => x.Nome).ToList();
        ddlEstado.DataTextField = "nome";
        ddlEstado.DataValueField = "id";
        ddlEstado.DataBind();
    }
    protected void carregarCidades()
    {

        IList<Cidade> estados = repoCidade.All().Where(x => x.Estado.Id == Convert.ToInt32(ddlEstado.SelectedValue)).OrderBy(x => x.Nome).ToList();
        ddlCidade.DataSource = estados;
        ddlCidade.DataTextField = "nome";
        ddlCidade.DataValueField = "id";
        ddlCidade.DataBind();
    }

    protected void Carregar()
    {
        try
        {

            Bairro colecaoEstado = repoBairro.FindBy(Codigo);

            if (colecaoEstado != null)
            {
                txtNome.Text = colecaoEstado.Nome;
                txtID.Text = colecaoEstado.Id.ToString();
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

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Bairro estado = new Bairro();
            if ((txtNome.Text != ""))
            {
                estado.Nome = txtNome.Text.Trim();
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(ddlCidade.SelectedValue);
                estado.Cidade = cidade;
                repoBairro.Add(estado);
                MetodosFE.mostraMensagem(" " + nome2 + " " + estado.Nome + " cadastrado com sucesso.", "sucesso");
                this.Limpar();
            }
            else
            {
                MetodosFE.mostraMensagem(" Nome é um campo Obrigatório.");
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
            Bairro estado = repoBairro.FindBy(Convert.ToInt32(txtID.Text));

            if ((txtNome.Text != ""))
            {
                estado.Id = Convert.ToInt32(txtID.Text);
                estado.Nome = txtNome.Text.Trim();
                Cidade cidade = new Cidade();
                cidade.Id = Convert.ToInt32(ddlCidade.SelectedValue);
                estado.Cidade = cidade;
                repoBairro.Update(estado);
                MetodosFE.mostraMensagem(nome2 + " alterado com sucesso.", "sucesso");
                this.Limpar();
            }
            else
            {
                MetodosFE.mostraMensagem("Nome é um campo Obrigatório.");
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
            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }



    protected void gvObjeto_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        if (ordenacao == Ordenacao)
            asc = !asc;
        Ordenacao = ordenacao;
        Pesquisar();
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

    private string Ordenacao
    {
        get
        {
            if (ViewState["Ordenacao"] == null)
                ViewState["Ordenacao"] = "id";
            return (string)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
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

    #endregion

    protected void gvObjeto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvObjeto.PageIndex = e.NewPageIndex;
        Pesquisar();
    }
    protected void gvObjeto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Bairro bairro = repoBairro.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));
            repoBairro.Delete(bairro);
            MetodosFE.mostraMensagem(nome2 + " " + bairro.Nome + " excluído com sucesso.", "sucesso");
            Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }
    protected void gvObjeto_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect(this.AppRelativeVirtualPath + "?Codigo=" + Convert.ToInt32(gvObjeto.DataKeys[e.NewEditIndex].Value), false);
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregarCidades();
    }
}
