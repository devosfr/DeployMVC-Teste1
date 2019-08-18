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

    protected void Page_Load(object sender, EventArgs e)
    {

        nome = "Estoque";
        nome2 = "Estoque";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;


        if(Page.Request.QueryString["Codigo"] != null)
            Codigo = Convert.ToInt32(Page.Request.QueryString["Codigo"].ToString());

     
        if (!Page.IsPostBack)
        {  
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Carregar();
            }
            else
            {
                Pesquisar("");
                btnPesquisar.Visible = true;
            }

        }
    }

    protected string totalEstoque(Produto prod)
    {
        string retorno = "";

        List<Estoque> estoques = RepoEstoque.FilterBy(x => x.Produto.Id == prod.Id).ToList();
        if(estoques != null && estoques.Count > 0)
        {
            int x = 0;

            foreach(Estoque est in estoques)
            {
                if (String.IsNullOrEmpty(est.Tipo))
                {
                    //Legacy
                    x = x + est.Quantidade;
                }
                else
                {
                    if(est.Tipo.Equals("E"))
                    {
                        x = x + est.Quantidade;
                    }

                    if (est.Tipo.Equals("S"))
                    {
                        x = x - est.Quantidade;
                    }
                }
            }

            retorno = x.ToString();
        }

        return retorno;
    }

    protected void Pesquisar(string ordenacao)
    {
        try
        {
            var pesquisa = RepoProduto.All();

            string nome = "";
            if (!String.IsNullOrEmpty(txtRefBusca.Text))
            {
                nome = txtRefBusca.Text.Trim();
                pesquisa = pesquisa.Where(x => x.Referencia != null && x.Referencia.ToLower().Contains(nome.ToLower()));
            }

            IList<Produto> cores = pesquisa.ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = cores.OrderByDescending(x => x.Nome).ToList();
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
            Estoque estoque = RepoEstoque.FindBy(Codigo);

            if (estoque != null)
            {

            }
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
        Response.Redirect("~/Controle/Cadastro/Produto/PosEstoque.aspx?Produto=" + Convert.ToInt32(gvObjeto.DataKeys[e.NewEditIndex].Value), false);
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
