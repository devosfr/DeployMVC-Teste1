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

    public Repository<Marca> RepoMarca 
    {
        get 
        {
            return new Repository<Marca>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        nome = "Marcas";
        nome2 = "Marcas";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            uplCor.Visible = false;
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                Pesquisar("");
            }
            else
            {
                Pesquisar("");
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }
        }
    }

    protected void Pesquisar(string ordenacao)
    {
        try
        {
            var pesquisa = RepoMarca.All();

            string nome = "";
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x=>x.Nome!=null && x.Nome.ToLower().Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }


            IList<Marca> cores = pesquisa.ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = cores;
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

            Marca cor = RepoMarca.FindBy(Codigo);

            if (cor != null)
            {
                txtID.Text = cor.Id.ToString();
                txtNome.Text = cor.Nome;

                uplCor.Codigo = Codigo;
                
                UploadTela configuracoes = new UploadTela();

                configuracoes.Configuracao = 2;
                configuracoes.QtdeFotos = 1;
                configuracoes.TamFotoGrW = 700;
                configuracoes.TamFotoGrH = 700;
                configuracoes.TamFotoPqW = 50;
                configuracoes.TamFotoPqH = 50;
                configuracoes.Qualidade = 80;
                configuracoes.Configuracao = 2;

                uplCor.setConfiguracoes(configuracoes);
                uplCor.Visible = true;

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
        Pesquisar("");
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Marca cor = new Marca();
            if (String.IsNullOrEmpty(txtNome.Text ))
                throw new Exception(" Nome é um campo Obrigatório.");

            cor.Nome = txtNome.Text.Trim();

            RepoMarca.Add(cor);

            MetodosFE.mostraMensagem(" " + nome2 + " " + cor.Nome + " cadastrado com sucesso.", "sucesso");
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
            Marca cor = RepoMarca.FindBy(Codigo);
            
            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            cor.Nome = txtNome.Text.Trim();

            RepoMarca.Update(cor);
            MetodosFE.mostraMensagem(" " + nome2 + " " + cor.Nome + " cadastrado com sucesso.", "sucesso");
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
            Marca cor = RepoMarca.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));
            RepoMarca.Delete(cor);

            MetodosFE.mostraMensagem(nome2 + " " + cor.Nome + " excluído com sucesso.", "sucesso");
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
