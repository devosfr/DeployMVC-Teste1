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
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                    Pesquisar();
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
        String nome = "SegmentoPai";

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
                uplSegPai.Codigo = Codigo;
                uplSegPai.setConfiguracoes(tela.upload);

            }
            else
                uplSegPai.Visible = false;

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
            var pesquisa = repoSegmentoPai.FilterBy(x=>x.tela.Id == CodigoTela);

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

            IList<SegmentoPaiVO> colecaoSegmento = pesquisa.OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();


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
            SegmentoPaiVO segPai = repoSegmentoPai.FindBy(x => x.Id == Codigo && CodigoTela == x.tela.Id);

            if (segPai != null)
            {
                txtNome.Text = segPai.nome;
                txtDescricao.Text = segPai.descricao;
                txtOrdem.Text = segPai.ordem;
                chkVisivel.Checked = segPai.visivel;

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
            repoSegmentoPai.Delete(repoSegmentoPai.FindBy(Convert.ToInt32(gvSegmento.DataKeys[e.RowIndex].Value)));
            MetodosFE.mostraMensagem("Segmento pai excluido com sucesso", "sucesso");
            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void gvSegmento_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        if (ordenacao == Ordenacao)
            asc = !asc;
        Ordenacao = ordenacao;
        Pesquisar();
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            SegmentoPaiVO segPai = new SegmentoPaiVO();

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("É preciso definir o nome do segmento pai.");

            segPai.nome = txtNome.Text;
            segPai.descricao = txtDescricao.Text;
            segPai.ordem = txtOrdem.Text;
            segPai.visivel = chkVisivel.Checked;
            segPai.tela = new Tela() { Id = CodigoTela };


            segPai.chave = ( segPai.nome).ToSeoUrl();

            IList<SegmentoPaiVO> categorias = repoSegmentoPai.All().Where(x => x.chave == segPai.chave).ToList();

            if (categorias.Count > 0)
            {
                //if (categorias[0].id != Codigo)
                for (int cont = 0; ; cont++)
                {
                    segPai.chave = segPai.nome.ToSeoUrl() + cont;
                    categorias = repoSegmentoPai.All().Where(x => x.chave == segPai.chave).ToList();
                    if (categorias.Count == 0)
                        break;
                }
            }

            repoSegmentoPai.Add(segPai);
            MetodosFE.mostraMensagem("Segmento pai " + segPai.nome + " cadastrado com sucesso.", "sucesso");
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
            SegmentoPaiVO segPai = repoSegmentoPai.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("É preciso definir o nome do segmento pai.");

            segPai.nome = txtNome.Text;
            segPai.descricao = txtDescricao.Text;
            segPai.ordem = txtOrdem.Text;
            segPai.visivel = chkVisivel.Checked;
            segPai.tela = new Tela() { Id = CodigoTela };

            segPai.chave = (segPai.nome).ToSeoUrl();

            IList<SegmentoPaiVO> categorias = repoSegmentoPai.All().Where(x => x.chave == segPai.chave).ToList();

            if (categorias.Count > 0)
            {
                if (categorias[0].Id != Codigo)
                    for (int cont = 0; ; cont++)
                    {
                        segPai.chave = segPai.nome.ToSeoUrl() + cont;
                        categorias = repoSegmentoPai.All().Where(x => x.chave == segPai.chave).ToList();
                        if (categorias.Count == 0)
                            break;
                    }
            }

            repoSegmentoPai.Update(segPai);
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

    protected void CarregarDropTela()
    {
        IList<Tela> telas = null;
        if (ControleLogin.GetUsuarioLogado().tipo != "AA")
        {
            var permissoes = repoPermissao.FilterBy(x => x.usuario.Id == ControleLogin.GetUsuarioLogado().Id);
            telas = repoTela.All().Fetch(x => x.campos).Where(x => x.pagina != null && permissoes.Any(y => y.paginaDeControle != null && x.pagina != null && y.paginaDeControle.Id == x.pagina.Id) && x.campos.Any(z => z.destino == "DropSegmentoPai")).ToList();
        }
        else
            telas = repoTela.All().Fetch(x => x.campos).Where(x => x.pagina != null && x.campos.Any(z => z.destino == "DropSegmentoPai")).ToList();

        ddlTela.DataSource = telas;
        ddlTela.DataTextField = "nome";
        ddlTela.DataValueField = "id";
        ddlTela.DataBind();

        if (Request.QueryString["Tela"] != null)
            ddlTela.SelectedValue = Request.QueryString["Tela"];

        if (CodigoTela == 0)
            CodigoTela = Convert.ToInt32(ddlTela.SelectedValue);
        else
            ddlTela.SelectedValue = CodigoTela.ToString();
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
