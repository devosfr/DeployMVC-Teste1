using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Modelos;
using System.Web.Security;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for ControleLogin
/// </summary>
public class ControleLoginUsuario
{

    public static int idDoCliente;

    public ControleLoginUsuario()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static Repository<UsuarioJB> RepositorioUsuarioJB
    {
        get
        {
            return new Repository<UsuarioJB>(NHibernateHelper.CurrentSession);
        }
    }



    //Pagina para onde irá ao se logar e não ter acessado nenhuma outra página que necessite login antes
    private static string PAGINA_DESTINO
    {
        get
        {
            return MetodosFE.BaseURL;
        }
    }
    private static string PAGINA_LOGIN
    {
        get
        {
            return MetodosFE.BaseURL + "/Default";
        }
    }

    /// <summary>
    /// Returns the name of the virtual folder where our project lives
    /// </summary>
    /// 
    public static string BaseURL
    {
        get
        {
            try
            {
                string retorno = VirtualPathUtility.ToAbsolute("~/");

                return retorno.Remove(retorno.Length - 1);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }

    /// <summary>
    /// Returns the name of the virtual folder where our project lives
    /// </summary>
    /// 
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }

    /// <summary>
    /// Returns the name of the virtual folder where our project lives
    /// </summary>
    /// 
    public static Boolean recuperaSenhaViaEmail(string email)
    {

        //if (HttpContext.Current.Session["TentativasRecuperacao"] != null)
        //{
        //    int tentativas = (int)HttpContext.Current.Session["TentativasRecuperacao"];
        //    if (tentativas < 6)
        //    {
        //        tentativas++;
        //        HttpContext.Current.Session["TentativasRecuperacao"] = tentativas;
        //    }
        //    else
        //        throw new Exception("Aguarde alguns minutos e tente novamente.");
        //}
        //else
        //{
        //    HttpContext.Current.Session["TentativasRecuperacao"] = 0;
        //}

        UsuarioJB usuario = RepositorioUsuarioJB.FilterBy(x => x.email.Equals(email)).FirstOrDefault();

        if (usuario == null)
        {
            throw new Exception("E-mail não encontrado. Confira suas informações e tente novamente.");
        }
        else
        {

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            EnvioEmailsVO envio = new EnvioEmailsVO();
            envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = usuario.email;
            envio.assuntoMensagem = "Recuperação de senha";

            string senhaNova = usuario.senha.Substring(0, 8);

            usuario.senha = GetSHA1Hash(senhaNova);
            RepositorioUsuarioJB.Update(usuario);


            //Atribui ao método Body a texto da mensagem
            string v_recebe = "";


            v_recebe += "Conforme foi solicitado no site " + Configuracoes.getSetting("NomeSite") + ", estamos enviando uma nova senha para sua conta: <br/><br/>";

            v_recebe += "<br/>Senha: " + senhaNova;

            //v_recebe += "<br/><br/>É recomendável que altere a senha para alguma mais familiar assim que entrar no site.";

            envio.conteudoMensagem = v_recebe;


            bool recebeu = EnvioEmails.envioemails(envio);

            if (recebeu)
            {
                return true;
            }
            else
            {
                throw new Exception("Ocorreram problemas no envio do e-mail. Tente mais tarde.");
            }
        }
        return false;
    }

    public static bool GetUsuarioAtualizado()
    {
        try
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                return false;
            }
            else
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string dadosUsuario = authTicket.UserData;
                if (dadosUsuario.Split('|').Length == 1)
                {
                    return false;
                }
            }

        }
        catch (System.Threading.ThreadAbortException)
        {
            // Do nothing. ASP.NET is redirecting.
            // Always comment this so other developers know why the exception 
            // is being swallowed.
        }
        return true;
    }

    public static void statusLogin(string pagina = null)
    {
        try
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                gravarPagina();
                HttpContext.Current.Response.Redirect(PAGINA_LOGIN);
            }
            else
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string dadosUsuario = authTicket.UserData;
                if (dadosUsuario.Split('|').Length > 1)
                {
                    FormsAuthentication.SignOut();
                    gravarPagina();
                    HttpContext.Current.Response.Redirect(PAGINA_LOGIN);
                }
            }
        }
        catch (System.Threading.ThreadAbortException ex)
        {
            // Do nothing. ASP.NET is redirecting.
            // Always comment this so other developers know why the exception 
            // is being swallowed.
        }
    }

    public static void Logout()
    {
        FormsAuthentication.SignOut();


        HttpContext.Current.Response.Redirect("~/");
    }

    public static void SetCookieLogin(UsuarioJB usuario)
    {
        // sometimes used to persist user roles
        string userData = usuario.Id.ToString();

        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
          1,                                     // ticket version
          usuario.nome,                              // authenticated username
          DateTime.Now,                          // issueDate
          DateTime.Now.AddMinutes(30),           // expiryDate
          true,                          // true to persist across browser sessions
          userData,                              // can be used to store additional user data
          FormsAuthentication.FormsCookiePath);  // the path for the cookie

        // Encrypt the ticket using the machine key
        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

        // Add the cookie to the request to save it
        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        cookie.HttpOnly = true;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    public static string GetSHA1Hash(string input)
    {
        //SHA1 hash
        SHA1 SHA1Hash = SHA1.Create();

        // Convert the input string to a byte array and compute the hash.
        byte[] data = SHA1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    public static void alterarsenha(string senhaatual, string senhanova, int idCliente)
    {
        idDoCliente = idCliente;

        try
        {
            UsuarioJB usuario = GetUsuarioLogado();

            if (usuario.senha != senhaatual)
            {
                usuario.senha = GetSHA1Hash(senhanova);
                RepositorioUsuarioJB.Update(usuario);
            }
            else throw new Exception("Senha Atual Incorreta!");
        }
        catch (System.Threading.ThreadAbortException ex)
        {
            // do nothing. asp.net is redirecting.
            // always comment this so other developers know why the exception 
            // is being swallowed.
        }
    }

    public static void alterarDados(string senhaatual, string email, string genero, string telefone,
        string senhanova, int idCliente)
    {
        idDoCliente = idCliente;

        try
        {
            UsuarioJB usuario = GetUsuarioLogadoCl();

            usuario.senha = GetSHA1Hash(senhanova);
            usuario.email = email;
            usuario.sexo = genero;
            usuario.telefone = telefone;
            RepositorioUsuarioJB.Update(usuario);

        }
        catch (System.Threading.ThreadAbortException)
        {
            // do nothing. asp.net is redirecting.
            // always comment this so other developers know why the exception 
            // is being swallowed.
        }
    }

    public static UsuarioJB GetUsuarioLogado()
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        if (authCookie != null)
        {
            //Extract the forms authentication cookie
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            string[] dadosUsuario = authTicket.UserData.Split('|');
            // If caching roles in userData field then extract
            //if (dadosUsuario.Length > 1)
            //{
            //    return null;
            //}

            string idString = dadosUsuario[0];



            int idUsuario = Convert.ToInt32(idString);

            UsuarioJB usuario = RepositorioUsuarioJB.FindBy(idUsuario);

            return usuario;
        }

        return null;
    }

    public static UsuarioJB GetUsuarioLogadoCl()
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        if (authCookie != null)
        {
            //Extract the forms authentication cookie
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            string[] dadosUsuario = authTicket.UserData.Split('|');
            // If caching roles in userData field then extract
            //if (dadosUsuario.Length > 1)
            //{
            //    return null;
            //}

            string idString = dadosUsuario[0];



            int idUsuario = Convert.ToInt32(idString);

            UsuarioJB usuario = RepositorioUsuarioJB.FindBy(idDoCliente);

            return usuario;
        }

        return null;
    }

    public static void login(string login, string senha)
    {

        //if (HttpContext.Current.Session["TentativasLogin"] != null)
        //{
        //    int tentativas = (int)HttpContext.Current.Session["TentativasLogin"];
        //    if (tentativas < 999999)
        //    {
        //        tentativas++;
        //        HttpContext.Current.Session["TentativasLogin"] = tentativas;
        //    }
        //    else
        //        throw new Exception("Ocorreram muitas tentativas de efetuar login. Aguarde alguns minutos e tente novamente.");
        //}
        //else
        //{
        //    HttpContext.Current.Session["TentativasLogin"] = 0;
        //}


        if (!String.IsNullOrEmpty(login) || !String.IsNullOrEmpty(senha))
        {
            if (senha.Contains("<") || senha.Contains(">") || senha.Contains("/") || senha.Contains("*") || senha.Contains("--") || senha.Contains("{") || senha.Contains("}") || senha.Contains("\\") || senha.Contains("%") ||
    login.Contains("<") || login.Contains(">") || login.Contains("/") || login.Contains("*") || login.Contains("--") || login.Contains("{") || login.Contains("}") || login.Contains("\\") || login.Contains("%"))
            {
                throw new Exception("E-mail ou Senha Incorretos");
            }
            else
            {
                string senhaHash = GetSHA1Hash(senha);

                //UsuarioJB usuario = RepositorioUsuarioJB.FindBy(x => x.email.Equals(login) && x.senha.Equals(senhaHash) && x.status.Equals("AT"));
                UsuarioJB usuario = RepositorioUsuarioJB.FindBy(x => x.email.Equals(login) && x.senha.Equals(senhaHash) && x.status.Equals("AT"));
                if (usuario != null)
                {

                    SetCookieLogin(usuario);
                    string paginaDestino = getPaginaLogin();
                    if (!String.IsNullOrEmpty(paginaDestino))
                    {

                        HttpContext.Current.Response.Redirect(paginaDestino, false);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(PAGINA_DESTINO, false);
                    }
                }
                else
                {
                    throw new Exception("Login ou Senha Incorretos");
                }
            }
        }
    }

    public static Boolean emailDisponivel(String email)
    {
        UsuarioJB usuario = RepositorioUsuarioJB.All().Where(x => x.email == email).FirstOrDefault();
        if (usuario != null)
            if (!usuario.Equals(ControleLogin.UsuarioAtual))
                return false;


        return true;
    }

    protected static string getPaginaLogin()
    {

        return (String)HttpContext.Current.Session["pagina_cliente"];

    }

    public static void gravarPagina(string pagina = null)
    {
        if (pagina == null)
            HttpContext.Current.Session["pagina_cliente"] = HttpContext.Current.Request.Url.OriginalString;
        else
            HttpContext.Current.Session["pagina_cliente"] = pagina;
    }

    public static void CadastraUsuarioJB(UsuarioJB usuario)
    {
        //usuario.status = "AT";
        RepositorioUsuarioJB.Add(usuario);
    }

    public static void AtualizaUsuarioJB(UsuarioJB usuario)
    {
        RepositorioUsuarioJB.Add(usuario);
    }

    public static Boolean validaEmail(String email)
    {
        //Formato: *@*.*
        //         01234567890123
        if (String.IsNullOrEmpty(email))
            return false;

        bool arroba = false;
        bool pontoDepoisDoArroba = false;
        for (int i = 0; i < email.Length; i++)
        {
            char c = email[i];
            if (c == '@')
                arroba = true;
            if (c == '.')
                if (arroba)
                {
                    pontoDepoisDoArroba = true;
                    break;
                }
        }

        if (arroba && pontoDepoisDoArroba)
            return true;
        return false;
    }


    public static Boolean validaCNPJ(String cnpj)
    {

        if (Regex.IsMatch(cnpj, @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)"))
        {

            return true;

        }

        else
        {

            return false;

        }

    }

}