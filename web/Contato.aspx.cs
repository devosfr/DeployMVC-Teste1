using System;
using System.Collections.Generic;
using System.Web.UI;
using Modelos;
using System.Linq;

public partial class _contato : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MaintainScrollPositionOnPostBack = true;

        litSucesso.Text = litErro.Text = "";

        if (!IsPostBack)
        {
            Page.Title = "Contato - " + Configuracoes.getSetting("NomeSite");

            DadoVO contato = null;
            contato = MetodosFE.getTela("Contato");

            if (contato != null)
            {
                litInfos.Text = contato.resumo;
                litMap.Text = contato.descricao;
            }
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        litErro.Text = "";
        litSucesso.Text = "";

        try
        {
            if (String.IsNullOrEmpty(txtNome.Text) || !ControleValidacao.validaEmail(txtMail.Text))
                throw new Exception("Campos Nome e E-mail obrigatórios.");


            EnvioEmailsVO envio = new EnvioEmailsVO();

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            if (dado != null)
            {
                DadoVO dadosContato = MetodosFE.getTela("Contato");
                string email = null;
                if (dadosContato != null)
                    if (!String.IsNullOrEmpty(dadosContato.nome))
                        email = dadosContato.nome;

                if (String.IsNullOrEmpty(email))
                    email = dado.referencia;

                envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
                envio.emailRemetente = dado.referencia;
                envio.emailDestinatario = email;
                envio.assuntoMensagem = "Contato do Site";
                envio.emailResposta = txtMail.Text;

                string mensagem = "";
                mensagem += "<br/>Nome: " + txtNome.Text;
                mensagem += "<br/>E-mail: " + txtMail.Text;
                mensagem += "<br/>Telefone: " + txtFone.Text;
                mensagem += "<br/>Comentários: " + txtComent.Text;

                envio.conteudoMensagem = mensagem;

                bool vrecebe = EnvioEmails.envioemails(envio);

                if (vrecebe)
                {
                    litSucesso.Text = "E-mail enviado com sucesso !";

                    LimparCampos();
                }
                else
                {
                    litErro.Text = "Ocorreram problemas no envio do e-mail. Tente mais tarde.";

                }
            }
            else
                throw new Exception("Problemas ocorreram na configuração de E-mail.");
        }
        catch (Exception ex)
        {
            
            litErro.Text = ex.Message;

        }
    }
    
    protected void LimparCampos()
    {
        try
        {
            txtComent.Text = "";
            txtMail.Text = "";
            txtNome.Text = "";
            txtFone.Text = "";
        }
        catch (ApplicationException er)
        {
            litSucesso.Text = "";
           
            litErro.Text = er.Message;
        }
    }

    
}
