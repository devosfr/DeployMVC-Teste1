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


    private Repository<UsuarioJB> repoUsuarioJB
    {
        get
        {
            return new Repository<UsuarioJB>(NHibernateHelper.CurrentSession);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Meus Dados" + " - " + Configuracoes.getSetting("nomeSite");

        ControleLoginCliente.statusLogin();

        if (!IsPostBack)
        {

            Cliente cliente = ControleLoginCliente.GetClienteLogado();


            //UsuarioJB usuario = ControleLoginUsuario.GetUsuarioLogado();

            if (cliente == null)
            {
                Response.Redirect("~/Area-Do-Usuario/Login.aspx");
            }
            else
            {
                if (!String.IsNullOrEmpty(cliente.CNPJ))
                {
                    //divPJ.Visible = true;
                    txtRazaoSocial.Text = cliente.Nome;
                    txtCNPJ.Text = cliente.CNPJ;
                    txtInscricaoEstadual.Text = cliente.InscricaoEstadual;
                    txtFonePJ.Text = cliente.Telefone;
                    txtWhatsappPJ.Text = cliente.Whatsapp;
                    txtMailPJ.Text = cliente.Email;

                    //txtRazaoSocial.Text = usuario.nome;
                    //txtCNPJ.Text = usuario.cpf;
                    //txtFonePJ.Text = usuario.telefone;
                    //txtMailPJ.Text = usuario.email;


                }
                else
                {
                    //divPF.Visible = true;
                    txtNomePF.Text = cliente.Nome;
                    txtCPF.Text = cliente.CPF;
                    //ddlGenero.Items.FindByText(cliente.Genero).Selected = true;
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

            UsuarioJB usuario = ControleLoginUsuario.GetUsuarioLogado();
            cliente = ControleLoginCliente.GetClienteLogado();

            EnvioEmailsVO envio = new EnvioEmailsVO();

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            if (!String.IsNullOrEmpty(txtCNPJ.Text) && dado != null)
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

                //usuario
                if (String.IsNullOrEmpty(txtCNPJ.Text))
                    throw new Exception("CNPJ inválido");
                usuario.cpf = txtCNPJ.Text;

                if (String.IsNullOrEmpty(txtRazaoSocial.Text))
                    throw new Exception("Razão Social inválida");
                usuario.nome = txtRazaoSocial.Text;

                if (String.IsNullOrEmpty(txtFonePJ.Text))
                    throw new Exception("Telefone inválido");
                usuario.telefone = txtFonePJ.Text;


                if (String.IsNullOrEmpty(txtMailPJ.Text))
                    throw new Exception("E-mail inválido");
                usuario.email = txtMailPJ.Text;



                ControleLoginCliente.AtualizaCliente(cliente);
                ControleLoginUsuario.AtualizaUsuarioJB(usuario);

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
                {
                    throw new Exception("Por favor selecione um gênero");
                    cadastroPJ.Visible = false;
                }
                else
                {
                    cliente.Genero = ddlGenero.SelectedItem.Text;
                }

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

                //usuario
                if (String.IsNullOrEmpty(txtNomePF.Text))
                    throw new Exception("Nome inválido");
                usuario.nome = txtNomePF.Text;

                if (String.IsNullOrEmpty(txtCPF.Text))
                    throw new Exception("CPF inválido");
                usuario.cpf = txtCPF.Text;

                if (ddlGenero.SelectedIndex == 0)
                    throw new Exception("Por favor selecione um gênero");
                else
                    usuario.sexo = ddlGenero.SelectedItem.Text;
                DateTime dataHoje = new DateTime();

                if (DateTime.TryParse(txtNascimento.Text, out dataHoje))
                {
                    usuario.dataInicio = dataHoje;
                }
                else
                    throw new Exception("Data inválida");

                if (String.IsNullOrEmpty(txtFonePF.Text))
                    throw new Exception("Telefone inválido");
                usuario.telefone = txtFonePF.Text;


                if (String.IsNullOrEmpty(txtMailPF.Text))
                    throw new Exception("E-mail inválido");
                usuario.email = txtMailPF.Text;


                DadoVO dadosContato = MetodosFE.getTela("Contato");
                string email = null;
                if (dadosContato != null)
                    if (!String.IsNullOrEmpty(dadosContato.referencia))
                        email = dadosContato.referencia;

                if (String.IsNullOrEmpty(email))
                    email = dado.referencia;

                envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
                envio.emailRemetente = dado.referencia;
                envio.emailDestinatario = email;

                envio.assuntoMensagem = "Contato do Site";

                envio.emailResposta = txtMailPF.Text;


                string mensagem = "";

                if (String.IsNullOrEmpty(txtNomePF.Text))
                {
                    mensagem += "<br/><Strong>Assunto:</Strong> ATUALIZAÇÃO DE CADASTRO";
                    mensagem += "<br/><Strong>Site:</Strong> Savel";
                    mensagem += "<br/><Strong>Nome:</Strong>  " + txtRazaoSocial.Text;
                    mensagem += "<br/><Strong>E-mail:</Strong>  " + txtMailPJ.Text;
                    mensagem += "<br/><Strong>Cnpj:</Strong>  " + txtCNPJ.Text;
                    mensagem += "<br/><Strong>Fone:</Strong>  " + txtFonePJ.Text;
                    mensagem += "<br/><Strong>Whattsap:</Strong>  " + txtWhatsappPJ.Text;
                    mensagem += "<br/><Strong>Senha:</Strong>  " + txtSenhaPJ.Text;

                }

                if (String.IsNullOrEmpty(txtRazaoSocial.Text))
                {
                    mensagem += "<br/><Strong>Assunto:</Strong> ATUALIZAÇÃO DE CADASTRO";
                    mensagem += "<br/><Strong>Site:</Strong> Savel";
                    mensagem += "<br/><Strong>Nome:</Strong>  " + txtNomePF.Text;
                    mensagem += "<br/><Strong>E-mail:</Strong>  " + txtMailPF.Text;

                    if (txtCPF.Text != "")
                    {
                        mensagem += "<br/><Strong>Cpf:</Strong>  " + txtCPF.Text;
                    }
                    if (txtCNPJ.Text != "")
                    {
                        mensagem += "<br/><Strong>Cnpj:</Strong>  " + txtCNPJ.Text;
                    }
                    mensagem += "<br/><Strong>Data de Nascimento:</Strong>  " + txtNascimento.Text;
                    mensagem += "<br/><Strong>Fone:</Strong>  " + txtFonePF.Text;
                    mensagem += "<br/><Strong>Whattsap:</Strong>  " + txtWhatsPF.Text;
                    mensagem += "<br/><Strong>Senha:</Strong>  " + txtSenhaPF.Text;

                }


                envio.conteudoMensagem = mensagem;


                bool vrecebe = EnvioEmails.envioemails(envio);

                ControleLoginCliente.AtualizaCliente(cliente);
                ControleLoginUsuario.AtualizaUsuarioJB(usuario);

                divMensagemSucesso.Visible = true;
                litSucesso.Text = "Dados atualizados com sucesso";
                emailEnviado.Value = "true";
                cadastroPJ.Visible = false;

                //Response.Redirect("~/Area-Do-Usuario/meus-dados", false);
            }
        }
        catch (Exception ex)
        {
            divMensagemErro.Visible = true;
            litErro.Text = ex.Message;
        }
    }
}
