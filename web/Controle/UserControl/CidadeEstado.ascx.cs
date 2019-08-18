using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class ZepolControl_DadosTexto : System.Web.UI.UserControl
{
    private Repository<Estado> repoEstado
    {
        get
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Cidade> repoCidade
    {
        get
        {
            return new Repository<Cidade>(NHibernateHelper.CurrentSession);
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {

        ClientScriptManager teste = Page.ClientScript;
        //teste.RegisterForEventValidation(ddlEstado.UniqueID);
        teste.RegisterForEventValidation(ddlEstado.UniqueID);
        teste.RegisterForEventValidation(ddlCidade.UniqueID);
        foreach (ListItem item in ddlEstado.Items)
            teste.RegisterForEventValidation(ddlEstado.UniqueID, item.Value);
        IList<Cidade> cidades = repoCidade.All().ToList();
        foreach (Cidade item in cidades)
            teste.RegisterForEventValidation(ddlCidade.UniqueID, item.Id.ToString());
        base.Render(writer);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            carregarEstados();
        }
    }
    public void carregarEstados(int idEstado = 0)
    {
        IList<Estado> estados = repoEstado.All().OrderBy(x => x.Nome).ToList();
        ddlEstado.DataSource = estados;
        ddlEstado.DataValueField = "id";
        ddlEstado.DataTextField = "nome";
        ddlEstado.Items.Insert(0, new ListItem("Selecione", "0"));
        ddlEstado.DataBind();
        if (idEstado > 0)
        {
            ddlEstado.SelectedValue = idEstado.ToString();
            hfEstado.Value = idEstado.ToString();
        }
    }
    public void carregarCidades(int idCidade = 0)
    {
        IList<Cidade> cidades = repoCidade.FilterBy(x => x.Estado.Id == Convert.ToInt32(ddlEstado.SelectedValue)).OrderBy(x => x.Nome).ToList();
        ddlCidade.DataSource = cidades;
        ddlCidade.DataValueField = "id";
        ddlCidade.DataTextField = "nome";
        ddlCidade.DataBind();
        ddlCidade.Items.Insert(0, new ListItem("Selecione", "0"));
        if (idCidade > 0)
        {
            ddlCidade.SelectedValue = idCidade.ToString();
            hfCidade.Value = idCidade.ToString();
        }
    }

    public int getEstadoID() 
    {
        return Convert.ToInt32(hfEstado.Value);
    }

    public int getCidadeID() 
    {
        return Convert.ToInt32(hfCidade.Value);
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
