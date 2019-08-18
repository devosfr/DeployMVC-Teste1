using System;
using Modelos;

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

        Page.Title = "Carrinho" + " - " + Configuracoes.getSetting("nomeSite");

        if (!IsPostBack)
        {
            if (Session["adicionar"] != null)
            {
                string idProduto = Session["adicionar"].ToString();

                if (!string.IsNullOrEmpty(idProduto))
                {
                    int idProdutoInt = Convert.ToInt32(idProduto);

                    if (idProdutoInt > 0)
                    {
                        Produto produto = RepoProduto.FindBy(idProdutoInt);

                        if (produto != null)
                        {
                            var tamanho = RepoTamanhosCamisas.FindBy(x => x.Nome.Equals("Único"));

                            //ControleCarrinho.AdicionaItem(produto, tamanho.ToString(), null, 1, "", 0);

                            //Response.Redirect(MetodosFE.BaseURL + "/Carrinho");
                        }
                    }
                }
            }
        }
    }
}
