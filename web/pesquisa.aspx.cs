using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class _pesquisa : System.Web.UI.Page
{
    protected Repository<Produto> RepoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<Cor> RepoCor
    {
        get
        {
            return new Repository<Cor>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<Tamanhos> RepoTamanho
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<Tamanhos> RepoTamanhosCamisas
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<Album> RepoAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }
    protected Repository<ImagemProduto> RepoImagem
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Estoque> RepositorioEstoque
    {
        get
        {
            return new Repository<Estoque>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<ImagemProduto> RepositorioImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Album> RepositorioAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }


    protected int idProduto { get; set; }
    protected int idTamanho { get; set; }
    protected int contagemDestaque { get; set; }

    public string cortaTexto(string entrada)
    {
        string saida = "";

        if (entrada.Length > 280)
        {
            saida = entrada.Substring(0, 277) + "...";
        }
        else
        {
            saida = entrada;
        }

        return saida;
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

    public string GetImg(Produto prod)
    {
        string retorno = "";
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

    public string isVisible(Produto prod)
    {
        string retorno = "";

        if (!prod.Novidade && !prod.Indisponivel && !sobEncomenda(prod, 0).Equals("encomenda"))
        {
            return retorno = "hidden-lg hidden-md hidden-sm hidden-xs";
        }

        return retorno;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["q"] != null)
            {
                string pesq = Request.QueryString["q"].ToLower().ToString();

                IList<Produto> produtos = RepoProduto.All().ToList();
                IList<Produto> produtosInt = RepoProduto.All().ToList();



                produtos = produtos.Where(x => x.Nome != null && x.Nome.ToLower().Contains(pesq)
                                                 || x.Descricao != null && x.Descricao.ToLower().Contains(pesq)
                                                 || x.Segmentos != null && x.Segmentos.Any(y => y.Nome.ToLower().Contains(pesq))
                                                 || x.SubSegmentos != null && x.SubSegmentos.Any(y => y.Nome.ToLower().Contains(pesq))
                                                 || x.Descricao != null && x.Descricao.ToLower().Contains(pesq)
                                                 || x.Referencia != null && x.Referencia.ToLower().Equals(pesq)
                                                 || x.Marca != null && x.Marca.Nome.ToLower().Equals(pesq)
                                                 ).ToList();

                produtos = produtos.Where(x => x.Visivel).ToList();



                if (produtos == null || produtos.Count == 0)
                {
                    produtos = RepoProduto.All().ToList();

                    produtos = produtos.Where(x => x.Nome != null && x.Nome.ToLower().Equals(pesq)
                                                     || x.Descricao != null && x.Descricao.ToLower().Equals(pesq)
                                                     || x.Segmentos != null && x.Segmentos.Any(y => y.Nome.ToLower().Equals(pesq))
                                                     || x.SubSegmentos != null && x.SubSegmentos.Any(y => y.Nome.ToLower().Equals(pesq))
                                                     || x.Descricao != null && x.Descricao.ToLower().Equals(pesq)
                                                     || x.Referencia != null && x.Referencia.ToLower().Equals(pesq)
                                                     || x.Marca != null && x.Marca.Nome.ToLower().Equals(pesq)
                                                     ).ToList();

                    produtos = produtos.Where(x => x.Visivel).ToList();
                }


                if (produtos != null && produtos.Count > 0)
                {
                    repProd.DataSource = produtos;
                    repProd.DataBind();
                }else if (produtosInt != null && produtosInt.Count > 0)
                {
                    for (int i = 0; i < produtosInt.Count; i++)
                    {
                        string Id = produtosInt[i].Id.ToString();
                        if (Id == pesq)
                        {
                            int Id2 = Convert.ToInt32(Id);

                            repProd.DataSource = produtosInt.Where(x => x.Id == Id2);
                            repProd.DataBind();

                        }
                    }
                }
                else
                {
                    div.Visible = true;
                    div.InnerHtml = "<center><h1 style='padding-top:150px;padding-bottom:150px;'>Nenhum resultado encontrado! :(</h1></center>";
                }




                


            }
            else
            {
                div.Visible = true;
                div.InnerHtml = "<center><h1 style='padding-top:150px;padding-bottom:150px;'>Nenhum resultado encontrado! :(</h1></center>";
            }
        }
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
}