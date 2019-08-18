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

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Alterar Senha" + " - " + Configuracoes.getSetting("nomeSite");
        ControleLoginCliente.statusLogin();

        if (!IsPostBack)
        {
            divMensagemSucesso.Visible = divMensagemErro.Visible = false;
        }
    }

    protected void btnAlterarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagemErro.Visible = divMensagemSucesso.Visible = false;

            divMensagemErro.Visible = true;
            if (String.IsNullOrEmpty(txtSenhaAtual.Text))
                litErro.Text = "Preencha o campo Senha Atual para realizar a operação.";
            else if (String.IsNullOrEmpty(txtSenhaNova.Text))
                litErro.Text = "Preencha o campo Senha Nova para realizar a operação.";
            else if (String.IsNullOrEmpty(txtConfirmarSenha.Text))
                litErro.Text = "Preencha o campo Confirmar Senha com a nova senha para confirmação";
            else if (!txtConfirmarSenha.Text.Equals(txtSenhaNova.Text))
                litErro.Text = "A senha nova está diferente no campo Confirmar Senha.";
            else if (ControleLoginCliente.GetClienteLogado() == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                ControleLoginCliente.alterarSenha(txtSenhaAtual.Text, txtSenhaNova.Text);
                divMensagemErro.Visible = false;
                litSucesso.Text = "Senha alterada com sucesso.";
                divMensagemSucesso.Visible = true;
            }
        }
        catch (Exception ex)
        {
            litErro.Text = ex.Message;
            divMensagemErro.Visible = true;
        }
    }
}
