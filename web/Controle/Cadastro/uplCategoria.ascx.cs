using System;

using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;

public partial class controle_uplLogo : uplImage
{
    private Repository<ImagemCategoriaVO> repoFotos 
    { 
        get 
        {
            return new Repository<ImagemCategoriaVO>(NHibernateHelper.CurrentSession);
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


}