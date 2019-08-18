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

    private Repository<Chamado> RepositorioChamado
    {
        get
        {
            return new Repository<Chamado>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Resposta> RepositorioRepostas
    {
        get
        {
            return new Repository<Resposta>(NHibernateHelper.CurrentSession);
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

        nome = "Chamados";
        nome2 = "Chamado";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;

        if (!Page.IsPostBack)
        {

            ddlCliente.DataSource = RepositorioCliente.All().OrderBy(x=>x.Nome).ToList();
            ddlCliente.DataTextField = "nome";
            ddlCliente.DataValueField = "id";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0,new ListItem("Selecione um cliente",""));
            
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
            }
        }
    }

    protected void Pesquisar()
    {
        try
        {

            var pesquisa = RepositorioChamado.All();

            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x => x.Assunto.Contains(nome));
            }

            if (ddlCliente.SelectedIndex > 0)
            {
                int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
                pesquisa = pesquisa.Where(x => x.Cliente.Id == idCliente);
            }


            IList<Chamado> chamados = pesquisa.OrderBy(x => x.DataSolicitacao).ThenByDescending(x => x.Status).ToList();

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = chamados;
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

            Chamado chamado = RepositorioChamado.FindBy(Codigo);

            if (chamado != null)
            {
                txtAssunto.Text = chamado.Assunto;
                txtCliente.Text = chamado.Cliente.Nome;
                txtCodigo.Text = chamado.Codigo;
                txtNomeProduto.Text = chamado.NomeProduto;
                txtNF.Text = chamado.NF;
                txtModelo.Text = chamado.Modelo;
                txtSerie.Text = chamado.Serie;
                txtDescricao.Text = chamado.Descricao;
                txtData.Text = chamado.DataCompra.ToShortDateString();

                txtStatus.Text = chamado.DataFechamento == null ? "Aberto" : "Fechado";


                if (chamado.DataFechamento != null)
                {
                    txtMensagemRetorno.Visible = false;
                    btnEnviarMensagem.Visible = false;
                    btnAlterar.Visible = false;
                }
                repRepostas.DataSource = chamado.Respostas;
                repRepostas.DataBind();

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

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            Chamado chamado = RepositorioChamado.FindBy(Codigo);



            //if (ativo)
            //    if (RepositorioCupom.All().Any(x => x.Cliente.Id == cliente.Id && x.Ativo && x.Id != cupom.Id))
            //        throw new Exception("Este cliente já possui um cupom ativo.");

            //if (String.IsNullOrEmpty(codigo))
            //    throw new Exception("Código inválido.");

            //if (RepositorioCupom.All().Any(x => x.Codigo.Equals(codigo) && x.Id != cupom.Id))
            //    throw new Exception("Código já em uso.");

            //cupom.Ativo = ativo;
            //cupom.Codigo = codigo;
            //cupom.Cliente = cliente;

            RepositorioChamado.Update(chamado);
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
            Chamado chamado = RepositorioChamado.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));
            RepositorioChamado.Delete(chamado);
            MetodosFE.mostraMensagem(nome2 + " " + chamado.Id + " excluído com sucesso.", "sucesso");
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


    protected void btnEnviarMensagem_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtMensagemRetorno.Text))
        {
            Chamado chamado = RepositorioChamado.FindBy(Codigo);

            Resposta resposta = new Resposta();
            resposta.Origem = (int)Resposta.CodigoOrigem.Administrador;
            resposta.Descricao = txtMensagemRetorno.Text;
            resposta.DataEnvio = DateTime.Now;

            RepositorioRepostas.Add(resposta);

            chamado.Respostas.Add(resposta);

            RepositorioChamado.Update(chamado);

            txtMensagemRetorno.Text = "";
        }
    }
}
