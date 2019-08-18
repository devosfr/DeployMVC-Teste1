using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Usuario : System.Web.UI.Page
{
    private Repository<UsuarioVO> repoUsuario
    {
        get
        {
            return new Repository<UsuarioVO>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                Pesquisar("login");
            }
            else
            {
                Pesquisar("login");
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }
        }
    }
    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }



    protected void Pesquisar(string ordenacao)
    {
        try
        {
            Ordenacao = ordenacao;

            var pesquisa = repoUsuario.FilterBy(x=> !x.tipo.Equals("AA"));

            string login = null;
            if (!String.IsNullOrEmpty(txtBuscaLogin.Text))
            {
                login = txtBuscaLogin.Text.Trim().ToLower();
                pesquisa = pesquisa.Where(x => x.login.ToLower().Contains(login));
            }


            int id = 0;
            try
            {
                if (!String.IsNullOrEmpty(txtIDBusca.Text))
                {
                    id = Convert.ToInt32(txtIDBusca.Text);
                    pesquisa = pesquisa.Where(x => x.Id == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Valor de ID inválido.");
            }

            IList<UsuarioVO> colecaoUsuario = pesquisa.OrderBy(ordenacao).ToList();
            //(id:id, tipo:tipo, idCliente:idCliente,login:login, orderby:Ordenacao);

            GridView1.DataSourceID = String.Empty;
            GridView1.DataSource = colecaoUsuario;
            GridView1.DataBind();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void Carregar()
    {
        try
        {
            UsuarioVO usuario = repoUsuario.FindBy(Codigo);

            if (usuario != null)
            {
                txtLogin.Text = usuario.login;
                //txtSenha.Text = usuario.senha;
                txtId.Text = usuario.Id.ToString();
                ddlStatus.SelectedValue = usuario.status;
                txtNome.Text = usuario.nome;

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
        Pesquisar("");
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioVO usuario = new UsuarioVO();
            usuario.nome = txtNome.Text.Trim();
            usuario.login = txtLogin.Text.Trim();
            if (txtSenha.Text.Trim() != usuario.senha)
                usuario.senha = ControleLogin.GetSHA1Hash(txtSenha.Text.Trim());
            usuario.status = ddlStatus.SelectedValue;
            usuario.tipo = "AD";

            repoUsuario.Add(usuario);

            MetodosFE.mostraMensagem("Usuário " + usuario.login + " cadastrado com sucesso.", "sucesso");
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
            UsuarioVO usuario = repoUsuario.FindBy(Codigo);
            usuario.nome = txtNome.Text.Trim();
            usuario.login = txtLogin.Text.Trim();
            if (!String.IsNullOrEmpty(txtSenha.Text))
                usuario.senha = ControleLogin.GetSHA1Hash(txtSenha.Text.Trim());
            usuario.status = ddlStatus.SelectedValue;

            repoUsuario.Update(usuario);
            MetodosFE.mostraMensagem("Usuário " + usuario.login + " alterado com sucesso.", "sucesso");

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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        Pesquisar(ordenacao);
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
            if (ViewState["Ordenacao"] == null) ViewState["Ordenacao"] = "";
            return (String)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
    }

    #endregion

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        Pesquisar("");
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            UsuarioVO usuario = repoUsuario.FindBy(Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
            repoUsuario.Delete(usuario);
            MetodosFE.mostraMensagem("Usuário " + usuario.login + " alterado com sucesso.", "sucesso");
            Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Codigo = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("Codigo", Codigo.ToString());
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "?" + nameValues.ToString();
        string urlFinal = url + updatedQueryString;
        e.Cancel = true;
        Response.Redirect(urlFinal, false);
    }
}
