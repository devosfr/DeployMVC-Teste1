<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Net.Http" %>
<%@ Import Namespace="NHibernate.Context" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Modelos" %>

<script RunAt="server">

    protected void Application_Start(object sender, EventArgs e)
    {
        RegisterRoutes(RouteTable.Routes);
    }

    public static void RegisterRoutes(RouteCollection routes)
    {

        routes.Ignore("{resource}.axd/{*pathInfo}");

        routes.MapHttpRoute(
   name: "ActionApi",
   routeTemplate: "api2/{controller}/{action}/{id}",
   defaults: new { id = System.Web.Http.RouteParameter.Optional }
);

        routes.MapHttpRoute(
       name: "DefaultApi",
       routeTemplate: "api/{controller}/{id}",
       defaults: new { id = System.Web.Http.RouteParameter.Optional }

   );



        routes.MapPageRoute("Gerenciador",
    "Controle/Cadastro/{Grupo}/{Pagina}/",
    "~/Controle/Cadastro/Telas.aspx", true);
        routes.MapPageRoute("",
    "Controle/Cadastro/{Grupo}/{Pagina}",
    "~/Controle/Cadastro/Telas.aspx", true);

        routes.MapPageRoute("",
            "Dicas/{Chave}",
            "~/Dicas.aspx", true);

        routes.MapPageRoute("",
            "Ajuda/{Chave}",
            "~/Ajuda.aspx", true);

        routes.MapPageRoute("",
        "Produtos/{Chave}",
        "~/Produtos.aspx", true);

        routes.MapPageRoute("",
        "Carrinho/{Chave}",
        "~/Carrinho.aspx", true);

        routes.MapPageRoute("",
       "blog/{Chave}",
       "~/blog.aspx", true);

        routes.MapPageRoute("",
"detalhe-blog/{Chave}",
"~/detalhe-blog.aspx", true);

        routes.MapPageRoute("",
        "Produtos2/{Chave}",
        "~/Produtos2.aspx", true);

        routes.MapPageRoute("",
        "Produtos/{Segmento}/{Subsegmento}",
        "~/Produtos.aspx", true);


        routes.MapPageRoute("",
            "Produto/{Chave}",
            "~/Produto.aspx", true);

        routes.MapPageRoute("",
    "Compra-Deposito-Finalizada/{Chave}",
    "~/TextoDeposito.aspx", true);

        routes.MapPageRoute("",
          "noticia/{Chave}",
          "~/noticia.aspx", true);


        routes.MapPageRoute("",
            "Servico/{Chave}",
            "~/Servicos.aspx", true);

        routes.MapPageRoute("",
    "Texto/{Chave}",
    "~/Texto.aspx", true);

        routes.MapPageRoute("",
            "Institucional/{Chave}",
            "~/Texto.aspx", true);

        routes.MapPageRoute("",
    "area-do-usuario/Solicitacao/{ID}",
    "~/area-do-usuario/Solicitacao.aspx", true);

        routes.MapPageRoute("",
            "area-do-usuario/{Pagina}",
            "~/area-do-usuario/{Pagina}.aspx", true);

        routes.MapPageRoute("",
            "{Pagina}/",
            "~/{Pagina}.aspx", true);

        routes.MapPageRoute("",
            "{Pagina}",
            "~/{Pagina}.aspx", true);
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        var url = Request.Url.AbsolutePath.ToLower();


        if (EmManutencao())
        {
            List<string> pastasLivres = new List<string>();
            pastasLivres.Add("/imagenshq/");
            pastasLivres.Add("/imagenslq/");
            pastasLivres.Add("/userfiles/");
            pastasLivres.Add("/webservices/");
            pastasLivres.Add("/controle/");
            pastasLivres.Add("/js/");
            pastasLivres.Add("/assets/");
            pastasLivres.Add("/app_code/");
            pastasLivres.Add("/ckeditor/");

            if (!pastasLivres.Any(x => url.ToLower().Contains(x)))
            {
                HttpContext.Current.Response.Redirect("~/index.html");
            }
        }

        List<String> extensoes = new List<string>();
        extensoes.Add(".js");
        extensoes.Add(".css");
        extensoes.Add(".img");
        extensoes.Add(".png");
        extensoes.Add(".jpeg");
        extensoes.Add(".pdf");
        //extensoes.Add("");
        string extensao = System.IO.Path.GetExtension(url);

        List<string> pastas = new List<string>();
        pastas.Add("/imagenshq/");
        pastas.Add("/imagenslq/");
        pastas.Add("/userfiles/");

        if (!extensoes.Contains(extensao) && !pastas.Any(x => url.ToLower().Contains(x)))
        {
            if (CurrentSession == null)
                CurrentSession = NHibernateHelper.OpenSession();
        }
        //}

    }

    protected void Application_EndRequest(object sender, EventArgs e)
    {

        if (CurrentSession != null)
        {
            CurrentSession.Dispose();
            System.Diagnostics.Debug.WriteLine("Session Disposed");
        }
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    public static ISession CurrentSession
    {
        get { return (ISession)HttpContext.Current.Items["current.session"]; }
        set { HttpContext.Current.Items["current.session"] = value; }
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        MailMessage msg = new MailMessage();
        HttpContext ctx = HttpContext.Current;

        HttpException lastErrorWrapper = Server.GetLastError() as HttpException;

        Exception lastError = lastErrorWrapper;
        if (lastErrorWrapper.InnerException != null)
            lastError = lastErrorWrapper.InnerException;

        string lastErrorTypeName = lastError.GetType().ToString();
        string lastErrorMessage = lastError.Message;
        string lastErrorStackTrace = lastError.StackTrace;

        if (lastErrorTypeName != "System.Web.HttpException")
        {
            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            msg.To.Add(new MailAddress(Configuracoes.getSetting("EmailErro")));
            msg.From = new MailAddress(dado.referencia, "Controle de Erro");
            msg.Subject = "EXCEPTION: " + Configuracoes.getSetting("NomeSite") + " - " + lastErrorTypeName;
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;
            msg.Body = string.Format(@"
  <h1>ONE DAMN BIG Exception Appeared!!</h1>
  <table cellpadding=""5"" cellspacing=""0"" border=""1"">
  <tr>
  <td text-align: right;font-weight: bold"">URL:</td>
  <td>{0}</td>
  </tr>
  <tr>
  <td text-align: right;font-weight: bold"">User:</td>
  <td>{1}</td>
  </tr>
  <tr>
  <td text-align: right;font-weight: bold"">Exception Type:</td>
  <td>{2}</td>
  </tr>
  <tr>
  <td text-align: right;font-weight: bold"">Message:</td>
  <td>{3}</td>
  </tr>
  <tr>
  <td text-align: right;font-weight: bold"">Stack Trace:</td>
  <td>{4}</td>
  </tr>
  </table>",
                        Request.Url,
                        Request.ServerVariables["LOGON_USER"],
                        lastErrorTypeName,
                        lastErrorMessage,
                        lastErrorStackTrace.Replace(Environment.NewLine, "<br />"));

            // Attach the Yellow Screen of Death for this error
            string YSODmarkup = lastErrorWrapper.GetHtmlErrorMessage();
            if (!string.IsNullOrEmpty(YSODmarkup))
            {
                Attachment YSOD = Attachment.CreateAttachmentFromString(YSODmarkup, "YSOD.htm");
                msg.Attachments.Add(YSOD);
            }

            //CONFIGURE SMTP OBJECT
            System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

            dado = MetodosFE.getTela("Configuração de E-mail");
            if (dado != null)
            {
                objSmtp.Host = dado.nome;
                objSmtp.Port = Convert.ToInt32(dado.valor);
                objSmtp.Credentials = new System.Net.NetworkCredential(dado.referencia, dado.ordem);
            }
            else
                throw new Exception("Problemas ocorreram na configuração de E-mail.");

            //SEND EMAIL
            objSmtp.Send(msg);

            //REDIRECT USER TO ERROR PAGE
            //Server.Transfer("~/ErrorPage.aspx");
        }
    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {

    }

    protected void Application_PostAuthorizeRequest()
    {
        if (IsWebApiRequest())
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }

    protected void Application_PutAuthorizeRequest()
    {
        if (IsWebApiRequest())
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }

    private bool IsWebApiRequest()
    {
        return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
    }

    private bool EmManutencao()
    {
        if (HttpContext.Current.Application["Manutencao"] == null)
        {
            string enderecoPasta = Server.MapPath("~/userfiles/import-log/");

            if (!Directory.Exists(enderecoPasta))
                Directory.CreateDirectory(enderecoPasta);

            string nomeArquivo = "status";
            string sFileXLS = enderecoPasta + "\\" + nomeArquivo;
            if (!File.Exists(sFileXLS))
            {
                File.Create(sFileXLS).Close();
                using (StreamWriter file = new StreamWriter(sFileXLS, true))
                {
                    file.Write(false.ToString());
                    file.Close();
                }
            }


            string status = null;
            using (StreamReader sr = new StreamReader(sFileXLS))
            {
                // Read the stream to a string, and write the string to the console.
                status = sr.ReadLine();
            }

            bool statusBool = Convert.ToBoolean(status);

            if (statusBool)
                HttpContext.Current.Application["Manutencao"] = true;
            else
                HttpContext.Current.Application["Manutencao"] = false;
        }


        bool manutencao = Convert.ToBoolean(HttpContext.Current.Application["Manutencao"]);
        return manutencao;



    }

    protected void Application_PreSendRequestHeaders()
    {
        //Response.Headers.Remove("Server");
        Response.Headers.Set("Server", "ComaTortaServer");
        //Response.Headers.Remove("X-AspNet-Version"); //alternative to above solution
        //Response.Headers.Remove("X-AspNetMvc-Version"); //alternative to above solution
    }
</script>
