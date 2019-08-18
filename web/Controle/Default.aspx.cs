using System;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        try
        {

            String login = txtUsuario.Text.Trim();
            String senha = txtSenha.Text.Trim();


            if ((!String.IsNullOrEmpty(senha)) && (!String.IsNullOrEmpty(login)))
            {
                ControleLogin.loginGerenciador(login, senha);

            }
            else
            {
                lblMensagem.Text = "Login e Senha são obrigatórios.";
            }
        }
        catch (Exception er)
        {
            lblMensagem.Text = "Login ou Senha Incorretos";
        }

    }
    protected string BaseURL
    {
        get
        {
            try
            {
                return string.Format("http://{0}{1}",
                                     HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                                     (VirtualFolder.Equals("/")) ? string.Empty : VirtualFolder);
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
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }
}