using System;
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

    public Repository<Cliente> RepoCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        nome = "Clientes";
        nome2 = "Cliente";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                Pesquisar("");
            }
            else
            {
                Pesquisar("");
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
                if (ddlTipo.SelectedIndex == 1)
                {
                    divPJ.Visible = true;
                    divPF.Visible = false;
                }
                if (ddlTipo.SelectedIndex == 0)
                {
                    divPJ.Visible = false;
                    divPF.Visible = true;
                }

            }
        }
    }

    protected void Pesquisar(string ordenacao)
    {
        try
        {
            var pesquisa = RepoCliente.All();

            string nome = "";
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text.Trim();
                pesquisa = pesquisa.Where(x => x.Nome != null && x.Nome.ToLower().Contains(nome.ToLower()));
            }

            int id = 0;
            if (!String.IsNullOrEmpty(txtIDBusca.Text))
            {
                id = Convert.ToInt32(txtIDBusca.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }


            IList<Cliente> cores = pesquisa.ToList();

            gvObjeto.PageIndex = Pagina;

            gvObjeto.DataSourceID = String.Empty;
            gvObjeto.DataSource = cores;
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
            bool pj = false;

            Cliente cliente = RepoCliente.FindBy(Codigo);

            if(ddlStatus.Items.FindByValue(cliente.Status) != null)
                ddlStatus.Items.FindByValue(cliente.Status).Selected = true;

            if (cliente != null)
            {
                if (String.IsNullOrEmpty(cliente.CNPJ))
                {
                    ddlTipo.Items.FindByValue("F").Selected = true;
                }
                else
                {
                    ddlTipo.Items.FindByValue("J").Selected = true;
                    pj = true;
                }

                ddlTipo_SelectedIndexChanged(null, new EventArgs());

                //CAMPOS GERAIS
                txtID.Text = cliente.Id.ToString();
                txtNome.Text = cliente.Nome;
                txtEmail.Text = cliente.Email;
                txtTelefone.Text = cliente.Telefone;
                txtCelular.Text = cliente.Whatsapp;

                //CAMPOS ESPECIFICOS
                if (pj)
                {
                    //carrega empresa
                    txtCNPJ.Text = cliente.CNPJ;
                    txtInscricao.Text = cliente.InscricaoEstadual;
                }
                else
                {
                    //carrega pessoa
                    if (!String.IsNullOrEmpty(cliente.Genero))
                        ddlGenero.Items.FindByText(cliente.Genero).Selected = true;

                    if (!String.IsNullOrEmpty(cliente.DataNascimento.ToString()) && cliente.DataNascimento != new DateTime())
                        txtNascimento.Text = cliente.DataNascimento.ToShortDateString();

                    txtCPF.Text = cliente.CPF;
                }

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
            bool pj = false;

            Cliente cliente = new Cliente();

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            cliente.Status = ddlStatus.SelectedValue.ToString();

            cliente.Nome = txtNome.Text.Trim();

            if (!String.IsNullOrEmpty(txtCNPJ.Text))
            {
                pj = true;
            }
            
            if (pj)
            {
                cliente.InscricaoEstadual = txtInscricao.Text;

                cliente.CNPJ = txtCNPJ.Text;
            }
            else
            {
                cliente.CPF = txtCPF.Text;

                cliente.Genero = ddlGenero.SelectedItem.Text;

                DateTime data = new DateTime();

                if (DateTime.TryParse(txtNascimento.Text, out data))
                {
                    cliente.DataNascimento = data;
                }
                else
                    cliente.DataNascimento = data;
            }

            if (String.IsNullOrEmpty(txtTelefone.Text))
                throw new Exception(" Telefone é um campo Obrigatório.");

            cliente.Telefone = txtTelefone.Text;

            cliente.Whatsapp = txtCelular.Text;

            if (String.IsNullOrEmpty(txtEmail.Text) && !ControleValidacao.validaEmail(txtEmail.Text))
                throw new Exception(" E-mail inválido.");

            cliente.Email = txtEmail.Text;

            if (String.IsNullOrEmpty(txtSenha.Text) || txtSenha.Text.Length < 6)
                throw new Exception("Senha inválida (Min. 6 caractéres)");

            cliente.Senha = ControleLogin.GetSHA1Hash(txtSenha.Text);

            ControleLoginCliente.CadastraCliente(cliente);

            RepoCliente.Add(cliente);

            MetodosFE.mostraMensagem(" " + nome2 + " " + cliente.Nome + " cadastrado com sucesso.", "sucesso");
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
            Cliente cliente = RepoCliente.FindBy(Codigo);

            bool pj = false;

            if (String.IsNullOrEmpty(txtNome.Text))
                throw new Exception(" Nome é um campo Obrigatório.");

            cliente.Nome = txtNome.Text.Trim();

            cliente.Status = ddlStatus.SelectedValue.ToString();

            if (!String.IsNullOrEmpty(txtCNPJ.Text))
            {
                pj = true;
            }

            if (pj)
            {
                cliente.InscricaoEstadual = txtInscricao.Text;

                cliente.CNPJ = txtCNPJ.Text;
            }
            else
            {
                cliente.CPF = txtCPF.Text;

                cliente.Genero = ddlGenero.SelectedItem.Text;

                DateTime data = new DateTime();

                if (DateTime.TryParse(txtNascimento.Text, out data))
                {
                    cliente.DataNascimento = data;
                }
                else
                    cliente.DataNascimento = data;
            }

            if (String.IsNullOrEmpty(txtTelefone.Text))
                throw new Exception(" Telefone é um campo Obrigatório.");

            cliente.Telefone = txtTelefone.Text;

            cliente.Whatsapp = txtCelular.Text;

            if (String.IsNullOrEmpty(txtEmail.Text))
                throw new Exception(" E-mail inválido.");

            cliente.Email = txtEmail.Text;

            if (!String.IsNullOrEmpty(txtSenha.Text))
            {
                if(txtSenha.Text.Length < 6)
                    throw new Exception("Senha inválida (Min. 6 caractéres)");
                else
                    cliente.Senha = ControleLogin.GetSHA1Hash(txtSenha.Text);
            }
               
            RepoCliente.Update(cliente);

            MetodosFE.mostraMensagem(" " + nome2 + " " + cliente.Nome + " cadastrado com sucesso.", "sucesso");
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

    private int Deleta
    {
        get
        {
            if (ViewState["Deleta"] == null) ViewState["Deleta"] = 0;
            return (Int32)ViewState["Deleta"];
        }
        set { ViewState["Deleta"] = value; }
    }
    private int Pagina
    {
        get
        {
            if (Session["Pagina"] == null) Session["Pagina"] = 0;
            return (Int32)Session["Pagina"];
        }
        set { Session["Pagina"] = value; }
    }

    #endregion

    protected void gvObjeto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvObjeto.PageIndex = e.NewPageIndex;
        Pagina = gvObjeto.PageIndex;
        Pesquisar("");
    }
    protected void gvObjeto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Cliente cliente = RepoCliente.FindBy(Convert.ToInt32(gvObjeto.DataKeys[e.RowIndex].Value));
            RepoCliente.Delete(cliente);

            MetodosFE.mostraMensagem(nome2 + " " + cliente.Nome + " excluído com sucesso.", "sucesso");
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


    protected string BaseURL
    {
        get
        {
            try
            {
                return string.Format("http://{0}{1}",
                                     HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                                     (VirtualFolder.Equals("/")) ? string.Empty : VirtualFolder);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }
    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipo.SelectedIndex == 1)
        {
            divPJ.Visible = true;
            divPF.Visible = false;
        }
        if (ddlTipo.SelectedIndex == 0)
        {
            divPJ.Visible = false;
            divPF.Visible = true;
        }
    }
}
