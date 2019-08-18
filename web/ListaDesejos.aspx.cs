using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

public partial class _carrinho : System.Web.UI.Page
{
    protected Repository<Produto> RepoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Tamanhos> RepoTamanhosCamisas
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Lista de Desejos" + " - " + Configuracoes.getSetting("nomeSite");

        if (!IsPostBack)
        {
            if (Request.QueryString["adicionar"] != null)
            {
                string idProduto = Request.QueryString["adicionar"].ToString();

                if (!string.IsNullOrEmpty(idProduto))
                {
                    int idProdutoInt = Convert.ToInt32(idProduto.Replace("'",""));

                    if (idProdutoInt > 0)
                    {
                        Produto produto = RepoProduto.FindBy(idProdutoInt);

                        if (produto != null)
                        {
                            var tamanho = RepoTamanhosCamisas.FindBy(x => x.Nome.Equals("Único"));
                            
                            IList<ItemListaDesejo> listaDesejos = null;
                            listaDesejos = ControleListaDesejos.GetItensLista();

                            if (listaDesejos != null && listaDesejos.Count > 0)
                            {
                                ItemListaDesejo itemExcluido = null;
                                itemExcluido = listaDesejos.FirstOrDefault(x => x.Produto.Id == idProdutoInt);

                                listaDesejos = listaDesejos.Where(x => x.Produto.Id == idProdutoInt).ToList();

                                if (listaDesejos != null && listaDesejos.Count > 0)
                                {
                                    ControleCarrinho.AdicionaItem(itemExcluido.Produto, tamanho, itemExcluido.Cor, 1);

                                    ControleListaDesejos.RemoveItens(listaDesejos);
                                }
                            }

                            Response.Redirect(MetodosFE.BaseURL + "/ListaDesejos.aspx");
                        }
                    }
                }
            }
        }
    }
}
