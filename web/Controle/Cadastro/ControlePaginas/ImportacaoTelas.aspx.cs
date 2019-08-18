using System;
using System.Web;
using System.IO;
using System.Xml;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Importação de Telas";
        nome2 = "Controle de SiteMap";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;

        if (!IsPostBack)
        {
        }
    }


    protected void btnCarregar_Click(object sender, EventArgs e)
    {
        try
        {

            //string caminhoBase = HttpContext.Current.Server.MapPath("~/sitemap.xml");

            //File 

            if (fulSiteMap.HasFile)
            {
                bool bValido = false;

                string fileExtension = System.IO.Path.GetExtension(fulSiteMap.FileName).ToLower();
                foreach (string ext in new string[] { ".xml" })
                {
                    if (fileExtension == ext)
                        bValido = true;
                }
                if (!bValido)
                    throw new Exception("Extensão inválida de arquivo.");
            }

            ControleImportacaoTelas.loadConfiguracoesToDictionary(fulSiteMap.PostedFile.InputStream);

            MetodosFE.mostraMensagem("Telas carregadas.", "sucesso");



        }
        catch (Exception ex)
        {
            litErro.Text = (ex.Message);
        }
    }
}
