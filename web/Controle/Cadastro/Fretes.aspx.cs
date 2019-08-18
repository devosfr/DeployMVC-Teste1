using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    private Repository<Estado> RepositorioEstado
    {
        get
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Cidade> RepositorioCidade
    {
        get
        {
            return new Repository<Cidade>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<OpcaoFreteLocalidade> RepositorioOpcaoFreteLocalidade
    {
        get
        {
            return new Repository<OpcaoFreteLocalidade>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CarregarEstados();
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                btnSalvar.Visible = false;
                btnPesquisar.Visible = false;
                btnAlterar.Visible = true;
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

            var pesquisa = RepositorioOpcaoFreteLocalidade.All();

            IList<OpcaoFreteLocalidade> listaOpcoes = pesquisa.ToList();

            GridView1.DataSourceID = String.Empty;
            GridView1.DataSource = listaOpcoes;
            GridView1.DataBind();

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

    protected void CarregarEstados()
    {
        IList<Estado> estados = RepositorioEstado.All().OrderBy(x => x.Nome).ToList();

        ddlEstado.DataSource = estados;
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataValueField = "Id";
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, new ListItem("Selecione", ""));
    }

    protected void CarregarCidades()
    {
        string idEstadoString = ddlEstado.SelectedValue;

        IList<Cidade> cidades = null;
        if (!String.IsNullOrEmpty(idEstadoString))
        {
            int id = Convert.ToInt32(idEstadoString);
            cidades = RepositorioCidade.FilterBy(x => x.Estado.Id == id).OrderBy(x => x.Nome).ToList();

        }

        ddlCidade.DataSource = cidades;
        ddlCidade.DataTextField = "Nome";
        ddlCidade.DataValueField = "Id";
        ddlCidade.DataBind();
        ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));

    }

    protected void Carregar()
    {
        try
        {

            OpcaoFreteLocalidade opcao = RepositorioOpcaoFreteLocalidade.FindBy(Codigo);

            if (opcao != null)
            {
                txtNome.Text = opcao.Nome;
                txtDias.Text = opcao.Prazo.ToString();
                txtPreco.Text = opcao.Preco.ToString();
                //txtValorMinimo.Text = opcao.ValorMinimoCompra.ToString();
                if (opcao.Estado != null)
                {
                    ddlEstado.SelectedValue = opcao.Estado.Id.ToString();
                    CarregarCidades();
                }
                if (opcao.Cidade != null)
                    ddlCidade.SelectedValue = opcao.Cidade.Id.ToString();

                txtBairro.Text = opcao.Bairro;
                //txtPesoMaximo.Text = opcao.PesoMaximo.ToString();

                cbAtivo.Checked = opcao.Ativo;
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
            OpcaoFreteLocalidade opcao = new OpcaoFreteLocalidade();

            opcao.Nome = txtNome.Text;
            //opcao.PesoMaximo = float.Parse(txtPesoMaximo.Text);
            opcao.Preco = Convert.ToDecimal(txtPreco.Text);

            opcao.Prazo = Convert.ToInt32(txtDias.Text);

            if (String.IsNullOrEmpty(txtBairro.Text))
                opcao.Bairro = null;
            else
                opcao.Bairro = txtBairro.Text;
            if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
            {
                int id = Convert.ToInt32(ddlEstado.SelectedValue);
                opcao.Estado = RepositorioEstado.FindBy(id);
            }

            if (!String.IsNullOrEmpty(ddlCidade.SelectedValue))
            {
                int id = Convert.ToInt32(ddlCidade.SelectedValue);
                opcao.Cidade = RepositorioCidade.FindBy(id);
            }

            opcao.Ativo = cbAtivo.Checked;

            //if (!String.IsNullOrEmpty(txtValorMinimo.Text))
            //    opcao.ValorMinimoCompra = Convert.ToDecimal(txtValorMinimo.Text);
            //else
            opcao.ValorMinimoCompra = 0;

            RepositorioOpcaoFreteLocalidade.Add(opcao);
            MetodosFE.mostraMensagem("Opção de frete " + opcao.Nome + " cadastrado com sucesso.", "sucesso");
            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }

    }

    protected void ValidaDados()
    {
        if (String.IsNullOrEmpty(txtNome.Text))
            throw new Exception("Campo nome não preenchido.");
        //if (String.IsNullOrEmpty(txtPesoMaximo.Text))
        //    throw new Exception("Peso máximo não preenchido.");

    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            OpcaoFreteLocalidade opcao = RepositorioOpcaoFreteLocalidade.FindBy(Codigo);

            opcao.Nome = txtNome.Text;
            //opcao.PesoMaximo = float.Parse(txtPesoMaximo.Text);
            opcao.Prazo = Convert.ToInt32(txtDias.Text);
            opcao.Preco = Convert.ToDecimal(txtPreco.Text);
            if (String.IsNullOrEmpty(txtBairro.Text))
                opcao.Bairro = null;
            else
                opcao.Bairro = txtBairro.Text;

            if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
            {
                int id = Convert.ToInt32(ddlEstado.SelectedValue);
                opcao.Estado = RepositorioEstado.FindBy(id);
            }

            if (!String.IsNullOrEmpty(ddlCidade.SelectedValue))
            {
                int id = Convert.ToInt32(ddlCidade.SelectedValue);
                opcao.Cidade = RepositorioCidade.FindBy(id);
            }

            //if(!String.IsNullOrEmpty(txtValorMinimo.Text))
            //    opcao.ValorMinimoCompra = Convert.ToDecimal(txtValorMinimo.Text);
            //else
            opcao.ValorMinimoCompra = 0;

            opcao.Ativo = cbAtivo.Checked;

            RepositorioOpcaoFreteLocalidade.Update(opcao);
            MetodosFE.mostraMensagem("Opção de frete " + opcao.Nome + " atualizado com sucesso.", "sucesso");
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
            Pesquisar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        if (ordenacao == Ordenacao)
            asc = !asc;
        Ordenacao = ordenacao;
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
    private int CodigoTela
    {
        get
        {
            if (ViewState["CodigoTela"] == null) ViewState["CodigoTela"] = 0;
            return (Int32)ViewState["CodigoTela"];
        }
        set { ViewState["CodigoTela"] = value; }
    }
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
    private bool asc
    {
        get
        {
            if (ViewState["asc"] == null) ViewState["asc"] = true;
            return (bool)ViewState["asc"];
        }
        set { ViewState["asc"] = value; }
    }
    #endregion

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        Pesquisar();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            OpcaoFreteLocalidade opcao = RepositorioOpcaoFreteLocalidade.FindBy(Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
            RepositorioOpcaoFreteLocalidade.Delete(opcao);
            MetodosFE.mostraMensagem("Opcao de frete " + opcao.Nome + " excluido com sucesso.", "sucesso");
            Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect(this.AppRelativeVirtualPath + "?Codigo=" + Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value), false);
    }
    protected void ddlEstado_TextChanged(object sender, EventArgs e)
    {
        CarregarCidades();
    }
}
