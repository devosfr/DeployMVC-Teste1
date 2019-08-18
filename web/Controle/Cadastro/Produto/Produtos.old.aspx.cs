using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controle_Cadastro_produtos : System.Web.UI.Page
{
    private Repository<SegmentoProduto> RepoSegmento
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<SubSegmentoProduto> RepoSubSegmento
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<CategoriaProduto> RepoCategoria
    {
        get
        {
            return new Repository<CategoriaProduto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Tamanho> RepoTamanho
    {
        get
        {
            return new Repository<Tamanho>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Produto> RepoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Destaque> RepositorioDestaques
    {
        get
        {
            return new Repository<Destaque>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<ItemCarrinho> RepoCarrinho
    {
        get
        {
            return new Repository<ItemCarrinho>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<ItemPedido> RepoItemPedido
    {
        get
        {
            return new Repository<ItemPedido>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<DescontoCupom> RepoDescontoCupom
    {
        get
        {
            return new Repository<DescontoCupom>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Album> RepoAlbuns
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Tela> RepoTela
    {
        get
        {
            return new Repository<Tela>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Preco> RepoPreco
    {
        get
        {
            return new Repository<Preco>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Peso> RepoPeso
    {
        get
        {
            return new Repository<Peso>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<InformacaoProduto> RepositorioInformacao
    {
        get
        {
            return new Repository<InformacaoProduto>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Tamanhos> RepoTamanhoCamisas
   {
      get
      {
          return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
      }
   }
   private Repository<Cor> RepoCores
   {
      get
      {
         return new Repository<Cor>(NHibernateHelper.CurrentSession);
      }
   }

   protected void Page_Load(object sender, EventArgs e)
    {
        MaintainScrollPositionOnPostBack = true;
        lblProdutos.Text = "Cadastro de Produtos";

        if (!Page.IsPostBack)
      {
          uplImagemProduto.TamFotoGrH = 512;
          uplImagemProduto.TamFotoGrW = 512;

          uplImagemProduto.TamFotoPqW = 405;
          uplImagemProduto.TamFotoPqH = 405;

          uplImagemProduto.QtdeFotos = 18;

          uplImagemProduto.Qualidade = 100;
          uplImagemProduto.Configuracao = 1;

         //txtDescricaoInformacao.Toolbar = txtDescricao.ToolbarBasic;
         CarregarDestaques();
            if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
            {
                Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                Carregar();
            }
            else
            {
                try
                {
                    Pesquisar();
                    CarregarArvoreSegmentos();                    
                    CarregarArvoreTamanhos();
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                    btnCancelar.Visible = false;
                }
                catch (Exception er)
                {
                    MetodosFE.mostraMensagem(er.Message);
                }
            }
        }
        else
        {
        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }
   
   

    protected void CarregarDestaques() 
    {
        IList<Destaque> destaques = RepositorioDestaques.All().OrderBy(x => x.Nome).ToList();

        //cblDestaques.DataSource = destaques;
        //cblDestaques.DataTextField = "Nome";
        //cblDestaques.DataValueField = "Id";
        //cblDestaques.DataBind();
    }




    protected void Pesquisar()
    {
        try
        {
            string busca = null;

            int id = 0;
            
            var pesquisa = RepoProduto.All();
            
            if (!String.IsNullOrEmpty(txtBuscaID.Text))
            {
                id = Convert.ToInt32(txtBuscaID.Text);
                pesquisa = pesquisa.Where(x=>x.Id==id);
            }

            if (!String.IsNullOrEmpty(txtBuscaNome.Text))
            {
                busca = txtBuscaNome.Text;
                pesquisa = pesquisa.Where(x=>x.Nome.ToLower().Contains(busca.ToLower()));
            }

            var retornoPesquisa = pesquisa.ToList();

            gvDados.PageIndex = Pagina;
            gvDados.DataSourceID = String.Empty;
            gvDados.DataSource = retornoPesquisa;
            gvDados.DataBind();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void Carregar()
    {
        try
      {
         CarregarArvoreTamanhos();
         CarregarArvoreCor();
         CarregarArvoreSegmentos();
            //CarregarDdlTamanho();

            Produto produto = RepoProduto.FindBy(Codigo);

            if (produto != null)
            {
                IList<InformacaoProduto> informacoes = RepositorioInformacao.FilterBy(x => x.Produto.Id == Codigo).ToList();

              
                txtIdDados.Text = produto.Id.ToString();
        
                txtNome.Text = produto.Nome;
                txtDescricao.Text = produto.Descricao;
                txtResumo.Text = produto.Resumo;
                txtreferencia.Text = produto.Referencia;
                chkVisivel.Checked = produto.Visivel;
                chkIndisponivel.Checked = produto.Indisponivel;
                chkDestaque.Checked = produto.Destaque;
                txtPreco.Text = produto.Preco.Valor.ToString();
                txtPrecoDe.Text = produto.Preco.ValorSemPromocao.ToString();
                if (produto.Peso != null)
                {
                    txtPeso.Text = produto.Peso.Valor.ToString();
                    
                }

                txtAltura.Text = produto.Peso.Altura.ToString();
                txtLargura.Text = produto.Peso.Largura.ToString();
                txtProfundidade.Text = produto.Peso.Profundidade.ToString();

             

                btnSalvar.Visible = false;
                btnPesquisar.Visible = false;
                btnAlterar.Visible = true;

                if (Codigo != 0)
                {
                    //uplLogoProd1.Visible = true;
                    //uplLogoProd1.Codigo = Codigo;
                    //uplLogoProd1.reset();

                    //uplLogoProd1.setConfiguracoes(RepoTela.FindBy(x => x.nome.Equals("Produtos")).upload);
                    //uplLogoProd1.Carregar();

                    //try
                    //{
                    //    ControleCores.Codigo = Codigo;
                    //    ControleCores.Carregar();
                    //}
                    //catch (Exception ex)
                    //{

                    //    throw new Exception("Problema com cores. " + ex.Message + ex.InnerException);
                    //}
                }

                //ControleCores.Codigo = Codigo;
                //ControleCores.Carregar();
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (MessageBox.Show("Realmente deseja excluir este produto, junto com suas imagens e demais informações relacionadas?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //{
        try
        {
            //ProdutosxDetalhesBO.DeleteByProduto(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
            //FotosProdutosBO.DeleteByProduto(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
            //CookiesBO.DeleteByProduto(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));
               Produto produto = RepoProduto.FindBy(Convert.ToInt32(gvDados.DataKeys[e.RowIndex].Value));

            var itens = RepoCarrinho.FilterBy(x => x.Produto.Id == produto.Id).ToList();
            RepoCarrinho.Delete(itens);

            var itensPedido = RepoItemPedido.FilterBy(x => x.Produto.Id == produto.Id).ToList();
            if (itensPedido.Count > 0)
                throw new Exception("Este produto já esta dentro de itens de pedidos, não podendo ser excluído.");

            //var descontos = RepoDescontoCupom.FilterBy(x => x.Produto.Id == produto.Id).ToList();
            //if (descontos.Count > 0)
            //    throw new Exception("Este produto já esta dentro de cupons de desconto, não podendo ser excluído.");

            produto.Segmentos.Clear();
            produto.SubSegmentos.Clear();
            produto.Categorias.Clear();
            produto.Cores.Clear();
            produto.Tamanhos.Clear();

            var preco = produto.Preco;
            produto.Preco = null;
            RepoPreco.Delete(preco);

            var peso = produto.Peso;
            produto.Peso = null;
            RepoPeso.Delete(peso);
           
            RepoProduto.Update(produto);

            RepoProduto.Delete(produto);
            MetodosFE.mostraMensagem("Produto \"" + produto.Nome + "\" excluído com sucesso.", "sucesso");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }

        //}

        gvDados.DataBind();
        Pesquisar();
    }

    protected void gvDados_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (Ordenacao.Equals(e.SortExpression))
            Asc = !Asc;
        Ordenacao = e.SortExpression;

        Pesquisar();
    }

    protected void btnPesquisar_Clique(object sender, EventArgs e)
    {
        Pagina = 0;
        Pesquisar();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                throw new Exception("É preciso definir o nome do produto.");
            }
            Produto produto = new Produto();
          
            
            produto.Nome = txtNome.Text.Trim();
            produto.Destaque = chkDestaque.Checked;
            Preco preco = new Preco();
            Peso peso = new Peso();
            RepoPeso.Add(peso);
            RepoPreco.Add(preco);

            produto.Peso = peso;
            produto.Preco = preco;
                        
            RepoProduto.Add(produto);

            AdicionaSegmentos(produto);
            AdicionaCores(produto);
            //AdicionaTamanho(produto);

            Response.Redirect("~/controle/cadastro/Produto/Produtos.aspx?Codigo=" + produto.Id + "", false);
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
            //lblmensagem.Text = ;
        }
    }

    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
           
            Produto produto = RepoProduto.FindBy(Codigo);

            produto.Nome = txtNome.Text.Trim();
            produto.Descricao = HttpUtility.HtmlDecode(txtDescricao.Text.Trim());
            produto.Resumo = HttpUtility.HtmlDecode(txtResumo.Text.Trim());
            produto.Referencia = txtreferencia.Text.Trim();
            produto.Visivel = chkVisivel.Checked;
            produto.Indisponivel = chkIndisponivel.Checked;
            produto.Destaque = chkDestaque.Checked;
            bool tentativa = false;
            if (String.IsNullOrEmpty(txtPreco.Text))
                produto.Preco = null;
            else
            {
                decimal resultado = 0;
                tentativa = Decimal.TryParse(txtPreco.Text, out resultado);
                if (!tentativa)
                    throw new Exception("É preciso definir um preço para o produto.");

                produto.Preco.Valor = resultado;
            }
            
            if (!String.IsNullOrEmpty(txtAltura.Text))
                produto.Peso.Altura = Convert.ToDecimal(txtAltura.Text);
            else
                produto.Peso.Altura = 0;

            if (!String.IsNullOrEmpty(txtProfundidade.Text))
                produto.Peso.Profundidade = Convert.ToDecimal(txtProfundidade.Text);
            else
                produto.Peso.Profundidade = 0;

            if (!String.IsNullOrEmpty(txtLargura.Text))
                produto.Peso.Largura = Convert.ToDecimal(txtLargura.Text);
            else
                produto.Peso.Largura = 0;

            if (!String.IsNullOrEmpty(txtPrecoDe.Text))
                produto.Preco.ValorSemPromocao = Convert.ToDecimal(txtPrecoDe.Text);
            else
                produto.Preco.ValorSemPromocao = 0;

         

         float peso = 0;
            tentativa = float.TryParse(txtPeso.Text, out peso);
            if (!tentativa)
                throw new Exception("É preciso definir um peso para o produto.");

            produto.Peso.Valor = peso;
            //produto.Peso.Tamanho = RepoTamanho.FindBy(Convert.ToInt32(ddlTamanho.SelectedValue));

            produto.Chave = (produto.Id + " " + produto.Nome).ToSeoUrl();

            produto.Destaques.Clear();
           
            RepoProduto.Update(produto);
            AdicionaSegmentos(produto);
            AdicionaTamanho(produto);
            AdicionaCores(produto);

         
            MetodosFE.mostraMensagem("Produto alterado com sucesso.", "sucesso");

            Limpar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Limpar();
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

    private string Ordenacao
    {
        get
        {
            if (ViewState["Ordenacao"] == null)
                ViewState["Ordenacao"] = "id";
            return (string)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
    }

    private bool Asc
    {
        get
        {
            if (ViewState["asc"] == null) ViewState["asc"] = true;
            return (bool)ViewState["asc"];
        }
        set { ViewState["asc"] = value; }
    }

    private int Codigo
    {
        get
        {
            if (ViewState["Codigo"] == null) ViewState["Codigo"] = 0;
            return (Int32)ViewState["Codigo"];
        }
        set { ViewState["Codigo"] = value; }
    }

    private int CodigoInformacao
    {
        get
        {
            if (ViewState["CodigoInformacao"] == null) ViewState["CodigoInformacao"] = 0;
            return (Int32)ViewState["CodigoInformacao"];
        }
        set { ViewState["CodigoInformacao"] = value; }
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

    #endregion Guardamos o Código no ViewState

    protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void gvDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Pagina = e.NewPageIndex;
            Pesquisar();
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }
    
   protected void CarregarArvoreSegmentos()
   {
      try
      {
         tvSegmentos.Nodes.Clear();
         Produto produto = RepoProduto.FindBy(Codigo);
         if (produto == null)
            produto = new Produto();

         IList<SegmentoProduto> segmentosProduto = produto.Segmentos;

         IList<SubSegmentoProduto> subSegmentosProduto = produto.SubSegmentos;

         IList<CategoriaProduto> categoriasProduto = produto.Categorias;

         IList<SegmentoProduto> segmentos = RepoSegmento.All().ToList();


         foreach (var segmento in segmentos)
         {
            TreeNode nodo = new TreeNode();
            nodo.Text = segmento.Nome;
            nodo.Value = segmento.Id.ToString();
            nodo.ToolTip = "Segmento";
            nodo.ImageUrl = "";
            nodo.ImageToolTip = "Imagem do Nodo";

            if (segmentosProduto.Any(x => x.Id == segmento.Id))
               nodo.Checked = true;

            List<SubSegmentoProduto> subSegmentos = segmento.SubSegmentos.ToList();

            foreach (var subSegmento in subSegmentos)
            {
               TreeNode nodoSubSegmento = new TreeNode();
               nodoSubSegmento.Text = subSegmento.Nome;
               nodoSubSegmento.Value = subSegmento.Id.ToString();
               nodoSubSegmento.ToolTip = "SubSegmento";
               nodoSubSegmento.ImageUrl = "";


               if (subSegmentosProduto.Any(x => x.Id == subSegmento.Id))
               {
                  nodoSubSegmento.Checked = true;
               }

               //List<CategoriaProduto> categorias = subSegmento.Categorias.ToList();
               //foreach (var categoria in categorias)
               //{
               //   TreeNode nodoCategoria = new TreeNode();
               //   nodoCategoria.Text = categoria.Nome;
               //   nodoCategoria.Value = categoria.Id.ToString();
               //   nodoCategoria.ToolTip = "Categoria";

               //   nodoCategoria.ImageUrl = "";

               //   if (categoriasProduto.Any(x => x.Id == categoria.Id))
               //   {
               //      nodoCategoria.Checked = true;
               //   }
               //   nodoSubSegmento.ChildNodes.Add(nodoCategoria);
               //}

               nodoSubSegmento.CollapseAll();

               nodo.ChildNodes.Add(nodoSubSegmento);

            }
            nodo.CollapseAll();
            tvSegmentos.Nodes.Add(nodo);

            // }
            //tvPermissoes.Nodes.Add(nodoPai);
         }
      }
      catch (Exception ex)
      {
      }
   }
   public void AdicionaSegmentos(Produto produto)
   {
      try
      {
         produto.Segmentos.Clear();
         produto.SubSegmentos.Clear();
         produto.Categorias.Clear();

         IList<SegmentoProduto> segmentos = RepoSegmento.All().ToList();

         foreach (TreeNode nodo in tvSegmentos.CheckedNodes)
         {
            switch (nodo.ToolTip)
            {
               case "Segmento":


                  int idSegmento = Convert.ToInt32(nodo.Value);

                  var segmento = segmentos.FirstOrDefault(x => x.Id == idSegmento);

                  produto.Segmentos.Add(segmento);

                  break;

            }
         }

         RepoProduto.Update(produto);


      }
      catch (Exception ex)
      {

      }


   }
   
   protected void CarregarArvoreTamanhos()
   {
      try
      {
         treViewTamanhos.Nodes.Clear();
         Produto produto = RepoProduto.FindBy(Codigo);
         if (produto == null)
            produto = new Produto();

         IList <Tamanhos> tamanhoCamisasProduto = produto.Tamanhos;

         IList<Tamanhos> tamanhoCamisas = RepoTamanhoCamisas.All().ToList();

         foreach (var tamanhoCamisa in tamanhoCamisas)
         {
            TreeNode nodo = new TreeNode();
            nodo.Text = tamanhoCamisa.Nome;
            nodo.Value = tamanhoCamisa.Id.ToString();
            nodo.ToolTip = "Tamanhos";
            nodo.ImageUrl = "";
            nodo.ImageToolTip = "Imagem do Nodo";

            if (tamanhoCamisasProduto.Any(x => x.Id == tamanhoCamisa.Id))
               nodo.Checked = true;

            treViewTamanhos.Nodes.Add(nodo);

         }
      }
      catch (Exception ex)
      {
      }
   }
   public void AdicionaTamanho(Produto produto)
   {
      int contador = 0;
      try
      {
         produto.Tamanhos.Clear();
        
         IList<Tamanhos> tamanhos = RepoTamanhoCamisas.All().ToList();

         foreach (TreeNode nodo in treViewTamanhos.CheckedNodes)
         {
            switch (nodo.ToolTip)
            {
               case "Tamanhos":

                  int idTamanho = Convert.ToInt32(nodo.Value);

                  var tamanho = tamanhos.FirstOrDefault(x => x.Id == idTamanho);

                  produto.Tamanhos.Add(tamanho);

                  break;
            }
            contador++;
         }
         if (contador == 0)
         {
             throw new Exception("É preciso definir pelo menos um tamanho para o produto.");
         }

         RepoProduto.Update(produto);

      }
      catch (Exception ex)
      {
          throw new Exception("É preciso definir pelo menos um tamanho para o produto.");
      }


   }
   
   protected void CarregarArvoreCor()
   {
      try
      {
         treViewCores.Nodes.Clear();
         Produto produto = RepoProduto.FindBy(Codigo);
         if (produto == null)
            produto = new Produto();

         IList<Cor> coresProduto = produto.Cores;

         IList<Cor> cores = RepoCores.All().ToList();

         foreach (var cor in cores)
         {
            TreeNode nodo = new TreeNode();
            nodo.Text = cor.Nome;
            nodo.Value = cor.Id.ToString();
            nodo.ToolTip = "Cor";
            nodo.ImageUrl = "";
            nodo.ImageToolTip = "Imagem do Nodo";

            if (coresProduto.Any(x => x.Id == cor.Id))
               nodo.Checked = true;

            treViewCores.Nodes.Add(nodo);

         }
      }
      catch (Exception ex)
      {
      }
   }
   public void AdicionaCores(Produto produto)
   {
      try
      {
         produto.Cores.Clear();

         IList<Cor> cores = RepoCores.All().ToList();

         foreach (TreeNode nodo in treViewCores.CheckedNodes)
         {
            switch (nodo.ToolTip)
            {
                  case "Cor":

                  int idCor = Convert.ToInt32(nodo.Value);

                  var cor = cores.FirstOrDefault(x => x.Id == idCor);

                  produto.Cores.Add(cor);

                  break;
            }
         }

         RepoProduto.Update(produto);

      }
      catch (Exception ex)
      {

      }


   }
}