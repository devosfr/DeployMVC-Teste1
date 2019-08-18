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

public partial class Controle_Cadastro_SegmentoPai : System.Web.UI.Page
{

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CarregarDropTela();
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                carregarDadosTela();
                Carregar();
            }
            else
            {
                try
                {
                    carregarDadosTela();
                    Pesquisar();
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                }
                catch (Exception er)
                {
                    MetodosFE.mostraMensagem(er.Message);

                }
            }
        }


    }

    protected void carregarDadosTela()
    {
        String nome = "SegmentoFilho";

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
                uplSegFilho.Codigo = Codigo;
                uplSegFilho.setConfiguracoes(tela.upload);

            }
            else
                uplSegFilho.Visible = false;

        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    protected void Pesquisar()
    {
        try
        {
            var pesquisa = repoSegmentoFilho.FilterBy(x => x.tela.Id == CodigoTela);

            if (String.IsNullOrEmpty(Ordenacao))
                Ordenacao = "id";

            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x => x.nome != null && x.nome.ToLower().Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }

            IList<SegmentoFilhoVO> colecaoSegmento = pesquisa.OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();


            gvSegmento.DataSourceID = String.Empty;
            gvSegmento.DataSource = colecaoSegmento;
            gvSegmento.DataBind();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void Carregar()
    {
        try
        {
            SegmentoFilhoVO segFilho = repoSegmentoFilho.FindBy(x => x.Id == Codigo && CodigoTela == x.tela.Id);

            if (segFilho != null)
            {
                txtNome.Text = segFilho.nome;
                txtDescricao.Text = segFilho.descricao;
                txtOrdem.Text = segFilho.ordem;
                chkVisivel.Checked = segFilho.visivel;

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
            repoSegmentoFilho.Delete(repoSegmentoFilho.FindBy(Convert.ToInt32(gvSegmento.DataKeys[e.RowIndex].Value)));
            MetodosFE.mostraMensagem("Segmento filho excluido com sucesso", "sucesso");
            this.Limpar();
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
            SegmentoFilhoVO segFilho = new SegmentoFilhoVO();

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("É preciso definir o nome do Segmento filho.");

            segFilho.nome = txtNome.Text;
            segFilho.descricao = txtDescricao.Text;
            segFilho.ordem = txtOrdem.Text;
            segFilho.visivel = chkVisivel.Checked;
            segFilho.tela = new Tela() { Id = CodigoTela };
            segFilho.segPai = new SegmentoPaiVO() { Id = Convert.ToInt32(ddlSegmentoPai.SelectedValue) };


            segFilho.chave = ( segFilho.nome).ToSeoUrl();

            IList<SegmentoFilhoVO> categorias = repoSegmentoFilho.All().Where(x => x.chave == segFilho.chave).ToList();

            if (categorias.Count > 0)
            {
                //if (categorias[0].id != Codigo)
                for (int cont = 0; ; cont++)
                {
                    segFilho.chave = segFilho.nome.ToSeoUrl() + cont;
                    categorias = repoSegmentoFilho.All().Where(x => x.chave == segFilho.chave).ToList();
                    if (categorias.Count == 0)
                        break;
                }
            }

            repoSegmentoFilho.Add(segFilho);
            MetodosFE.mostraMensagem("Segmento filho " + segFilho.nome + " cadastrado com sucesso.", "sucesso");
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
            SegmentoFilhoVO segFilho = repoSegmentoFilho.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("É preciso definir o nome do Segmento filho.");

            segFilho.nome = txtNome.Text;
            segFilho.descricao = txtDescricao.Text;
            segFilho.ordem = txtOrdem.Text;
            segFilho.visivel = chkVisivel.Checked;
            segFilho.segPai = new SegmentoPaiVO() { Id = Convert.ToInt32(ddlSegmentoPai.SelectedValue) };
            segFilho.tela = new Tela() { Id = CodigoTela };

            segFilho.chave = (segFilho.nome).ToSeoUrl();

            IList<SegmentoFilhoVO> categorias = repoSegmentoFilho.All().Where(x => x.chave == segFilho.chave).ToList();

            if (categorias.Count > 0)
            {
                if (categorias[0].Id != Codigo)
                    for (int cont = 0; ; cont++)
                    {
                        segFilho.chave = segFilho.nome.ToSeoUrl() + cont;
                        categorias = repoSegmentoFilho.All().Where(x => x.chave == segFilho.chave).ToList();
                        if (categorias.Count == 0)
                            break;
                    }
            }

            repoSegmentoFilho.Update(segFilho);
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
            Pesquisar();
            btnAlterar.Visible = false;
            btnPesquisar.Visible = true;
            btnSalvar.Visible = true;
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

    protected void gvSegmento_Sorting(object sender, GridViewSortEventArgs e)
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
    private int CodigoTela
    {
        get
        {
            if (ViewState["CodigoTela"] == null) ViewState["CodigoTela"] = 0;
            return (Int32)ViewState["CodigoTela"];
        }
        set { ViewState["CodigoTela"] = value; }
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

    protected void CarregarDropTela()
    {
        IList<Tela> telas = null;
        if (ControleLogin.GetUsuarioLogado().tipo != "AA")
        {
            var permissoes = repoPermissao.FilterBy(x => x.usuario.Id == ControleLogin.GetUsuarioLogado().Id);
            telas = repoTela.All().Fetch(x => x.campos).Where(x => x.pagina != null && permissoes.Any(y => y.paginaDeControle != null && y.paginaDeControle.Id == x.pagina.Id) && x.campos.Any(z => z.destino == "DropSegmentoFilho")).ToList();
        }
        else
            telas = repoTela.All().Fetch(x => x.campos).Where(x => x.campos.Any(z => z.destino == "DropSegmentoFilho")).ToList();

        ddlTela.DataSource = telas;
        ddlTela.DataTextField = "nome";
        ddlTela.DataValueField = "id";
        ddlTela.DataBind();



        if (Request.QueryString["Tela"] != null)
            ddlTela.SelectedValue = Request.QueryString["Tela"];

        if(ddlTela.Items.Count > 0)
        CodigoTela = Convert.ToInt32(ddlTela.SelectedValue);


        IList<SegmentoPaiVO> segmentoPai = repoSegmentoPai.FilterBy(x => x.tela.Id == CodigoTela).OrderBy(x => x.nome).ToList();

        ddlSegmentoPai.DataSource = segmentoPai;
        ddlSegmentoPai.DataTextField = "nome";
        ddlSegmentoPai.DataValueField = "id";
        ddlSegmentoPai.DataBind();



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

    protected void gvSegmento_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Codigo = Convert.ToInt32(gvSegmento.DataKeys[e.NewEditIndex].Value);

            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("Codigo", Codigo.ToString());
            string url = Request.Url.AbsolutePath;
            //nameValues.Remove("Codigo");
            string updatedQueryString = "?" + nameValues.ToString();
            string urlFinal = url + updatedQueryString;
            e.Cancel = true;
            Response.Redirect(urlFinal);
        }
        catch (Exception ex) { }
    }
}
