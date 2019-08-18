using System;

using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;
using System.IO;
using System.Web;

public partial class controle_uplLogo : uplImage
{
    private Repository<Marca> RepositorioMarca
    { 
        get 
        {
            return new Repository<Marca>(NHibernateHelper.CurrentSession);
        } 
    }

    private Repository<ImagemMarca> RepositorioImagemMarca
    {
        get
        {
            return new Repository<ImagemMarca>(NHibernateHelper.CurrentSession);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Session["Codigo"] = 0;
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Marca marca = RepositorioMarca.FindBy(Codigo);
                IList<ImagemMarca> imagens = RepositorioImagemMarca.FilterBy(x => x.Marca.Id == marca.Id).ToList();
                if(imagens != null && imagens.Count > 0)
                {
                    repImagens.DataSource = imagens.Take(1);
                    repImagens.DataBind();
                }
                //
            }
        }
        //DataList2.DataSource = fotosSemCodigo;
        //DataList2.DataBind();
    }

    #region Guardamos o Código no ViewState
    public int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }
    public string nomeTela
    {
        get
        {
            if (ViewState["nomeTela"] == null) ViewState["nomeTela"] = "";
            return ViewState["nomeTela"].ToString();
        }
        set { ViewState["nomeTela"] = value; }
    }
    #endregion



    protected void btnSubir_Click(object sender, EventArgs e)
    {
        if (fulMarca.HasFile)
        {
            HttpPostedFile file = fulMarca.PostedFile;

            ImagemMarca img = new ImagemMarca();

            string subPath = "~/ImagensHQ";

            file.SaveAs(Server.MapPath(subPath + Path.GetFileName(file.FileName)));

            img.Nome = "/ImagensHQ/" + Path.GetFileName(file.FileName);

            Marca marca = RepositorioMarca.FindBy(Codigo);

            img.Marca = marca;

            RepositorioImagemMarca.Add(img);

            marca.Imagem = img;

            RepositorioMarca.Update(marca);

            IList<ImagemMarca> imgs = new List<ImagemMarca>();

            imgs.Add(img);

            repImagens.DataSource = imgs;
            repImagens.DataBind();
        }
    }
    

    protected void linkExcluir_Command(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        ImagemMarca img = RepositorioImagemMarca.FindBy(id);
        Marca marca = img.Marca;
        marca.Imagem = null;
        RepositorioMarca.Update(marca);
        RepositorioImagemMarca.Delete(img);
        repImagens.DataSource = new List<ImagemMarca>();
        repImagens.DataBind();
    }
}