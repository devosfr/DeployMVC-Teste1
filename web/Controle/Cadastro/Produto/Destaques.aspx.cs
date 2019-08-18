using System;
using System.Linq;
using NHibernate.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }

    public Repository<Destaque> RepositorioDestaque
    {
        get
        {
            return new Repository<Destaque>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<SegmentoProduto> RepositorioSegmento
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<SubSegmentoProduto> RepositorioSubSegmento
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<CategoriaProduto> RepositorioCategoria
    {
        get
        {
            return new Repository<CategoriaProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        nome = "Destaque";
        nome2 = "Destaque";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            uplDestaque.Visible = false;
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
            var pesquisa = RepositorioDestaque.All();
            string nome = "";
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x => x.Nome != null && x.Nome.ToLower().Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }


            IList<Destaque> destaques = pesquisa.ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = destaques;
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

    protected void CarregarArvoreSegmentos()
    {
        try
        {
            tvProdutos.Nodes.Clear();



            IList<SegmentoProduto> segmentosProdutosMarcados = RepositorioProduto.FilterBy(x => x.Destaques.Any(y => y.Id == Codigo)).SelectMany(x => x.Segmentos).Distinct().ToList();

            IList<SubSegmentoProduto> subSegmentosProdutosMarcados = RepositorioProduto.FilterBy(x => x.Destaques.Any(y => y.Id == Codigo)).SelectMany(x => x.SubSegmentos).Distinct().ToList();

            IList<CategoriaProduto> categoriasProdutosCategorias = RepositorioProduto.FilterBy(x => x.Destaques.Any(y => y.Id == Codigo)).SelectMany(x => x.Categorias).Distinct().ToList();

            IList<Produto> produtosMarcados = RepositorioProduto.FilterBy(x => x.Destaques.Any(y => y.Id == Codigo)).ToList();

            IList<SegmentoProduto> segmentos = RepositorioSegmento.All().OrderBy(x => x.Nome).ToList();

            foreach (var segmento in segmentos)
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = segmento.Nome;
                nodo.Value = segmento.Id.ToString();
                nodo.ToolTip = "Segmento";
                nodo.ImageUrl = "";
                nodo.ImageToolTip = "Imagem do Nodo";

                if (segmentosProdutosMarcados.Any(x => x.Id == segmento.Id))
                    nodo.Checked = true;

                List<SubSegmentoProduto> subSegmentos = segmento.SubSegmentos.ToList();

                foreach (var subSegmento in subSegmentos)
                {
                    TreeNode nodoSubSegmento = new TreeNode();
                    nodoSubSegmento.Text = subSegmento.Nome;
                    nodoSubSegmento.Value = subSegmento.Id.ToString();
                    nodoSubSegmento.ToolTip = "SubSegmento";
                    nodoSubSegmento.ImageUrl = "";


                    if (subSegmentosProdutosMarcados.Any(x => x.Id == subSegmento.Id))
                        nodoSubSegmento.Checked = true;


                    List<CategoriaProduto> categorias = subSegmento.Categorias.ToList();
                    foreach (var categoria in categorias)
                    {
                        TreeNode nodoCategoria = new TreeNode();
                        nodoCategoria.Text = categoria.Nome;
                        nodoCategoria.Value = categoria.Id.ToString();
                        nodoCategoria.ToolTip = "Categoria";

                        nodoCategoria.ImageUrl = "";

                        if (categoriasProdutosCategorias.Any(x => x.Id == categoria.Id))
                            nodoCategoria.Checked = true;

                        List<Produto> produtos = RepositorioProduto.FilterBy(x => x.Categorias.Any(y => y.Id == categoria.Id)).ToList();
                        foreach (var produto in produtos)
                        {
                            TreeNode nodoProduto = new TreeNode();
                            nodoProduto.Text = produto.Nome;
                            nodoProduto.Value = produto.Id.ToString();
                            nodoProduto.ToolTip = "Produto";

                            nodoProduto.ImageUrl = "";

                            if (produtosMarcados.Any(x => x.Id == produto.Id))
                                nodoProduto.Checked = true;

                            nodoCategoria.ChildNodes.Add(nodoProduto);
                        }
                        if (nodoCategoria.ChildNodes.Count > 0)
                        nodoSubSegmento.ChildNodes.Add(nodoCategoria);
                    }

                    nodoSubSegmento.CollapseAll();

                    if(nodoSubSegmento.ChildNodes.Count > 0)
                    nodo.ChildNodes.Add(nodoSubSegmento);

                }
                nodo.CollapseAll();
                if (nodo.ChildNodes.Count > 0)
                tvProdutos.Nodes.Add(nodo);

             
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Carregar()
    {
        try
        {

            Destaque destaque = RepositorioDestaque.FindBy(Codigo);

            if (destaque != null)
            {
                txtID.Text = destaque.Id.ToString();
                txtNome.Text = destaque.Nome;
                cbVisivel.Checked = destaque.Visivel;
                txtPrioridade.Text = destaque.Prioridade.ToString();

                uplDestaque.Codigo = Codigo;

                UploadTela configuracoes = new UploadTela();

                configuracoes.Configuracao = 2;
                configuracoes.QtdeFotos = 1;
                configuracoes.TamFotoGrW = 80;
                configuracoes.TamFotoGrH = 50;
                configuracoes.TamFotoPqW = 80;
                configuracoes.TamFotoPqH = 50;
                configuracoes.Qualidade = 80;
                configuracoes.Configuracao = 2;

                uplDestaque.setConfiguracoes(configuracoes);
                uplDestaque.Visible = true;

                CarregarArvoreSegmentos();

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
            Destaque destaque = new Destaque();
            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            destaque.Nome = txtNome.Text.Trim();

            destaque.Visivel = cbVisivel.Checked;
            if (String.IsNullOrEmpty(txtPrioridade.Text))
                destaque.Prioridade = 0;
            else
                destaque.Prioridade = Convert.ToInt32(txtPrioridade.Text);
            RepositorioDestaque.Add(destaque);

            MetodosFE.mostraMensagem(" " + nome2 + " " + destaque.Nome + " cadastrado com sucesso.", "sucesso");
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
            Destaque destaque = RepositorioDestaque.FindBy(Codigo);

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            destaque.Nome = txtNome.Text.Trim();
            destaque.Visivel = cbVisivel.Checked;
            if (String.IsNullOrEmpty(txtPrioridade.Text))
                destaque.Prioridade = 0;
            else
                destaque.Prioridade = Convert.ToInt32(txtPrioridade.Text);
            RepositorioDestaque.Update(destaque);
            AdicionaSegmentos();
            MetodosFE.mostraMensagem(" " + nome2 + " " + destaque.Nome + " cadastrado com sucesso.", "sucesso");
            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    public void AdicionaSegmentos()
    {
        try
        {

            IList<int> ids = new List<int>();

            foreach (TreeNode nodo in tvProdutos.CheckedNodes)
            {
                if (nodo.ToolTip.Equals("Produto"))
                {
                    int idProduto = Convert.ToInt32(nodo.Value);
                    ids.Add(idProduto);
                }


            }

            ids = ids.Distinct().ToList();

            IList<Produto> produtos = RepositorioProduto.FilterBy(x => ids.Contains(x.Id) && !x.Destaques.Any(y => y.Id == Codigo)).FetchMany(x => x.Destaques).ToList();

            Destaque destaque = RepositorioDestaque.FindBy(Codigo);

            foreach (var produto in produtos)
            {
                produto.Destaques.Add(destaque);
            }

            RepositorioProduto.Update(produtos);

            produtos = RepositorioProduto.FilterBy(x => !ids.Contains(x.Id) && x.Destaques.Any(y => y.Id == Codigo)).FetchMany(x => x.Destaques).ToList();

            foreach (var produto in produtos)
            {
                produto.Destaques.Remove(destaque);
            }
            RepositorioProduto.Update(produtos);
        }
        catch (Exception ex)
        {
            throw ex;
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
            Destaque destaque = RepositorioDestaque.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));


            IList<Produto> produtos = RepositorioProduto.FilterBy(x => x.Destaques.Any(y => y.Id == destaque.Id)).FetchMany(x => x.Destaques).ToList();

            foreach (var produto in produtos)
            {
                produto.Destaques.Remove(destaque);
                RepositorioProduto.Update(produto);
            }

            destaque.ExcluirArquivos();

            RepositorioDestaque.Delete(destaque);



            MetodosFE.mostraMensagem(nome2 + " " + destaque.Nome + " excluído com sucesso.", "sucesso");
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
