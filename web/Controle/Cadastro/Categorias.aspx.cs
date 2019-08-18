using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using NHibernate.Linq;
using System.Linq.Dynamic;
using Modelos;

public partial class Controle_Cadastro_Categorias : System.Web.UI.Page
{

    private Repository<CategoriaVO> repoCategoria
    {
        get
        {
            return new Repository<CategoriaVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<SegmentoFilhoVO> repoSegmentoFilho
    {
        get
        {
            return new Repository<SegmentoFilhoVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<SegmentoPaiVO> repoSegmentoPai
    {
        get
        {
            return new Repository<SegmentoPaiVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Tela> repoTela
    {
        get
        {
            return new Repository<Tela>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PermissaoVO> repoPermissao
    {
        get
        {
            return new Repository<PermissaoVO>(NHibernateHelper.CurrentSession);
        }
    }


    public string nome { get; set; }
    public string nome2 { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                CarregarDropTela();

                if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
                {
                    Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                    carregarDadosTela();
                    Carregar();
                    divLista.Style.Add("display", "none");

                }
                else
                {
                    Pesquisar();
                    divDados.Style.Add("display", "none");
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                    carregarDadosTela();


                }
            }
            catch (Exception er)
            {
                MetodosFE.mostraMensagem(er.Message);
            }
        }


    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    protected void CarregarDropTela()
    {
        IList<Tela> telas = null;
        if (ControleLogin.GetUsuarioLogado().tipo != "AA")
        {
            var permissoes = repoPermissao.FilterBy(x => x.usuario.Id == ControleLogin.GetUsuarioLogado().Id);
            telas = repoTela.All().Fetch(x => x.campos).Where(x => permissoes.Any(y => y.paginaDeControle != null && y.paginaDeControle.Id == x.pagina.Id) && x.campos.Any(z => z.destino == "DropCategoria")).ToList();
        }
        else
            telas = repoTela.All().Fetch(x => x.campos).Where(x => x.campos.Any(z => z.destino == "DropCategoria")).ToList();

        ddlTela.DataSource = telas;
        ddlTela.DataTextField = "nome";
        ddlTela.DataValueField = "id";
        ddlTela.DataBind();

        if (Request.QueryString["Tela"] != null)
            ddlTela.SelectedValue = Request.QueryString["Tela"];
        if(ddlTela.Items.Count>0)
        CodigoTela = Convert.ToInt32(ddlTela.SelectedValue);

        CarregarSegmentosPai();

        CarregarSegmentosFilho();
    }

    protected void CarregarSegmentosPai()
    {

        var segmentosPai = repoSegmentoPai.FilterBy(x => x.tela.Id == CodigoTela).OrderBy(x => x.nome).ToList();

        ddlSegmentoPai.DataSource = segmentosPai;
        ddlSegmentoPai.DataTextField = "nome";
        ddlSegmentoPai.DataValueField = "id";
        ddlSegmentoPai.DataBind();

        ddlBuscaSegmentoPai.DataSource = segmentosPai;
        ddlBuscaSegmentoPai.DataTextField = "nome";
        ddlBuscaSegmentoPai.DataValueField = "id";
        ddlBuscaSegmentoPai.DataBind();
        ddlBuscaSegmentoPai.Items.Insert(0, new ListItem("Selecione", ""));
    }

    protected void CarregarSegmentosFilho()
    {
        try
        {
            int idSegPai = 0;
            IList<SegmentoFilhoVO> segmentosFilho = null;
            if (!String.IsNullOrEmpty(ddlSegmentoPai.SelectedValue))
            {
                idSegPai = Convert.ToInt32(ddlSegmentoPai.SelectedValue);
                segmentosFilho = repoSegmentoFilho.FilterBy(x => x.tela.Id == CodigoTela && x.segPai.Id == idSegPai).OrderBy(x => x.nome).ToList();


                ddlSegmentoFilho.DataSourceID = String.Empty;
                ddlSegmentoFilho.DataSource = segmentosFilho;
                ddlSegmentoFilho.DataTextField = "nome";
                ddlSegmentoFilho.DataValueField = "id";
                ddlSegmentoFilho.DataBind();
            }
            else
            {
                ddlSegmentoFilho.DataSource = new List<SegmentoFilhoVO>();
                ddlSegmentoFilho.DataTextField = "nome";
                ddlSegmentoFilho.DataValueField = "id";
                ddlSegmentoFilho.DataBind();
            }

            if (!String.IsNullOrEmpty(ddlBuscaSegmentoPai.SelectedValue))
            {
                idSegPai = Convert.ToInt32(ddlBuscaSegmentoPai.SelectedValue);
                segmentosFilho = repoSegmentoFilho.FilterBy(x => x.tela.Id == CodigoTela && x.segPai.Id == idSegPai).OrderBy(x => x.nome).ToList();

                ddlBuscaSegmentoFilho.DataSourceID = String.Empty;
                ddlBuscaSegmentoFilho.DataSource = segmentosFilho;
                ddlBuscaSegmentoFilho.DataTextField = "nome";
                ddlBuscaSegmentoFilho.DataValueField = "id";
                ddlBuscaSegmentoFilho.DataBind();
                ddlBuscaSegmentoFilho.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlBuscaSegmentoFilho.DataSource = new List<SegmentoFilhoVO>();
                ddlBuscaSegmentoFilho.DataTextField = "nome";
                ddlBuscaSegmentoFilho.DataValueField = "id";
                ddlBuscaSegmentoFilho.DataBind();
                ddlBuscaSegmentoFilho.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void Pesquisar()
    {
        try
        {

            var pesquisa = repoCategoria.FilterBy(x=>x.tela.Id == CodigoTela);

            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x => x.nome != null && x.nome.ToLower().Contains(nome.ToLower()));
            }

            int idSegFilho = 0;
            if (ddlBuscaSegmentoFilho.SelectedIndex > 0)
            {
                idSegFilho = Convert.ToInt32(ddlBuscaSegmentoFilho.SelectedValue);
                pesquisa = pesquisa.Where(x=>x.segFilho.Id == idSegFilho);
            }

            IList<CategoriaVO> colecaoSegmento = pesquisa.OrderBy(Ordenacao+ (asc?" asc": " desc")).ToList();

            gvSegmento.DataSourceID = String.Empty;
            gvSegmento.DataSource = colecaoSegmento;
            gvSegmento.DataBind();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void carregarDadosTela()
    {
        String nome = "Categoria";

        Tela tela = repoTela.FindBy(x => x.nomeFixo == nome);
        if (tela != null)
        {
            nome = tela.nome;
            litTitulo.Text = nome;
            Page.Title = tela.nome;
            CampoTela campo = tela.campos.FirstOrDefault(x => x.destino == "txtOrdem");

            if (campo == null)
                liCampo2.Visible = false;


            campo = tela.campos.FirstOrDefault(x => x.destino == "txtDescricao");

            if (campo == null)

                liDescricao.Visible = false;


            if (tela.upload != null && Codigo != 0)
            {
                uplCategoria.Codigo = Codigo;
                uplCategoria.setConfiguracoes(tela.upload);

            }
            else
                uplCategoria.Visible = false;

        }
    }

    protected void Carregar()
    {
        try
        {
            CategoriaVO categoria = repoCategoria.FindBy(x=>x.Id==Codigo&&x.tela.Id==CodigoTela);

            if (categoria != null)
            {
                CarregarSegmentosPai();
                ddlSegmentoPai.SelectedValue = categoria.segFilho.segPai.Id.ToString();

                CarregarSegmentosFilho();
                ddlSegmentoFilho.SelectedValue = categoria.segFilho.Id.ToString();

                txtNome.Text = categoria.nome.ToString();
                txtDescricao.Text = categoria.descricao;
                chkVisivel.Checked = categoria.visivel;
                txtOrdem.Text = categoria.ordem;

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

    protected void gvSegmento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            repoCategoria.Delete(repoCategoria.FindBy(Convert.ToInt32(gvSegmento.DataKeys[e.RowIndex].Value)));
            MetodosFE.mostraMensagem("Categoria excluida com sucesso.", "sucesso");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }


        gvSegmento.DataBind();
        Pesquisar();
    }

    protected void gvSegmento_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        if (ordenacao == Ordenacao)
            asc = !asc;
        Ordenacao = ordenacao;
        Pesquisar();
    }

    protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Codigo = Convert.ToInt32(gvSegmento.DataKeys[e.NewEditIndex].Value);

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("Codigo", Codigo.ToString());
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "?" + nameValues.ToString();
        string urlFinal = url + updatedQueryString;
        e.Cancel = true;
        Response.Redirect(urlFinal, false);

        //Carregar();

    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CategoriaVO categoria = new CategoriaVO();

            if (String.IsNullOrEmpty(txtNome.Text))
                MetodosFE.mostraMensagem("Campo Nome e Segmento obrigatórios.");

            categoria.nome = txtNome.Text.Trim();
            categoria.descricao = txtDescricao.Text;
            categoria.segFilho = repoSegmentoFilho.FindBy(Convert.ToInt32(ddlSegmentoFilho.SelectedValue));
            categoria.visivel = chkVisivel.Checked;
            categoria.tela = new Tela() { Id = CodigoTela };


            categoria.chave = ( categoria.nome).ToSeoUrl();
            IList<CategoriaVO> categorias = repoCategoria.All().Where(x => x.chave == categoria.chave).ToList();

            if (categorias.Count > 0)
            {
                for (int cont = 0; ; cont++)
                {
                    categoria.chave = categoria.nome.ToSeoUrl() + cont;
                    categorias = repoCategoria.All().Where(x => x.chave == categoria.chave).ToList();
                    if (categorias.Count == 0)
                        break;
                }
            }

            repoCategoria.Add(categoria);
            MetodosFE.mostraMensagem("Categoria " + categoria.nome + " cadastrada com sucesso.", "sucesso");
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
            CategoriaVO categoria = repoCategoria.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("É preciso definir o nome da categoria.");

            categoria.nome = txtNome.Text;
            categoria.descricao = txtDescricao.Text;
            categoria.segFilho = repoSegmentoFilho.FindBy(Convert.ToInt32(ddlSegmentoFilho.SelectedValue));
            categoria.visivel = chkVisivel.Checked;
            categoria.tela = new Tela() { Id = CodigoTela };

            categoria.chave = ( categoria.nome).ToSeoUrl();

            IList<CategoriaVO> categorias = repoCategoria.All().Where(x => x.chave == categoria.chave).ToList();

            if (categorias.Count > 0)
            {
                if (categorias[0].Id != Codigo)
                    for (int cont = 0; ; cont++)
                    {
                        categoria.chave = categoria.nome.ToSeoUrl() + cont;
                        categorias = repoCategoria.All().Where(x => x.chave == categoria.chave).ToList();
                        if (categorias.Count == 0)
                            break;
                    }
            }

            repoCategoria.Update(categoria);
            MetodosFE.mostraMensagem("Dados alterados com sucesso.", "sucesso");
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
    private int CodigoTela
    {
        get
        {
            if (ViewState["CodigoTela"] == null) ViewState["CodigoTela"] = 0;
            return (Int32)ViewState["CodigoTela"];
        }
        set { ViewState["CodigoTela"] = value; }
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

    protected void gvSegmento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvSegmento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSegmento.PageIndex = e.NewPageIndex;
            Pesquisar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void ddlBuscaSegmentoPai_TextChanged(object sender, EventArgs e)
    {
        CarregarSegmentosFilho();
    }

    protected void ddlSegmentoPai_TextChanged(object sender, EventArgs e)
    {
        CarregarSegmentosFilho();
    }

    protected void ddlTela_TextChanged(object sender, EventArgs e)
    {
        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        MetodosFE.recuperaMensagem();
        nameValues.Clear();
        nameValues.Add("Tela", ddlTela.SelectedValue);
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "";
        if (nameValues.Count > 0)
            updatedQueryString = "?" + nameValues.ToString();

        string urlFinal = url + updatedQueryString;
        Response.Redirect(urlFinal, false);
    }
}
