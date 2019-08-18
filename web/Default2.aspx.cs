using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Modelos;
using NHibernate.Linq;
using System.Web;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DadoVO dado = null;

        dado = MetodosFE.getTela("Rodapé");
        if (dado != null)
        {
            litFone2.Text = dado.nome;
            litWhats2.Text = dado.referencia;
            litfoneTopo.Text = dado.referencia;
            litInfoRodapeBaixo.Text = dado.descricao;
            string fone = dado.referencia.Replace("-", "").Replace(")", "").Replace("(", "").Replace("+", "").Replace(".", "");
            linkWhats2.HRef = dado.keywords;
            lkfonewhats.HRef = dado.keywords;
        }

        IList<DadoVO> slider = null;
        slider = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Slider Land") && x.visivel).ToList();
        if (slider != null && slider.Count > 0)
        {
            repSlider.DataSource = slider.OrderBy(x => MetodosFE.verificaOrdem(x.ordem));
            repSlider.DataBind();
        }

        dado = MetodosFE.getTela("Informações Land");
        if (dado != null)
        {
            litTitulo.Text = dado.nome;
            litTexto.Text = dado.descricao;
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
                DadoVO dadosContato = MetodosFE.getTela("Informações Land");
                string email = null;
                if (dadosContato != null)
                    if (!String.IsNullOrEmpty(dadosContato.referencia))
                        email = dadosContato.referencia;

                if (String.IsNullOrEmpty(email))
                    email = dado.referencia;

                envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
                envio.emailRemetente = dado.referencia;
                envio.emailDestinatario = email;
                envio.assuntoMensagem = "Contato Landpage do Site";
                envio.emailResposta = txtMail.Text;

                string mensagem = "";
                mensagem += "<br/>Nome: " + txtNome.Text;
                mensagem += "<br/>E-mail: " + txtMail.Text;

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
            txtMail.Text = "";
            txtNome.Text = "";
        }
        catch (ApplicationException er)
        {
            litSucesso.Text = "";

            litErro.Text = er.Message;
        }
    }
}