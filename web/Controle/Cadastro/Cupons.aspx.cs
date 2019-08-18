using System;
using System.Data;
using System.Linq;
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
    private Repository<DescontoCupom> RepositorioDesconto
    {
        get
        {
            return new Repository<DescontoCupom>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Carrinho> RepositorioCarrinho
    {
        get
        {
            return new Repository<Carrinho>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Pedido> RepositorioPedido
    {
        get
        {
            return new Repository<Pedido>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Cupons";
        nome2 = "Cupom";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;

        if (!Page.IsPostBack)
        {
            liDescontos.Visible = false;
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
            }
            else
            {
                Pesquisar();
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }
        }
    }

    protected void Pesquisar()
    {
        try
        {
            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
            }

            int id = 0;

            IList<Cupom> cupons = RepositorioCupom.FilterBy(x => !x.Modelo).ToList();

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = cupons;
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

    protected void Carregar()
    {
        try
        {
            Cupom cupom = RepositorioCupom.FindBy(Codigo);

            if (cupom != null)
            {
                txtCodigo.Text = cupom.Codigo;
                txtDesconto.Text = Convert.ToString(cupom.Desconto);
                txtDescontoPercentual.Text = Convert.ToString(cupom.DescontoPercentual);
                txtValidade.Text = Convert.ToString(cupom.Validade);
                liDescontos.Visible = true;
                ddlAtivo.SelectedValue = cupom.Ativo ? "S" : "N";

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
        Pesquisar();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Cupom cupom = new Cupom();

            bool ativo = ddlAtivo.SelectedValue.Equals("S") ? true : false;

            string codigo = txtCodigo.Text;

            if (String.IsNullOrEmpty(codigo))
                throw new Exception("Código inválido.");

            if (RepositorioCupom.All().Any(x => x.Codigo.Equals(codigo)))
                throw new Exception("Código já em uso, favor gerar um outro código");

            cupom.Codigo = codigo;
            cupom.Ativo = ativo;

            RepositorioCupom.Add(cupom);

            MetodosFE.mostraMensagem(" " + nome2 + " cadastrado com sucesso.", "sucesso");
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
            Cupom cupom = RepositorioCupom.FindBy(Codigo);

            bool ativo = ddlAtivo.SelectedValue.Equals("S") ? true : false;

            string codigo = txtCodigo.Text;


            if (String.IsNullOrEmpty(codigo))
                throw new Exception("Código inválido.");

            if (RepositorioCupom.All().Any(x => x.Codigo.Equals(codigo) && x.Id != cupom.Id))
                throw new Exception("Código já em uso.");

            if (txtDesconto.Text == "" && txtDescontoPercentual.Text == "0")
            {
                throw new Exception("Deve ser definido um tipo de desconto.");
            }
            if (txtDesconto.Text == "")
            {
                cupom.Desconto = 0;
            }
            else
            {
                decimal totalDesconto = Convert.ToDecimal(txtDesconto.Text);
                cupom.Desconto = totalDesconto;
            }

            if (txtDescontoPercentual.Text == "")
            {
                cupom.DescontoPercentual = 0;
            }
            else
            {
                cupom.DescontoPercentual = Convert.ToInt32(txtDescontoPercentual.Text);
            }


            cupom.Validade = Convert.ToDateTime(txtValidade.Text);

            cupom.Ativo = ativo;
            cupom.Codigo = codigo;

            RepositorioCupom.Update(cupom);
            MetodosFE.mostraMensagem(nome2 + " alterado com sucesso.", "sucesso");
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
        Pesquisar();
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

    protected void gvObjeto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvObjeto.PageIndex = e.NewPageIndex;

        Pesquisar();
    }
    protected void gvObjeto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Cupom cupom = RepositorioCupom.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));

            IList<Pedido> cuponsEmPedidos = RepositorioPedido.FilterBy(x => x.Cupom.Codigo == cupom.Codigo).ToList();

            if (cuponsEmPedidos.Count > 0)
            {
                throw new Exception("Cupom não pode ser excluído pois há pedidos vinculados, favor desativa-lo");
            }

            IList<Carrinho> cuponsNoCarrinho = RepositorioCarrinho.FilterBy(x => x.Cupom.Codigo == cupom.Codigo).ToList();

            foreach (var carrinho in cuponsNoCarrinho)
            {
                carrinho.Cupom = null;

                RepositorioCarrinho.Update(carrinho);
            }

            RepositorioCupom.Delete(cupom);


            MetodosFE.mostraMensagem(nome2 + " " + cupom.Codigo + " excluído com sucesso.", "sucesso");
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

    protected void btnGerarCodigo_Click(object sender, EventArgs e)
    {
        string codigo = "";

        codigo = GerarStringRandomica(6);

        txtCodigo.Text = codigo;
    }

    public string GerarStringRandomica(int tamanho)
    {
        Random rand = new Random();

        string AlfabetoComNumeros = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        char[] codigo = new char[tamanho];

        for (int i = 0; i < tamanho; i++)
        {
            codigo[i] = AlfabetoComNumeros[rand.Next(AlfabetoComNumeros.Length)];
        }

        return new string(codigo);
    }
}
