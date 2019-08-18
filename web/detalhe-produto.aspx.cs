using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Linq;
using System.Linq;

public partial class _Default : System.Web.UI.Page
{

    protected Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Estoque> RepositorioEstoque
    {
        get
        {
            return new Repository<Estoque>(NHibernateHelper.CurrentSession);
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

    private Repository<Carrinho> repoCarrinho
    {
        get
        {
            return new Repository<Carrinho>(NHibernateHelper.CurrentSession);
        }
    }

    IList<Carrinho> listaIdsProdutos = new List<Carrinho>();

    protected Repository<Cor> RepositorioCor
    {
        get
        {
            return new Repository<Cor>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Tamanhos> RepositorioTamanhos
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Album> RepositorioAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Marca> RepositorioMarca
    {
        get
        {
            return new Repository<Marca>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<ImagemProduto> RepositorioImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<SegmentoProduto> RepositorioSegmentoProduto
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Preco> RepositorioPreco
    {
        get
        {
            return new Repository<Preco>(NHibernateHelper.CurrentSession);
        }
    }

    public static IList<Produto> resultado { get; set; }

    public int count = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Detalhe do produto " + Configuracoes.getSetting("nomeSite");

        if (!IsPostBack)
        {
            if (Request.QueryString["q"] != null)
            {
                string query = Request.QueryString["q"].ToString();
                Produto prod = null;


                SegmentoProduto segprod = null;
                segprod = RepositorioSegmentoProduto.FindBy(x => x.Chave.Equals(query));
                if (segprod != null)
                {
                    query = segprod.Chave;
                }

                prod = RepositorioProduto.FindBy(x => x.Chave.Equals(query));




                if (prod != null)
                {

                    getCores(prod);

                    getImgsOutrasCores(prod);
                    getImagens(prod);
                    repCaracteristicas.DataSource = prod.Informacoes.ToList();
                    repCaracteristicas.DataBind();
                    litNome.Text = prod.Nome;
                    litCodigo.Text = prod.Referencia;
                    litPrecoSemPromocao.Text = prod.Preco.ValorSemPromocao.ToString();
                    //litSubSegmento.Text = prod.SubSegmentos.First().Nome;
                    //litSegmento.Text = prod.Segmentos.First().Nome;
                    //litProduto.Text = prod.Nome;
                    litDescricao.Text = prod.Descricao;
                    litPreco.Text = prod.Preco.Valor.ToString();
                    carregaTamanhos(prod);
                    if (prod.SubSegmentos.Count > 0 && prod.Segmentos.Count > 0)
                    {
                        segmento.Text = prod.Segmentos[0].Nome + " /";
                    }
                    else if(prod.Segmentos.Count > 0)
                    {
                        segmento.Text = prod.Segmentos[0].Nome;
                    }


                    if (prod.SubSegmentos.Count > 0)
                    {
                        subseg.Text = prod.SubSegmentos[0].Nome;
                    }

                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:$(function () { idProduto = " + prod.Id + "; }); ", true);
                    getEtiqueta(prod);
                    //divDesconto.Text = prod.GetDescontoDiv();

                    
                    if (prod.Segmentos.Count > 0)
                    {
                        List<Produto> quemviu = null;
                        quemviu = RepositorioProduto.FilterBy(x => x.Segmentos.Any(y => y.Nome.Equals(prod.Segmentos.First().Nome))).ToList();
                        if (quemviu != null && quemviu.Count > 0)
                        {
                            //repDestaque.DataSource = quemviu.OrderBy(x => x.NumeroVisitas);
                            //repDestaque.DataBind();
                        }
                    }
                    

                    if (!prod.Indisponivel)
                    {
                        string textoG = "";

                        DadoVO dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Texto Preço"));
                        if (dado != null)
                        {
                            textoG = dado.nome;
                        }

                        if (prod.Preco != null)
                        {
                            if (prod.Preco.Valor > 0)
                            {
                                if (prod.Preco.ValorSemPromocao > 0)
                                {     //litDe.Text = prod.Preco.ValorSemPromocao.ToString("C");

                                    //if(prod.Preco.ValorAvista != prod.Preco.Valor)
                                    //{
                                    //    litPrecoParc.Text = prod.Preco.Valor.ToString("C");
                                    //    litPreco.Text = prod.Preco.ValorAvista.ToString("C");
                                    //    litTextoUnico.Text = prod.Preco.Texto;
                                    //    divOu.Visible = true;
                                    //    divOu2.Visible = true;
                                    //    litTextoGeral.Text = textoG;
                                    //}
                                    //else
                                    //{
                                    //    divPrecoUnico.Visible = true;
                                    //    litPrecoUnico.Text = prod.Preco.Valor.ToString("C");
                                    //    litTextoUnico.Text = prod.Preco.Texto;
                                    //}

                                    //if(prod.Preco.ValorAvista == 0)
                                    //{
                                    //    litTextoGeral.Text = "";
                                    //    litPrecoParc.Text = "";
                                    //    litPreco.Text = "";
                                    //    litPrecoUnico.Text = prod.Preco.Valor.ToString("C");
                                    //    divOu.Visible = false;
                                    //    divOu2.Visible = false;
                                    //    divPrecoUnico.Visible = true;
                                    //    litTextoUnico.Text = "";
                                    //}

                                }
                                else
                                {
                                    //litPrecoParc.Text = prod.Preco.ValorAvista.ToString("C");
                                    ////litDe.Text = prod.Preco.ValorSemPromocao.ToString("C");

                                    //divOu.Visible = false;
                                }
                            }
                            else
                            {
                                Preco preco = RepositorioPreco.FindBy(x => x.Produto.Id == prod.Id);

                                if (preco.Valor > 0)
                                {
                                    if (preco.ValorSemPromocao > 0)
                                        //litDe.Text = preco.ValorSemPromocao.ToString("C");

                                        if (preco.ValorAvista != preco.Valor)
                                        {
                                            //litPrecoParc.Text = preco.Valor.ToString("C");
                                            //litPreco.Text = preco.ValorAvista.ToString("C");
                                            //litTextoUnico.Text = preco.Texto;
                                            //divOu.Visible = true;
                                            //divOu2.Visible = true;
                                        }
                                        else
                                        {
                                            //divPrecoUnico.Visible = true;
                                            //litPrecoUnico.Text = preco.Valor.ToString("C");
                                        }

                                    if (preco.ValorAvista == 0)
                                    {
                                        //litTextoGeral.Text = "";
                                        //litPrecoParc.Text = "";
                                        //litPreco.Text = "";
                                        //litPrecoUnico.Text = preco.Valor.ToString("C");
                                        //divOu.Visible = false;
                                        //divOu2.Visible = false;
                                        //divPrecoUnico.Visible = true;
                                        //litTextoUnico.Text = "";
                                    }

                                }
                                else
                                {
                                    //litPrecoParc.Text = preco.ValorAvista.ToString("C");
                                    ////litDe.Text = preco.ValorSemPromocao.ToString("C");

                                    //divOu.Visible = false;
                                }
                            }

                        }
                        else
                        {
                            //divPrecos.Visible = false;
                        }

                        if (prod.Personalizavel)
                        {

                        }
                    }
                }









            }

        }
    }

    protected string getCores(Produto produto)
    {
        string retorno = "";

        IList<Album> albuns = RepositorioAlbum.FilterBy(x => x.Produto.Id == produto.Id).ToList();
        IList<string> listaCores = new List<string>();
        

        foreach (Album alb in albuns)
        {
            if (alb != null && alb.Cor != null)
            {
           
                listaCores.Add(alb.Cor.Nome);
                //if (String.IsNullOrEmpty(retorno))
                //    retorno = alb.Cor.Nome;
                //else
                //    retorno += ", " + alb.Cor.Nome;
            }
        }



        if (listaCores.Count == 1)
        {
            //listaCores[1] = retorno;
        }



        ddlCor.DataSource = listaCores;
        ddlCor.DataBind();
        //litCor.Text = retorno;
        return retorno;
    }



    public string isVisible(Produto prod)
    {
        string retorno = "";

        if (!prod.Novidade && !prod.Indisponivel && !sobEncomenda(prod, 0).Equals("encomenda"))
        {
            return retorno = "hidden-lg hidden-md hidden-sm hidden-xs";
        }

        return retorno;
    }

    public string sobEncomenda(Produto prod, int y)
    {
        string retorno = "";

        bool estoque = false;

        DadoVO dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Habilitar Estoque"));
        if (dado != null)
        {
            estoque = dado.visivel;
        }

        List<Estoque> est = RepositorioEstoque.FilterBy(x => x.Produto.Referencia.Equals(prod.Referencia)).ToList();
        if (est != null && est.Count > 0)
        {
            int total = 0;
            foreach (Estoque temp in est)
            {
                total += total + temp.Quantidade;
            }

            if (total <= 0 && estoque == true)
            {
                if (y == 0)
                    retorno = "encomenda";
                else
                    retorno = "sob encomenda";
            }
        }

        return retorno;
    }

    public void getEtiqueta(Produto produto)
    {
        string retorno = "<a class='product-review ";

        bool estoque = false;

        DadoVO dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Habilitar Estoque"));
        if (dado != null)
        {
            estoque = dado.visivel;
        }

        List<Estoque> est = RepositorioEstoque.FilterBy(x => x.Produto.Referencia.Equals(produto.Referencia)).ToList();
        int total = 0;

        if (est != null && est.Count > 0)
        {

            foreach (Estoque temp in est)
            {
                total += total + temp.Quantidade;
            }

            if (total <= 0 && estoque == true)
            {
                retorno += "encomenda";
            }
        }

        if (produto.Novidade)
            retorno += "novidade";

        if (produto.Indisponivel)
            retorno += "indisponivel";

        retorno += "'>";


        if (produto.Novidade)
            retorno += "NOVIDADE";

        if (produto.Indisponivel)
            retorno += "INDISPONÍVEL";


        if (total <= 0 && estoque == true)
        {
            retorno += "SOB ENCOMENDA";
        }

        if (!retorno.Equals("<a class='product-review '>"))
            litEtiqueta.Text = retorno + "</a>";
        else
        {
            //divEtiqueta.Visible = false;
        }


    }

    public void carregaTamanhos(Produto prod)
    {
        bool estoque = false;

        DadoVO dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Habilitar Estoque"));
        if (dado != null)
        {
            estoque = dado.visivel;
        }

        List<Tamanhos> tamanhos = new List<Tamanhos>();

        if (estoque)
        {
            foreach (Estoque est in RepositorioEstoque.FilterBy(x => x.Produto.Referencia.Equals(prod.Referencia)).ToList())
            {
                if (est.Tamanho != null)
                {
                    if (!tamanhos.Any(x => x.Equals(est.Tamanho.Id)))
                    {
                        tamanhos.Add(est.Tamanho);
                    }
                }
            }
        }
        else
        {
            List<string> ids = new List<string>();

            tamanhos = new List<Tamanhos>();

            if (!string.IsNullOrEmpty(prod.Tamanhos))
            {
                if (prod.Tamanhos.Contains(','))
                {
                    ids = prod.Tamanhos.Split(',').ToList();
                }
                else
                {
                    ids.Add(prod.Tamanhos);
                }
            }


            foreach (string id in ids)
            {
                int i = Convert.ToInt32(id);

                Tamanhos tam = RepositorioTamanhos.FindBy(i);
                if (tam != null)
                {
                    tamanhos.Add(tam);
                }
            }
        }
        ddlTamanho.DataSource = tamanhos;
        ddlTamanho.DataValueField = "id";
        ddlTamanho.DataTextField = "nome";
        ddlTamanho.DataBind();
    }

    public void getImagens(Produto prod)
    {
        List<ImagemProduto> imagens = new List<ImagemProduto>();

        foreach (ImagemProduto img in RepositorioImagemProduto.All().ToList())
        {
            if (img.Album != null && img.Album.Produto != null && img.Album.Produto.Id == prod.Id)
            {
                imagens.Add(img);
            }
        }

        //repThumbs.DataSource = imagens;
        //repThumbs.DataBind();

        repImagens.DataSource = imagens;
        repImagens.DataBind();
    }

    public List<ImagemProduto> getThumbs(ImagemProduto imagem)
    {
        List<ImagemProduto> imagens = new List<ImagemProduto>();

        foreach (ImagemProduto img in RepositorioImagemProduto.All().ToList())
        {
            if (img.Album != null && img.Album.Id == imagem.Id)
            {
                imagens.Add(img);
            }
        }

        return imagens;
    }

    public string GetImg(Produto prod)
    {
        string retorno = "";
        Album alb = RepositorioAlbum.FindBy(x => x.Produto.Id == prod.Id);
        if (alb != null)
        {
            IList<ImagemProduto> imgs = RepositorioImagemProduto.FilterBy(x => x.Album.Id == alb.Id).ToList();
            retorno = MetodosFE.BaseURL + "/ImagensHQ/" + imgs.First().Nome.Replace("jgp", "jpg");
        }
        return retorno;
    }

    public void getImgsOutrasCores(Produto prod)
    {
        List<ImagemProduto> imagens = new List<ImagemProduto>();
        ImagemProduto aa = new ImagemProduto();

        if (!String.IsNullOrEmpty(prod.Carrossel))
        {
            List<string> a = prod.Carrossel.Replace(" ", "").Split(',').ToList();

            Produto temp = null;

            foreach (string rf in a)
            {
                temp = RepositorioProduto.FindBy(x => x.Referencia.Equals(rf));
                if (temp != null)
                {
                    List<Album> albns = RepositorioAlbum.FilterBy(x => x.Produto.Id == temp.Id).ToList();
                    if (albns != null && albns.Count > 0)
                    {
                        aa = albns.First().Imagens.First();
                        aa.Nome = aa.Nome.Replace("jgp", "jpg").Replace("pgn", "png");
                        aa.produto = temp;
                        imagens.Add(aa);
                    }
                }
            }
        }

    }
    protected void btnAdd_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["q"] != null)
            {
                string query = Request.QueryString["q"].ToString();
                Produto prod = null;
                Carrinho produto = new Carrinho();
                prod = RepositorioProduto.FindBy(x => x.Chave.Equals(query));
                if (prod != null)
                {

                    Session["adicionar"] = prod.Id.ToString();

                    repoCarrinho.Add(produto);
                    ControleCarrinho.AdicionaItem(prod, ddlTamanho.SelectedItem.Text, null, Convert.ToInt32(txtQuantidade.Text), "", 0);

                    Response.Redirect("~/carrinho.aspx");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    //protected void btnADD_Click(object sender, EventArgs e)
    //{
    //    string query = Request.QueryString["q"].ToString();
    //    Produto prod = null;
    //    prod = RepositorioProduto.FindBy(x => x.Chave.Equals(query));


    //    DadoVO item = null;
    //    Carrinho produto = new Carrinho();

    //    //produto.imagemURL = prod.getPrimeiraImagemProdutoHQ();
    //    produto.descricao = prod.Descricao;
    //    produto.nome = prod.Nome;
    //    produto.quantidade = "1";
    //    produto.idproduto = prod.Id;
    //    produto.valorunitario = prod.Preco.Valor.ToString();

    //    repoCarrinho.Add(produto);

    //    listaIdsProdutos = (List<Carrinho>)Session["Carrinho"];



    //    if (listaIdsProdutos != null && listaIdsProdutos.Count > 0)
    //    {
    //        listaIdsProdutos.Add(produto);
    //    }
    //    else
    //    {
    //        List<Carrinho> listaIdsProdutosNovos = new List<Carrinho>();


    //        listaIdsProdutosNovos.Add(produto);


    //        listaIdsProdutos = listaIdsProdutosNovos;


    //        Session["Carrinho"] = listaIdsProdutos;

    //    }

    //    Response.Redirect(MetodosFE.BaseURL + "/carrinho.aspx");
    //}
















}