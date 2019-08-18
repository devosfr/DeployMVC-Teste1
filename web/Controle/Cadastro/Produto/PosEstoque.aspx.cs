using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }

    public Repository<Estoque> RepoEstoque
    {
        get
        {
            return new Repository<Estoque>(NHibernateHelper.CurrentSession);
        }
    }

    public Repository<Produto> RepoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }
    public Repository<Tamanhos> RepoTamanhos
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        nome = "Estoque";
        nome2 = "Estoque";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;


        if (Page.Request.QueryString["Codigo"] != null)
            Codigo = Convert.ToInt32(Page.Request.QueryString["Codigo"].ToString());


        if (!Page.IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["Produto"]))
            {
                int x = Convert.ToInt32(Request.QueryString["Produto"]);

                Produto prod = RepoProduto.FindBy(x);

                litProduto.Text = prod.Nome;

                List<string> ids = new List<string>();

                List<Tamanhos> tamanhos = new List<Tamanhos>();

                if (!string.IsNullOrEmpty(prod.Tamanhos))
                {
                    if (!prod.Tamanhos.Contains(','))
                    {
                        ids = prod.Tamanhos.Split(',').ToList();
                    }
                }

                foreach (string id in ids)
                {
                    int i = Convert.ToInt32(id);

                    Tamanhos tam = RepoTamanhos.FindBy(i);
                    if (tam != null)
                    {
                        tamanhos.Add(tam);
                    }
                }

                if (tamanhos != null && tamanhos.Count > 0)
                {
                    ddlTamanho.DataSource = tamanhos.OrderBy(y => y.Nome);
                    ddlTamanho.DataValueField = "id";
                    ddlTamanho.DataTextField = "nome";
                    ddlTamanho.DataBind();
                    ddlTamanho.Items.Insert(0, new ListItem("Selecione", "0"));
                }

                btnAlterar.Visible = false;

            }

            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Carregar();
            }
            else
            {
                Pesquisar("");
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }

        }
    }

    protected void Pesquisar(string ordenacao)
    {
        try
        {
            string produto = Request.QueryString["Produto"];

            var pesquisa = RepoEstoque.FilterBy(x => x.Produto.Id.ToString().Equals(produto));

            string nome = "";
            if (!String.IsNullOrEmpty(txtRefBusca.Text))
            {
                nome = txtRefBusca.Text.Trim();
                pesquisa = pesquisa.Where(x => x.Produto.Referencia != null && x.Produto.Referencia.ToLower().Contains(nome.ToLower()));
            }

            IList<Estoque> cores = pesquisa.ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = cores.OrderByDescending(x => x.Data).ToList();
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

    protected void Carregar()
    {
        try
        {
            List<string> ids = new List<string>();

            List<Tamanhos> tamanhos = new List<Tamanhos>();

            int aa = Convert.ToInt32(Request.QueryString["Codigo"]);

            Estoque estoque = RepoEstoque.FindBy(aa);

            Produto prod = RepoProduto.FindBy(estoque.Produto.Id);
            
            if(prod != null)
            {
                litProduto.Text = prod.Nome;
            }

            litProduto.Text = prod.Nome;

            txtQuantidade.Text = estoque.Quantidade.ToString();

            if (!string.IsNullOrEmpty(prod.Tamanhos))
            {
                if (!prod.Tamanhos.Contains(','))
                {
                    ids = prod.Tamanhos.Split(',').ToList();
                }
            }

            foreach (string id in ids)
            {
                int i = Convert.ToInt32(id);

                Tamanhos tam = RepoTamanhos.FindBy(i);
                if (tam != null)
                {
                    tamanhos.Add(tam);
                }
            }

            if (tamanhos != null && tamanhos.Count > 0)
            {
                ddlTamanho.DataSource = tamanhos.OrderBy(y => y.Nome);
                ddlTamanho.DataValueField = "id";
                ddlTamanho.DataTextField = "nome";
                ddlTamanho.DataBind();
                ddlTamanho.Items.Insert(0, new ListItem("Selecione", "0"));
            }

            if(ddlTamanho.Items.FindByValue(estoque.Tamanho.Id.ToString()) != null)
            {
                ddlTamanho.Items.FindByValue(estoque.Tamanho.Id.ToString()).Selected = true;
            }

            btnSalvar.Visible = false;
            btnPesquisar.Visible = false;
            btnAlterar.Visible = true;
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar("");
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Estoque estoque = new Estoque();

            if (String.IsNullOrEmpty(txtQuantidade.Text))
                throw new Exception("Por favor, digite a quantidade");

            estoque.Quantidade = Convert.ToInt32(txtQuantidade.Text);

            int aa = Convert.ToInt32(Request.QueryString["Produto"]);

            Produto prod = RepoProduto.FindBy(aa);

            estoque.Produto = prod;

            if (ddlTamanho.SelectedIndex > 0)
            {
                int id = Convert.ToInt32(ddlTamanho.SelectedValue);

                Tamanhos tam = RepoTamanhos.FindBy(id);

                estoque.Tamanho = tam;
            }

            estoque.Tipo = ddlTipo.SelectedValue;

            estoque.Data = DateTime.Now;

            estoque.Produto = prod;

            RepoEstoque.Add(estoque);

            MetodosFE.mostraMensagem(" Registro de estoque cadastrado com sucesso.", "sucesso");
            this.Limpar();
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
            Estoque estoque = RepoEstoque.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtQuantidade.Text))
                throw new Exception("Por favor, digite a quantidade");

            estoque.Quantidade = Convert.ToInt32(txtQuantidade.Text);

            int aa = Convert.ToInt32(Request.QueryString["Produto"]);

            Produto prod = RepoProduto.FindBy(aa);

            if(ddlTamanho.SelectedIndex > 0)
            {
                int id = Convert.ToInt32(ddlTamanho.SelectedValue);

                Tamanhos tam = RepoTamanhos.FindBy(id);

                estoque.Tamanho = tam;
            }

            estoque.Tipo = ddlTipo.SelectedValue;           

            estoque.Data = DateTime.Now;

            RepoEstoque.Update(estoque);

            MetodosFE.mostraMensagem("Registro de estoque cadastrado com sucesso.", "sucesso");

            Response.Redirect("~/Controle/Cadastro/Produto/PosEstoque.aspx?Produto=" + estoque.Produto.Id.ToString(), false);
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
        Pesquisar(ordenacao);
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
    private int Pagina
    {
        get
        {
            if (Session["Pagina"] == null) Session["Pagina"] = 0;
            return (Int32)Session["Pagina"];
        }
        set { Session["Pagina"] = value; }
    }

    #endregion

    protected void gvObjeto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvObjeto.PageIndex = e.NewPageIndex;
        Pagina = gvObjeto.PageIndex;
        Pesquisar("");
    }
    protected void gvObjeto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Estoque cor = RepoEstoque.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));
            RepoEstoque.Delete(cor);

            MetodosFE.mostraMensagem(" Registro de estoque excluído com sucesso.", "sucesso");
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


    protected string BaseURL
    {
        get
        {
            try
            {
                return string.Format("http://{0}{1}",
                                     HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                                     (VirtualFolder.Equals("/")) ? string.Empty : VirtualFolder);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }
}
