using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }

    private Repository<Tela> repoTela
    {
        get
        {
            return new Repository<Tela>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<UploadTela> repoUpload
    {
        get
        {
            return new Repository<UploadTela>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<GrupoDePaginasVO> repoGrupoPaginas
    {
        get
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PaginaDeControleVO> repoPaginasControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }

    //private Repository<EstadoVO> repoEstado
    //{
    //    get
    //    {
    //        return new Repository<EstadoVO>(NHibernateHelper.CurrentSession);
    //    }
    //}
    //private Repository<CidadeVO> repoCidade
    //{
    //    get
    //    {
    //        return new Repository<CidadeVO>(NHibernateHelper.CurrentSession);
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Tela de Seg. Filho";
        nome2 = "Tela de Seg. Filho";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            //carregarGrupoPaginas();
            Carregar();
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
            Tela tela = repoTela.FindBy(x => x.nomeFixo == "SegmentoFilho");

            if (tela == null)
            {

                tela = new Tela() { nome = "Segmento Filho", nomeFixo = "SegmentoFilho", pagina = null };
                repoTela.Add(tela);
            }

            Codigo = tela.Id;

            txtNome.Text = tela.nome;
            //ddlGrupoDePagina.SelectedValue = tela.grupo.id.ToString();

            CampoTela campo;

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtOrdem");
            if (campo != null)
            {
                chkOrdem.Checked = true;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtDescricao");
            if (campo != null)
            {
                chkDescricao.Checked = true;
                //ddlNomeClasse.SelectedValue = campo.classe;
            }

            if (tela.pagina != null)
                chkTelaVisivel.Checked = true;


            if (tela.upload != null)
            {
                chkUpload.Checked = true;
                txtUplLarguraG.Text = tela.upload.TamFotoGrW.ToString();
                txtUplAlturaG.Text = tela.upload.TamFotoGrH.ToString();
                txtUplLarguraP.Text = tela.upload.TamFotoPqW.ToString();
                txtUplAlturaP.Text = tela.upload.TamFotoPqH.ToString();
                txtUplQuantidade.Text = tela.upload.QtdeFotos.ToString();
                txtUplQualidade.Text = tela.upload.Qualidade.ToString();
                txtUplCor.Text = tela.upload.Cor;
                ddlUplConfiguracao.SelectedValue = tela.upload.Configuracao.ToString();
            }

            btnAlterar.Visible = true;
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
            Tela tela = repoTela.FindBy(x => x.nomeFixo == "SegmentoFilho");

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("Campo Nome é obrigatório.");

            if (repoTela.FindBy(x => x.nome == txtNome.Text && x.Id != Codigo) != null)
                throw new Exception("Já existe página com este nome neste mesmo grupo.");






            //tela.grupo = repoGrupoPaginas.FindBy(Convert.ToInt32(ddlGrupoDePagina.SelectedValue));


            UploadTela upload = null;
            if (chkUpload.Checked)
            {
                if (tela.upload == null)
                {
                    tela.upload = new UploadTela();
                    repoUpload.Add(tela.upload);
                }

                try
                {
                    tela.upload.TamFotoGrW = Convert.ToInt32(txtUplLarguraG.Text);
                    tela.upload.TamFotoGrH = Convert.ToInt32(txtUplAlturaG.Text);
                    tela.upload.TamFotoPqW = Convert.ToInt32(txtUplLarguraP.Text);
                    tela.upload.TamFotoPqH = Convert.ToInt32(txtUplAlturaP.Text);
                    tela.upload.QtdeFotos = Convert.ToInt32(txtUplQuantidade.Text);
                    tela.upload.Qualidade = Convert.ToInt32(txtUplQualidade.Text);
                    tela.upload.Cor = !String.IsNullOrEmpty(txtUplCor.Text) ? txtUplCor.Text : null;
                    tela.upload.Configuracao = Convert.ToInt32(ddlUplConfiguracao.SelectedValue);
                    repoUpload.Update(tela.upload);
                }
                catch (Exception)
                {
                    throw new Exception("Valores para o upload inválido.");
                }
            }
            else
            {
                upload = tela.upload;
                if (tela.upload != null)
                    //repoUpload.Delete(tela.upload);
                    tela.upload = null;
            }




            tela.campos.Clear();

            CampoTela campo;

            if (chkOrdem.Checked)
            {

                campo = new CampoTela() { nome = "", destino = "txtOrdem" };
                tela.campos.Add(campo);
            }


            if (chkDescricao.Checked)
            {

                campo = new CampoTela() { nome = "", destino = "txtDescricao", classe = "" };
                tela.campos.Add(campo);
            }

            //foreach (var item in tela.campos)
            //    item.tela = tela;


            PaginaDeControleVO pagina = null;
            if (chkTelaVisivel.Checked)
            {
                pagina = repoPaginasControle.FindBy(x =>  x.nome == tela.nome);

                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = tela.nome, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/SegmentosFilho.aspx",fixa=true };
                    repoPaginasControle.Add(pagina);
                }

                tela.pagina = pagina;
            }
            else
            {
                if (tela.pagina != null)
                    pagina = tela.pagina;
                tela.pagina = null;
            }

            tela.nome = txtNome.Text;

            if (tela.pagina != null)
            {    
                pagina.nome = tela.nome;
                //pagina.grupoDePaginas = tela.grupo;
            }

            repoTela.Update(tela);
            if (!chkTelaVisivel.Checked)
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            if (upload != null)
                repoUpload.Delete(upload);


            MetodosFE.mostraMensagem(nome2 + " alterado com sucesso.", "sucesso");
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

}
