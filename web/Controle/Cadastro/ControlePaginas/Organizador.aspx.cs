using System;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Tela de Organização";
        nome2 = "Tela de Organização";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
    }

    
}
