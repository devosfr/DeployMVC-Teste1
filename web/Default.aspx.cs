using System;
using System.Data;
using System.Linq;
using NHibernate.Linq;
using System.Collections.Generic;
using Modelos;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

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

    protected Repository<Tamanhos> RepositorioTamanhos
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Cor> RepositorioCor
    {
        get
        {
            return new Repository<Cor>(NHibernateHelper.CurrentSession);
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

    protected Repository<Album> RepositorioAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<ImagemProduto> RepositorioImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Marca> RepositorioMarca
    {
        get
        {
            return new Repository<Marca>(NHibernateHelper.CurrentSession);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session["cadastrado"] = null;



            DadoVO homeTitulo = null;
            homeTitulo = MetodosFE.getTela("Home Conteúdo Título");

            if (homeTitulo != null)
            {
                lithomeTitulo.Text = homeTitulo.nome;
            }


            IList<DadoVO> slider = null;
            slider = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Slider") && x.visivel).ToList();
            if (slider != null && slider.Count > 0)
            {
                repSlider.DataSource = slider;
                repSlider.DataBind();
            }

            

            DadoVO IntagramLista = null;
            IntagramLista = MetodosFE.getTela("Intagram Lista");

            if (IntagramLista != null)
            {
                litIntagramLista.Text = IntagramLista.meta;
            }

            
            IList<DadoVO> Noticia = null;
            Noticia = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Publicações")).OrderByDescending(x => x.data).ToList();

            if (Noticia != null && Noticia.Count > 0)
            {

                repNoticia.DataSource = Noticia.Take(2);
                repNoticia.DataBind();
            }








            IList<DadoVO> homeContItens = null;
            homeContItens = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Home Conteúdo Ítens") && x.visivel).ToList();
            if (homeContItens != null && homeContItens.Count > 0)
            {

                rephomeContItens.DataSource = homeContItens;
                rephomeContItens.DataBind();

            }
            //Lista de Produtos
            IList<Produto> destaques = null;
            destaques = RepositorioProduto.FilterBy(x => x.Destaque && x.Visivel).ToList();
            if (destaques != null && destaques.Count > 0)
            {

                //for (int i = 0; i < destaques.Count; i++)
                //{


                //    ImagemProduto imagemProduto = new ImagemProduto();
                //    imagemProduto.Nome = GetImg(destaques[i]);
                //    destaques[i].Imagem = imagemProduto;
                //}


                //repDestaque.DataSource = destaques.Where(x => !String.IsNullOrEmpty(GetImg(x))).ToList();
                //repDestaque.DataBind();
            }

            IList<Produto> maisVisitados = null;
            maisVisitados = RepositorioProduto.FilterBy(x => x.Visivel).ToList();
            if (destaques != null && destaques.Count > 0)
            {
                //repMaisVisitados.DataSource = maisVisitados.OrderBy(x => x.NumeroVisitas).Where(x => !String.IsNullOrEmpty(GetImg(x))).Take(7);
                //repMaisVisitados.DataBind();
            }
        }
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

    protected void linkComprar_Command(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument.ToString());

        Produto prod = RepositorioProduto.FindBy(x => x.Id == id);
        Tamanhos tam = RepositorioTamanhos.FindBy(x => x.Id == 6);
        Cor cor = RepositorioCor.FindBy(x => x.Id == 295);
        //ControleCarrinho.AdicionaItem(prod, tam, cor, 1);

        Response.Redirect("~/Carrinho");
    }
}
