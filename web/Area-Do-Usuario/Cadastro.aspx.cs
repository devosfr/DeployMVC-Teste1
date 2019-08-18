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

    protected Repository<Cliente> RepositorioCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Registro " + Configuracoes.getSetting("nomeSite");
        divMensagemErro.Visible = divMensagemSucesso.Visible = false;


        if (Session["emailrepetido"] != null)
        {
            divMensagemErro.Visible = true;
            litErro.Text = "Este E-Mail já Existe em nossa Base de Dados";
            Session["emailrepetido"] = null;
        }


        if (!IsPostBack)
        {
            LimparCampos();

            DadoVO CadastroConteudo = null;
            CadastroConteudo = MetodosFE.getTela("Cadastro Conteudo");
            if (CadastroConteudo != null)
            {
                litTexto.Text = CadastroConteudo.descricao;
                litTitulo.Text = CadastroConteudo.nome;
            }

            Cliente cliente = ControleLoginCliente.GetClienteLogado();
            if (cliente != null)
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
        Session["cadastrado"] = null;

        litErro.Text = "";
        litSucesso.Text = "";
        divMensagemSucesso.Visible = false;
        divMensagemErro.Visible = false;


        if (IsPostBack)
        {
            IList<Cliente> clientes = null;
            clientes = RepositorioCliente.All().ToList();           

            for(int i = 0; i < clientes.Count; i++)
            {

                if (clientes[i].Email == txtMailPF.Text)
                {
                    divMensagemErro.Visible = true;
                    litErro.Text = "Este E-Mail já Existe em nossa Base de Dados";
                    Session["emailrepetido"] = true;
                    Response.Redirect("~/Area-Do-Usuario/Cadastro.aspx");
                }

            }


            try
            {
                Cliente cliente = new Cliente();
                UsuarioJB tblUsuarioJB = new UsuarioJB();

                EnvioEmailsVO envio = new EnvioEmailsVO();

                DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

                if (!String.IsNullOrEmpty(validaPF()))
                {
                    throw new Exception("Confira os seguintes campos " + validaPF());
                }
                else
                {
                    if (dado != null)
                    {
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

                        mensagem += "<br/><Strong>Olá " + txtNomePF.Text + "<br/>Seu cadastro no site da Savel foi concluído com sucesso!</Strong><br/>";
                        mensagem += "<br/><Strong>Seu login para acesso:</Strong>";
                        mensagem += "<br/><Strong>E-mail:</Strong>  " + txtMailPF.Text;
                        mensagem += "<br/><Strong>Senha:</Strong>" + txtSenhaPF.Text;
                        mensagem += "<br/><br/><Strong> Att,<br/> Equipe Savel</Strong>";


                        envio.conteudoMensagem = mensagem;

                        tblUsuarioJB.email = txtMailPF.Text;
                        tblUsuarioJB.senha = ControleLogin.GetSHA1Hash(txtSenhaPF.Text);
                        tblUsuarioJB.nome = txtNomePF.Text;
                        tblUsuarioJB.telefone = txtFonePF.Text;
                        tblUsuarioJB.cpf = txtCPF.Text;
                        tblUsuarioJB.status = "AT";

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

                        cliente.Status = "AT";

                        string EmailArmaz = txtMailPF.Text;
                        string senhaArmaz = txtSenhaPF.Text;


                        ControleLoginCliente.CadastraCliente(cliente);

                        ControleLoginUsuario.CadastraUsuarioJB(tblUsuarioJB);


                        //ControleLoginUsuario.login(EmailArmaz, senhaArmaz);
                        //ControleLoginCliente.login(EmailArmaz, senhaArmaz);

                        Session["cadastrado"] = true;

                        divMensagemSucesso.Visible = true;
                        litSucesso.Text = "Cadastrado Sucesso!";
                        emailEnviado.Value = "true";
                        litErro.Text = "";
                        LimparCampos();

                        //Response.Redirect("~/Area-Do-Usuario/Login.aspx");


                    }

                }
            }
            catch (Exception ex)
            {
                divMensagemErro.Visible = true;
                litErro.Text = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPF();", true);
            }
        }


    }

    protected void cadastroPJ_ServerClick(object sender, EventArgs e)
    {

        Session["cadastrado"] = null;

        litErro.Text = "";
        litSucesso.Text = "";
        divMensagemSucesso.Visible = false;
        divMensagemErro.Visible = false;


        if (IsPostBack)
        {


            IList<Cliente> clientes = null;
            clientes = RepositorioCliente.All().ToList();

            for (int i = 0; i < clientes.Count; i++)
            {

                if (clientes[i].Email == txtMailPJ.Text)
                {
                    divMensagemErro.Visible = true;
                    litErro.Text = "Este E-Mail já Existe em nossa Base de Dados";
                    Session["emailrepetido"] = true;
                    Response.Redirect("~/Area-Do-Usuario/Cadastro.aspx");
                }

            }




            try
            {
                Cliente cliente = new Cliente();
                UsuarioJB tblUsuarioJB = new UsuarioJB();
                if (!String.IsNullOrEmpty(validaPJ()))
                {
                    throw new Exception("Confira os seguintes campos " + validaPJ());
                }
                else
                {

                    EnvioEmailsVO envio = new EnvioEmailsVO();

                    DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

                    if (dado != null)
                    {

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


                        envio.emailResposta = txtMailPJ.Text;

                        string mensagem = "";

                        mensagem += "<br/><Strong>Olá " + txtRazaoSocial.Text + "<br/>Seu cadastro no site da Savel foi concluído com sucesso!</Strong><br/>";
                        mensagem += "<br/><Strong>Seu login para acesso:</Strong>";
                        mensagem += "<br/><Strong>E-mail:</Strong>  " + txtMailPJ.Text;
                        mensagem += "<br/><Strong>Senha:</Strong>" + txtSenhaPJ.Text;
                        mensagem += "<br/><br/><Strong> Att,<br/> Equipe Advoco</Strong>";


                        envio.conteudoMensagem = mensagem;


                        cliente.Nome = txtRazaoSocial.Text;

                        cliente.InscricaoEstadual = txtInscricaoEstadual.Text;

                        cliente.CNPJ = txtCNPJ.Text;

                        cliente.Telefone = txtFonePJ.Text;

                        //cliente.Whatsapp = txtWhatsappPJ.Text;

                        cliente.Email = txtMailPJ.Text;

                        cliente.DataCadastro = DateTime.Today;

                        cliente.Status = "AT";

                        cliente.Senha = ControleLogin.GetSHA1Hash(txtSenhaPJ.Text);

                        tblUsuarioJB.nome = txtRazaoSocial.Text;
                        tblUsuarioJB.email = txtMailPJ.Text;
                        tblUsuarioJB.senha = ControleLogin.GetSHA1Hash(txtSenhaPJ.Text);
                        tblUsuarioJB.telefone = txtFonePJ.Text;
                        tblUsuarioJB.cpf = txtCNPJ.Text;
                        tblUsuarioJB.status = "AT";



                        ControleLoginCliente.CadastraCliente(cliente);
                        ControleLoginUsuario.CadastraUsuarioJB(tblUsuarioJB);

                        //ControleLoginCliente.login(txtMailPJ.Text, txtSenhaPJ.Text);
                        //ControleLoginUsuario.login(txtMailPJ.Text, txtSenhaPJ.Text);

                        Session["cadastrado"] = true;

                        divMensagemSucesso.Visible = true;
                        litSucesso.Text = "Cadastrado com Sucesso!";
                        emailEnviado.Value = "true";
                        litErro.Text = "";
                        LimparCampos();

                        //Response.Redirect("~/Area-Do-Usuario/Login.aspx");

                    }
                }
            }
            catch (Exception ex)
            {
                divMensagemErro.Visible = true;
                litErro.Text = ex.Message.ToString();
                //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPJ();", true);
            }

        }


    }

    protected void LimparCampos()
    {
        try
        {

            txtNomePF.Text = "";
            txtMailPF.Text = "";
            txtCPF.Text = "";
            ddlGenero.Text = "";
            txtNascimento.Text = "";
            txtFonePF.Text = "";
            txtWhatsPF.Text = "";
            txtSenhaPF.Text = "";

            txtRazaoSocial.Text = "";
            txtMailPJ.Text = "";
            txtCNPJ.Text = "";
            txtInscricaoEstadual.Text = "";
            txtFonePJ.Text = "";
            //txtWhatsappPJ.Text = "";
            txtDataCadastroDJ.Text = "";
            txtSenhaPJ.Text = "";



        }
        catch (ApplicationException er)
        {
            litSucesso.Text = "";
            litErro.Text = er.Message;
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
        //return null;
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

