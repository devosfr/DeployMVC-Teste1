using System;

using System.Linq;
using NHibernate.Linq;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Net;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Modelos;

public partial class controle_uplLogo : uplImage
{
    private Repository<ImagemProduto> repoFotos 
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
        Carregar();
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
                IList<ImagemProduto> fotos = repoFotos.All().Where(x => x.produto.Id == Codigo).OrderBy(x => x.Ordem).ToList();
                repImagens.DataSource = fotos;
                repImagens.DataBind();
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


}