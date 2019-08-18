using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Linq;


public partial class _Default : System.Web.UI.Page
{

    public string Nome;

    public string Fone;

    public string Email;

    public int cont = 0;

    Int16 numPorPaginas = 8;

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

    private Repository<Carrinho> repoCarrinho
    {
        get
        {
            return new Repository<Carrinho>(NHibernateHelper.CurrentSession);
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

    public static IList<SegmentoProduto> filtro_segmentos { get; set; }
    //public static IList<string> filtro_genero { get; set; }
    //public static IList<string> filtro_esporte { get; set; }
    public static IList<Marca> filtro_marca { get; set; }
    public static IList<Tamanhos> filtro_tamanho { get; set; }
    public static IList<Cor> filtro_cor { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {


        Page.Title = "Produtos " + Configuracoes.getSetting("nomeSite");


        if (!IsPostBack)
        {


            IList<Produto> prodLista = null;
            prodLista = RepositorioProduto.All().ToList();

            if (Request.QueryString["seg"] != null)
            {

                //divMenu.Visible = true;
                //divMenuDois.Visible = false;

                string varQuery = Request.QueryString["seg"];

                SegmentoProduto segProd = RepositorioSegmento.FindBy(x => x.Chave.Equals(varQuery));

                //litsegProd.Text = segProd.Nome;

                //Carregando op produtos conforme pelo segmento
                IList<Produto> lista = null;
                lista = RepositorioProduto.All().ToList();


                for (int i = 0; i < lista.Count; i++)
                {


                    repProdutos.DataSource = lista.Skip((MetodosFE.GetPagerIndex() - 1) * numPorPaginas).Take(numPorPaginas)
                        .Where(x => x.Segmentos[0].Chave == varQuery).ToList();
                    repProdutos.DataBind();


                }



                IList<SubSegmentoProduto> novoSubSegmentoProduto = null;
                novoSubSegmentoProduto = RepositorioSubSegmento.All().ToList();
                if (novoSubSegmentoProduto != null)
                {
                    //repSubSegmento1.DataSource = novoSubSegmentoProduto.Where(x => x.Segmento.Chave == Request.QueryString["seg"]);
                    //repSubSegmento1.DataBind();

                }

                //Carregando o Menu
                IList<SegmentoProduto> segmentoproduto = null;
                segmentoproduto = RepositorioSegmento.All().ToList();

                IList<SubSegmentoProduto> subsegmentoproduto = null;
                subsegmentoproduto = RepositorioSubSegmento.All().ToList();


                if (segmentoproduto != null)
                {


                    segmentoproduto1.Text = segmentoproduto[0].Nome;
                    linkSeg1.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoproduto[0].Chave;
                    //repSubSegmentoProduto1.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto1.Text);
                    //repSubSegmentoProduto1.DataBind();

                    if (segmentoproduto.Count > 1)
                    {
                        separador2.Visible = true;
                        lista2.Visible = true;
                        segmentoproduto2.Text = segmentoproduto[1].Nome;
                        linkSeg2.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoproduto[1].Chave;
                        //repSubSegmentoProduto2.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto2.Text);
                        //repSubSegmentoProduto2.DataBind();
                    }

                    if (segmentoproduto.Count > 2)
                    {
                        separador3.Visible = true;
                        lista3.Visible = true;
                        segmentoproduto3.Text = segmentoproduto[2].Nome;
                        linkSeg3.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoproduto[2].Chave;
                        //repSubSegmentoProduto3.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto3.Text);
                        //repSubSegmentoProduto3.DataBind();
                    }


                    if (segmentoproduto.Count > 3)
                    {
                        separador4.Visible = true;
                        lista4.Visible = true;
                        segmentoproduto4.Text = segmentoproduto[3].Nome;
                        linkSeg4.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoproduto[3].Chave;
                        //repSubSegmentoProduto4.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto4.Text);
                        //repSubSegmentoProduto4.DataBind();
                    }



                }
                else if (Request.QueryString["subseg"] != null)
                {
                    //divMenu.Visible = true;
                    //divMenuDois.Visible = false;

                    string querySub = Request.QueryString["subseg"];

                    IList<Produto> produtoLista = null;
                    produtoLista = RepositorioProduto.All().ToList();
                    SubSegmentoProduto subSeg = RepositorioSubSegmento.FindBy(x => x.Chave == querySub);
                    SegmentoProduto segProdObj = RepositorioSegmento.FindBy(x => x.Nome.Equals(subSeg.Segmento.Nome));

                    //litsegProd.Text = segProdObj.Nome;
                    //Montando o menu de subsegmentos
                    IList<SubSegmentoProduto> subSegLista = null;
                    subSegLista = RepositorioSubSegmento.All().ToList();
                    if (subSegLista != null)
                    {
                        //repSubSegmento1.DataSource = subSegLista.Where(x => x.Segmento.Chave == segProdObj.Chave);
                        //repSubSegmento1.DataBind();
                    }


                    repProdutos.DataSource = produtoLista.Where(x => x.SubSegmentos.Contains(subSeg)).Skip((MetodosFE.GetPagerIndex() - 1) * numPorPaginas).Take(numPorPaginas);
                    repProdutos.DataBind();



                }


            }

            if (Request.QueryString["q"] == null && Request.QueryString["seg"] == null && Request.QueryString["subseg"] == null)
            {
              
                resultado = RepositorioProduto.FilterBy(x => x.Visivel).ToList();
                if (resultado != null && resultado.Count > 0)
                {

                    repProdutos.DataSource = resultado.Skip((MetodosFE.GetPagerIndex() - 1) * numPorPaginas).Take(numPorPaginas).ToList();
                    repProdutos.ItemDataBound += rep__ItemDataBound;
                    repProdutos.DataBind();                   


                }


                IList<SegmentoProduto> segmentoprodutoMenu = null;
                segmentoprodutoMenu = RepositorioSegmento.All().ToList();

                IList<SubSegmentoProduto> subsegmentoprodutoMenu = null;
                subsegmentoprodutoMenu = RepositorioSubSegmento.All().ToList();


                if (segmentoprodutoMenu != null)
                {


                    segmentoproduto1.Text = segmentoprodutoMenu[0].Nome;
                    linkSeg1.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoprodutoMenu[0].Chave;
                    //repSubSegmentoProduto1.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto1.Text);
                    //repSubSegmentoProduto1.DataBind();

                    if (segmentoprodutoMenu.Count > 1)
                    {
                        separador2.Visible = true;
                        lista2.Visible = true;
                        segmentoproduto2.Text = segmentoprodutoMenu[1].Nome;
                        linkSeg2.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoprodutoMenu[1].Chave;
                        //repSubSegmentoProduto2.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto2.Text);
                        //repSubSegmentoProduto2.DataBind();
                    }

                    if (segmentoprodutoMenu.Count > 2)
                    {
                        separador3.Visible = true;
                        lista3.Visible = true;
                        segmentoproduto3.Text = segmentoprodutoMenu[2].Nome;
                        linkSeg3.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoprodutoMenu[2].Chave;
                        //repSubSegmentoProduto3.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto3.Text);
                        //repSubSegmentoProduto3.DataBind();
                    }


                    if (segmentoprodutoMenu.Count > 3)
                    {
                        separador4.Visible = true;
                        lista4.Visible = true;
                        segmentoproduto4.Text = segmentoprodutoMenu[3].Nome;
                        linkSeg4.HRef = MetodosFE.BaseURL + "/produtos/?seg=" + segmentoprodutoMenu[3].Chave;
                        //repSubSegmentoProduto4.DataSource = subsegmentoproduto.Where(x => x.Segmento.Nome == segmentoproduto4.Text);
                        //repSubSegmentoProduto4.DataBind();
                    }


                    CarregarFiltros("");

                    filtro_segmentos = null;
                    filtro_tamanho = null;
                    filtro_marca = null;
                    filtro_tamanho = null;
                    filtro_cor = null;

                    //litSeg.Text = "Todos";
                }


            }



            litPaginacao.Text = MetodosFE.buildSitePagination(prodLista.Count, numPorPaginas);

            MaintainScrollPositionOnPostBack = true;
        }
    }

    public List<Tamanhos> carregaTamanhos(Produto prod)
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

        return tamanhos;
        
    }


    protected IList<string> getCores(Produto produto)
    {
   
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

        //ddlCor.DataSource = listaCores;
        //ddlCor.DataBind();
        //litCor.Text = retorno;
        return listaCores;
    }




    void rep__ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Produto dados = (Produto)e.Item.DataItem;
            DropDownList dl = (DropDownList)e.Item.FindControl("ddlCor");
            DropDownList ddltamanho = (DropDownList)e.Item.FindControl("ddlTamanho");
            Literal litmarca = (Literal)e.Item.FindControl("litMarca");
            dl.DataSource = getCores(dados);
            dl.DataBind();
            ddltamanho.DataSource = carregaTamanhos(dados);
            ddltamanho.DataValueField = "id";
            ddltamanho.DataTextField = "nome";
            ddltamanho.DataBind();
            if (dados.Marca != null)
            {
                litmarca.Text = dados.Marca.Nome;
            }

            IList<Produto> produtos = new List<Produto>();
            produtos.Add(dados);
        }
    }

