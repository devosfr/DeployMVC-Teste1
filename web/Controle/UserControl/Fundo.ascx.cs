using System;
using System.Web;

public partial class ZepolControl_DadosTexto : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


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
