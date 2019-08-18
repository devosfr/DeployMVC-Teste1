using System;
using NHibernate.Linq;
using Modelos;

public partial class ZepolControl_DadosTexto : System.Web.UI.UserControl
{
    private Repository<Endereco> repoEndereco
    {
        get
        {
            return new Repository<Endereco>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

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
    #endregion
    public void carregarEndereco(int idCliente)
    {
        if (idCliente > 0)
        {
            var enderecos = repoEndereco.FilterBy(x => x.Cliente.Id == idCliente).Fetch(x => x.Estado).Fetch(x => x.Cidade);
            repEnderecos.DataSource = enderecos;
            repEnderecos.DataBind();
        }
    }

}
