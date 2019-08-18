using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Categorias : System.Web.UI.Page
{
   private Repository<Cliente> repoCliente
   {
      get
      {
         return new Repository<Cliente>(NHibernateHelper.CurrentSession);
      }
   }
   private Repository<Endereco> repoEndereco
   {
      get
      {
         return new Repository<Endereco>(NHibernateHelper.CurrentSession);
      }
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      if (!Page.IsPostBack)
      {

         txtID.Enabled = false;
         if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
         {
            Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
            Carregar();
            Pesquisar("id");
         }
         else
         {
            try
            {
               Pesquisar("id");
               //CarregarDropSegmentoFilho();  
               btnAlterar.Visible = false;
               btnPesquisar.Visible = true;
               btnSalvar.Visible = true;
            }
            catch (Exception er)
            {
               MetodosFE.mostraMensagem(er.Message);
            }
         }
      }
   }
   protected virtual void Page_LoadComplete(object sender, EventArgs e)
   {
      string mensagem = MetodosFE.confereMensagem();
      litErro.Text = mensagem != null ? mensagem : "";
   }



   protected void Pesquisar(string ordenacao)
   {
      try
      {
         var pesquisa = repoCliente.All();

         int id = 0;
         if (!String.IsNullOrEmpty(txtBuscaID.Text))
         {
            id = Convert.ToInt32(txtBuscaID.Text);
            pesquisa = pesquisa.Where(x => x.Id == id);
         }
         string nome = null;
         if (!String.IsNullOrEmpty(txtBuscaNome.Text))
         {
            nome = txtBuscaNome.Text;
            pesquisa = pesquisa.Where(x => x.Nome != null && x.Nome.ToLower().Contains(nome.ToLower()));
         }
         string CPFCNPJ = null;
         if (!String.IsNullOrEmpty(txtBuscaCPFCNPJ.Text))
         {
            CPFCNPJ = txtBuscaCPFCNPJ.Text;
            pesquisa = pesquisa.Where(x => (x.CPF != null && x.CPF.ToLower().Contains(CPFCNPJ.ToLower())) || (x.CNPJ != null && x.CNPJ.ToLower().Contains(CPFCNPJ.ToLower())));
         }


         IList<Cliente> colecaoSegmento = pesquisa.OrderBy(ordenacao).ToList();

         gvSegmento.DataSourceID = String.Empty;
         gvSegmento.DataSource = colecaoSegmento;
         gvSegmento.DataBind();

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


         Cliente cliente = null;

         cliente = repoCliente.FindBy(Codigo);


         if (cliente != null)
         {
            txtID.Text = cliente.Id.ToString();
            txtNome.Text = cliente.Nome;
            txtCPF.Text = cliente.CPF;
            txtEmail.Text = cliente.Email;
            txtTelefone.Text = cliente.Telefone;


            Endereco.carregarEndereco(cliente.Id);
            txtObservacoes.Text = cliente.Observacoes;
            ddlStatus.SelectedValue = cliente.Status;
            txtNumeroSerie.Text = cliente.numeroSerieProduto;

            ddlProduto.SelectedValue = cliente.nomeProduto;

            btnSalvar.Visible = false;
            btnPesquisar.Visible = false;
            btnAlterar.Visible = true;
            txtCPF.Enabled = false;
         }
      }
      catch (Exception er)
      {
         MetodosFE.mostraMensagem(er.Message);
      }
   }

   protected void gvSegmento_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {
      try
      {
         repoCliente.Delete(repoCliente.FindBy(Convert.ToInt32(gvSegmento.DataKeys[e.RowIndex].Value)));
      }
      catch (Exception er)
      {
         MetodosFE.mostraMensagem(er.Message);
      }


      gvSegmento.DataBind();
      Pesquisar("id");
   }

   protected void gvSegmento_Sorting(object sender, GridViewSortEventArgs e)
   {
      string ordenacao = e.SortExpression;
      Pesquisar(ordenacao);
   }
   protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
   {

      Codigo = Convert.ToInt32(gvSegmento.DataKeys[e.NewEditIndex].Value);

      var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
      nameValues.Set("Codigo", Codigo.ToString());
      string url = Request.Url.AbsolutePath;
      //nameValues.Remove("Codigo");
      string updatedQueryString = "?" + nameValues.ToString();
      string urlFinal = url + updatedQueryString;
      e.Cancel = true;
      Response.Redirect(urlFinal, false);

      //Carregar();

   }

   protected void btnPesquisar_Click(object sender, EventArgs e)
   {
      Pesquisar("id");
   }

   protected void btnSalvar_Click(object sender, EventArgs e)
   {

      int idEndereco = 0;
      try
      {
         Cliente cliente = new Cliente();


         cliente.Nome = txtNome.Text;
         cliente.Observacoes = HttpUtility.HtmlDecode(txtObservacoes.Text);
         cliente.Status = ddlStatus.SelectedValue;
         cliente.CPF = txtCPF.Text.Replace('_', ' ').Trim();
         //cliente.idLoja = ControleLoja.getIDLoja();

         //cliente.dtpagamento = txtDataPagamento.Text;

         repoCliente.Add(cliente);
         Limpar();

      }
      catch (Exception er)
      {
         if (idEndereco > 0)
            repoEndereco.Delete(repoEndereco.FindBy(idEndereco));
         MetodosFE.mostraMensagem(er.Message);


      }
   }

   protected void btnAlterar_Click(object sender, EventArgs e)
   {
      try
      {
         Cliente cliente = repoCliente.FindBy(Convert.ToInt32(txtID.Text));


         cliente.Nome = txtNome.Text;
         cliente.CPF = txtCPF.Text.Replace('_', ' ').Trim();
         cliente.Observacoes = HttpUtility.HtmlDecode(txtObservacoes.Text);
         //cliente.idLoja = ControleLoja.getIDLoja();
         cliente.Status = ddlStatus.SelectedValue;


         if (!string.IsNullOrEmpty(ddlProduto.SelectedItem.Text))
         {
            if (!ddlProduto.SelectedItem.Text.Equals("Selecione o Produto"))
            {
               cliente.nomeProduto = ddlProduto.SelectedItem.Text;
               cliente.numeroSerieProduto = txtNumeroSerie.Text;
            }
         }

         //cliente.perccomissao = Convert.ToInt32(txtPercComissao.Text);

         //cliente.dtpagamento = txtDataPagamento.Text;

         repoCliente.Update(cliente);

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
   #endregion

   protected void gvSegmento_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {

      }
   }

   protected void gvSegmento_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {
      try
      {
         gvSegmento.PageIndex = e.NewPageIndex;
         Pesquisar("id");
      }
      catch (Exception er)
      {
         MetodosFE.mostraMensagem(er.Message);
      }
   }
}
