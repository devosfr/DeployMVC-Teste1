using System;
using System.Data;
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

    private Repository<Cupom> RepositorioCupom
    {
        get
        {
            return new Repository<Cupom>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<DescontoCupom> RepositorioDesconto
    {
        get
        {
            return new Repository<DescontoCupom>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Cliente> RepositorioCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        nome = "Modelo de Cupom";
        nome2 = "Modelo de Cupom";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            CarregarProdutos();
            Carregar();

        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }


    protected void CarregarProdutos()
    {
        IList<Produto> produtos = RepositorioProduto.FilterBy(x=>x.Preco!= null && x.Peso.Valor > 0).Fetch(x=>x.Segmentos).OrderBy(x => x.Nome).ToList();

        IList<ProdutoDTO> produtosDTO = new List<ProdutoDTO>();

        foreach (var produto in produtos)
            produtosDTO.Add(new ProdutoDTO() { Nome = produto.Referencia + " - " + produto.Nome, Id = produto.Id });

        ddlProdutos.DataSource = produtosDTO;
        ddlProdutos.DataTextField = "Nome";
        ddlProdutos.DataValueField = "Id";
        ddlProdutos.DataBind();
    }

    protected void Carregar()
    {
        try
        {

            Cupom cupom = RepositorioCupom.FindBy(x=>x.Modelo);

            if (cupom != null)
            {
                gvDescontos.DataSource = cupom.Descontos;
                gvDescontos.DataBind();
                Codigo = cupom.Id;

 
            }
            else
            {
                cupom = new Cupom();
                cupom.Ativo = true;
                cupom.Modelo = true;
                cupom.Cliente = null;
                cupom.Ativo = false;
                RepositorioCupom.Add(cupom);
                Carregar();
            }
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
            return (String)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
    }

    #endregion


    protected void gvDescontos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try 
        {

            DescontoCupom desconto = RepositorioDesconto.FindBy(Convert.ToInt32(gvDescontos.DataKeys[e.RowIndex].Value));
            RepositorioDesconto.Delete(desconto);
            MetodosFE.mostraMensagem("Desconto de produto " + desconto.Produto + " excluído com sucesso.", "sucesso");
            Limpar();
        }
        catch (Exception ex) 
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }
    protected void btnAdicionarDesconto_Click(object sender, EventArgs e)
    {
        try
        {
            Cupom cupom = RepositorioCupom.FindBy(Codigo);
            DescontoCupom descontoCupom = new DescontoCupom();

            Produto produto = RepositorioProduto.FindBy(Convert.ToInt32(ddlProdutos.SelectedValue));

            Decimal desconto = Convert.ToDecimal(txtDesconto.Text);
            Decimal comissao = Convert.ToDecimal(txtComissao.Text);

            

            bool existe = cupom.Descontos.Any(x => x.Produto.Equals(produto) && x.Ativo);

            int tipoDesconto = Convert.ToInt32(ddlTipoDesconto.SelectedValue);

            if(tipoDesconto == (int)DescontoCupom.TipoDesconto.Porcentagem && desconto > 99)
                throw new Exception("Desconto acima de 99%.");

            if (tipoDesconto == (int)DescontoCupom.TipoDesconto.Fixo && desconto > produto.Preco.Valor)
                throw new Exception("Desconto maior que o valor do produto.");

            if(comissao > produto.Preco.Valor)
                throw new Exception("Comissão maior que o valor do produto.");

            if (existe)
                throw new Exception("Este produto já esta na lista. Exclua o item ou desative para adicionar um novo desconto para o mesmo produto.");

            descontoCupom.Ativo = true;

            descontoCupom.Comissao = comissao;
            descontoCupom.Desconto = desconto;
            descontoCupom.Produto = produto;
            descontoCupom.Tipo = tipoDesconto;

            

            RepositorioDesconto.Add(descontoCupom);

            cupom.Descontos.Add(descontoCupom);
            RepositorioCupom.Update(cupom);

            MetodosFE.mostraMensagem("Item adicionado com sucesso ao cupom.", "sucesso");

            Carregar();
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }
}
