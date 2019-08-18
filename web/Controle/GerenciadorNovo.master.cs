using System;

public partial class Gerenciador : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ControleLogin.statusLoginGerenciador();
    }


}
