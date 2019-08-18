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
        Page.Title = "Meus Dados" + " - " + Configuracoes.getSetting("nomeSite");

        ControleLoginCliente.statusLogin();

        if (!IsPostBack)
        {
            Cliente cliente = null;
            cliente = ControleLoginCliente.GetClienteLogado();
            if (cliente == null)
            {
                Response.Redirect("~/Area-Do-Usuario/Login.aspx");
            }
            else
            {
                if (!String.IsNullOrEmpty(cliente.CNPJ))
                {
                    divPJ.Visible = true;
                    txtRazaoSocial.Text = cliente.Nome;
                    txtCNPJ.Text = cliente.CNPJ;
                    txtInscricaoEstadual.Text = cliente.InscricaoEstadual;
                    txtFonePJ.Text = cliente.Telefone;
                    txtWhatsappPJ.Text = cliente.Whatsapp;
                    txtMailPJ.Text = cliente.Email;
                }
                else
                {
                    divPF.Visible = true;
                    txtNomePF.Text = cliente.Nome;
                    txtCPF.Text = cliente.CPF;
                    ddlGenero.Items.FindByText(cliente.Genero).Selected = true;
                    txtNascimento.Text = cliente.DataNascimento.ToShortDateString();
                    txtFonePF.Text = cliente.Telefone;
                    txtWhatsPF.Text = cliente.Whatsapp;
                    txtMailPF.Text = cliente.Email;
                }
            }
        }
    }

    protected void btnAtualizar_Click(object sender, EventArgs e)
    {
        divMensagemErro.Visible = false;
        litErro.Text = "";
        divMensagemSucesso.Visible = false;
        litSucesso.Text = "";

        try
        {
            Cliente cliente = null;
            cliente = ControleLoginCliente.GetClienteLogado();

            if (!String.IsNullOrEmpty(txtCNPJ.Text))
            {
                //atualizaPJ
                if (String.IsNullOrEmpty(txtCNPJ.Text))
                    throw new Exception("CNPJ inválido");
                cliente.CNPJ = txtCNPJ.Text;

                if (String.IsNullOrEmpty(txtRazaoSocial.Text))
                    throw new Exception("Razão Social inválida");
                cliente.Nome = txtRazaoSocial.Text;

                if (String.IsNullOrEmpty(txtInscricaoEstadual.Text))
                    throw new Exception("Inscrição Social inválida");
                cliente.InscricaoEstadual = txtInscricaoEstadual.Text;

                if (String.IsNullOrEmpty(txtFonePJ.Text))
                    throw new Exception("Telefone inválido");
                cliente.Telefone = txtFonePJ.Text;

                if (String.IsNullOrEmpty(txtWhatsappPJ.Text))
                    throw new Exception("Whatsapp inválido");
                cliente.Whatsapp = txtWhatsappPJ.Text;

                if (String.IsNullOrEmpty(txtMailPJ.Text))
                    throw new Exception("E-mail inválido");
                cliente.Email = txtMailPJ.Text;

                ControleLoginCliente.AtualizaCliente(cliente);

                divMensagemSucesso.Visible = true;
                litSucesso.Text = "Dados atualizados com sucesso";
            }
            else if (!String.IsNullOrEmpty(txtCPF.Text))
            {
                //atualizaPF
                if (String.IsNullOrEmpty(txtNomePF.Text))
                    throw new Exception("Nome inválido");
                cliente.Nome = txtNomePF.Text;

                if (String.IsNullOrEmpty(txtCPF.Text))
                    throw new Exception("CPF inválido");
                cliente.CPF = txtCPF.Text;

                if (ddlGenero.SelectedIndex == 0)
                    throw new Exception("Por favor selecione um gênero");
                else
                    cliente.Genero = ddlGenero.SelectedItem.Text;
                DateTime data = new DateTime();

                if (DateTime.TryParse(txtNascimento.Text, out data))
                {
                    cliente.DataNascimento = data;
                }
                else
                    throw new Exception("Data inválida");

                if (String.IsNullOrEmpty(txtFonePF.Text))
                    throw new Exception("Telefone inválido");
                cliente.Telefone = txtFonePF.Text;

                if (String.IsNullOrEmpty(txtWhatsPF.Text))
                    throw new Exception("Whatsapp inválido");
                cliente.Whatsapp = txtWhatsPF.Text;

                if (String.IsNullOrEmpty(txtMailPF.Text))
                    throw new Exception("E-mail inválido");
                cliente.Email = txtMailPF.Text;

                ControleLoginCliente.AtualizaCliente(cliente);

                divMensagemSucesso.Visible = true;
                litSucesso.Text = "Dados atualizados com sucesso";
            }
        }
        catch (Exception ex)
        {
            divMensagemErro.Visible = true;
            litErro.Text = ex.Message;
        }
    }
}