    protected void btnAdd_ServerClick(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

            try
            {

                //TextBox txtNome = (TextBox)(repProdutos.Items[0].FindControl("txtNome"));
                //TextBox txtFone = (TextBox)(repProdutos.Items[0].FindControl("txtFone"));
                //TextBox txtMail = (TextBox)(repProdutos.Items[0].FindControl("txtMail"));
                //TextBox txtComent = (TextBox)(repProdutos.Items[0].FindControl("txtComent"));

                LinkButton btn = (LinkButton)(sender);                

                Produto prod = null;
                //Carrinho produto = new Carrinho();
                prod = RepositorioProduto.FindBy(x => x.Chave.Equals(btn.CommandArgument));
                if (prod != null && prod.Indisponivel != true)
                {

                    Session["adicionar"] = prod.Id.ToString();
                    Carrinho produto = new Carrinho();

                    EnvioEmailsVO envio = new EnvioEmailsVO();

                
                    repoCarrinho.Add(produto);
                    ControleCarrinho.AdicionaItem(prod, prod.Tamanhos, null, 1, "", 0);

                    Response.Redirect("~/Carrinho");
                }

            }
            catch (Exception)
            {

            }
        }

    }



    public void CarregarFiltros(string query)
    {
        if (String.IsNullOrEmpty(query))
        {
            List<SegmentoProduto> segs = RepositorioSegmento.All().ToList();
            if (segs != null)
            {
                //repCategoria.DataSource = segs.OrderBy(x => x.Nome);
                //repCategoria.DataBind();
            }

            List<Marca> marcas = RepositorioMarca.All().ToList();
            if (marcas != null)
            {
                //repMarca.DataSource = marcas.OrderBy(x => x.Nome);
                //repMarca.DataBind();
            }

            List<Tamanhos> total_tams = new List<Tamanhos>();
            foreach (Produto prod in resultado)
            {
                total_tams.AddRange(getTamanhos(prod));
            }
            if (total_tams != null && total_tams.Count > 0)
            {
                //repTamanho.DataSource = total_tams.Distinct().OrderBy(x => x.Id);
                //repTamanho.DataBind();
            }

            List<Cor> cores = RepositorioCor.All().ToList();
            if (cores != null)
            {
                //repCor.DataSource = cores.OrderBy(x => x.Nome);
                //repCor.DataBind();
            }
        }
    }

    public string GetImg(Produto prod)
    {
        string retorno = "";
        if (prod.Id == 193)
        {
            //
        }
        
        Album alb = RepositorioAlbum.FindBy(x => x.Produto.Id == prod.Id);
        if (alb != null)
        {
            IList<ImagemProduto> imgs = RepositorioImagemProduto.FilterBy(x => x.Album.Id == alb.Id).ToList();

            if (imgs == null)
                return "";
            if (imgs != null && imgs.Count <= 0)
                return "";

            retorno = MetodosFE.BaseURL + "/ImagensHQ/" + imgs.First().Nome.Replace("jgp", "jpg");
        }


        return retorno;
    }


    public IList<string> GetImgLista(Produto prod)
    {

        getCores(prod);
        IList<string> retorno = new List<string>();
        IList<Album> albuns = null;
        albuns = RepositorioAlbum.FilterBy(x => x.Produto.Id == prod.Id).ToList();
       
        for (int i = 0; i < albuns.Count; i++)
        {
            for (int i2 = 0; i2 < albuns[i].Imagens.Count; i2++)
            {
                if (albuns[i].Imagens != null)
                {
                    retorno.Add(MetodosFE.BaseURL + "/ImagensHQ/" + albuns[i].Imagens[i2].Nome);
                }
            }
        }


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

    public string verificaPreco(string precoSPromo, bool ind)
    {
        if (String.IsNullOrEmpty(precoSPromo))
        {
            return "";
        }
        else
        {
            if (precoSPromo.Equals("0,00"))
            {
                return "";
            }
            else
            {
                if (ind)
                {
                    return "";
                }
                else
                {
                    return "<span class='title-price line-through'>R$" + precoSPromo + "</span>";
                }
            }
        }
    }

    protected void ddlOrdem_TextChanged(object sender, EventArgs e)
    {
        //if (ddlOrdem.SelectedIndex == 0)
        //{
        //    repProdutos.DataSource = resultado.OrderBy(x => x.NumeroVendas);
        //    repProdutos.DataBind();
        //}
        //if (ddlOrdem.SelectedIndex == 1)
        //{
        //    repProdutos.DataSource = resultado.OrderByDescending(x => getValor(x));
        //    repProdutos.DataBind();
        //}
        //if (ddlOrdem.SelectedIndex == 2)
        //{
        //    repProdutos.DataSource = resultado.OrderBy(x => getValor(x));
        //    repProdutos.DataBind();
        //}
    }

    protected decimal getValor(Produto prod)
    {
        decimal retorno = new decimal();

        Preco preco = RepositorioPreco.FindBy(x => x.Produto.Id == prod.Id);

        if (preco != null)
        {
            retorno = preco.ValorAvista;
        }

        return retorno;
    }

    protected List<Tamanhos> getTamanhos(Produto prod)
    {
        List<Tamanhos> retorno = new List<Tamanhos>();



        List<string> ids = new List<string>();

        List<Tamanhos> tamanhos = new List<Tamanhos>();

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

        retorno = tamanhos.ToList();

        return retorno;
    }

    #region FILTRO

    public void filtrar()
    {
        int cont = 0;

        List<Produto> temp = new List<Produto>();
        List<Produto> filtered = resultado.ToList();
        if (filtro_segmentos != null && filtro_segmentos.Count > 0)
        {
            foreach (SegmentoProduto seg in filtro_segmentos)
            {
                temp.AddRange(filtered.Where(x => x.Segmentos.First().Nome.Equals(seg.Nome)));
            }
            filtered = temp;
            temp = new List<Produto>();
        }
        else
            cont++;

        if (filtro_marca != null && filtro_marca.Count > 0)
        {
            foreach (Marca marc in filtro_marca)
            {
                temp.AddRange(filtered.Where(x => x.Marca.Nome.Equals(marc.Nome)));
            }
            filtered = temp;
            temp = new List<Produto>();
        }
        else
            cont++;

        if (filtro_tamanho != null && filtro_tamanho.Count > 0)
        {
            foreach (Tamanhos tam in filtro_tamanho)
            {
                temp.AddRange(filtered.Where(x => getTamanhos(x).Any(y => y.Equals(tam))));
            }
            filtered = temp;
            temp = new List<Produto>();
        }
        else
            cont++;

        if (filtro_cor != null && filtro_cor.Count > 0)
        {
            foreach (Cor cor in filtro_cor)
            {
                temp.AddRange(filtered.Where(x => x.Cores.First().Nome.Equals(cor.Nome)));
            }
            filtered = temp;
            temp = new List<Produto>();
        }
        else
            cont++;

        if (Request.QueryString["d"] != null)
        {
            string x = Request.QueryString["d"].ToString();

            if (x.Equals("goleiro"))
            {
                temp = filtered.Where(y => y.Goleiro).ToList();

                //litSeg.Text = "Goleiros";
            }

            if (x.Equals("infantil"))
            {
                temp = filtered.Where(y => y.Infantil).ToList();
                //litSeg.Text = "Infantil";
            }

            filtered = temp;
        }

        if (Request.QueryString["ss"] != null)
        {
            string x = Request.QueryString["ss"].ToString();

            SubSegmentoProduto sub = RepositorioSubSegmento.FindBy(e => e.Chave.Equals(x));

            if (sub != null)
            {
                temp = filtered.Where(y => y.SubSegmentos != null && y.SubSegmentos.Any(t => t.Id == sub.Id)).ToList();
                //litSeg.Text = sub.Nome;
            }

            filtered = temp;
        }


        repProdutos.DataSource = null;
        repProdutos.DataSourceID = null;
        repProdutos.DataBind();
        repProdutos.DataSource = filtered;
        repProdutos.DataBind();

        if (cont == 4 && Request.QueryString["ss"] == null)
        {
            repProdutos.DataSource = null;
            repProdutos.DataSourceID = null;
            repProdutos.DataBind();
            repProdutos.DataSource = resultado;
            repProdutos.DataBind();
        }
    }

    //ADICIONA FILTRO
    protected void chkCategoria_CheckedChanged(object sender, EventArgs e)
    {
        //for (int i = 0; i < repCategoria.Items.Count; i++)
        //{
        //    CheckBox chk = (CheckBox)repCategoria.Items[i].FindControl("chkCategoria");
        //    if (chk.Checked == true)
        //    {
        //        HiddenField hdn = (HiddenField)repCategoria.Items[i].FindControl("hdn");
        //        if (hdn != null)
        //        {
        //            if (filtro_segmentos == null)
        //            {
        //                filtro_segmentos = new List<SegmentoProduto>();
        //            }
        //            if (!filtro_segmentos.Any(x => x.Nome.Equals(hdn.Value)))
        //            {
        //                SegmentoProduto temp = RepositorioSegmento.FindBy(x => x.Nome.Equals(hdn.Value));
        //                filtro_segmentos.Add(temp);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        HiddenField hdn = (HiddenField)repCategoria.Items[i].FindControl("hdn");
        //        if (hdn != null)
        //        {
        //            if (filtro_segmentos == null)
        //            {
        //                filtro_segmentos = new List<SegmentoProduto>();
        //            }
        //            if (filtro_segmentos.Any(x => x.Nome.Equals(hdn.Value)))
        //            {
        //                filtro_segmentos.Remove(filtro_segmentos.FirstOrDefault(x => x.Nome.Equals(hdn.Value)));
        //            }
        //        }
        //    }
        //}

        //repCategoriaSelecionada.DataSource = filtro_segmentos.OrderBy(x => x.Nome);
        //repCategoriaSelecionada.DataBind();
        filtrar();
    }
    protected void chkEsporte_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkMarca_CheckedChanged(object sender, EventArgs e)
    {
        //for (int i = 0; i < repMarca.Items.Count; i++)
        //{
        //    CheckBox chk = (CheckBox)repMarca.Items[i].FindControl("chkMarca");
        //    if (chk.Checked == true)
        //    {
        //        HiddenField hdn = (HiddenField)repMarca.Items[i].FindControl("hdn");
        //        if (hdn != null)
        //        {
        //            if (filtro_marca == null)
        //            {
        //                filtro_marca = new List<Marca>();
        //            }
        //            if (!filtro_marca.Any(x => x.Nome.Equals(hdn.Value)))
        //            {
        //                Marca temp = RepositorioMarca.FindBy(x => x.Nome.Equals(hdn.Value));
        //                filtro_marca.Add(temp);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        HiddenField hdn = (HiddenField)repMarca.Items[i].FindControl("hdn");
        //        if (hdn != null)
        //        {
        //            if (filtro_marca == null)
        //            {
        //                filtro_marca = new List<Marca>();
        //            }
        //            if (filtro_marca.Any(x => x.Nome.Equals(hdn.Value)))
        //            {
        //                filtro_marca.Remove(filtro_marca.FirstOrDefault(x => x.Nome.Equals(hdn.Value)));
        //            }
        //        }
        //    }
        //}

        //repMarcaSelecionado.DataSource = filtro_marca.OrderBy(x => x.Nome);
        //repMarcaSelecionado.DataBind();
        filtrar();
    }
    //protected void chkTamanho_CheckedChanged(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < repTamanho.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)repTamanho.Items[i].FindControl("chkTamanho");
    //        if (chk.Checked == true)
    //        {
    //            HiddenField hdn = (HiddenField)repTamanho.Items[i].FindControl("hdn");
    //            if (hdn != null)
    //            {
    //                if (filtro_tamanho == null)
    //                {
    //                    filtro_tamanho = new List<Tamanhos>();
    //                }
    //                if (!filtro_tamanho.Any(x => x.Equals(hdn.Value)))
    //                {
    //                    Tamanhos temp = RepositorioTamanhos.FindBy(x => x.Id.ToString().Equals(hdn.Value));
    //                    filtro_tamanho.Add(temp);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            HiddenField hdn = (HiddenField)repTamanho.Items[i].FindControl("hdn");
    //            if (hdn != null)
    //            {
    //                if (filtro_tamanho == null)
    //                {
    //                    filtro_tamanho = new List<Tamanhos>();
    //                }
    //                if (filtro_tamanho.Any(x => x.Equals(hdn.Value)))
    //                {
    //                    filtro_tamanho.Remove(filtro_tamanho.FirstOrDefault(x => x.Equals(hdn.Value)));
    //                }
    //            }
    //        }
    //    }

    //    repTamSelecionados.DataSource = filtro_tamanho.OrderBy(x => x);
    //    repTamSelecionados.DataBind();
    //    filtrar();
    //}
    //protected void chkCor_CheckedChanged(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < repCor.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)repCor.Items[i].FindControl("chkCor");
    //        if (chk.Checked == true)
    //        {
    //            HiddenField hdn = (HiddenField)repCor.Items[i].FindControl("hdn");
    //            if (hdn != null)
    //            {
    //                if (filtro_cor == null)
    //                {
    //                    filtro_cor = new List<Cor>();
    //                }
    //                if (!filtro_cor.Any(x => x.Nome.Equals(hdn.Value)))
    //                {
    //                    Cor temp = RepositorioCor.FindBy(x => x.Nome.Equals(hdn.Value));
    //                    filtro_cor.Add(temp);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            HiddenField hdn = (HiddenField)repCor.Items[i].FindControl("hdn");
    //            if (hdn != null)
    //            {
    //                if (filtro_cor == null)
    //                {
    //                    filtro_cor = new List<Cor>();
    //                }
    //                if (filtro_cor.Any(x => x.Nome.Equals(hdn.Value)))
    //                {
    //                    filtro_cor.Remove(filtro_cor.FirstOrDefault(x => x.Nome.Equals(hdn.Value)));
    //                }
    //            }
    //        }
    //    }

    //    repCorSelecionados.DataSource = filtro_cor.OrderBy(x => x.Nome);
    //    repCorSelecionados.DataBind();
    //    filtrar();
    //}

    //EXCLUI FILTRO
    //protected void linkFecharTamanho_Command(object sender, CommandEventArgs e)
    //{
    //    for (int i = 0; i < repTamanho.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)repTamanho.Items[i].FindControl("chkTamanho");

    //        HiddenField hdn = (HiddenField)repTamanho.Items[i].FindControl("hdn");
    //        if (hdn != null)
    //        {
    //            if (hdn.Value.Equals(e.CommandArgument.ToString()))
    //            {
    //                if (filtro_tamanho.Any(x => x.Equals(hdn.Value)))
    //                {
    //                    filtro_tamanho.Remove(filtro_tamanho.FirstOrDefault(x => x.Equals(hdn.Value)));
    //                }
    //                chk.Checked = false;
    //            }

    //        }
    //    }
    //    repTamSelecionados.DataSource = filtro_tamanho.OrderBy(x => x);
    //    repTamSelecionados.DataBind();
    //    filtrar();
    //}

    //protected void linkFecharCategoria_Command(object sender, CommandEventArgs e)
    //{
    //    for (int i = 0; i < repCategoria.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)repCategoria.Items[i].FindControl("chkCategoria");

    //        HiddenField hdn = (HiddenField)repCategoria.Items[i].FindControl("hdn");
    //        if (hdn != null)
    //        {
    //            if (hdn.Value.Equals(e.CommandArgument.ToString()))
    //            {
    //                if (filtro_segmentos.Any(x => x.Nome.Equals(hdn.Value)))
    //                {
    //                    filtro_segmentos.Remove(filtro_segmentos.FirstOrDefault(x => x.Nome.Equals(hdn.Value)));
    //                }
    //                chk.Checked = false;
    //            }

    //        }
    //    }
    //    repCategoriaSelecionada.DataSource = filtro_segmentos.OrderBy(x => x.Nome);
    //    repCategoriaSelecionada.DataBind();
    //    filtrar();
    //}

    //protected void linkFecharMarca_Command(object sender, CommandEventArgs e)
    //{
    //    for (int i = 0; i < repMarca.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)repMarca.Items[i].FindControl("chkMarca");

    //        HiddenField hdn = (HiddenField)repMarca.Items[i].FindControl("hdn");
    //        if (hdn != null)
    //        {
    //            if (hdn.Value.Equals(e.CommandArgument.ToString()))
    //            {
    //                if (filtro_marca.Any(x => x.Nome.Equals(hdn.Value)))
    //                {
    //                    filtro_marca.Remove(filtro_marca.FirstOrDefault(x => x.Nome.Equals(hdn.Value)));
    //                }
    //                chk.Checked = false;
    //            }

    //        }
    //    }
    //    //repMarcaSelecionado.DataBind();
    //    //repMarcaSelecionado.DataSource = filtro_marca.OrderBy(x => x.Nome);
    //    filtrar();
    //}

    //protected void linkFecharCor_Command(object sender, CommandEventArgs e)
    //{
    //    for (int i = 0; i < repCor.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)repCor.Items[i].FindControl("chkCor");

    //        HiddenField hdn = (HiddenField)repCor.Items[i].FindControl("hdn");
    //        if (hdn != null)
    //        {
    //            if (hdn.Value.Equals(e.CommandArgument.ToString()))
    //            {
    //                if (filtro_cor.Any(x => x.Nome.Equals(hdn.Value)))
    //                {
    //                    filtro_cor.Remove(filtro_cor.FirstOrDefault(x => x.Nome.Equals(hdn.Value)));
    //                }
    //                chk.Checked = false;
    //            }
    //        }
    //    }
    //    repCorSelecionados.DataSource = filtro_cor.OrderBy(x => x.Nome);
    //    repCorSelecionados.DataBind();
    //    filtrar();
    //}

    #endregion
}















