using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Modelos;
using System.Web.Security;

/// <summary>
/// Summary description for ControleLogin
/// </summary>
public class ControleLoginCliente
{
    public static int idDoCliente;

    public ControleLoginCliente()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static Repository<Cliente> RepositorioCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Cupom> RepositorioCupom
    {
        get
        {
            return new Repository<Cupom>(NHibernateHelper.CurrentSession);
        }
    }

    //Pagina para onde irá ao se logar e não ter acessado nenhuma outra página que necessite login antes
    private static string PAGINA_DESTINO
    {
        get
        {
            return BaseURL + "/area-do-usuario/meus-dados";
        }
    }
    private static string PAGINA_LOGIN
    {
        get
        {
            return BaseURL + "/area-do-usuario/login";
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

        if (HttpContext.Current.Session["TentativasRecuperacao"] != null)
        {
            int tentativas = (int)HttpContext.Current.Session["TentativasRecuperacao"];
            if (tentativas < 5)
            {
                tentativas++;
                HttpContext.Current.Session["TentativasRecuperacao"] = tentativas;
            }
            else
                throw new Exception("Aguarde alguns minutos e tente novamente.");
        }
        else
        {
            HttpContext.Current.Session["TentativasRecuperacao"] = 0;
        }

        Cliente cliente = RepositorioCliente.FilterBy(x => x.Email.Equals(email)).FirstOrDefault();

        if (cliente == null)
        {
            throw new Exception("E-mail não encontrado. Confira suas informações e tente novamente.");
        }
        else
        {

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            EnvioEmailsVO envio = new EnvioEmailsVO();
            envio.nomeRemetente = "SAVEL";
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = cliente.Email;
            envio.assuntoMensagem = "Recuperação de senha";

            string senhaNova = cliente.Senha.Substring(0, 8);

            cliente.Senha = GetSHA1Hash(senhaNova);
            RepositorioCliente.Update(cliente);


            //Atribui ao método Body a texto da mensagem
            string v_recebe = "";


            v_recebe += "Conforme foi solicitado no site " + Configuracoes.getSetting("NomeSite") + ", estamos enviando uma nova senha para sua conta: <br/><br/>";

            v_recebe += "<br/>Senha: " + senhaNova;

            v_recebe += "<br/><br/>É recomendável que altere a senha para alguma mais familiar assim que entrar no site.";

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

    public static bool ClienteLogado()
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
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }


    public static void alterarDados(string senhaAtual, string email, string cpf, string genero, string telefone,
       string inscricao, string celular, string senhaNova, int idCliente)
    {

        idDoCliente = idCliente;

        try
        {
            Cliente cliente = GetClienteLogado();

            //HttpContext.Current.Response.Redirect(PAGINA_LOGIN);


            cliente.Senha = GetSHA1Hash(senhaNova);
            cliente.Email = email;
            cliente.Genero = genero;
            cliente.Telefone = telefone;

            cliente.InscricaoEstadual = inscricao;
            cliente.Whatsapp = celular;
            cliente.Telefone = telefone;
            RepositorioCliente.Update(cliente);

        }
        catch (System.Threading.ThreadAbortException)
        {
            // Do nothing. ASP.NET is redirecting.
            // Always comment this so other developers know why the exception 
            // is being swallowed.
        }
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
    }

    private static void setCookieLogin(Cliente cliente)
    {
        // sometimes used to persist user roles
        string userData = cliente.Id.ToString();

        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
          1,                                     // ticket version
          cliente.Nome,                              // authenticated username
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

    public static void alterarSenha(string senhaAtual, string senhaNova)
    {
        try
        {
            Cliente usuario = GetClienteLogado();
            if (usuario == null)
                HttpContext.Current.Response.Redirect(PAGINA_LOGIN);

            //Cliente usuario = (Cliente)HttpContext.Current.Session["UsuarioLogado"];

            if (usuario.Senha.Equals(GetSHA1Hash(senhaAtual)))
            {

                usuario.Senha = GetSHA1Hash(senhaNova);
                RepositorioCliente.Update(usuario);
            }
            else throw new Exception("Senha atual errada.");
        }
        catch (System.Threading.ThreadAbortException)
        {
            // Do nothing. ASP.NET is redirecting.
            // Always comment this so other developers know why the exception 
            // is being swallowed.
        }
    }

    public static Cliente GetClienteLogado()
    {
        try
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                //Extract the forms authentication cookie
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                string[] dadosUsuario = authTicket.UserData.Split('|');
                // If caching roles in userData field then extract
                if (dadosUsuario.Length > 1)
                {
                    return null;
                }

                string idString = dadosUsuario[0];

                int idCliente = Convert.ToInt32(idString);

                Cliente cliente = RepositorioCliente.FindBy(idCliente);

                return cliente;
            }
        }
        catch (Exception ex) 
        {
            return null;
        }
        return null;
    }

    public static void login(string login, string senha)
    {
        //if (HttpContext.Current.Session["TentativasLogin"] != null)
        //{
        //    int tentativas = (int)HttpContext.Current.Session["TentativasLogin"];
        //    if (tentativas < 5)
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

                Cliente cliente = RepositorioCliente.FindBy(x => x.Email.Equals(login) && x.Senha.Equals(senhaHash) && x.Status.Equals("AT"));
                if (cliente != null)
                {
                    setCookieLogin(cliente);
                    string paginaDestino = getPaginaLogin();
                    if (!String.IsNullOrEmpty(paginaDestino))
                    {

                        HttpContext.Current.Response.Redirect(paginaDestino, false);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/Area-Do-Usuario/meus-dados.aspx", false);
                    }
                }
                else
                {
                    throw new Exception("Login ou Senha Incorretos");

                }
            }
        }
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

    public static void CadastraCliente(Cliente cliente)
    {
        cliente.Status = "AT";
        RepositorioCliente.Add(cliente);
    }

    public static void AtualizaCliente(Cliente cliente)
    {
        RepositorioCliente.Add(cliente);
    }


}