using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Modelos;

public partial class _login : System.Web.UI.Page
{

    private static Repository<Cliente> RepositorioCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }



    private static Repository<UsuarioJB> RepositorioUsuarioJB
    {
        get
        {
            return new Repository<UsuarioJB>(NHibernateHelper.CurrentSession);
        }
    }


    public static int idDoCliente;

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Login - " + Configuracoes.getSetting("NomeSite");

        

        if (!IsPostBack)
        {

            mensagemSucesso.Visible = false;
            mensagemErro.Visible = false;
        }
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string login = txtMail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if ((!string.IsNullOrEmpty(senha)) || (!string.IsNullOrEmpty(login)))
            {
                ControleLoginCliente.login(login, senha);            
                
            }
            else
            {
                throw new Exception("É preciso digitar o E-mail ou Login e a Senha");
            }
        }
        catch (Exception ex)
        {
            mensagemSucesso.Visible = false;
            litSucesso.Text = "";
            mensagemErro.Visible = true;
            litErro.Text = ex.Message;
        }

        try
        {
            string email, senha;

            email = txtMail.Text;
            senha = txtSenha.Text;

            UsuarioJB usuario = RepositorioUsuarioJB.FindBy(x => x.email == email && x.senha == senha);
            Cliente getCliente = RepositorioCliente.FindBy(x => x.Email == email && x.Senha == senha);

            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(senha))
            {

                try
                {
                    
                    ControleLoginCliente.login(email, senha);
                    
                    mensagemSucesso.Visible = true;
                    litSucesso.Text = "Login Efetuado com Sucesso!";
                    mensagemErro.Visible = false;
                    Response.Redirect("~/Area-Do-Usuario/meus-dados", false);
                }
                catch (Exception ex)
                {
                    mensagemErro.Visible = true;
                    litErro.Text = ex.Message;
                }
            }
            else
            {
                mensagemErro.Visible = true;
                litErro.Text = "Erro ao Tentar Fazer Login";
            }
        }

        catch (Exception)
        {


        }

    }

    
    protected void validaCampos()
    {
        string erros = "";

        if (!ControleLoginUsuario.validaEmail(txtMail.Text))
            mensagemErro.Visible = true;
            erros += "E-mail <br/>";

        if (string.IsNullOrEmpty(txtSenha.Text))
            mensagemErro.Visible = true;
            erros += "Senha <br/>";

        if (!string.IsNullOrEmpty(erros))
        {
            mensagemErro.Visible = true;
            throw new Exception(litErro.Text = "Verifique os seguintes campos: <br/>" + erros);
        }
    }
    protected void BtnCadastro_Click(object sender, EventArgs e)
    {
        Response.Redirect(MetodosFE.BaseURL + "/Area-Do-Usuario/Cadastro.aspx");

    }
















}
