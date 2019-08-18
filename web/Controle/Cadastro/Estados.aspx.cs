using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Modelos;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    private Repository<Estado> repoEstado 
    {
        get 
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (!String.IsNullOrEmpty(Request.QueryString["Codigo"]))
                {
                    Codigo = Convert.ToInt32(Request.QueryString["Codigo"].ToString());
                    Carregar();
                    Pesquisar("nome");
                }
                else
                {
                    Pesquisar("nome");
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
                    btnSalvar.Visible = true;
                }
            }
            else
            {
                Pesquisar("nome");
                btnAlterar.Visible = false;
                btnPesquisar.Visible = true;
                btnSalvar.Visible = true;
            }
        }

        protected void Pesquisar(string ordenacao)
        {
            try
            {
                IList<Estado> colecaoEstado = repoEstado.All().OrderBy(ordenacao).ToList();


                    GridView1.DataSourceID = String.Empty;
                    GridView1.DataSource = colecaoEstado;
                    GridView1.DataBind();

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

                Estado colecaoEstado = repoEstado.FindBy(Codigo);

                if (colecaoEstado!=null)
                {
                    txtEstado.Text = colecaoEstado.Nome;
                    txtSigla.Text = colecaoEstado.Sigla;
                    txtId.Text = colecaoEstado.Id.ToString();

                    btnSalvar.Visible = false;
                    btnPesquisar.Visible = false;
                    btnAlterar.Visible = true;
                }
            }
            catch (Exception er)
            {
                MetodosFE.mostraMensagem(er.Message);
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar("nome");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Estado estado = new Estado();
                if ((txtEstado.Text != "") && (txtSigla.Text != ""))
                {
                    estado.Nome = txtEstado.Text.Trim();
                    estado.Sigla = txtSigla.Text.Trim();


                    repoEstado.Add(estado);
                    MetodosFE.mostraMensagem("Estado "+estado.Nome+" cadastrado com sucesso.", "sucesso");
                    this.Limpar();
                }
                else
                {
                    MetodosFE.mostraMensagem(" Estado e Sigla são campos Obrigatórios.");
                }
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
                Estado estado = repoEstado.FindBy(Codigo);
                if ((txtEstado.Text != "") && (txtSigla.Text != ""))
                {
                    estado.Id = Convert.ToInt32(txtId.Text);
                    estado.Nome = txtEstado.Text.Trim();
                    estado.Sigla = txtSigla.Text.Trim();


                    repoEstado.Update(estado);
                    MetodosFE.mostraMensagem("Dados alterados com sucesso.", "sucesso");
                    this.Limpar();
                }
                else
                {
                    MetodosFE.mostraMensagem("Estado e Sigla são campos Obrigatórios.");
                }
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
                Pesquisar("nome");
            }
            catch (Exception er)
            {
                MetodosFE.mostraMensagem(er.Message);
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string ordenacao = e.SortExpression;
            Pesquisar(ordenacao);
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

        protected virtual void Page_LoadComplete(object sender, EventArgs e)
        {
            string mensagem = MetodosFE.confereMensagem();
            litErro.Text = mensagem != null ? mensagem : "";
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

        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            Pesquisar("nome");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Estado cor = repoEstado.FindBy(Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
                repoEstado.Delete(cor);
                MetodosFE.mostraMensagem("Estado " + cor.Nome + " alterado com sucesso.", "sucesso");
                Limpar();
            }
            catch (Exception er)
            {
                MetodosFE.mostraMensagem(er.Message);
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect(this.AppRelativeVirtualPath + "?Codigo=" + Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value), false);
        }
}
