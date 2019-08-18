using System;
using System.Net.Mail; // importe o namespace .Net.Mail
using Modelos;

public class EnvioEmails
{
    public static bool envioemails(EnvioEmailsVO Emails)
    {
        return envioemails(Emails, null);
    }

    public static bool envioemails(EnvioEmailsVO Emails, Attachment arquivo)
    {
        string nomeRemetente = Emails.nomeRemetente;
        string emailRemetente = Emails.emailRemetente;

        string emailDestinatario = Emails.emailDestinatario;
        string emailComCopia = Emails.emailComCopia;
        string emailComCopiaOculta = Emails.emailComCopiaOculta;

        string assuntoMensagem = Emails.assuntoMensagem;
        string conteudoMensagem = Emails.conteudoMensagem;


        DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

        //Cria objeto com dados do e-mail.
        MailMessage objEmail = new MailMessage();

        //Define o Campo From e ReplyTo do e-mail.
        if(String.IsNullOrEmpty(emailRemetente))
            objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + dado.referencia + ">");
        else
            objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

        //Define os destinatários do e-mail.
        objEmail.To.Add(emailDestinatario);

        //Enviar cópia para.
        //objEmail.CC.Add(emailComCopia);

        //Enviar cópia oculta para.
        //objEmail.Bcc.Add(emailComCopiaOculta);

        //Define a prioridade do e-mail.
        objEmail.Priority = System.Net.Mail.MailPriority.Normal;

        //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
        objEmail.IsBodyHtml = true;


        //Definir para onde a reply vai ser enviada.
        if (!String.IsNullOrEmpty(Emails.emailResposta))
        {
            objEmail.ReplyTo = (new MailAddress(Emails.emailResposta));
        }

        //Define título do e-mail.
        objEmail.Subject = assuntoMensagem;

        //Define o corpo do e-mail.
        objEmail.Body = conteudoMensagem;

        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


        // Caso queira enviar um arquivo anexo
        //Caminho do arquivo a ser enviado como anexo
        //string arquivo = Server.MapPath("arquivo.jpg");

        if (arquivo != null)
        {
            // Ou especifique o caminho manualmente
            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagemn

            objEmail.Attachments.Add(arquivo);
        }
        //Cria objeto com os dados do SMTP
        System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

        
        if (dado != null)
        {
            objSmtp.Host = dado.nome;
            objSmtp.Port = Convert.ToInt32(dado.valor);
            objSmtp.Credentials = new System.Net.NetworkCredential(dado.referencia, dado.ordem);
        }
        else
            throw new Exception("Problemas ocorreram na configuração de E-mail.");
        //Alocamos o endereço do host para enviar os e-mails, localhost(recomendado) 


        //Enviamos o e-mail através do método .send()
        try
        {
            objSmtp.Send(objEmail);
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();
            //anexo.Dispose();
        }
        return true;
    }
}
