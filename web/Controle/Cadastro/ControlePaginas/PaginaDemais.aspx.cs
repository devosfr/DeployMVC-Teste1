using System;
using System.Web;
using System.Web.UI;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }


    private Repository<GrupoDePaginasVO> repoGrupoPaginas
    {
        get
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PaginaDeControleVO> repoPaginasControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Tela das Demais Telas";
        nome2 = "Tela de Demais Telas";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;
        if (!Page.IsPostBack)
        {
            //carregarGrupoPaginas();
            Carregar();
        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    protected void Carregar()
    {
        try
        {
            PaginaDeControleVO pagina;


            pagina = repoPaginasControle.FindBy(x => x.nome == "Usuários" && x.fixa);

            if (pagina != null)
            {
                chkUsuarios.Checked = true;
                //ddlUsuariosGrupo.SelectedValue = pagina.grupoDePaginas.id.ToString();
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Controle de Acesso" && x.fixa);

            if (pagina != null)
            {
                chkControleAcesso.Checked = true;
                //ddlControleAcessoGrupo.SelectedValue = pagina.grupoDePaginas.id.ToString();
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Estados" && x.fixa);

            if (pagina != null)
            {
                chkEstados.Checked = true;
                //ddlEstadosGrupo.SelectedValue = pagina.grupoDePaginas.id.ToString();
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Controle de Sitemap" && x.fixa);

            if (pagina != null)
            {
                chkControleSiteMap.Checked = true;
                //ddlEstadosGrupo.SelectedValue = pagina.grupoDePaginas.id.ToString();
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Cidades" && x.fixa);

            if (pagina != null)
            {
                chkCidades.Checked = true;
                //ddlCidadesGrupo.SelectedValue = pagina.grupoDePaginas.id.ToString();
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Bairros" && x.fixa);

            if (pagina != null)
            {
                chkBairros.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Segmentos de Produtos" && x.fixa);

            if (pagina != null)
            {
                chkSegmentosProdutos.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "SubSegmentos de Produtos" && x.fixa);

            if (pagina != null)
            {
                chkSubSegmentosProdutos.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Categorias de Produtos" && x.fixa);

            if (pagina != null)
            {
                chkCategoriasProdutos.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Cores de Produtos" && x.fixa);

            if (pagina != null)
            {
                chkCoresProdutos.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Clientes" && x.fixa);

            if (pagina != null)
            {
                chkClientes.Checked = true;
            }
            pagina = repoPaginasControle.FindBy(x => x.nome == "Pedidos" && x.fixa);

            if (pagina != null)
            {
                chkPedidos.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Tamanhos" && x.fixa);

            if (pagina != null)
            {
                chkTamanhosProdutos.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Cupons" && x.fixa);

            if (pagina != null)
            {
                chkCupons.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Modelo de Cupom" && x.fixa);

            if (pagina != null)
            {
                chkModeloCupom.Checked = true;
            }


            pagina = repoPaginasControle.FindBy(x => x.nome == "Importação" && x.fixa);

            if (pagina != null)
            {
                chkImportacao.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Controle de Preço" && x.fixa);

            if (pagina != null)
            {
                chkPreco.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Importação de Categoria" && x.fixa);

            if (pagina != null)
            {
                chkImportacaoCategoria.Checked = true;
            }


            pagina = repoPaginasControle.FindBy(x => x.nome == "Apoio ao Cliente" && x.fixa);

            if (pagina != null)
            {
                chkApoioCliente.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Opções de Fretes" && x.fixa);

            if (pagina != null)
            {
                chkOpcoesFrete.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Destaques" && x.fixa);

            if (pagina != null)
            {
                chkDestaques.Checked = true;
            }

            pagina = repoPaginasControle.FindBy(x => x.nome == "Rastreamento" && x.fixa);

            if (pagina != null)
            {
                chkRastreamentoCorreios.Checked = true;
            }

            btnAlterar.Visible = true;
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            PaginaDeControleVO pagina = null;

            pagina = repoPaginasControle.FindBy(x => x.nome == "Usuários" && x.fixa);
            if (chkUsuarios.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Usuários", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Usuarios.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlUsuariosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Controle de Acesso" && x.fixa);
            if (chkControleAcesso.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Controle de Acesso", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/ControleAcesso.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Estados" && x.fixa);
            if (chkEstados.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Estados", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Estados.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlEstadosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Cidades" && x.fixa);
            if (chkCidades.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Cidades", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Cidades.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlCidadesGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);



            pagina = repoPaginasControle.FindBy(x => x.nome == "Controle de Sitemap" && x.fixa);
            if (chkControleSiteMap.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Controle de Sitemap", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/ControleSitemap.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);


            pagina = repoPaginasControle.FindBy(x => x.nome == "Bairros" && x.fixa);
            if (chkBairros.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Bairros", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Bairros.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Segmentos de Produtos" && x.fixa);
            if (chkSegmentosProdutos.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Segmentos de Produtos", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Produto/Segmento.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "SubSegmentos de Produtos" && x.fixa);
            if (chkSubSegmentosProdutos.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "SubSegmentos de Produtos", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Produto/SubSegmento.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);


            pagina = repoPaginasControle.FindBy(x => x.nome == "Categorias de Produtos" && x.fixa);
            if (chkCategoriasProdutos.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Categorias de Produtos", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Produto/Categoria.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Cores de Produtos" && x.fixa);
            if (chkCoresProdutos.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Cores de Produtos", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Produto/Cores.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            
            pagina = repoPaginasControle.FindBy(x => x.nome == "Pedidos" && x.fixa);
            if (chkPedidos.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Pedidos", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Pedidos.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Clientes" && x.fixa);
            if (chkClientes.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Clientes", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Clientes.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);


            pagina = repoPaginasControle.FindBy(x => x.nome == "Tamanhos" && x.fixa);
            if (chkTamanhosProdutos.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Tamanhos", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Produto/Tamanhos.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Cupons" && x.fixa);
            if (chkCupons.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Cupons", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Cupons.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Modelo de Cupom" && x.fixa);
            if (chkModeloCupom.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Modelo de Cupom", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/CupomModelo.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlBairrosGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Importação" && x.fixa);
            if (chkImportacao.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Importação", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Importacao/Importacao.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Controle de Preço" && x.fixa);
            if (chkPreco.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Controle de Preço", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Importacao/ControlePreco.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);


            pagina = repoPaginasControle.FindBy(x => x.nome == "Importação de Categoria" && x.fixa);
            if (chkImportacaoCategoria.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Importação de Categoria", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Importacao/ImportacaoCategoria.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Apoio ao Cliente" && x.fixa);
            if (chkApoioCliente.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Apoio ao Cliente", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/ApoioCliente.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);


            pagina = repoPaginasControle.FindBy(x => x.nome == "Opçoes de Fretes" && x.fixa);
            if (chkOpcoesFrete.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Opçoes de Fretes", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Fretes.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Destaques" && x.fixa);
            if (chkDestaques.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Destaques", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/Produto/Destaques.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            pagina = repoPaginasControle.FindBy(x => x.nome == "Ratreamento" && x.fixa);
            if (chkRastreamentoCorreios.Checked)
            {
                if (pagina == null)
                {
                    pagina = new PaginaDeControleVO() { nome = "Ratreamento", fixa = true, pagina = MetodosFE.BaseURL + "/Controle/Cadastro/RatreamentoCorreios.aspx" };
                    repoPaginasControle.Add(pagina);
                }
                //pagina.grupoDePaginas = new GrupoDePaginasVO() { id = Convert.ToInt32(ddlControleAcessoGrupo.SelectedValue) };
                repoPaginasControle.Update(pagina);
            }
            else
                if (pagina != null)
                    repoPaginasControle.Delete(pagina);

            MetodosFE.mostraMensagem(nome2 + " alterado com sucesso.", "sucesso");
            this.Limpar();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void Limpar()
    {
        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        MetodosFE.recuperaMensagem();
        nameValues.Remove("Codigo");
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "";
        if (nameValues.Count > 0)
            updatedQueryString = "?" + nameValues.ToString();

        string urlFinal = url + updatedQueryString;
        Response.Redirect(urlFinal, false);
    }

    #region Guardamos o Código no ViewState
    private int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }

    private int Deleta
    {
        get
        {
            if (ViewState["Deleta"] == null) ViewState["Deleta"] = 0;
            return (Int32)ViewState["Deleta"];
        }
        set { ViewState["Deleta"] = value; }
    }
    private int Pagina
    {
        get
        {
            if (Session["Pagina"] == null) Session["Pagina"] = 0;
            return (Int32)Session["Pagina"];
        }
        set { Session["Pagina"] = value; }
    }

    #endregion

}
