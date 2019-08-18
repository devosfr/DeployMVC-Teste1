using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }

    public string nome2 { get; set; }

    public Repository<SubSegmentoProduto> RepositorioSubSegmento
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    public Repository<SegmentoProduto> RepositorioSegmento
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    public Repository<CategoriaProduto> RepositorioCategoria
    {
        get
        {
            return new Repository<CategoriaProduto>(NHibernateHelper.CurrentSession);
        }
    }
    public Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Categorias";
        nome2 = "Categorias";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            CarregarSegmentos();
            CarregarSubSegmentos();
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
            var categoriasPesquisa = RepositorioCategoria.All();

            string nome = "";
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                categoriasPesquisa = categoriasPesquisa.Where(x => x.Nome != null && x.Nome.Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                categoriasPesquisa = categoriasPesquisa.Where(x => x.Id == id);
            }

            IList<CategoriaProduto> categorias = categoriasPesquisa.OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = categorias;
            gvObjeto.DataBind();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void CarregarSegmentos()
    {
        var segmentos = RepositorioSegmento.All().ToList();
        ddlSegmento.DataSource = segmentos;
        ddlSegmento.DataTextField = "Nome";
        ddlSegmento.DataValueField = "Id";
        ddlSegmento.DataBind();
    }

    protected void CarregarSubSegmentos()
    {
        if (!String.IsNullOrEmpty(ddlSegmento.SelectedValue))
        {
            var subSegmentos = RepositorioSubSegmento.FilterBy(x => x.Segmento.Id == Convert.ToInt32(ddlSegmento.SelectedValue)).ToList();

            ddlSubSegmento.DataSource = subSegmentos;
            ddlSubSegmento.DataTextField = "Nome";
            ddlSubSegmento.DataValueField = "Id";
            ddlSubSegmento.DataBind();
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
            CategoriaProduto categoria = RepositorioCategoria.FindBy(Codigo);

            if (categoria != null)
            {
                txtNome.Text = categoria.Nome;
                ddlSegmento.SelectedValue = categoria.SubSegmento.Segmento.Id.ToString();
                ddlSubSegmento.SelectedValue = categoria.SubSegmento.Id.ToString();
                txtOrdem.Text = categoria.Ordem.ToString();


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
            CategoriaProduto categoria = new CategoriaProduto();
            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");



            categoria.Nome = txtNome.Text.Trim();
            categoria.Chave = ("categoria " + categoria.Nome).ToSeoUrl();

            int contagem = RepositorioCategoria.FilterBy(x => x.Chave.Equals(categoria.Chave) && x.Id != categoria.Id).Count();
            if (contagem > 0)
                categoria.Chave = ("categoria " + categoria.Id + " " + categoria.Nome).ToSeoUrl();


            if (!String.IsNullOrEmpty(txtOrdem.Text))
                categoria.Ordem = Convert.ToInt32(txtOrdem.Text);
            else
                categoria.Ordem = 0;

            SubSegmentoProduto subSegmento = RepositorioSubSegmento.FindBy(Convert.ToInt32(ddlSubSegmento.SelectedValue));
            categoria.SubSegmento = subSegmento;

            RepositorioCategoria.Add(categoria);

            MetodosFE.mostraMensagem(" " + nome2 + " " + subSegmento.Nome + " cadastrado com sucesso.", "sucesso");
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
            CategoriaProduto categoria = RepositorioCategoria.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            categoria.Nome = txtNome.Text.Trim();
            categoria.Chave = ("categoria " + categoria.Nome).ToSeoUrl();

            int contagem = RepositorioCategoria.FilterBy(x => x.Chave.Equals(categoria.Chave) && x.Id != categoria.Id).Count();
            if (contagem > 0)
                categoria.Chave = ("categoria " + categoria.Id + " " + categoria.Nome).ToSeoUrl();

            if (!String.IsNullOrEmpty(txtOrdem.Text))
                categoria.Ordem = Convert.ToInt32(txtOrdem.Text);
            else
                categoria.Ordem = 0;

            SubSegmentoProduto subSegmento = RepositorioSubSegmento.FindBy(Convert.ToInt32(ddlSubSegmento.SelectedValue));
            categoria.SubSegmento = subSegmento;

            RepositorioCategoria.Add(categoria);
            MetodosFE.mostraMensagem(" " + nome2 + " " + subSegmento.Nome + " cadastrado com sucesso.", "sucesso");
            this.Limpar();
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
    private int CodigoTela
    {
        get
        {
            return (Int32)ViewState["CodigoTela"];
        }
        set { ViewState["CodigoTela"] = value; }
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

    private int Pagina
    {
        get
        {
            if (ViewState["Pagina"] == null) ViewState["Pagina"] = 0;
            return (Int32)ViewState["Pagina"];
        }
        set { ViewState["Pagina"] = value; }
    }

    private int Pesquisa
    {
        get
        {
            if (ViewState["Pesquisa"] == null) ViewState["Pesquisa"] = 0;
            return (Int32)ViewState["Pesquisa"];
        }
        set { ViewState["Pesquisa"] = value; }
    }
    private string Ordenacao
    {
        get
        {
            if (ViewState["Ordenacao"] == null) ViewState["Ordenacao"] = "Id";
            return (string)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
    }
    #endregion

    protected void gvObjeto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvObjeto.PageIndex = e.NewPageIndex;
        Pagina = gvObjeto.PageIndex;
        Pesquisar();
    }

    protected void gvObjeto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            CategoriaProduto categoria = RepositorioCategoria.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));

            var produtos = RepositorioProduto.FilterBy(x => x.Categorias.Contains(categoria)).ToList();

            foreach (var produto in produtos)
                produto.Categorias.Remove(categoria);

            RepositorioProduto.Update(produtos);

            RepositorioCategoria.Delete(categoria);

            MetodosFE.mostraMensagem(nome2 + " " + categoria.Nome + " excluído com sucesso.", "sucesso");
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
                
                return null;
            }
        }
    }

    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }

    protected void ddlSegmento_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarSubSegmentos();

    }
}