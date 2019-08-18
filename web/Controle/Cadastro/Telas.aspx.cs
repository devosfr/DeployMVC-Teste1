using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using Modelos;

public partial class Controle_Cadastro_Cursos : System.Web.UI.Page
{

    private Repository<DadoVO> repoDado
    {
        get
        {
            return new Repository<DadoVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Tela> repoTela
    {
        get
        {
            return new Repository<Tela>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<SegmentoPaiVO> repoSegPai
    {
        get
        {
            return new Repository<SegmentoPaiVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<SegmentoFilhoVO> repoSegFilho
    {
        get
        {
            return new Repository<SegmentoFilhoVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<CategoriaVO> repoCategoria
    {
        get
        {
            return new Repository<CategoriaVO>(NHibernateHelper.CurrentSession);
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
            try
            {
                setAllInv();
                divDados.DefaultButton = btnSalvar.ID;
                //CarregarDropSegmentoPai();
                if (Page.RouteData.Values["Grupo"] != null && Page.RouteData.Values["Pagina"] != null)
                {
                    ajustaTela();
                    if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
                    {
                        Codigo = Convert.ToInt32(Request.QueryString["Codigo"]);
                        Carregar();
                    }
                    else
                    {
                        CarregarDropSegmentoPai();
                        CarregarDropSegmentoFilho();
                        CarregarDropCategoria();
                    }

                }
            }
            catch (Exception ex)
            {
                MetodosFE.mostraMensagem(ex.GetType() + " " + ex.Message);
            }
        }
        else
        {
        }
    }
    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litMensagem.Text = mensagem != null ? mensagem : "";
    }

    protected void ajustaTela()
    {

        Tela tela = repoTela.FindBy(x => x.pagina.Id.ToString() == Page.RouteData.Values["Pagina"] && x.pagina.grupoDePaginas.Id.ToString() == Page.RouteData.Values["Grupo"].ToString());
        CodigoTela = tela.Id;

        setAllInv();

        lblPagina.Text = tela.nome;
        lblSegmentoPai.Text = "Tela:";

        if (tela != null)
        {
            foreach (CampoTela campo in tela.campos)
            {
                switch (campo.destino)
                {
                    case "txtNome":
                        lblNome.Text = lblBuscaNome.Text = campo.nome;
                        liBuscaNome.Visible = true;
                        liNome.Visible = true;
                        //if(String.IsNullOrEmpty(campo.campoCSS))
                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            txtNome.Style.Add(css.Key, css.Value);
                        //        }

                        if (!String.IsNullOrEmpty(campo.classe))
                            txtNome.CssClass += " " + campo.classe;
                        gvDados.Columns[1].HeaderText = campo.nome;
                        break;
                    case "txtResumo":
                        liResumo.Visible = true;
                        lblResumo.Text = campo.nome;
                        break;
                    case "txtReferencia":
                        lblReferencia.Text = lblBuscaReferencia.Text = campo.nome;
                        liReferencia.Visible = liBuscaReferencia.Visible = true;
                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            txtReferencia.Style.Add(css.Key, css.Value);
                        //        }
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtReferencia.CssClass += " " + campo.classe;
                        break;
                    case "DropDestaque":
                        lblDestaque.Text = lblBuscaDestaque.Text = campo.nome;
                        liDestaque.Visible = liBuscaVisivel.Visible = true;
                        gvDados.Columns[6].HeaderText = campo.nome;
                        gvDados.Columns[6].Visible = true;

                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            DropDestaque.Style.Add(css.Key, css.Value);
                        //        }
                        //if(!String.IsNullOrEmpty(campo.classe))
                        //   txtNome.CssClass += campo.classe;
                        break;
                    case "txtValor":
                        lblvalor.Text = campo.nome;
                        liPreco.Visible = true;
                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            txtPreco.Style.Add(css.Key, css.Value);
                        //        }
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtPreco.CssClass += " " + campo.classe;

                        break;
                    case "txtMeta":
                        lblMeta.Text = campo.nome;
                        
                        liMeta.Visible = true;
                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            txtPreco.Style.Add(css.Key, css.Value);
                        //        }
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtMeta.CssClass += " " + campo.classe;
                        break;
                    case "txtData":
                        lblData.Text = campo.nome;
                        liData.Visible = true;
                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            txtPreco.Style.Add(css.Key, css.Value);
                        //        }
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtData.CssClass += " " + campo.classe;

                        gvDados.Columns[9].Visible = true;
                        gvDados.Columns[9].HeaderText = campo.nome;
                        break;
                    case "DropVisivel":
                        lblvisivel.Text = lblBuscaVisivel.Text = "Visivel:";
                        liVisivel.Visible = liBuscaVisivel.Visible = true;
                        gvDados.Columns[7].Visible = true;
                        gvDados.Columns[7].HeaderText = campo.nome;
                        //if (!String.IsNullOrEmpty(campo.classe))
                        //    txtNome.CssClass += campo.classe;
                        break;

                    case "DropSegmentoPai":
                        lblSegmentoPai.Text = lblBuscaSegmentoPai.Text = campo.nome;
                        liSegPai.Visible = liBuscaSegmentoPai.Visible = true;
                        gvDados.Columns[5].Visible = true;
                        gvDados.Columns[5].HeaderText = campo.nome;
                        //if (!String.IsNullOrEmpty(campo.classe))
                        //    txtNome.CssClass += campo.classe;
                        break;

                    case "DropSegmentoFilho":
                        lblSegmentoFilho.Text = lblBuscaSegmentoFilho.Text = campo.nome;
                        liSegFilho.Visible = liBuscaSegmentoFilho.Visible = true;
                        gvDados.Columns[4].Visible = true;
                        gvDados.Columns[4].HeaderText = campo.nome;

                        //if (!String.IsNullOrEmpty(campo.classe))
                        //    txtNome.CssClass += campo.classe;
                        break;
                    case "DropCategoria":
                        lblCategoria.Text = lblBuscaCategoria.Text = campo.nome;
                        liCategoria.Visible = liBuscaCategoria.Visible = true;
                        gvDados.Columns[3].Visible = true;
                        gvDados.Columns[3].HeaderText = campo.nome;
                        //if (!String.IsNullOrEmpty(campo.classe))
                        //    txtNome.CssClass += campo.classe;
                        break;
                    case "txtDescricao":
                        liDescricao.Visible = true;
                        lblDescricao.Text = campo.nome;
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtNome.CssClass += " " + campo.classe;
                        break;
                    case "txtKeywords":
                        liKeywords.Visible = true;
                        lblKeywords.Text = campo.nome;
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtKeywords.CssClass += " " + campo.classe;
                        break;
                    case "txtOrdem":
                        lblOrdem.Text = campo.nome;
                        liOrdem.Visible = true;
                        //if (campo.campoCSS != null)
                        //    if (campo.campoCSS.Count > 0)
                        //        foreach (KeyValuePair<string, string> css in campo.campoCSS)
                        //        {
                        //            txtOrdem.Style.Add(css.Key, css.Value);
                        //        }
                        if (!String.IsNullOrEmpty(campo.classe))
                            txtOrdem.CssClass += " " + campo.classe;
                        gvDados.Columns[8].Visible = true;
                        gvDados.Columns[8].HeaderText = campo.nome;
                        break;
                    //case "uplLogo":
                    //    liUpload.Visible = true;
                    //    break;
                }
            }

            CarregaDados();
        }
    }

    protected void setAllInv()
    {
        liSegPai.Visible = false;
        liSegFilho.Visible = false;
        liBuscaSegmentoFilho.Visible = false;
        liCategoria.Visible = false;
        liBuscaCategoria.Visible = false;
        liOrdem.Visible = false;
        liData.Visible = false;
        liKeywords.Visible = false;
        txtMeta.RemovePlugins = "forms";
        txtResumo.RemovePlugins = "forms";

        txtDescricao.RemovePlugins = "forms";

        liBuscaSegmentoPai.Visible = false;
        liNome.Visible = false;
        liBuscaNome.Visible = false;
        liResumo.Visible = false;

        liReferencia.Visible = false;
        liBuscaReferencia.Visible = false;
        liDestaque.Visible = false;
        liBuscaDestaque.Visible = false;
        liPreco.Visible = false;

        liVisivel.Visible = false;
        liBuscaVisivel.Visible = false;
        liDescricao.Visible = false;
        liMeta.Visible = false;

        divLista.Style.Add("display", "none");

        liUpload.Visible = false;

        gvDados.Columns[9].Visible = false;
        gvDados.Columns[8].Visible = false;
        gvDados.Columns[7].Visible = false;
        gvDados.Columns[6].Visible = false;
        gvDados.Columns[5].Visible = false;
        gvDados.Columns[4].Visible = false;
        gvDados.Columns[3].Visible = false;
        gvDados.Columns[2].Visible = false;



        btnSalvar.Visible = false;
        btnAlterar.Visible = false;
        btnCancelar.Visible = false;

        lblPagina.Text = "";
    }

    protected void CarregarDropSegmentoPai()
    {
        try
        {
            //Prenche o drop.
            var segmentosPai = repoSegPai.FilterBy(x => x.tela.Id == CodigoTela).OrderBy(x => x.nome).ToList();

            DropSegmentoPai.DataSource = segmentosPai;
            DropSegmentoPai.DataTextField = "nome";
            DropSegmentoPai.DataValueField = "id";
            DropSegmentoPai.DataBind();
            DropSegmentoPai.Items.Insert(0, new ListItem("Selecione", ""));

            DropBuscaSegmentPai.DataSourceID = String.Empty;
            DropBuscaSegmentPai.DataSource = segmentosPai;
            DropBuscaSegmentPai.DataTextField = "nome";
            DropBuscaSegmentPai.DataValueField = "id";
            DropBuscaSegmentPai.DataBind();
            DropBuscaSegmentPai.Items.Insert(0, new ListItem("Selecione", "0"));

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }
    }

    protected void CarregarDropSegmentoFilho()
    {
        try
        {
            //Prenche o drop pai.

            SegmentoPaiVO segPai = repoSegPai.FilterBy(x => x.Id.ToString() == DropSegmentoPai.SelectedValue).FirstOrDefault();
            IList<SegmentoFilhoVO> colecaosegmentos = new List<SegmentoFilhoVO>();
            if (segPai != null)
                colecaosegmentos = repoSegFilho.FilterBy(x => x.segPai.Id == segPai.Id).ToList();


            DropSegmento.DataSourceID = String.Empty;
            DropSegmento.DataSource = colecaosegmentos;
            DropSegmento.DataTextField = "nome";
            DropSegmento.DataValueField = "id";
            DropSegmento.DataBind();
            DropSegmento.Items.Insert(0, new ListItem("Selecione", ""));


            DropBuscaSegmentoFilho.DataSourceID = String.Empty;
            DropBuscaSegmentoFilho.DataSource = colecaosegmentos;
            DropBuscaSegmentoFilho.DataTextField = "nome";
            DropBuscaSegmentoFilho.DataValueField = "id";
            DropBuscaSegmentoFilho.DataBind();
            DropBuscaSegmentoFilho.Items.Insert(0, new ListItem("Selecione", ""));



        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }

    }

    protected void CarregarDropCategoria()
    {
        try
        {
            //Prenche o drop.
            IList<CategoriaVO> colecaosegmentos;
            if (!String.IsNullOrEmpty(DropSegmento.SelectedValue))
                colecaosegmentos = repoCategoria.FilterBy(x => x.segFilho.Id == Convert.ToInt32(DropSegmento.SelectedValue)).OrderBy(x => x.nome).ToList();
            else
                colecaosegmentos = new List<CategoriaVO>();

            DropCategoria.DataSourceID = String.Empty;
            DropCategoria.DataSource = colecaosegmentos;
            DropCategoria.DataTextField = "nome";
            DropCategoria.DataValueField = "id";
            DropCategoria.DataBind();
            DropCategoria.Items.Insert(0, new ListItem("Selecione", ""));

            DropBuscaCategoria.DataSourceID = String.Empty;
            DropBuscaCategoria.DataSource = colecaosegmentos;
            DropBuscaCategoria.DataTextField = "nome";
            DropBuscaCategoria.DataValueField = "id";
            DropBuscaCategoria.DataBind();
            DropBuscaCategoria.Items.Insert(0, new ListItem("Selecione", ""));

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }
    }

    //protected void CarregarDropLoja()
    //{
    //    IList<LojaVO> lojas = repoLoja.All().ToList();
    //    DropLoja.Items.Clear();

    //    for (int i = 0; i < lojas.Count; i++)
    //    {
    //        DropLoja.Items.Add(new ListItem(lojas[i].nome, lojas[i].id.ToString()));
    //    }
    //    if (DropLoja.Items.Count == 1)
    //    {
    //        divLojas.Visible = false;
    //    }

    //}



    protected void Pesquisar()
    {
        try
        {
            var pesquisa = repoDado.FilterBy(x=> x.tela.Id == CodigoTela);

            if (String.IsNullOrEmpty(Ordenacao))
            {
                Ordenacao = "id";
            }

            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text)) 
            {
                nome = txtBuscaNome.Text.ToLower();
                pesquisa = pesquisa.Where(x => (x.nome.ToLower().Contains(nome) || x.referencia.ToLower().Contains(nome) || x.resumo.ToLower().Contains(nome) || x.descricao.ToLower().Contains(nome)));
            }
                
            string referencia = null;
            if (!String.IsNullOrEmpty(txtReferencia.Text))
            {
                referencia = txtReferencia.Text;
                pesquisa = pesquisa.Where(x => x.referencia.ToLower().Contains(referencia.ToLower()));
            }

            int idCategoria = 0;
            if (!String.IsNullOrEmpty(DropBuscaCategoria.SelectedValue)) { 
                idCategoria = Convert.ToInt32(DropBuscaCategoria.SelectedValue);
                pesquisa = pesquisa.Where(x => x.categoria != null && x.categoria.Id == idCategoria);
            }

            int idSegFilho = 0;
            if (!String.IsNullOrEmpty(DropBuscaSegmentoFilho.SelectedValue)) { 
                idSegFilho = Convert.ToInt32(DropBuscaSegmentoFilho.SelectedValue);
                pesquisa = pesquisa.Where(x => x.segFilho != null && x.segFilho.Id == idSegFilho);
            }

            int idSegPai = 0;
            if (!String.IsNullOrEmpty(DropBuscaSegmentPai.SelectedValue))
            {
                idSegPai = Convert.ToInt32(DropBuscaSegmentPai.SelectedValue);
                pesquisa = pesquisa.Where(x => x.segPai != null && x.segPai.Id == idSegPai);
            }
                

            string valorDestaque = null;
            if (!String.IsNullOrEmpty(DropBuscaDestaque.SelectedValue))
            {
                valorDestaque = DropBuscaDestaque.SelectedValue;
                pesquisa = pesquisa.Where(x => x.destaque != null && x.destaque.Equals(valorDestaque));
            }


            Boolean? visivel = null;
            if (liVisivel.Visible)
            {
                if (!String.IsNullOrEmpty(DropBuscaVisivel.SelectedValue))
                {
                    visivel = Convert.ToBoolean(DropBuscaVisivel.SelectedValue);
                    pesquisa = pesquisa.Where(x => x.visivel == visivel);
                }
                    

            }

            //SegmentoPaiVO segPai = repoSegPai.FilterBy(x => x.nome == DropSegmentoPai.SelectedItem.Text).FirstOrDefault();

            IList<DadoVO> colecaoDados = pesquisa.OrderBy(Ordenacao + (asc ? " asc" : " desc")).ToList();

            //(from dado in repoDado.All() where dado.segPai.id == segPai.id and (dado.nome.Contains(nome) or dado.referencia.Contains(nome) or dado.resumo.Contains(nome) or  dado.descricao.Contains(nome)) and  (idSegFilho >0? dado.segFilho.id == idSegFilho:true) and (idCategoria >0 ? dado.categoria.id == idCategoria:true) and (!String.IsNullOrEmpty(valorDestaque)> dado.destaque == valorDestaque) select dado).OrderBy(ordenacao).ToList() ; 
            //repoDado.All().().FindAll(idSegmentoPai: SegmentoPaiBO.getCodSegPai(DropSegmentoPai.SelectedItem.Text), busca: nome, id: id, idSegmentoFilho: idSegFilho, idCategoria: idCategoria, referencia: referencia, destaque: valorDestaque, orderby: ordenacao, asc: asc);

            gvDados.PageIndex = Pagina;
            gvDados.DataSourceID = String.Empty;
            gvDados.DataSource = colecaoDados;
            gvDados.DataBind();
            

            Pesquisa = 1;
            divLista.Style.Add("display", "block");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }
    }

    protected void Carregar()
    {
        try
        {
            //dados.idSegmentoPai = Tela;
            string script = "<script type=\"text/javascript\">$('#divLista').slideUp();</script>";
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);




            DadoVO dado = repoDado.FindBy(Codigo);

            if (dado != null)
            {
                if (liSegPai.Visible)
                {
                    CarregarDropSegmentoPai();
                    if (dado.segPai != null)
                        DropSegmentoPai.SelectedValue = dado.segPai.Id.ToString();
                }

                if (liSegFilho.Visible)
                {
                    CarregarDropSegmentoFilho();
                    if (dado.segFilho != null)
                        DropSegmento.SelectedValue = dado.segFilho.Id.ToString();
                }

                if (liCategoria.Visible)
                {
                    CarregarDropCategoria();
                    if (dado.categoria != null)
                        DropCategoria.SelectedValue = dado.categoria.Id.ToString();
                }

                txtNome.Text = dado.nome;
                txtDescricao.Text = dado.descricao;
                txtResumo.Text = dado.resumo;
                txtKeywords.Text = dado.keywords;
                txtPreco.Text = dado.valor.ToString();
                txtData.Text = dado.data.ToShortDateString();
                txtOrdem.Text = dado.ordem;
                txtMeta.Text = dado.meta;
                txtReferencia.Text = dado.referencia;
                DropDestaque.SelectedValue = dado.destaque;
                DropVisivel.SelectedValue = dado.visivel.ToString().ToLower();


                btnAlterar.Visible = true;
                btnCancelar.Visible = true;


                btnSalvar.Visible = false;


                Tela tela = repoTela.FindBy(CodigoTela);

                if (tela.upload != null)
                {
                    uplLogoProd1.Codigo = Codigo;
                    uplLogoProd1.setConfiguracoes(tela.upload);
                    uplLogoProd1.nomeTela = tela.nome;
                    liUpload.Visible = true;

                }

                divDados.DefaultButton = btnAlterar.ID;
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }
    }

    protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DadoVO dado = repoDado.FindBy(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
            repoDado.Delete(dado);
            MetodosFE.mostraMensagem("Registro de " + gvDados.Columns[1].HeaderText + " \"" + dado.nome + "\" excluído com sucesso", "sucesso");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }

        gvDados.DataBind();
        Pesquisar();
    }
    //GridViewDeleteEventArgs
    protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Codigo = Convert.ToInt32(gvDados.DataKeys[e.NewEditIndex].Value);

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
        //Carregar();

    }

    protected void gvDados_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        if (ordenacao == Ordenacao)
            asc = !asc;
        Ordenacao = ordenacao;
        Pesquisar();
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pagina = 0;
        Pesquisar();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            DadoVO dados = new DadoVO();

            if (liSegPai.Visible)
                if (!String.IsNullOrEmpty(DropSegmentoPai.SelectedValue))
                    dados.segPai = repoSegPai.FilterBy(x => x.Id.ToString() == DropSegmentoPai.SelectedValue).FirstOrDefault();
                else
                    dados.segPai = null;
            if (liSegFilho.Visible)
                if (!String.IsNullOrEmpty(DropSegmento.SelectedValue))
                    dados.segFilho = repoSegFilho.FindBy(Convert.ToInt32(DropSegmento.SelectedValue));
                else
                    dados.segFilho = null;
            if (liCategoria.Visible)
                if (!String.IsNullOrEmpty(DropCategoria.SelectedValue))
                    dados.categoria = repoCategoria.FindBy(Convert.ToInt32(DropCategoria.SelectedValue));
                else
                    dados.categoria = null;

            dados.tela = repoTela.FindBy(CodigoTela);
            dados.nome = txtNome.Text.Trim();
            dados.keywords = txtKeywords.Text;
            dados.descricao = HttpUtility.HtmlDecode(txtDescricao.Text.Trim());
            dados.resumo = HttpUtility.HtmlDecode(txtResumo.Text.Trim());
            dados.referencia = txtReferencia.Text.Trim();
            if (txtPreco.Text.Trim() != "")
            {
                dados.valor = txtPreco.Text.Trim();
            }
            else
            {
                dados.valor = "0";
            }

            dados.meta = txtMeta.Text;
            dados.ordem = txtOrdem.Text;
            dados.destaque = DropDestaque.SelectedValue;
            dados.visivel = Convert.ToBoolean(DropVisivel.SelectedValue);

            dados.chave = dados.nome.ToSeoUrl();

            IList<DadoVO> dadosChave = repoDado.FilterBy(x => x.chave == dados.chave).ToList();

            if (liData.Visible)
            {
                DateTime data;
                if (DateTime.TryParse(txtData.Text, out data))
                    dados.data = data;
                else
                    throw new Exception("Verifique a data e tente novamente.");
            }

            if (dadosChave.Count > 0)
            {
                for (int cont = 0; ; cont++)
                {
                    dados.chave = dados.nome.ToSeoUrl() + cont;
                    dadosChave = repoDado.FilterBy(x => x.chave == dados.chave).ToList();
                    if (dadosChave.Count == 0)
                        break;
                }
            }

            repoDado.Add(dados);
            MetodosFE.mostraMensagem("Cadastro concluído com sucesso.", "sucesso");
            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem( er.Message);
        }
    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            DadoVO dados = repoDado.FindBy(Codigo);

            if (liSegPai.Visible)
                if (!String.IsNullOrEmpty(DropSegmentoPai.SelectedValue))
                    dados.segPai = repoSegPai.FilterBy(x => x.Id.ToString() == DropSegmentoPai.SelectedValue).FirstOrDefault();
                else
                    dados.segPai = null;
            if (liSegFilho.Visible)
                if (!String.IsNullOrEmpty(DropSegmento.SelectedValue))
                    dados.segFilho = repoSegFilho.FindBy(Convert.ToInt32(DropSegmento.SelectedValue));
                else
                    dados.segFilho = null;
            if (liCategoria.Visible)
                if (!String.IsNullOrEmpty(DropCategoria.SelectedValue))
                    dados.categoria = repoCategoria.FindBy(Convert.ToInt32(DropCategoria.SelectedValue));
                else
                    dados.categoria = null;

            if (liData.Visible)
            {
                DateTime data;
                if (DateTime.TryParse(txtData.Text, out data))
                    dados.data = data;
                else
                    throw new Exception("Verifique a data e tente novamente.");
            }
            dados.keywords = txtKeywords.Text;
            dados.nome = txtNome.Text.Trim();
            dados.descricao = HttpUtility.HtmlDecode(txtDescricao.Text.Trim());
            dados.resumo = HttpUtility.HtmlDecode(txtResumo.Text.Trim());
            dados.meta = txtMeta.Text;
            dados.chave = dados.nome.ToSeoUrl();

            IList<DadoVO> dadosChave = repoDado.FilterBy(x => x.chave == dados.chave).ToList();

            if (dadosChave.Count > 0)
            {
                if (dadosChave[0].Id != Codigo)
                    for (int cont = 0; ; cont++)
                    {
                        dados.chave = dados.nome.ToSeoUrl() + cont;
                        dadosChave = repoDado.FilterBy(x => x.chave == dados.chave).ToList();
                        if (dadosChave.Count == 0)
                            break;
                    }
            }

            dados.referencia = txtReferencia.Text.Trim();
            dados.valor = txtPreco.Text.Trim();
            dados.ordem = txtOrdem.Text;
            dados.destaque = DropDestaque.SelectedValue;
            dados.visivel = Convert.ToBoolean(DropVisivel.SelectedValue);
            if (liCategoria.Visible)
            {
                if (!String.IsNullOrEmpty(DropCategoria.SelectedValue))
                    dados.categoria = repoCategoria.FindBy(Convert.ToInt32(DropCategoria.SelectedValue));
                else
                    dados.categoria = null;

            }



            repoDado.Update(dados);

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
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Remove("Codigo");
            string url = Request.Url.AbsolutePath;
            string updatedQueryString = "?" + nameValues.ToString();
            string urlFinal = url + updatedQueryString;
            Response.Redirect(urlFinal);

        }
        catch (Exception er)
        {

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
            return (string)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
    }
    #endregion

    protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Pagina = e.NewPageIndex;
            Pesquisar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void CarregaDados()
    {
        PermissaoVO permissao = null;
        //ObjTela tela = Configuracoes.getTela(DropSegmentoPai.SelectedItem.Text);

        Tela tela = repoTela.FindBy(CodigoTela);



        if (tela.multiplo)//Carrega lista, e haverá botão salvar
        {
            Pesquisar();
            tituloBusca.Visible = true;
            divLista.Visible = true;
            btnSalvar.Visible = true;


            btnAlterar.Visible = false;
            btnCancelar.Visible = false;

            if (liSegPai.Visible)
                CarregarDropSegmentoPai();
            if (liSegFilho.Visible)
                CarregarDropSegmentoFilho();
            if (liCategoria.Visible)
                CarregarDropCategoria();
        }
        else //Não haverá lista, o objeto dados será carregado na tela e só será possível altera-lo
        {
            tituloBusca.Visible = false;
            divLista.Visible = false;

            DadoVO dado = repoDado.FindBy(x => x.tela.Id == CodigoTela);

            //List<DadoVO> colecaoDados = DadosBO.FindAll(idSegmentoPai: SegmentoPaiBO.getCodSegPai(DropSegmentoPai.SelectedItem.Text));

            if (dado != null)
            {
                Codigo = dado.Id;
                Carregar();
            }
            else
            {
                criaDadosUnico();
                dado = repoDado.FindBy(x => x.tela.Id == CodigoTela);
                Codigo = dado.Id;
                Carregar();
            }
        }
    }

    protected void criaDadosUnico()
    {


        try
        {
            DadoVO dados = new DadoVO();

            if (liSegPai.Visible)
                dados.segPai = repoSegPai.FilterBy(x => x.Id.ToString() == DropSegmentoPai.SelectedValue).FirstOrDefault();
            if (liSegFilho.Visible)
                dados.segFilho = String.IsNullOrEmpty(DropSegmento.SelectedValue) ? null : repoSegFilho.FindBy(Convert.ToInt32(DropSegmento.SelectedValue));
            if (liCategoria.Visible)
                dados.categoria = String.IsNullOrEmpty(DropCategoria.SelectedValue) ? null : repoCategoria.FindBy(Convert.ToInt32(DropCategoria.SelectedValue));
            dados.nome = "";
            dados.descricao = "";
            dados.resumo = "";
            dados.referencia = "";
            if (txtPreco.Text.Trim() != "")
            {
                dados.valor = txtPreco.Text.Trim();
            }
            else
            {
                dados.valor = "0";
            }
            dados.tela = repoTela.FindBy(CodigoTela);
            dados.destaque = DropDestaque.SelectedValue;
            dados.visivel = true;

            repoDado.Add(dados);

            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void criaDados()
    {
        CarregarDropSegmentoFilho();

        try
        {
            DadoVO dados = new DadoVO();

            if (liSegPai.Visible)
                dados.segPai = repoSegPai.FilterBy(x => x.Id.ToString() == DropSegmentoPai.SelectedValue).FirstOrDefault();
            if (liSegFilho.Visible)
                dados.segFilho = repoSegFilho.FindBy(Convert.ToInt32(DropSegmento.SelectedValue));
            if (liCategoria.Visible)
                dados.categoria = repoCategoria.FindBy(Convert.ToInt32(DropCategoria.SelectedValue));

            dados.tela = repoTela.FindBy(CodigoTela);
            dados.nome = "";
            dados.descricao = "";
            dados.resumo = "";
            dados.referencia = "";
            if (txtPreco.Text.Trim() != "")
            {
                dados.valor = txtPreco.Text.Trim();
            }
            else
            {
                dados.valor = "0";
            }
            dados.destaque = DropDestaque.SelectedValue;
            dados.visivel = Convert.ToBoolean(DropVisivel.SelectedValue);

            repoDado.Add(dados);

            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void DropSegmentoPai_TextChanged(object sender, EventArgs e)
    {
        if(liSegFilho.Visible)
        CarregarDropSegmentoFilho();
    }

    protected void DropSegmento_TextChanged(object sender, EventArgs e)
    {
        if (liCategoria.Visible)
        CarregarDropCategoria();
    }
}
