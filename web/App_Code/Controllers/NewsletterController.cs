using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class NewsletterController : ApiController
{
    // POST api/<controller>
    [HttpPost]
    public string EnviarEmail(string txtEmailNew)
    {
        ControleValidacao.ContaTentativas("Newsletter");

        if (ControleValidacao.validaEmail(txtEmailNew))
        {
            EnvioEmailsVO envio = new EnvioEmailsVO();

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");
            DadoVO dadosContato = MetodosFE.getTela("Contato");
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
            return "E-mail inválido.";
        }
    }
}
