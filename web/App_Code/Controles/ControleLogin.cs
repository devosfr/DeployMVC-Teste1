using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Modelos;
using System.Web.Security;

/// <summary>
/// Summary description for ControleLogin
/// </summary>
public class ControleLogin
{
    public ControleLogin()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static Repository<UsuarioVO> RepositorioUsuario
    {
        get
        {
            return new Repository<UsuarioVO>(NHibernateHelper.CurrentSession);
        }
    }

    //Pagina para onde irá ao se logar e não ter acessado nenhuma outra página que necessite login antes
    private static string PAGINA_DESTINO_GERENCIADOR
    {
        get
        {
            return BaseURL + "/Controle/Login.aspx";
        }
    }
    private static string PAGINA_LOGIN_GERENCIADOR
    {
        get
        {
            return BaseURL + "/Controle/Default.aspx";
        }
    }

    public static UsuarioVO UsuarioAtual
    {
        get { return (UsuarioVO)HttpContext.Current.Items["current.usuario"]; }
        set { HttpContext.Current.Items["current.usuario"] = value; }
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

    public static void statusLoginGerenciador()
    {
        try
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                gravarPaginaGerenciador();
                HttpContext.Current.Response.Redirect(PAGINA_LOGIN_GERENCIADOR);
            }
            else
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string dadosUsuario = authTicket.UserData;
                if (dadosUsuario.Split('|').Length < 2)
                {
                    FormsAuthentication.SignOut();
                    gravarPaginaGerenciador();
                    HttpContext.Current.Response.Redirect(PAGINA_LOGIN_GERENCIADOR);
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

    public static bool AdmLogado()
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
                if (dadosUsuario.Split('|').Length < 2)
                {
                    return false;
                }
            }
            
        }
        catch (System.Threading.ThreadAbortException ex)
        {
            // Do nothing. ASP.NET is redirecting.
            // Always comment this so other developers know why the exception 
            // is being swallowed.
        }
        return true;
    }

    public static UsuarioVO GetUsuarioLogado()
    {

        if (UsuarioAtual != null)
            return UsuarioAtual;
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

        if (authCookie != null)
        {
            //Extract the forms authentication cookie
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            string[] dadosUsuario = authTicket.UserData.Split('|');
            // If caching roles in userData field then extract
            if (dadosUsuario.Length < 2)
            {
                FormsAuthentication.SignOut();
                gravarPaginaGerenciador();
                HttpContext.Current.Response.Redirect(PAGINA_LOGIN_GERENCIADOR);
            }

            string idString = dadosUsuario[0];



            int idUsuario = Convert.ToInt32(idString);

            UsuarioVO usuario = RepositorioUsuario.FindBy(idUsuario);
            UsuarioAtual = usuario;
            return usuario;
        }

        return null;
    }

    public static void Logout() 
    {
        FormsAuthentication.SignOut();
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

    public static void loginGerenciador(string login, string senha)
    {
        if (!String.IsNullOrEmpty(login) || !String.IsNullOrEmpty(senha))
        {
            if (senha.Contains("<") || senha.Contains(">") || senha.Contains("/") || senha.Contains("*") || senha.Contains("--") || senha.Contains("{") || senha.Contains("}") || senha.Contains("\\") || senha.Contains("%") ||
    login.Contains("<") || login.Contains(">") || login.Contains("/") || login.Contains("*") || login.Contains("--") || login.Contains("{") || login.Contains("}") || login.Contains("\\") || login.Contains("%"))
            {
                throw new Exception("Login ou Senha Incorretos");
            }
            else
            {
                List<String> tiposAdm = new List<string>();
                tiposAdm.Add("AA");
                tiposAdm.Add("AD");

                string senhaHash = GetSHA1Hash(senha);
                UsuarioVO usuario = RepositorioUsuario.All().Where(x => x.login == login && senhaHash == x.senha && x.status == "AT" && tiposAdm.Contains(x.tipo)).FirstOrDefault();

                if (usuario != null)
                {
                    setCookieLogin(usuario);
                    HttpContext.Current.Session["MensagemString"] = null;
                    string paginaDestino = getPaginaLoginGerenciador();
                    if (!String.IsNullOrEmpty(paginaDestino))
                    {
                        HttpContext.Current.Response.Redirect(paginaDestino, false);
                    }
                    else HttpContext.Current.Response.Redirect(PAGINA_DESTINO_GERENCIADOR, false);
                }
                else
                {
                    throw new Exception("Login ou Senha Incorretos");
                }
            }
        }
    }

    private static void setCookieLogin(UsuarioVO usuario)
    {
        // sometimes used to persist user roles
        string userData = usuario.Id.ToString() + "|" + usuario.tipo;

        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
          1,                                     // ticket version
          usuario.login,                              // authenticated username
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

    protected static string getPaginaLoginGerenciador()
    {
        return (String)HttpContext.Current.Session["PaginaLoginGerenciador"];
    }


    protected static void gravarPaginaGerenciador()
    {
        HttpContext.Current.Session["PaginaLoginGerenciador"] = !HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToUpper().Contains("ASPX") ? null : HttpContext.Current.Request.Url.OriginalString;
    }



}