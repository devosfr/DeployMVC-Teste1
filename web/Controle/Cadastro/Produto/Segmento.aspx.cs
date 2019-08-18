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

    public Repository<SegmentoProduto> RepositorioSegmento
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    public Repository<SubSegmentoProduto> RepositorioSubSegmento
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
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
        nome = "Segmentos";
        nome2 = "Segmento";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
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
            var segmentosPesquisa = RepositorioSegmento.All();
            string nome = "";
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                segmentosPesquisa = segmentosPesquisa.Where(x => x.Nome != null && x.Nome.ToLower().Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                segmentosPesquisa = segmentosPesquisa.Where(x => x.Id == id);
            }

            IList<SegmentoProduto> segmentos = segmentosPesquisa.OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = segmentos;
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
            SegmentoProduto segmento = RepositorioSegmento.FindBy(Codigo);

            if (segmento != null)
            {   
                txtNome.Text = segmento.Nome;
                txtOrdem.Text = segmento.Ordem.ToString();
                chkVisivel.Checked = segmento.Visivel;
                img.Text = segmento.Texto;
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
            SegmentoProduto segmento = new SegmentoProduto();
            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            segmento.Nome = txtNome.Text.Trim();
            segmento.Chave = ("segmento " + segmento.Nome).ToSeoUrl();
            segmento.Visivel = chkVisivel.Checked;
            segmento.Texto = img.Text;

            int contagem = RepositorioSegmento.FilterBy(x => x.Chave.Equals(segmento.Chave) && x.Id != segmento.Id).Count();
            if (contagem > 0)
                segmento.Chave = ("segmento " + segmento.Id + " " + segmento.Nome).ToSeoUrl();

            if (!String.IsNullOrEmpty(txtOrdem.Text))
                segmento.Ordem = Convert.ToInt32(txtOrdem.Text);
            else
                segmento.Ordem = 0;

            segmento.Visivel = true;
            RepositorioSegmento.Add(segmento);

            MetodosFE.mostraMensagem(" " + nome2 + " " + segmento.Nome + " cadastrado com sucesso.", "sucesso");
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
            SegmentoProduto segmentoProduto = RepositorioSegmento.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            segmentoProduto.Nome = txtNome.Text.Trim();
            segmentoProduto.Chave = ("segmento " + segmentoProduto.Nome).ToSeoUrl();
            segmentoProduto.Visivel = chkVisivel.Checked;
            segmentoProduto.Texto = img.Text;

            int contagem = RepositorioSegmento.FilterBy(x => x.Chave.Equals(segmentoProduto.Chave) && x.Id != segmentoProduto.Id).Count();
            if (contagem > 0)
                segmentoProduto.Chave = ("segmento " + segmentoProduto.Id + " " + segmentoProduto.Nome).ToSeoUrl();

            if (!String.IsNullOrEmpty(txtOrdem.Text))
                segmentoProduto.Ordem = Convert.ToInt32(txtOrdem.Text);
            else
                segmentoProduto.Ordem = 0;

            RepositorioSegmento.Add(segmentoProduto);
            MetodosFE.mostraMensagem(" " + nome2 + " " + segmentoProduto.Nome + " cadastrado com sucesso.", "sucesso");
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
            SegmentoProduto segmento = RepositorioSegmento.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));

            var subSegmentos = RepositorioSubSegmento.FilterBy(x => x.Segmento.Id == segmento.Id).ToList();

            var categorias = RepositorioCategoria.FilterBy(x => x.SubSegmento != null && subSegmentos.Contains(x.SubSegmento)).ToList();

            var produtos = RepositorioProduto.FilterBy(x => x.Segmentos.Contains(segmento) || x.SubSegmentos.Any(y => subSegmentos.Contains(y)) || x.Categorias.Any(y => categorias.Contains(y))).ToList();

            foreach (var produto in produtos)
            {
                produto.Segmentos.Remove(segmento);

                foreach (var categoria in categorias)
                    produto.Categorias.Remove(categoria);

                foreach (var subSegmento in subSegmentos)
                    produto.SubSegmentos.Remove(subSegmento);
            }

            RepositorioProduto.Update(produtos);

            RepositorioCategoria.Delete(categorias);

            RepositorioSubSegmento.Delete(subSegmentos);

            RepositorioSegmento.Delete(segmento);

            MetodosFE.mostraMensagem(nome2 + " " + segmento.Nome + " excluído com sucesso.", "sucesso");
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