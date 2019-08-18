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
        nome = "Páginas";
        nome2 = "Página";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            //carregarGrupoPaginas();
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
            //string nome = null;
            //if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            //{
            //    nome = txtBuscaNome.Text.Trim();
            //}

            //int id = 0;
            //if (!String.IsNullOrEmpty(txtIDBusca.Text))
            //{
            //    id = Convert.ToInt32(txtIDBusca.Text);
            //}
            string orde =  Ordenacao + (asc ? " asc" : " desc") ;
            var colecaoEstado = repoTela.FilterBy(x => x.nomeFixo == null);//.FilterBy(x => (id > 0 && x.id == id || true) && (!String.IsNullOrEmpty(nome) && x.nome.Contains(nome) || true)).OrderBy(ordenacao).ToList();

            IList<Tela> colecaoInvisiveis = colecaoEstado.Where(x => x.pagina == null).ToList();

            IList<Tela> listagem = colecaoEstado.Where(x => x.pagina != null).OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();

            listagem = listagem.Concat(colecaoInvisiveis).ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = listagem;
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
            Tela tela = repoTela.FindBy(Codigo);

            if (tela == null)
                throw new Exception("Tela não encontrada.");

            txtNome.Text = tela.nome;

            if (tela.pagina != null)
                chkTelaVisivel.Checked = true;

            //ddlGrupoDePagina.SelectedValue = tela.grupo.id.ToString();

            chkMulti.Checked = tela.multiplo;

            CampoTela campo;

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtNome");
            if (campo != null)
            {
                chkNome.Checked = true;
                txtNomeNome.Text = campo.nome;
                ddlNomeClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtReferencia");
            if (campo != null)
            {
                chkReferencia.Checked = true;
                txtReferenciaNome.Text = campo.nome;
                ddlReferenciaClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "DropDestaque");
            if (campo != null)
            {
                chkDestaque.Checked = true;
                txtNomeDestaque.Text = campo.nome;
                
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtData");
            if (campo != null)
            {
                chkData.Checked = true;
                txtNomeData.Text = campo.nome;
                //ddlReferenciaClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtKeywords");
            if (campo != null)
            {
                chkKeywords.Checked = true;
                txtNomeKeywords.Text = campo.nome;
                //ddlReferenciaClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtValor");
            if (campo != null)
            {
                chkValor.Checked = true;
                txtValorNome.Text = campo.nome;
                ddlValorClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtOrdem");
            if (campo != null)
            {
                chkOrdem.Checked = true;
                txtOrdemNome.Text = campo.nome;
                ddlOrdemClasse.SelectedValue = campo.classe;
            }


            campo = tela.campos.FirstOrDefault(x => x.destino == "txtMeta");
            if (campo != null)
            {
                chkMeta.Checked = true;
                txtNomeMeta.Text = campo.nome;
                //ddlReferenciaClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtResumo");
            if (campo != null)
            {
                chkResumo.Checked = true;
                txtResumoNome.Text = campo.nome;
                //ddlResumoClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "txtDescricao");
            if (campo != null)
            {
                chkDescricao.Checked = true;
                txtDescricaoNome.Text = campo.nome;
                //ddlNomeClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "DropSegmentoPai");
            if (campo != null)
            {
                chkSegmentoPai.Checked = true;
                txtSegmentoPaiNome.Text = campo.nome;
                //ddlNomeClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "DropSegmentoFilho");
            if (campo != null)
            {
                chkSegmentoFilho.Checked = true;
                txtSegmentoFilhoNome.Text = campo.nome;
                //ddlNomeClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "DropCategoria");
            if (campo != null)
            {
                chkCategoria.Checked = true;
                txtCategoriaNome.Text = campo.nome;
                //ddlNomeClasse.SelectedValue = campo.classe;
            }

            campo = tela.campos.FirstOrDefault(x => x.destino == "DropVisivel");
            if (campo != null)
            {
                chkVisivel.Checked = true;
                txtVisivelNome.Text = campo.nome;
                //ddlVisivelClasse.SelectedValue = campo.classe;
            }

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

            btnSalvar.Visible = false;
            btnPesquisar.Visible = false;
            btnAlterar.Visible = true;

            //BairroVO colecaoEstado = repoBairro.FindBy(Codigo);

            //if (colecaoEstado != null)
            //{
            //    txtNome.Text = colecaoEstado.nome;
            //    txtID.Text = colecaoEstado.id.ToString();
            //    
            //}
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
            Tela tela = new Tela();

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("Campo Nome é obrigatório.");

            if (repoTela.FindBy(x => x.nome == txtNome.Text) != null)
                throw new Exception("Já existe página com este nome neste mesmo grupo.");



            tela.nome = txtNome.Text;

            tela.multiplo = chkMulti.Checked;

            //tela.grupo = repoGrupoPaginas.FindBy(Convert.ToInt32(ddlGrupoDePagina.SelectedValue));

            UploadTela upload;

            if (chkUpload.Checked)
            {
                upload = new UploadTela();

                try
                {
                    upload.TamFotoGrW = Convert.ToInt32(txtUplLarguraG.Text);
                    upload.TamFotoGrH = Convert.ToInt32(txtUplAlturaG.Text);
                    upload.TamFotoPqW = Convert.ToInt32(txtUplLarguraP.Text);
                    upload.TamFotoPqH = Convert.ToInt32(txtUplAlturaP.Text);
                    upload.QtdeFotos = Convert.ToInt32(txtUplQuantidade.Text);
                    upload.Qualidade = Convert.ToInt32(txtUplQualidade.Text);
                    upload.Cor = !String.IsNullOrEmpty(txtUplCor.Text) ? txtUplCor.Text : null;
                    upload.Configuracao = Convert.ToInt32(ddlUplConfiguracao.SelectedValue);
                    repoUpload.Add(upload);
                }
                catch (Exception)
                {
                    throw new Exception("Valores para o upload inválido.");
                }

                tela.upload = upload;
            }

            CampoTela campo;
            if (chkNome.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeNome.Text, destino = "txtNome", classe = ddlNomeClasse.SelectedValue };
                tela.campos.Add(campo);
            }
            if (chkReferencia.Checked)
            {
                if (String.IsNullOrEmpty(txtReferenciaNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtReferenciaNome.Text, destino = "txtReferencia", classe = ddlReferenciaClasse.SelectedValue };
                tela.campos.Add(campo);
            }
            if (chkValor.Checked)
            {
                if (String.IsNullOrEmpty(txtValorNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtValorNome.Text, destino = "txtValor", classe = ddlValorClasse.SelectedValue };
                tela.campos.Add(campo);
            }
            if (chkOrdem.Checked)
            {
                if (String.IsNullOrEmpty(txtOrdemNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtOrdemNome.Text, destino = "txtOrdem", classe = ddlOrdemClasse.SelectedValue };
                tela.campos.Add(campo);
            }

            if (chkDestaque.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeDestaque.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeDestaque.Text, destino = "DropDestaque", classe = ddlValorClasse.SelectedValue };
                tela.campos.Add(campo);
            }
            if (chkKeywords.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeKeywords.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeKeywords.Text, destino = "txtKeywords", classe = "" };
                tela.campos.Add(campo);
            }

            if (chkMeta.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeMeta.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeMeta.Text, destino = "txtMeta", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkResumo.Checked)
            {
                if (String.IsNullOrEmpty(txtResumoNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtResumoNome.Text, destino = "txtResumo", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkDescricao.Checked)
            {
                if (String.IsNullOrEmpty(txtDescricaoNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtDescricaoNome.Text, destino = "txtDescricao", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkData.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeData.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeData.Text, destino = "txtData", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkSegmentoPai.Checked)
            {
                if (String.IsNullOrEmpty(txtSegmentoPaiNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtSegmentoPaiNome.Text, destino = "DropSegmentoPai", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkSegmentoFilho.Checked)
            {
                if (String.IsNullOrEmpty(txtSegmentoFilhoNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtSegmentoFilhoNome.Text, destino = "DropSegmentoFilho", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkCategoria.Checked)
            {
                if (String.IsNullOrEmpty(txtCategoriaNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtCategoriaNome.Text, destino = "DropCategoria", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkVisivel.Checked)
            {
                if (String.IsNullOrEmpty(txtVisivelNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtVisivelNome.Text, destino = "DropVisivel", classe = "" };
                tela.campos.Add(campo);
            }

            repoTela.Add(tela);

            PaginaDeControleVO pagina = new PaginaDeControleVO();
            pagina.nome = tela.nome;
            pagina.pagina = "";
            pagina.grupoDePaginas = null;
            repoPaginasControle.Add(pagina);
            //pagina.pagina = MetodosFE.BaseURL + "/Controle/Cadastro/" + ddlGrupoDePagina.SelectedValue + "/" + pagina.id;

            tela.pagina = pagina;
            repoTela.Update(tela);

            //foreach (var item in tela.campos)
            //    item.tela = tela;

            //repoTela.Update(tela);


            MetodosFE.mostraMensagem(" " + nome2 + " " + tela.nome + " cadastrado com sucesso.", "sucesso");
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
            Tela tela = repoTela.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception("Campo Nome é obrigatório.");

            if (repoTela.FindBy(x => x.nome == txtNome.Text && x.Id != Codigo) != null)
                throw new Exception("Já existe página com este nome neste mesmo grupo.");





            tela.multiplo = chkMulti.Checked;

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
            if (chkNome.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeNome.Text, destino = "txtNome", classe = ddlNomeClasse.SelectedValue };
                tela.campos.Add(campo);
            }

            if (chkReferencia.Checked)
            {
                if (String.IsNullOrEmpty(txtReferenciaNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtReferenciaNome.Text, destino = "txtReferencia", classe = ddlReferenciaClasse.SelectedValue };
                tela.campos.Add(campo);
            }
            if (chkKeywords.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeKeywords.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeKeywords.Text, destino = "txtKeywords", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkData.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeData.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeData.Text, destino = "txtData", classe = "" };
                tela.campos.Add(campo);
            }

            if (chkValor.Checked)
            {
                if (String.IsNullOrEmpty(txtValorNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtValorNome.Text, destino = "txtValor", classe = ddlValorClasse.SelectedValue };
                tela.campos.Add(campo);
            }
            if (chkDestaque.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeDestaque.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeDestaque.Text, destino = "DropDestaque", classe = ddlValorClasse.SelectedValue };
                tela.campos.Add(campo);
            }

            if (chkOrdem.Checked)
            {
                if (String.IsNullOrEmpty(txtOrdemNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtOrdemNome.Text, destino = "txtOrdem", classe = ddlOrdemClasse.SelectedValue };
                tela.campos.Add(campo);
            }

            if (chkMeta.Checked)
            {
                if (String.IsNullOrEmpty(txtNomeMeta.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtNomeMeta.Text, destino = "txtMeta", classe = "" };
                tela.campos.Add(campo);
            }

            if (chkResumo.Checked)
            {
                if (String.IsNullOrEmpty(txtResumoNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtResumoNome.Text, destino = "txtResumo", classe = "" };
                tela.campos.Add(campo);
            }


            if (chkDescricao.Checked)
            {
                if (String.IsNullOrEmpty(txtDescricaoNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtDescricaoNome.Text, destino = "txtDescricao", classe = "" };
                tela.campos.Add(campo);
            }

            if (chkSegmentoPai.Checked)
            {
                if (String.IsNullOrEmpty(txtSegmentoPaiNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtSegmentoPaiNome.Text, destino = "DropSegmentoPai", classe = "" };
                tela.campos.Add(campo);
            }
            if (chkSegmentoFilho.Checked)
            {
                if (String.IsNullOrEmpty(txtSegmentoFilhoNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtSegmentoFilhoNome.Text, destino = "DropSegmentoFilho", classe = "" };
                tela.campos.Add(campo);
            }

            if (chkCategoria.Checked)
            {
                if (String.IsNullOrEmpty(txtCategoriaNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtCategoriaNome.Text, destino = "DropCategoria", classe = "" };
                tela.campos.Add(campo);
            }

            if (chkVisivel.Checked)
            {
                if (String.IsNullOrEmpty(txtVisivelNome.Text))
                    throw new Exception("É preciso especificar o nome dos campos selecionados.");

                campo = new CampoTela() { nome = txtVisivelNome.Text, destino = "DropVisivel", classe = "" };
                tela.campos.Add(campo);
            }

            //foreach (var item in tela.campos)
            //    item.tela = tela;
            PaginaDeControleVO pagina = null;

            if (chkTelaVisivel.Checked)
            {
                pagina = repoPaginasControle.FindBy(x => x.nome == tela.nome);

                if (pagina == null)
                {

                    pagina = new PaginaDeControleVO();
                    pagina.nome = tela.nome;
                    pagina.pagina = "";
                    pagina.grupoDePaginas = null;
                    repoPaginasControle.Add(pagina);
                    
                }

                tela.pagina = pagina;
                //tela.pagina.pagina = MetodosFE.BaseURL + "/Controle/Cadastro/" + ddlGrupoDePagina.SelectedValue + "/" + pagina.id;
            }
            else
            {
                pagina = tela.pagina;
                tela.pagina = null;
            }

            tela.nome = txtNome.Text;

            if (chkTelaVisivel.Checked)
            {
                tela.pagina.nome = tela.nome;
                //tela.pagina.grupoDePaginas = tela.grupo;
                //tela.pagina.pagina = MetodosFE.BaseURL + "/Controle/Cadastro/" + ddlGrupoDePagina.SelectedValue + "/" + tela.id;
            }



            repoTela.Update(tela);
            if (upload != null)
                repoUpload.Delete(upload);
            if (!chkTelaVisivel.Checked)
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

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
                ViewState["Ordenacao"] = "nome";
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
        Pesquisar();
    }
    protected void gvObjeto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Tela tela = repoTela.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));
            repoTela.Delete(tela);
            if (tela.upload != null)
                repoUpload.Delete(tela.upload);
            if (tela.pagina != null)
                repoPaginasControle.Delete(tela.pagina);
            MetodosFE.mostraMensagem(nome2 + " " + tela.nome + " excluído com sucesso.", "sucesso");
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
}
