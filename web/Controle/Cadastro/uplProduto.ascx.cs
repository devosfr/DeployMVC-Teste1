using System;

using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;

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

    #region -- Métodos --



    public void Carregar()
    {
        try
        {
            if (Codigo > 0)
            {
                IList<Album> albuns = RepositorioAlbum.FilterBy(x => x.Produto.Id == Codigo).ToList();

                Dictionary<int, string> albunsDDL = new Dictionary<int, string>();
                foreach (var album in albuns)
                {
                    string status = null;
                    if (!album.Ativo)
                        status = "(Inativo)";

                    albunsDDL.Add(album.Id, album.Cor.Nome + status);
                }

                lbCores.DataSource = albunsDDL;
                lbCores.DataTextField = "Value";
                lbCores.DataValueField = "Key";
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
    protected void btnAtivar_Click(object sender, EventArgs e)
    {
        int idAlbum = Convert.ToInt32(lbCores.SelectedValue);

        Album album = RepositorioAlbum.FindBy(idAlbum);

        if (album != null)
        {

            album.Ativo = true;

            RepositorioAlbum.Update(album);

        }

        Carregar();
    }
    protected void btnDesativar_Click(object sender, EventArgs e)
    {
        int idAlbum = Convert.ToInt32(lbCores.SelectedValue);

        Album album = RepositorioAlbum.FindBy(idAlbum);

        if (album != null)
        {

            album.Ativo = false;

            RepositorioAlbum.Update(album);

        }

        Carregar();
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        int idAlbum = Convert.ToInt32(lbCores.SelectedValue);

        Album album = RepositorioAlbum.FindBy(idAlbum);

        if (album != null)
        {
            album.ExcluirImagens();
            RepositorioAlbum.Delete(album);
        }

        Carregar();
    }
    protected void btnAdicionarTodos_Click(object sender, EventArgs e)
    {
        IList<Album> albuns = RepositorioAlbum.FilterBy(x => x.Produto.Id == Codigo).ToList();

        IList<Cor> cores = RepositorioCor.All().ToList();

        cores = cores.Where(x => !albuns.Any(y => y.Cor.Id == x.Id)).ToList();

        Produto produto = RepositorioProduto.FindBy(Codigo);

        foreach(var cor in cores)
        {
            

            Album album = new Album();
            album.Cor = cor;
            album.Ativo = true;
            album.Produto = produto;

            RepositorioAlbum.Add(album);

        }
        Carregar();
    }
}