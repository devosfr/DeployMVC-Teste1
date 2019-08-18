﻿using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controle_Cadastro_produtos : System.Web.UI.Page
{
    private Repository<SegmentoProduto> RepoSegmento
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<SubSegmentoProduto> RepoSubSegmento
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<CategoriaProduto> RepoCategoria
    {
        get
        {
            return new Repository<CategoriaProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Tamanhos> RepoTamanho
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Produto> RepoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Destaque> RepositorioDestaques
    {
        get
        {
            return new Repository<Destaque>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<ItemCarrinho> RepoCarrinho
    {
        get
        {
            return new Repository<ItemCarrinho>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Marca> RepoMarca
    {
        get
        {
            return new Repository<Marca>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<ItemPedido> RepoItemPedido
    {
        get
        {
            return new Repository<ItemPedido>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<DescontoCupom> RepoDescontoCupom
    {
        get
        {
            return new Repository<DescontoCupom>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Album> RepoAlbuns
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Tela> RepoTela
    {
        get
        {
            return new Repository<Tela>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Preco> RepoPreco
    {
        get
        {
            return new Repository<Preco>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Peso> RepoPeso
    {
        get
        {
            return new Repository<Peso>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<InformacaoProduto> RepositorioInformacao
    {
        get
        {
            return new Repository<InformacaoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Estoque> RepositorioEstoque
    {
        get
        {
            return new Repository<Estoque>(NHibernateHelper.CurrentSession);
        }
    }

    public static List<string> ids = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        MaintainScrollPositionOnPostBack = true;
        lblProdutos.Text = "Cadastro de Produtos";

        if (!Page.IsPostBack)
        {
            //txtDescricaoInformacao.Toolbar = txtDescricao.ToolbarBasic;
            CarregarDestaques();
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                carregaTamanhos();
            }
            else
            {
                try
                {
                    Pesquisar("");
                    CarregarArvoreSegmentos();

                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                    btnCancelar.Visible = false;
                }
                catch (Exception er)
                {
                    MetodosFE.mostraMensagem(er.Message);
                }
            }
        }
        else
        {
        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    //protected void CarregarDdlSegmento()
    //{
    //    try
    //    {
    //        //Prenche o drop pai.
    //        var colecao = RepoSegmento.All().OrderBy(x => x.Nome);
    //        ddlSegmento.DataSource = colecao;
    //        ddlSegmento.DataTextField = "Nome";
    //        ddlSegmento.DataValueField = "Id";
    //        ddlSegmento.DataBind();
    //    }
    //    catch (Exception er)
    //    {
    //        MetodosFE.mostraMensagem(er.Message);
    //    }
    //}

    //protected void CarregarDdlSubSegmento()
    //{
    //    try
    //    {
    //        IQueryable<SubSegmentoProduto> colecaosegmentos = null;
    //        //Prenche o drop pai.
    //        if (ddlSegmento.Items.Count > 0)
    //            colecaosegmentos = RepoSubSegmento.FilterBy(x => x.Segmento.Id == Convert.ToInt32(ddlSegmento.SelectedValue));

    //        ddlSubSegmento.DataSource = colecaosegmentos;
    //        ddlSubSegmento.DataTextField = "Nome";
    //        ddlSubSegmento.DataValueField = "Id";
    //        ddlSubSegmento.DataBind();
    //    }
    //    catch (Exception er)
    //    {
    //        MetodosFE.mostraMensagem(er.Message);
    //    }
    //}

    //protected void CarregarDdlCategoria()
    //{
    //    try
    //    {
    //        //Prenche o drop.
    //        IQueryable<CategoriaProduto> colecaosegmentos = null;
    //        if (ddlSubSegmento.Items.Count > 0)
    //            colecaosegmentos = RepoCategoria.FilterBy(x => x.SubSegmento.Id == Convert.ToInt32(ddlSubSegmento.SelectedValue));

    //        ddlCategoria.DataSourceID = String.Empty;
    //        ddlCategoria.DataSource = colecaosegmentos;
    //        ddlCategoria.DataTextField = "Nome";
    //        ddlCategoria.DataValueField = "Id";
    //        ddlCategoria.DataBind();
    //    }
    //    catch (Exception er)
    //    {
    //        MetodosFE.mostraMensagem(er.Message);
    //    }
    //}

    protected void CarregarArvoreSegmentos()
    {
        try
        {
            tvSegmentos.Nodes.Clear();
            Produto produto = RepoProduto.FindBy(Codigo);
            if (produto == null)
                produto = new Produto();

            IList<SegmentoProduto> segmentosProduto = produto.Segmentos;

            IList<SubSegmentoProduto> subSegmentosProduto = produto.SubSegmentos;

            IList<CategoriaProduto> categoriasProduto = produto.Categorias;

            IList<SegmentoProduto> segmentos = RepoSegmento.All().ToList();

            foreach (var segmento in segmentos)
            {
                TreeNode nodo = new TreeNode();
                nodo.Text = segmento.Nome;
                nodo.Value = segmento.Id.ToString();
                nodo.ToolTip = "Segmento";
                nodo.ImageUrl = "";
                nodo.ImageToolTip = "Imagem do Nodo";

                if (segmentosProduto.Any(x => x.Id == segmento.Id))
                    nodo.Checked = true;

                List<SubSegmentoProduto> subSegmentos = segmento.SubSegmentos.ToList();

                foreach (var subSegmento in subSegmentos)
                {
                    TreeNode nodoSubSegmento = new TreeNode();
                    nodoSubSegmento.Text = subSegmento.Nome;
                    nodoSubSegmento.Value = subSegmento.Id.ToString();
                    nodoSubSegmento.ToolTip = "SubSegmento";
                    nodoSubSegmento.ImageUrl = "";


                    if (subSegmentosProduto.Any(x => x.Id == subSegmento.Id))
                    {
                        nodoSubSegmento.Checked = true;
                    }

                    List<CategoriaProduto> categorias = subSegmento.Categorias.ToList();
                    foreach (var categoria in categorias)
                    {
                        TreeNode nodoCategoria = new TreeNode();
                        nodoCategoria.Text = categoria.Nome;
                        nodoCategoria.Value = categoria.Id.ToString();
                        nodoCategoria.ToolTip = "Categoria";

                        nodoCategoria.ImageUrl = "";

                        if (categoriasProduto.Any(x => x.Id == categoria.Id))
                        {
                            nodoCategoria.Checked = true;
                        }
                        nodoSubSegmento.ChildNodes.Add(nodoCategoria);
                    }

                    nodoSubSegmento.CollapseAll();

                    nodo.ChildNodes.Add(nodoSubSegmento);

                }
                nodo.CollapseAll();
                tvSegmentos.Nodes.Add(nodo);

                // }
                //tvPermissoes.Nodes.Add(nodoPai);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void CarregarDestaques()
    {
        //IList<Destaque> destaques = RepositorioDestaques.All().OrderBy(x => x.Nome).ToList();

        //cblDestaques.DataSource = destaques;
        //cblDestaques.DataTextField = "Nome";
        //cblDestaques.DataValueField = "Id";
        //cblDestaques.DataBind();
    }

    public void AdicionaSegmentos(Produto produto)
    {
        try
        {
            produto.Segmentos.Clear();
            produto.SubSegmentos.Clear();
            produto.Categorias.Clear();

            IList<SubSegmentoProduto> subSegmentos = RepoSubSegmento.All().ToList();
            IList<CategoriaProduto> categorias = RepoCategoria.All().ToList();
            IList<SegmentoProduto> segmentos = RepoSegmento.All().ToList();

            foreach (TreeNode nodo in tvSegmentos.CheckedNodes)
            {
                switch (nodo.ToolTip)
                {
                    case "Segmento":


                        int idSegmento = Convert.ToInt32(nodo.Value);

                        var segmento = segmentos.FirstOrDefault(x => x.Id == idSegmento);

                        produto.Segmentos.Add(segmento);



                        break;
                    case "SubSegmento":


                        int idSubSegmento = Convert.ToInt32(nodo.Value);

                        var subSegmento = subSegmentos.FirstOrDefault(x => x.Id == idSubSegmento);

                        produto.SubSegmentos.Add(subSegmento);

                        break;
                    case "Categoria":

                        int idCategoria = Convert.ToInt32(nodo.Value);

                        var categoria = categorias.FirstOrDefault(x => x.Id == idCategoria);

                        produto.Categorias.Add(categoria);

                        break;
                }
            }

            RepoProduto.Update(produto);


        }
        catch (Exception ex)
        {

        }


    }

    //protected void CarregarDdlTamanho()
    //{
    //    try
    //    {
    //        //Prenche o drop.
    //        IQueryable<Tamanhos> colecaosegmentos = null;

    //        colecaosegmentos = RepoTamanho.All();

    //        ddlTamanho.DataSourceID = String.Empty;
    //        ddlTamanho.DataSource = colecaosegmentos;
    //        ddlTamanho.DataTextField = "Nome";
    //        ddlTamanho.DataValueField = "Id";
    //        ddlTamanho.DataBind();
    //    }
    //    catch (Exception er)
    //    {
    //        MetodosFE.mostraMensagem(er.Message);
    //    }
    //}

    protected void Pesquisar(string order)
    {
        try
        {
            string busca = null;

            int id = 0;





            var pesquisa = RepoProduto.All();

            if (!String.IsNullOrEmpty(txtBuscaID.Text))
            {
                id = Convert.ToInt32(txtBuscaID.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }

            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                busca = txtBuscaNome.Text;
                pesquisa = pesquisa.Where(x => x.Nome.ToLower().Contains(busca.ToLower()));
            }

            var retornoPesquisa = pesquisa.ToList();

            if (!string.IsNullOrEmpty(order))
            {
                if (order.Equals("Nome"))
                {
                    retornoPesquisa = retornoPesquisa.OrderBy(x => x.Nome.Trim()).ToList();
                }
                if (order.Equals("Id"))
                {
                    retornoPesquisa = retornoPesquisa.OrderBy(x => x.Id).ToList();
                }
            }

            gvDados.PageIndex = Pagina;
            gvDados.DataSourceID = String.Empty;
            gvDados.DataSource = retornoPesquisa;
            gvDados.DataBind();
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
            CarregarArvoreSegmentos();

            Produto produto = RepoProduto.FindBy(Codigo);

            if (produto != null)
            {
                List<Marca> marcas = new List<Marca>();

                marcas = RepoMarca.FilterBy(x => x.Visivel).ToList();

                ddlMarca.DataSource = marcas.OrderBy(x => x.Nome);
                ddlMarca.DataValueField = "id";
                ddlMarca.DataTextField = "nome";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Selecione", "0"));

                liGridInformacoesDado.Visible = true;

                IList<InformacaoProduto> informacoes = RepositorioInformacao.FilterBy(x => x.Produto.Id == Codigo).ToList();

                //gvInformacoes.DataSource = informacoes;
                //gvInformacoes.DataBind();

                txtIdDados.Text = produto.Id.ToString();
                //if (produto.Segmento != null)
                //    ddlSegmento.SelectedValue = produto.Segmento.Id.ToString();
                //CarregarDdlSubSegmento();
                //if (produto.SubSegmento != null)
                //    ddlSubSegmento.SelectedValue = produto.SubSegmento.Id.ToString();
                //CarregarDdlCategoria();
                //if (produto.Categoria != null)
                //    ddlCategoria.SelectedValue = produto.Categoria.Id.ToString(

                txtNome.Text = produto.Nome;
                txtDescricao.Text = produto.Descricao;
                //txtResumo.Text = produto.Resumo;
                txtreferencia.Text = produto.Referencia;
                chkVisivel.Checked = produto.Visivel;
                chkIndisponivel.Checked = produto.Indisponivel;
                //chkDestaque.Checked = produto.Destaque;

                if(produto.Marca != null && ddlMarca.Items.FindByValue(produto.Marca.Id.ToString()) != null)
                {
                    ddlMarca.Items.FindByValue(produto.Marca.Id.ToString()).Selected = true;
                }

                txtPreco.Text = produto.Preco.Valor.ToString();
                txtPrecoDe.Text = produto.Preco.ValorSemPromocao.ToString();
                //txtPrecoAvista.Text = produto.Preco.ValorAvista.ToString();
                //txtDeconto.Text = produto.Preco.Desconto.ToString();
                //txtTextoPreco.Text = produto.Preco.Texto;
                //chkPromoHome.Checked = produto.PromocaoHome;

                if (produto.Peso != null)
                {
                    txtPeso.Text = produto.Peso.Valor.ToString("f3");
                    //if (produto.Peso.Tamanho != null)
                    //    ddlTamanho.SelectedValue = produto.Peso.Tamanho.Id.ToString();
                }

                txtAltura.Text = produto.Peso.Altura.ToString();
                txtLargura.Text = produto.Peso.Largura.ToString();
                txtProfundidade.Text = produto.Peso.Profundidade.ToString();

                //foreach (ListItem item in cblDestaques.Items)
                //{
                //    if (produto.Destaques.Any(x => x.Id.ToString().Equals(item.Value)))
                //        item.Selected = true;
                //}

                btnSalvar.Visible = false;
                btnPesquisar.Visible = false;
                btnAlterar.Visible = true;

                if (Codigo != 0)
                {
                    uplLogoProd1.Visible = true;
                    uplLogoProd1.Codigo = Codigo;
                    uplLogoProd1.reset();

                    uplLogoProd1.setConfiguracoes(RepoTela.FindBy(x => x.nome.Equals("Produtos")).upload);
                    uplLogoProd1.Carregar();

                    //try
                    //{
                    //    ControleCores.Codigo = Codigo;
                    //    ControleCores.Carregar();
                    //}
                    //catch (Exception ex)
                    //{

                    //    throw new Exception("Problema com cores. " + ex.Message + ex.InnerException);
                    //}
                }

                //ControleCores.Codigo = Codigo;
                //ControleCores.Carregar();
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (MessageBox.Show("Realmente deseja excluir este produto, junto com suas imagens e demais informações relacionadas?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //{
        try
        {
            //ProdutosxDetalhesBO.DeleteByProduto(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
            //FotosProdutosBO.DeleteByProduto(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
            //CookiesBO.DeleteByProduto(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
            Produto produto = RepoProduto.FindBy(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));

            var itens = RepoCarrinho.FilterBy(x => x.Produto.Id == produto.Id).ToList();
            RepoCarrinho.Delete(itens);

            var itensPedido = RepoItemPedido.FilterBy(x => x.Produto.Id == produto.Id).ToList();
            if (itensPedido.Count > 0)
                throw new Exception("Este produto já esta dentro de itens de pedidos, não podendo ser excluído.");

            var descontos = RepoDescontoCupom.FilterBy(x => x.Produto.Id == produto.Id).ToList();
            if (descontos.Count > 0)
                throw new Exception("Este produto já esta dentro de cupons de desconto, não podendo ser excluído.");


            IList<Album> albuns = RepoAlbuns.FilterBy(x => x.Produto.Id == produto.Id).ToList();

            foreach (var album in albuns)
                album.ExcluirImagens();

            // produto.Albuns.Clear();

            RepoAlbuns.Delete(albuns);

            produto.Segmentos.Clear();
            produto.SubSegmentos.Clear();
            produto.Categorias.Clear();
            //Preco preco = produto.Preco;
            //produto.Preco = null;
            //RepoPreco.Delete(preco);

            //Peso peso = produto.Peso;
            //produto.Peso = null;
            //RepoPeso.Delete(peso);

            produto.Informacoes.Clear();

            RepoProduto.Update(produto);

            RepoProduto.Delete(produto);
            MetodosFE.mostraMensagem("Produto \"" + produto.Nome + "\" excluído com sucesso.", "sucesso");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }

        //}

        gvDados.DataBind();
        Pesquisar("");
    }

    protected void gvDados_Sorting(object sender, GridViewSortEventArgs e)
    {
         var order = e.SortExpression.ToString();
        Pesquisar(order);
    }

    protected void btnPesquisar_Clique(object sender, EventArgs e)
    {
        Pagina = 0;
        Pesquisar("");
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                throw new Exception("É preciso definir o nome do produto.");
            }
            Produto produto = new Produto();

            //if (!String.IsNullOrEmpty(ddlSegmento.SelectedValue))
            //    produto.Segmento = RepoSegmento.FindBy(Convert.ToInt32(ddlSegmento.SelectedValue));
            //if (!String.IsNullOrEmpty(ddlSubSegmento.SelectedValue))
            //    produto.SubSegmento = RepoSubSegmento.FindBy(Convert.ToInt32(ddlSubSegmento.SelectedValue));
            //if (!String.IsNullOrEmpty(ddlCategoria.SelectedValue))
            //    produto.Categoria = RepoCategoria.FindBy(Convert.ToInt32(ddlCategoria.SelectedValue));

            produto.Nome = txtNome.Text.Trim();
            Preco preco = new Preco();
            Peso peso = new Peso();
            RepoPeso.Add(peso);
            RepoPreco.Add(preco);

            produto.Peso = peso;
            produto.Preco = preco;

            RepoProduto.Add(produto);
            AdicionaSegmentos(produto);

            Response.Redirect("~/controle/cadastro/Produto/Produtos.aspx?Codigo=" + produto.Id + "", false);
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
            //lblmensagem.Text = ;
        }
    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            //ControleCores.Salvar();
            Produto produto = RepoProduto.FindBy(Codigo);

            //if (!String.IsNullOrEmpty(ddlSegmento.SelectedValue))
            //    produto.Segmento = RepoSegmento.FindBy(Convert.ToInt32(ddlSegmento.SelectedValue));
            //else
            //    produto.Segmento = null;
            //if (!String.IsNullOrEmpty(ddlSubSegmento.SelectedValue))
            //    produto.SubSegmento = RepoSubSegmento.FindBy(Convert.ToInt32(ddlSubSegmento.SelectedValue));
            //else
            //    produto.SubSegmento = null;
            //if (!String.IsNullOrEmpty(ddlCategoria.SelectedValue))
            //    produto.Categoria = RepoCategoria.FindBy(Convert.ToInt32(ddlSubSegmento.SelectedValue));
            //else
            //    produto.Categoria = null;

            produto.Nome = txtNome.Text.Trim();
            produto.Descricao = HttpUtility.HtmlDecode(txtDescricao.Text.Trim());
            //produto.Resumo = HttpUtility.HtmlDecode(txtResumo.Text.Trim());
            produto.Referencia = txtreferencia.Text.Trim();
            produto.Visivel = chkVisivel.Checked;
            produto.Indisponivel = chkIndisponivel.Checked;
            //produto.Destaque = chkDestaque.Checked;

            Marca marca = new Marca();

            if(ddlMarca.SelectedIndex > 0)
            {
                int idMarca = Convert.ToInt32(ddlMarca.SelectedValue);

                marca = RepoMarca.FindBy(idMarca);

                if(marca != null)
                {
                    produto.Marca = marca;
                }
            }
            else
            {
                produto.Marca = null;
            }

            //produto.PromocaoHome = chkPromoHome.Checked;

            bool tentativa = false;
            if (String.IsNullOrEmpty(txtPreco.Text))
                produto.Preco = null;
            else
            {
                decimal resultado = 0;
                tentativa = Decimal.TryParse(txtPreco.Text, out resultado);
                if (!tentativa)
                    throw new Exception("É preciso definir um preço para o produto.");

                produto.Preco.Valor = resultado;
            }

            tentativa = false;
            //if (String.IsNullOrEmpty(txtPrecoAvista.Text))
            //    produto.Preco = null;
            //else
            //{
            //    decimal resultado = 0;
            //    tentativa = Decimal.TryParse(txtPrecoAvista.Text, out resultado);
            //    if (!tentativa)
            //        throw new Exception("É preciso definir um preço a vista para o produto.");

            //    produto.Preco.ValorAvista = resultado;
            //}

            //produto.Preco.Texto = txtTextoPreco.Text;

            if (!String.IsNullOrEmpty(txtAltura.Text))
                produto.Peso.Altura = Convert.ToDecimal(txtAltura.Text);
            else
                produto.Peso.Altura = 0;

            if (!String.IsNullOrEmpty(txtProfundidade.Text))
                produto.Peso.Profundidade = Convert.ToDecimal(txtProfundidade.Text);
            else
                produto.Peso.Profundidade = 0;

            if (!String.IsNullOrEmpty(txtLargura.Text))
                produto.Peso.Largura = Convert.ToDecimal(txtLargura.Text);
            else
                produto.Peso.Largura = 0;

            if (!String.IsNullOrEmpty(txtPrecoDe.Text))
                produto.Preco.ValorSemPromocao = Convert.ToDecimal(txtPrecoDe.Text);
            else
                produto.Preco.ValorSemPromocao = 0;

            //if (!String.IsNullOrEmpty(txtDeconto.Text))
            //    produto.Preco.Desconto = Convert.ToDecimal(txtDeconto.Text);
            //else
            //    produto.Preco.Desconto = 0;

            float peso = 0;
            tentativa = float.TryParse(txtPeso.Text.Replace(".", ","), out peso);
            if (!tentativa)
                throw new Exception("É preciso definir um peso para o produto.");

            produto.Peso.Valor = peso;

            //produto.Peso.Tamanho = RepoTamanho.FindBy(Convert.ToInt32(ddlTamanho.SelectedValue));

            produto.Chave = (produto.Id + " " + produto.Nome).ToSeoUrl();

            produto.Destaques.Clear();
            //foreach (ListItem item in cblDestaques.Items)
            //{
            //    if (item.Selected)
            //    {
            //        int idDestaque = Convert.ToInt32(item.Value);
            //        var destaque = RepositorioDestaques.FindBy(idDestaque);
            //        if (destaque != null)
            //            produto.Destaques.Add(destaque);
            //    }
            //}


            RepoProduto.Update(produto);
            AdicionaSegmentos(produto);

            MetodosFE.mostraMensagem("Produto alterado com sucesso.", "sucesso");

            Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Limpar();
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

    private bool Asc
    {
        get
        {
            if (ViewState["asc"] == null) ViewState["asc"] = true;
            return (bool)ViewState["asc"];
        }
        set { ViewState["asc"] = value; }
    }

    private int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }

    private int CodigoInformacao
    {
        get
        {
            if (ViewState["CodigoInformacao"] == null) ViewState["CodigoInformacao"] = 0;
            return (Int32)ViewState["CodigoInformacao"];
        }
        set { ViewState["CodigoInformacao"] = value; }
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

    #endregion Guardamos o Código no ViewState

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
            Pesquisar("");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    //protected void gvInformacoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        InformacaoProduto informacao = RepositorioInformacao.FindBy(Convert.ToInt32(gvInformacoes.DataKeys[e.RowIndex].Value));

    //        RepositorioInformacao.Delete(informacao);

    //        MetodosFE.mostraMensagem("Informação \"" + informacao.Nome + "\" excluída com sucesso.", "sucesso");

    //        IList<InformacaoProduto> informacoes = RepositorioInformacao.FilterBy(x => x.Produto.Id == Codigo).ToList();

    //        gvInformacoes.DataSource = informacoes;
    //        gvInformacoes.DataBind();
    //    }
    //    catch (Exception er)
    //    {
    //        MetodosFE.mostraMensagem(er.Message);
    //    }

    //    gvDados.DataBind();
    //    Pesquisar("");
    //}

    protected void btnAdicionarInformacao_Click(object sender, EventArgs e)
    {
        try
        {
            InformacaoProduto informacao = new InformacaoProduto();
            if (CodigoInformacao != 0)
            {
                informacao = RepositorioInformacao.FindBy(CodigoInformacao);

                //informacao.Nome = txtNomeInformacao.Text;
                //informacao.Texto = txtDescricaoInformacao.Text;
                //informacao.Visivel = Convert.ToBoolean(ddlColunaVisivel.SelectedValue);
                RepositorioInformacao.Update(informacao);
            }
            else
            {
                //informacao.Nome = txtNomeInformacao.Text;
                //informacao.Texto = txtDescricaoInformacao.Text;
                //informacao.Visivel = Convert.ToBoolean(ddlColunaVisivel.SelectedValue);
                Produto produto = RepoProduto.FindBy(Codigo);

                informacao.Produto = produto;
                RepositorioInformacao.Add(informacao);
            }


            IList<InformacaoProduto> informacoes = RepositorioInformacao.FilterBy(x => x.Produto.Id == Codigo).ToList();

            //gvInformacoes.DataSource = informacoes;
            //gvInformacoes.DataBind();

            //txtNomeInformacao.Text = "";
            //txtDescricaoInformacao.Text = "";

            CodigoInformacao = 0;
            //btnAdicionarInformacao.Text = "Adicionar";
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }

    protected void btnEditarInformacao_Click(object sender, EventArgs e)
    {
        try
        {
            int idInformacao = Convert.ToInt32(((Button)sender).CommandArgument);
            InformacaoProduto informacao = RepositorioInformacao.FindBy(idInformacao);

            //txtNomeInformacao.Text = informacao.Nome;
            //txtDescricaoInformacao.Text = informacao.Texto;
            //informacao.Visivel = Convert.ToBoolean(ddlColunaVisivel.SelectedValue);
            CodigoInformacao = informacao.Id;
            //btnAdicionarInformacao.Text = "Editar";
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }

    protected void btnCancelarInformacao_Click(object sender, EventArgs e)
    {
        try
        {
            //txtNomeInformacao.Text = "";
            //txtDescricaoInformacao.Text = "";

            CodigoInformacao = 0;
            //btnAdicionarInformacao.Text = "Adicionar";

        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }
    protected void gvTamanho_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected string getCores(Produto produto)
    {
        string retorno = "";

        IList<Album> albuns = RepoAlbuns.FilterBy(x => x.Produto.Id == produto.Id).ToList();
        foreach(Album alb in albuns)
        {
            if(alb != null && alb.Cor != null)
            {
                if (String.IsNullOrEmpty(retorno))
                    retorno = alb.Cor.Nome;
                else
                    retorno += ", " + alb.Cor.Nome;
            }
        }

        return retorno;
    }

    #region Tamanhos

    protected string getTamanhos(Produto produto)
    {
        string retorno = "";

        if (!string.IsNullOrEmpty(produto.Tamanhos) && produto.Tamanhos.Contains(','))
        {
            ids = produto.Tamanhos.Split(',').ToList();
        }
        else
        {
            ids = new List<string>();
            if (!string.IsNullOrEmpty(produto.Tamanhos))
                ids.Add(produto.Tamanhos);
        }

        foreach(string id in ids)
        {
            int i = Convert.ToInt32(id);

            Tamanhos tmp = RepoTamanho.FindBy(i);
            if (tmp != null)
            {
                if (String.IsNullOrEmpty(retorno))
                    retorno = tmp.Nome;
                else
                    retorno += ", " + tmp.Nome;
            }
        }

        return retorno;
    }

    protected void carregaTamanhos()
    {

        List<Tamanhos> tam = RepoTamanho.All().ToList();
        if (tam != null && tam.Count > 0)
        {
            repTamahos.DataSource = tam;
            repTamahos.DataBind();
        }

        Produto produto = RepoProduto.FindBy(Codigo);

        if (!string.IsNullOrEmpty(produto.Tamanhos) && produto.Tamanhos.Contains(','))
        {
            ids = produto.Tamanhos.Split(',').ToList();
        }
        else
        {
            ids = new List<string>();
            if(!string.IsNullOrEmpty(produto.Tamanhos))
                ids.Add(produto.Tamanhos);
        }

        if (produto != null)
        {
            if (!String.IsNullOrEmpty(produto.Tamanhos))
            {
                foreach (RepeaterItem item in repTamahos.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkTamanho");
                    HiddenField hdn = (HiddenField)item.FindControl("hdnTam");

                    if (chk != null && hdn != null)
                    {
                        if (ids.Any(x => x.Equals(hdn.Value)))
                        {
                            chk.Checked = true;
                        }
                    }
                }

            }
        }
    }

    protected void chjTamanho_CheckedChanged(object sender, EventArgs e)
    {
        Produto produto = RepoProduto.FindBy(Codigo);

        CheckBox tb = sender as CheckBox;

        foreach (RepeaterItem item in repTamahos.Items)
        {
            CheckBox chk = (CheckBox)item.FindControl("chkTamanho");
            HiddenField hdn = (HiddenField)item.FindControl("hdnTam");

            if (chk.Text.Equals(tb.Text))
            {
                if (chk.Checked)
                {
                    ids.Add(hdn.Value);
                }
                else
                {
                    ids.Remove(hdn.Value);
                }
            }

            //if (chk != null && hdn != null)
            //{
            //    if (chk.Checked)
            //    {
            //        if (ids.Count > 0 && !ids.Any(x => !String.IsNullOrEmpty(x) && x != null && x.Equals(hdn.Value)))
            //        {
            //            ids.Add(hdn.Value);
            //        }
            //    }
            //    else
            //    {
            //        if (ids.Any(x => x.Equals(hdn.Value)))
            //        {
            //            ids.Remove(hdn.Value);
            //        }
            //    }
            //}
        }

        string tams = "";

        foreach (string id in ids)
        {
            if (String.IsNullOrEmpty(tams))
                tams = id;
            else
                tams += "," + id;
        }

        if (!String.IsNullOrEmpty(tams))
            produto.Tamanhos = tams;
        else
            produto.Tamanhos = null;

        RepoProduto.Update(produto);

        carregaTamanhos();
    }

    #endregion
}