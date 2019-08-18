using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for Newsletter
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Newsletter : System.Web.Services.WebService
{

    public Newsletter()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod(EnableSession = true)]
    public string enviaEmailNews(string txtEmailNew)
    {
        if (ControleValidacao.validaEmail(txtEmailNew) || !String.IsNullOrEmpty(txtEmailNew))
        {
            EnvioEmailsVO envio = new EnvioEmailsVO();

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");
            DadoVO dadosContato = MetodosFE.getTela("Contato - Newsletter");
            string email = dado.referencia;
            if (dadosContato != null)
                if (!String.IsNullOrEmpty(dadosContato.referencia))
                    email = dadosContato.referencia;


            envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = email;
            envio.assuntoMensagem = "E-mail Newsletter do Site";
            envio.conteudoMensagem += "<br/>E-mail: " + txtEmailNew;

            bool vrecebe = EnvioEmails.envioemails(envio);

            if (vrecebe)
            {
                return "Enviado com sucesso !";

            }
            else
            {
                return "Ocorreram problemas no envio do e-mail. Tente novamente.";
            }
        }
        else
        {
            return "Campo obrigatório: Email.";
        }
    }
}
