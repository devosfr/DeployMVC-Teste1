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

public partial class _recuperarSenha: System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Recuperar Senha" + " - " + Configuracoes.getSetting("nomeSite");
        if (!IsPostBack)
        {
            divMensagemErro.Visible = divMensagemSucesso.Visible = false;

            DadoVO dado = MetodosFE.getTela("Recuperar Senha - Texto");
            if (dado != null)
            {
                litTexto.Text = dado.descricao;
            }
        }
    }

    protected void btnRecuperar_Click(object sender, EventArgs e)
    {
        try
        {
            litErro.Text = litSucesso.Text = "";
            divMensagemErro.Visible = divMensagemSucesso.Visible = false;

            

            if (!String.IsNullOrEmpty(txtMail.Text))
            {
                bool envia = ControleLoginCliente.recuperaSenhaViaEmail(txtMail.Text);

                if (envia)
                {
                    litSucesso.Text = "Um e-mail com instruções será enviado dentro de momentos. Aguarde.";
                    divMensagemSucesso.Visible = true;
                    txtMail.Text = "";
                }
                else
                {
                    litErro.Text = "Ocorreram problemas no envio do e-mail. Tente mais tarde.";
                    divMensagemErro.Visible = true;
                }

            }
            else
            {
                litErro.Text = "Digite seu email para realizar a operação.";
                divMensagemErro.Visible = true;
            }
        }
        catch (Exception ex)
        {
            litErro.Text = ex.Message;
            divMensagemErro.Visible = true;
        }
    }
}
