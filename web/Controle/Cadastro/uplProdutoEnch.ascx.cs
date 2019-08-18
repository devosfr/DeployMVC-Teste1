using System;

using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;
using System.Web;
using System.IO;

public partial class controle_uplLogo : uplImage
{
    private Repository<ImagemProduto> repoImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Cor> RepositorioCor
    {
        get
        {
            return new Repository<Cor>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Album> RepositorioAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<ImagemProduto> RepositorioImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
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
                //
            }

        }
        //Carregar();
        //DataList2.DataSource = fotosSemCodigo;
        //DataList2.DataBind();
    }

    #region -- M�todos --



    public void Carregar()
    {
        try
        {
            if (Codigo > 0)
            {
                IList<Album> albuns = RepositorioAlbum.FilterBy(x => x.Produto.Id == Codigo).ToList();

                lbCores.DataSource = albuns;
                lbCores.DataBind();

                IList<Cor> cores = RepositorioCor.All().ToList();

                cores = cores.Where(x => !albuns.Any(y => y.Cor.Id == x.Id)).ToList();

                ddlCores.DataSource = cores;
                ddlCores.DataTextField = "Nome";
                ddlCores.DataValueField = "Id";
                ddlCores.DataBind();

            }
        }
        catch (Exception er)
        {
            lblMensagem.Text = er.Message;
        }
    }
    #endregion

    #region Guardamos o C�digo no ViewState
    public int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }
    #endregion


    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        int idCor = Convert.ToInt32(ddlCores.SelectedValue);

        Cor cor = RepositorioCor.FindBy(idCor);

        if (cor != null)
        {
            Produto produto = RepositorioProduto.FindBy(Codigo);

            Album album = new Album();
            album.Nome = cor.Nome;
            album.Cor = cor;
            album.Ativo = true;
            album.Produto = produto;

            RepositorioAlbum.Add(album);

        }

        Carregar();

    }

    protected void linkCor_Command(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        Album alb = RepositorioAlbum.FindBy(id);
        hdn.Value = alb.Id.ToString();
        divUpload.Visible = true;
        litCor.InnerText = alb.Cor.Nome;
        if (alb.Imagens != null && alb.Imagens.Count > 0)
        {
            IList<ImagemProduto> imagens = alb.Imagens.ToList();
            repImagens.DataSource = imagens;
            repImagens.DataBind();
        }
    }

    protected void lbExcluir_Command(object sender, CommandEventArgs e)
    {
        int idAlbum = Convert.ToInt32(e.CommandArgument.ToString());

        Album album = RepositorioAlbum.FindBy(idAlbum);

        if (album != null)
        {
            album.ExcluirImagens();
            RepositorioAlbum.Delete(album);
        }

        IList<Album> albuns = RepositorioAlbum.FilterBy(x => x.Produto.Id == Codigo).ToList();
        lbCores.DataSource = albuns;
        lbCores.DataBind();
        divUpload.Visible = false;
    }

    protected void linkExcluirImg_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            ImagemProduto img = repoImagemProduto.FindBy(id);
            if (img != null)
            {
                Album album = img.Album;
                album.Imagens.Remove(img);
                RepositorioAlbum.Update(album);
                repImagens.DataSource = album.Imagens;
                repImagens.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubir_Click(object sender, EventArgs e)
    {
        if (fulImagens.HasFile)
        {
            IList<HttpPostedFile> files = fulImagens.PostedFiles;

            Album album = RepositorioAlbum.FindBy(Convert.ToInt32(hdn.Value));

            foreach (HttpPostedFile file in files)
            {
                ImagemProduto img = new ImagemProduto();

                string subPath = "~/ImagensHQ/";

                file.SaveAs(Server.MapPath(subPath + Path.GetFileName(file.FileName)));

                img.Nome = Path.GetFileName(file.FileName);

                img.Album = album;

                RepositorioImagemProduto.Add(img);

                if (album.Imagens == null)
                    album.Imagens = new List<ImagemProduto>();

                album.Imagens.Add(img);

                RepositorioAlbum.Update(album);
            }

            repImagens.DataSource = album.Imagens.Distinct();
            repImagens.DataBind();
        }
    }
}