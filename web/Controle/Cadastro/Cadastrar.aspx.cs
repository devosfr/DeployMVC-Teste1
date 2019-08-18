using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Categorias : System.Web.UI.Page
{
    private Repository<Cliente> repoCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Endereco> repoEndereco
    {
        get
        {
            return new Repository<Endereco>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<UsuarioJB> repoUsuarioJB
    {
        get
        {
            return new Repository<UsuarioJB>(NHibernateHelper.CurrentSession);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

        
            if (Session["emailrepetido"] != null)
            {
        
                MetodosFE.mostraMensagem("Este E-Mail já Existe em nossa Base de Dados");
                litSucesso.Text = "";
                Session["emailrepetido"] = null;
            }

            litErro.Text = "";

            txtID.Enabled = false;
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
                Pesquisar("id");
            }
            else
            {
                try
                {
                    Pesquisar("id");
                    //CarregarDropSegmentoFilho();  
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                }
                catch (Exception er)
                {
                    MetodosFE.mostraMensagem(er.Message);
                }
            }
        }
    }
    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    protected virtual void ticulo_Cadastro(object sender, EventArgs e)
    {
        Session["cadastrando"] = true;        
    }

    


    protected void Pesquisar(string ordenacao)
    {
        try
        {
            var pesquisa = repoCliente.All();


            int id = 0;
            if (!String.IsNullOrEmpty(txtBuscaID.Text))
            {
                id = Convert.ToInt32(txtBuscaID.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }
            string nome = null;
            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                nome = txtBuscaNome.Text;
                pesquisa = pesquisa.Where(x => x.Nome != null && x.Nome.ToLower().Contains(nome.ToLower()));
            }


            IList<Cliente> colecaoSegmento = pesquisa.OrderBy(ordenacao).ToList();

            gvSegmento.DataSourceID = String.Empty;
            gvSegmento.DataSource = colecaoSegmento;
            gvSegmento.DataBind();

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
            bool pj = false;
            bool pf = false;

            Cliente cliente = null;

            cliente = repoCliente.FindBy(Codigo);


            if (cliente != null)
            {


                if (String.IsNullOrEmpty(cliente.CNPJ))
                {
                    ddlTipo.Items.FindByValue("F").Selected = true;
                    pf = true;
                }
                else if (String.IsNullOrEmpty(cliente.CPF))
                {
                    ddlTipo.Items.FindByValue("J").Selected = true;
                    pj = true;
                }

                ddlTipo_SelectedIndexChanged(null, new EventArgs());

                //CAMPOS ESPECIFICOS
                if (pj)
                {
                    //carrega empresa
                    txtID.Text = cliente.Id.ToString();
                    txtCNPJ.Text = cliente.CNPJ;
                    txtInscricao.Text = cliente.InscricaoEstadual;
                    txtNome.Text = cliente.Nome;
                    txtEmail.Text = cliente.Email;
                    txtTelefone.Text = cliente.Telefone;

                }
                else if (pf)
                {

                    txtID.Text = cliente.Id.ToString();
                    txtNome.Text = cliente.Nome;
                    txtCPF.Text = cliente.CPF;
                    txtEmail.Text = cliente.Email;
                    txtTelefone.Text = cliente.Telefone;

                }



                ddlStatus.SelectedValue = cliente.Status;
                btnSalvar.Visible = false;
                btnPesquisar.Visible = false;
                btnAlterar.Visible = true;
                txtCPF.Enabled = false;
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void gvSegmento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            var usuario = repoUsuarioJB.All();
            var clientes = repoCliente.All();

            IList<Cliente> colecaoSegmentoCliente = clientes.ToList();
            IList<UsuarioJB> colecaoSegmentoUsuarios = usuario.ToList();

            string cpfClienteClicado = null;
            int idUsuario = 0;

            cpfClienteClicado = colecaoSegmentoCliente[e.RowIndex].CPF;

            for (int i = 0; i < colecaoSegmentoUsuarios.Count; i++)
            {

                //colecaoSegmentoUsuarios[i].cpf = colecaoSegmentoUsuarios[i].cpf.Replace("-", "");
                //colecaoSegmentoUsuarios[i].cpf = colecaoSegmentoUsuarios[i].cpf.Replace(".", "");

                //cpfClienteClicado = cpfClienteClicado.Replace("-", "");
                //cpfClienteClicado = cpfClienteClicado.Replace(".", "");

                if (colecaoSegmentoUsuarios[i].cpf == cpfClienteClicado)
                {
                    idUsuario = colecaoSegmentoUsuarios[i].Id;
                }

            }

            repoCliente.Delete(repoCliente.FindBy(Convert.ToInt32(gvSegmento.DataKeys[e.RowIndex].Value)));

            repoUsuarioJB.Delete(repoUsuarioJB.FindBy(Convert.ToInt32(idUsuario)));
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }

        //ControleLoginUsuario.Logout();
        gvSegmento.DataBind();
        Pesquisar("id");

        MetodosFE.mostraMensagem("Excluido com Sucesso", "sucesso");

    }

    protected void gvSegmento_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        Pesquisar(ordenacao);
    }
    protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Codigo = Convert.ToInt32(gvSegmento.DataKeys[e.NewEditIndex].Value);

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("Codigo", Codigo.ToString());
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "?" + nameValues.ToString();
        string urlFinal = url + updatedQueryString;
        e.Cancel = true;
        Response.Redirect(urlFinal, false);

        //Carregar();

    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar("id");
    }


    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        litErro.Text = "";
        //litSucesso.Text = "";
        Session["emailrepetido"] = null;
        if (IsPostBack)
        {

            IList<Cliente> clientes = null;
            clientes = repoCliente.All().ToList();

            for (int i = 0; i < clientes.Count; i++)
            {

                if (clientes[i].Email == txtEmail.Text)
                {

                    MetodosFE.mostraMensagem("Este E-Mail já Existe em nossa Base de Dados");
                    Session["emailrepetido"] = true;
                    litSucesso.Text = "";
                }

            }

            if (Session["emailrepetido"] == null)
            {
                try
                {
                    if (ddlTipo.SelectedIndex == 0 && txtNome.Text == "" || txtNome.Text == null || txtSenha.Text == "" || txtSenha.Text == null || txtCPF.Text == ""
                           || txtCPF.Text == null)
                    {
                        litErro.Text = "Preencha todos os Campos";
                    }
                    else
                    {
                        EnvioEmailsVO envio = new EnvioEmailsVO();

                        DadoVO dado = MetodosFE.getTela("Configurações de SMTP");
                        Cliente cliente = new Cliente();
                        UsuarioJB tblUsuarioJB = new UsuarioJB();
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


                            envio.emailResposta = txtEmail.Text;


                            string mensagem = "";

                            mensagem += "<br/><Strong>Olá " + txtNome.Text + "<br/>Seu cadastro no site Advoco foi concluído com sucesso!</Strong><br/>";
                            mensagem += "<br/><Strong>Seu login para acesso:</Strong>";
                            mensagem += "<br/><Strong>E-mail:</Strong>  " + txtEmail.Text;
                            mensagem += "<br/><Strong>Senha:</Strong>" + txtSenha.Text;
                            mensagem += "<br/><br/><Strong> Att,<br/> Equipe Advoco</Strong>";


                            envio.conteudoMensagem = mensagem;

                            tblUsuarioJB.email = txtEmail.Text;
                            tblUsuarioJB.senha = ControleLogin.GetSHA1Hash(txtSenha.Text);
                            tblUsuarioJB.nome = txtNome.Text;
                            tblUsuarioJB.telefone = txtTelefone.Text;
                            tblUsuarioJB.cpf = txtCPF.Text;
                            tblUsuarioJB.status = ddlStatus.SelectedValue;


                            if (txtCPF.Text != "")
                            {
                                tblUsuarioJB.cpf = txtCPF.Text;
                            }
                            if (txtCNPJ.Text != "")
                            {
                                tblUsuarioJB.cpf = txtCNPJ.Text;
                            }

                            cliente.Nome = txtNome.Text;
                            if (txtCPF.Text != "")
                            {
                                cliente.CPF = txtCPF.Text;
                            }
                            if (txtCNPJ.Text != "")
                            {
                                cliente.CPF = txtCNPJ.Text;
                                cliente.Genero = ddlGenero.SelectedItem.Text;
                            }


                            DateTime data = new DateTime();

                            if (DateTime.TryParse(txtNascimento.Text, out data))
                            {
                                cliente.DataNascimento = data;
                            }
                            else
                                cliente.DataNascimento = data;

                            cliente.Telefone = txtTelefone.Text;

                            cliente.Whatsapp = txtCelular.Text;

                            cliente.Email = txtEmail.Text;

                            cliente.Status = ddlStatus.SelectedValue;


                            bool vrecebe = EnvioEmails.envioemails(envio);

                            cliente.Senha = ControleLogin.GetSHA1Hash(txtSenha.Text);

                            ControleLoginCliente.CadastraCliente(cliente);
                            ControleLoginUsuario.CadastraUsuarioJB(tblUsuarioJB);

                            //litSucesso.Text = "Enviado com Suscesso!";
                            MetodosFE.mostraMensagem("Enviado com Suscesso!", "sucesso");
                            //divMensagemErro.Visible = false;
                            litErro.Text = "";


                            LimparCampos();

                        }
                        else
                            litErro.Text = "Problemas ocorreram na configuração de E-mail.";


                    }




                }
                catch (Exception ex)
                {

                    litErro.Text = ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPF();", true);
                }
            }


         




        }


    }


    protected void LimparCampos()
    {
        try
        {

            txtNome.Text = "";
            txtEmail.Text = "";
            txtCPF.Text = "";
            ddlGenero.Text = "";
            txtNascimento.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtSenha.Text = "";

            txtEmail.Text = "";
            txtCNPJ.Text = "";
            txtInscricao.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtSenha.Text = "";



        }
        catch (ApplicationException er)
        {
            //litSucesso.Text = "";
            litErro.Text = er.Message;
        }



    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            try
            {
                Cliente cliente = repoCliente.FindBy(Convert.ToInt32(txtID.Text));
                UsuarioJB usuario = repoUsuarioJB.FindBy(x => x.cpf == txtCPF.Text || x.cpf == txtCNPJ.Text && x.cpf != "");

                /* PARA VERIFICAR SE O E-MAIL JA EXISTE */
                IList<Cliente> clientes2 = null;
                clientes2 = repoCliente.All().ToList();

                if (ViewState["Codigo"] == null)
                {
                    for (int i = 0; i < clientes2.Count; i++)
                    {

                        if (clientes2[i].Email == txtEmail.Text)
                        {
                            MetodosFE.mostraMensagem("Este E-Mail já Existe em nossa Base de Dados");


                        }
                        else
                        {
                            cliente.Email = txtEmail.Text;
                            cliente.Nome = txtNome.Text;

                            cliente.Senha = ControleLoginCliente.GetSHA1Hash(txtSenha.Text);
                            cliente.CPF = txtCPF.Text.Replace('_', ' ').Trim();
                            cliente.Status = ddlStatus.SelectedValue;

                            usuario.nome = txtNome.Text;



                            usuario.email = txtEmail.Text;
                            usuario.senha = ControleLoginUsuario.GetSHA1Hash(txtSenha.Text);
                            usuario.cpf = txtCPF.Text.Replace('_', ' ').Trim();
                            usuario.status = ddlStatus.SelectedValue;

                            repoUsuarioJB.Update(usuario);

                            repoCliente.Update(cliente);

                        }

                    }
                }else
                {
                    cliente.Nome = txtNome.Text;
                    cliente.Email = txtEmail.Text;
                    cliente.Senha = ControleLoginCliente.GetSHA1Hash(txtSenha.Text);
                    cliente.CPF = txtCPF.Text.Replace('_', ' ').Trim();
                    cliente.Status = ddlStatus.SelectedValue;

                    usuario.nome = txtNome.Text;
                    usuario.email = txtEmail.Text;
                    usuario.senha = ControleLoginUsuario.GetSHA1Hash(txtSenha.Text);
                    usuario.cpf = txtCPF.Text.Replace('_', ' ').Trim();
                    usuario.status = ddlStatus.SelectedValue;

                    repoUsuarioJB.Update(usuario);

                    repoCliente.Update(cliente);


                }





                //ControleLoginCliente.alterarDados(cliente.Senha, txtEmail.Text, ddlGenero.Text, txtTelefone.Text,
                //  txtInscricao.Text, txtCelular.Text, txtCPF.Text, txtSenha.Text, cliente.Id);
                //ControleLoginUsuario.alterarDados(cliente.Senha, txtEmail.Text, ddlGenero.Text, txtTelefone.Text,
                //    txtSenha.Text, usuario.Id);


                try
                {
                    EnvioEmailsVO envio = new EnvioEmailsVO();

                    DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

                    UsuarioJB tblUsuarioJB = new UsuarioJB();
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


                        envio.emailResposta = txtEmail.Text;


                        string mensagem = "";

                        mensagem += "<br/><Strong>Assunto:</Strong> Mudança de Senha";
                        mensagem += "<br/><Strong>Site:</Strong> Sociedade de Reumatologia";
                        mensagem += "<br/><Strong>Nome:</Strong>  " + txtNome.Text;
                        mensagem += "<br/><Strong>E-mail:</Strong>  " + txtEmail.Text;

                        if (txtCPF.Text != "")
                        {
                            mensagem += "<br/><Strong>Cpf:</Strong>  " + txtCPF.Text;
                        }
                        if (txtCNPJ.Text != "")
                        {
                            mensagem += "<br/><Strong>Cnpj:</Strong>  " + txtCNPJ.Text;
                        }
                        mensagem += "<br/><Strong>Data de Nascimento:</Strong>  " + txtNascimento.Text;
                        mensagem += "<br/><Strong>Fone:</Strong>  " + txtTelefone.Text;
                        mensagem += "<br/><Strong>Whattsap:</Strong>  " + txtCelular.Text;
                        mensagem += "<br/><Strong>Senha:</Strong>  " + txtSenha.Text;

                        envio.conteudoMensagem = mensagem;


                        bool vrecebe = EnvioEmails.envioemails(envio);
                        MetodosFE.mostraMensagem("Alterado com Sucesso", "sucesso");
                        LimparCampos();


                    }



                }
                catch (Exception ex)
                {

                    litErro.Text = ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "mostraPF();", true);
                }

                this.Limpar();
            }
            catch (Exception er)
            {
                MetodosFE.mostraMensagem(er.Message);
            }
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
    #endregion

    protected void gvSegmento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvSegmento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSegmento.PageIndex = e.NewPageIndex;
            Pesquisar("id");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }
}
