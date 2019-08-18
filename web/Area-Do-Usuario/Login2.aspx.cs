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

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Login " + Configuracoes.getSetting("nomeSite");
        divMensagemErro.Visible = divMensagemSucesso.Visible = false;

        if (!IsPostBack)
        {
            if (ControleLoginCliente.GetClienteLogado() != null)
                Response.Redirect("~/area-do-usuario/meus-dados", false);
        }

        DadoVO dado = MetodosFE.getTela("Login - Texto");
        if (dado != null)
        {
            //litLogin.Text = dado.descricao;
        }
    }
    protected void btnEntrar_Click(object sender, EventArgs e)
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
            divMensagemErro.Visible = true;
            litErro.Text = ex.Message;
        }
    }
}
