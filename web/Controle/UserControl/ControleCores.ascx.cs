using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;

public partial class ZepolControl_DadosTexto : System.Web.UI.UserControl
{
    public Repository<Cor> RepoCor
    {
        get
        {
            return new Repository<Cor>(NHibernateHelper.CurrentSession);
        }
    }
    public Repository<Produto> RepoProduto
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

        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    public int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }

    public void Carregar()
    {
        Produto produto = RepoProduto.FindBy(Codigo);

        IList<Cor> cores = RepoCor.All().ToList();

        IList<Cor> coresProduto = produto.Cores != null ? produto.Cores.ToList() : null;

        if (coresProduto != null)
            cores = cores.OrderBy(x => !coresProduto.Contains(x)).ThenBy(x => x.Nome).ToList();

        cblCores.DataSource = cores;
        cblCores.DataTextField = "Nome";
        cblCores.DataValueField = "Id";
        cblCores.DataBind();

        for (int i = 0; i < cblCores.Items.Count; i++)
        {
            int id = Convert.ToInt32(cblCores.Items[i].Value);
            if (coresProduto != null)
                if (coresProduto.Any(x => x.Id == id))
                    cblCores.Items[i].Selected = true;
        }

    }

    public void Salvar()
    {
        Produto produto = RepoProduto.FindBy(Codigo);

        IList<Cor> cores = RepoCor.All().ToList();

        for (int i = 0; i < cblCores.Items.Count; i++)
            if (cblCores.Items[i].Selected)
            {
                Cor cor = cores.First(x => x.Id == Convert.ToInt32(cblCores.Items[i].Value));
                if (!produto.Cores.Any(x => x.Id == cor.Id))
                {
                    produto.Cores.Add(cor);
                    RepoProduto.Update(produto);
                }
            }
            else
            {
                Cor cor = cores.First(x => x.Id == Convert.ToInt32(cblCores.Items[i].Value));
                if (!produto.Cores.Any(x => x.Id == cor.Id))
                {
                    produto.Cores.Add(cor);
                    RepoProduto.Update(produto);
                }
            }

        //RepoProduto.Update(produto);
    }

    protected void btnAdicionarDetalhe_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(txtNovoDetalhe.Text.Trim()))
                throw new Exception("É preciso digitar um nome para a cor.");

            Cor cor = new Cor();
            cor.Nome = txtNovoDetalhe.Text;

            RepoCor.Add(cor);

            this.Carregar();
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }
}
