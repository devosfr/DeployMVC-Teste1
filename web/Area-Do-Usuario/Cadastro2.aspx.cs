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

public partial class _cadastro : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Registro " + Configuracoes.getSetting("nomeSite");
        //divMensagemErro.Visible = divMensagemSucesso.Visible = false;

        if (!IsPostBack)
        {
            if (ControleLoginCliente.GetClienteLogado() != null)
                Response.Redirect("~/area-do-usuario/meus-dados", false);
            else
            {
               DadoVO texto = null;
                texto = MetodosFE.getTela("Cadastro - Texto");
                if (texto != null)
                {
                    litTexto.Text = texto.descricao;
                    litTitulo.Text = texto.nome;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPF();", true);
            }
        }
    }
    protected void cadastroPF_ServerClick(object sender, EventArgs e)
    {
        litErro.Text = "";
        litSucesso.Text = "";
        divMensagemSucesso.Visible = false;
        divMensagemErro.Visible = false;

        try
        {
            Cliente cliente = new Cliente();

            if (!String.IsNullOrEmpty(validaPF()))
            {
                throw new Exception("Confira os seguintes campos " + validaPF());
            }
            else
            {
                cliente.Nome = txtNomePF.Text;
                cliente.CPF = txtCPF.Text;
                cliente.Genero = ddlGenero.SelectedItem.Text;

                DateTime data = new DateTime();

                if (DateTime.TryParse(txtNascimento.Text, out data))
                {
                    cliente.DataNascimento = data;
                }
                else
                    cliente.DataNascimento = data;

                cliente.Telefone = txtFonePF.Text;

                cliente.Whatsapp = txtWhatsPF.Text;

                cliente.Email = txtMailPF.Text;

                cliente.Senha = ControleLogin.GetSHA1Hash(txtSenhaPF.Text);

                ControleLoginCliente.CadastraCliente(cliente);

                ControleLoginCliente.login(txtMailPF.Text, txtSenhaPF.Text);
            }
        }
        catch (Exception ex)
        {
            divMensagemErro.Visible = true;
            litErro.Text = ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPF();", true);
        }
    }

    protected void cadastroPJ_ServerClick(object sender, EventArgs e)
    {
        litErro.Text = "";
        litSucesso.Text = "";
        divMensagemSucesso.Visible = false;
        divMensagemErro.Visible = false;

        try
        {
            Cliente cliente = new Cliente();

            if (!String.IsNullOrEmpty(validaPJ()))
            {
                throw new Exception("Confira os seguintes campos " + validaPJ());
            }
            else
            {
                cliente.Nome = txtRazaoSocial.Text;

                cliente.InscricaoEstadual = txtInscricaoEstadual.Text;

                cliente.CNPJ = txtCNPJ.Text;

                cliente.Telefone = txtFonePJ.Text;

                cliente.Whatsapp = txtWhatsappPJ.Text;

                cliente.Email = txtMailPJ.Text;

                cliente.Senha = ControleLogin.GetSHA1Hash(txtSenhaPJ.Text);

                ControleLoginCliente.CadastraCliente(cliente);

                ControleLoginCliente.login(txtMailPJ.Text, txtSenhaPJ.Text);
            }
        }
        catch (Exception ex)
        {
            divMensagemErro.Visible = true;
            litErro.Text = ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPJ();", true);
        }
    }

    #region validações
    public string validaPF()
    {
        string error = "";

        if (String.IsNullOrEmpty(txtNomePF.Text))
            error += "Nome";

        if (String.IsNullOrEmpty(txtCPF.Text) || !ControleValidacao.validaCPF(txtCPF.Text))
            error += ", CPF";

        if (String.IsNullOrEmpty(txtFonePF.Text))
            error += ", Telefone";

        if (String.IsNullOrEmpty(txtMailPF.Text) || !ControleValidacao.validaEmail(txtMailPF.Text))
            error += ", E-mail";

        if (String.IsNullOrEmpty(txtSenhaPF.Text) || String.IsNullOrEmpty(txtConfirmaPF.Text) || txtSenhaPF.Text.Length < 6)
            error += ", Senha (Min. 6 caractéres)";

        return error;
    }

    public string validaPJ()
    {
        string error = "";

        if (String.IsNullOrEmpty(txtRazaoSocial.Text))
            error += "Razão Social";

        if (String.IsNullOrEmpty(txtCNPJ.Text) || !ControleValidacao.validaCNPJ(txtCNPJ.Text))
            error += ", CNPJ";

        if (String.IsNullOrEmpty(txtInscricaoEstadual.Text))
            error += ", Inscrição Estadual";

        if (String.IsNullOrEmpty(txtFonePJ.Text))
            error += ", Telefone";

        if (String.IsNullOrEmpty(txtSenhaPJ.Text) || String.IsNullOrEmpty(txtConfirmaPJ.Text) || txtSenhaPJ.Text.Length < 6)
            error += ", Senha (Min. 6 caractéres)";

        return error;
    }
    #endregion
}

