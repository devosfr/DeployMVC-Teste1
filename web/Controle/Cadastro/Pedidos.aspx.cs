using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Modelos;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq.Dynamic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using Excel;
using ExcelLibrary;

public partial class Controle_Cadastro_produtos : System.Web.UI.Page
{
    private Repository<Pedido> RepositorioPedido
    {
        get
        {
            return new Repository<Pedido>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<ItemPedido> RepositorioItemPedido
    {
        get
        {
            return new Repository<ItemPedido>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Cliente> RepositorioCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<MovimentoDeConta> RepositorioComissao
    {
        get
        {
            return new Repository<MovimentoDeConta>(NHibernateHelper.CurrentSession);
        }
    }

    public static DateTime agora = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CarregarDdlCliente();
            CarregaDdlSituacoes();
            CarregaDdlFormasPagamento();

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
                    btnAlterar.Visible = false;
                    btnPesquisar.Visible = true;
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

    //Metodo para colocar a informação do Pedido em edição na página
    protected void Carregar()
    {
        try
        {
            Pedido pedido = RepositorioPedido.FindBy(Codigo);

            if (pedido != null)
            {
                preencherCamposPedido(pedido);
                btnPesquisar.Visible = false;
                btnAlterar.Visible = true;
            }
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void CarregaDdlSituacoes()
    {
        IList<System.Web.UI.WebControls.ListItem> situacoes = new List<System.Web.UI.WebControls.ListItem>();
        foreach (var item in Enum.GetValues(typeof(Pedido.situacoes)))
        {

            situacoes.Add(new System.Web.UI.WebControls.ListItem() { Value = ((int)item).ToString(), Text = item.ToString() });
        }
        situacoes.Insert(0, new System.Web.UI.WebControls.ListItem() { Value = "", Text = "Selecione" });

        ddlSituacao.DataSource = situacoes;
        ddlSituacao.DataValueField = "Value";
        ddlSituacao.DataTextField = "Text";
        ddlSituacao.DataBind();

        ddlBuscaStatus.DataSource = situacoes;
        ddlBuscaStatus.DataValueField = "Value";
        ddlBuscaStatus.DataTextField = "Text";
        ddlBuscaStatus.DataBind();

    }

    protected void CarregaDdlFormasPagamento()
    {
        IList<System.Web.UI.WebControls.ListItem> formasDePagamento = new List<System.Web.UI.WebControls.ListItem>();
        foreach (var item in Enum.GetValues(typeof(Pedido.FormasDePagamento)))
        {
            formasDePagamento.Add(new System.Web.UI.WebControls.ListItem() { Value = ((int)item).ToString(), Text = item.ToString() });
        }
        formasDePagamento.Insert(0, new System.Web.UI.WebControls.ListItem() { Value = "", Text = "Selecione" });

        //ddlTipoPagamento.DataSource = formasDePagamento;
        //ddlTipoPagamento.DataValueField = "Value";
        //ddlTipoPagamento.DataTextField = "Text";
        //ddlTipoPagamento.DataBind();

        ddlBuscaFormasDePagamento.DataSource = formasDePagamento;
        ddlBuscaFormasDePagamento.DataValueField = "Value";
        ddlBuscaFormasDePagamento.DataTextField = "Text";
        ddlBuscaFormasDePagamento.DataBind();

    }

    protected void CarregarDdlCliente()
    {

        var clientes = RepositorioCliente.All().OrderBy(x => x.Nome);

        ddlNomeCliente.DataSource = clientes;
        ddlNomeCliente.DataValueField = "Id";
        ddlNomeCliente.DataTextField = "Nome";
        ddlNomeCliente.DataBind();
        ddlNomeCliente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));

        ddlBuscaCliente.DataSource = clientes;
        ddlBuscaCliente.DataValueField = "Id";
        ddlBuscaCliente.DataTextField = "Nome";
        ddlBuscaCliente.DataBind();
        ddlBuscaCliente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));
    }

    protected void preencherCamposPedido(Pedido pedido)
    {
        if (pedido != null)
        {
            txtIdPedido.Text = pedido.Id.ToString();
            ddlNomeCliente.SelectedValue = pedido.Cliente.Id.ToString();
            ddlSituacao.SelectedValue = pedido.Status.ToString();
            txtValorTotal.Text = pedido.GetTotalPedido().ToString("C");
            txtDataPedido.Text = pedido.DataPedido.ToString("d");
            txtTipoFrete.Text = pedido.ModoFrete;
            txtPrecoFrete.Text = pedido.PrecoFrete.ToString("C");
            //txtRastreamento.Text = pedido.Rastreamento;
            //if (pedido.Cupom != null)
            //    txtCupom.Text = pedido.Cupom.Codigo;

            //ddlTipoPagamento.SelectedValue = pedido.FormaDePagamento.ToString();

            if (pedido.FormaDePagamento == (int)Pedido.FormasDePagamento.Deposito)
            {
                txtInformacoes.Text = pedido.InformacoesDeposito.CortarTextoLimpo(1000);
                txtBanco.Text = pedido.BancoDeposito;

                if (!String.IsNullOrEmpty(pedido.ComprovanteDeposito))
                    linkComprovante.HRef = pedido.ComprovanteDeposito;
                else
                    linkComprovante.Visible = false;
            }
            else
            {
                liBanco.Visible = false;
                liInformacoes.Visible = false;
            }

            PesquisarItensPedido();

        }
    }

    protected void Pesquisar()
    {
        try
        {
            var pesquisa = RepositorioPedido.All().Where(x => x.Status != 5);

            int id = 0;
            if (!String.IsNullOrEmpty(txtBuscaID.Text))
            {
                id = Convert.ToInt32(txtBuscaID.Text);
                pesquisa = pesquisa.Where(x => x.Id == id);
            }

            if (!String.IsNullOrEmpty(ddlBuscaCliente.SelectedValue))
            {
                id = Convert.ToInt32(ddlBuscaCliente.SelectedValue);
                pesquisa = pesquisa.Where(x => x.Cliente.Id == id);
            }

            if (!String.IsNullOrEmpty(ddlBuscaStatus.SelectedValue))
            {
                id = Convert.ToInt32(ddlBuscaStatus.SelectedValue);
                var pesquisaStatus = RepositorioPedido.All();
                pesquisa = pesquisaStatus.Where(x => x.Status == id);
            }

            if (!String.IsNullOrEmpty(ddlBuscaFormasDePagamento.SelectedValue))
            {
                id = Convert.ToInt32(ddlBuscaFormasDePagamento.SelectedValue);
                pesquisa = pesquisa.Where(x => x.FormaDePagamento == id);
            }

            IList<Pedido> pedidos = pesquisa.OrderByDescending(x => x.Id).ToList();

            //if (colecaoPedidos.Count > 0)

            gvDados.DataSourceID = String.Empty;
            gvDados.DataSource = pedidos;
            gvDados.DataBind();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    protected void gvDados_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenacao = e.SortExpression;
        Pesquisar();
    }

    //Método para mudar os dados de um registro Pedido já existente
    protected void btnAlterar_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = RepositorioPedido.FindBy(Codigo);

            bool mudaStatus = false;
            int statusSelecionado = Convert.ToInt32(ddlSituacao.SelectedValue);
            if (pedido.Status != statusSelecionado)
                mudaStatus = true;
            pedido.Status = statusSelecionado;
            pedido.ModoFrete = txtTipoFrete.Text;
            //pedido.Rastreamento = txtRastreamento.Text;
            if (!String.IsNullOrEmpty(txtPrecoFrete.Text.Trim()))
            {
                //string valor = txtValorTotal.Text.Trim().Replace("R$", "");
                pedido.PrecoFrete = Convert.ToDecimal(txtPrecoFrete.Text.Trim().Replace("R$", ""));
            }
            else
            {
                pedido.PrecoFrete = 0;
            }

            RepositorioPedido.Update(pedido);

            if (mudaStatus)
            {

                //if (pedido.Status == (int)Pedido.situacoes.Separando && !pedido.Cliente.PossuiCupom())
                //    ControleCupom.CriarCupom(pedido.Cliente, pedido.Cupom);
                //if (pedido.Status == (int)Pedido.situacoes.Separando && pedido.Cupom != null)
                //    ControleComissao.AdicionarComissoes(pedido);

                enviaEmailStatus(pedido.Id);
            }

            MetodosFE.mostraMensagem("Alteração concluída com sucesso.", "sucesso");
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
            btnAlterar.Visible = false;
            btnPesquisar.Visible = true;
            //btnSalvar.Visible = true;
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }




    protected void Limpar()
    {

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Remove("Codigo");
        string url = Request.Url.AbsolutePath;
        //nameValues.Remove("Codigo");
        string updatedQueryString = "";
        if (nameValues.Count > 0)
            updatedQueryString = "?" + nameValues.ToString();
        MetodosFE.recuperaMensagem();
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
    private string Ordenacao
    {
        get
        {
            if (ViewState["Ordenacao"] == null) ViewState["Ordenacao"] = "";
            return (String)ViewState["Ordenacao"];
        }
        set { ViewState["Ordenacao"] = value; }
    }
    #endregion

    protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    public string getPreco(ItemPedido item)
    {
        //if (item.Pedido.FormaDePagamento == 2 || item.Pedido.FormaDePagamento == 3)
        //{
        //    return (item.Produto.Preco.ValorAvista * item.Quantidade).ToString();
        //}
        //else
        //{
        //    return item.GetTotal().ToString("C");
        //}

        return item.GetTotal().ToString("C");
    }

    protected void gvDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDados.PageIndex = e.NewPageIndex;
            Pesquisar();

        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
    }

    #region Controle da Lista de Itens do Pedido
    protected void PesquisarItensPedido()
    {
        BindData();
        gvItensPedido.Visible = true;
    }


    protected void TaskGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Set the edit index.
        gvItensPedido.EditIndex = e.NewEditIndex;
        //Bind data to the GridView control.
        BindData();
    }

    protected void TaskGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Reset the edit index.
        gvItensPedido.EditIndex = -1;
        //Bind data to the GridView control.
        BindData();
    }

    protected void TaskGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Update the values.
        GridViewRow row = gvItensPedido.Rows[e.RowIndex];

        int idItemPedido = Convert.ToInt32(e.Keys[0]);
        ItemPedido item = RepositorioItemPedido.FindBy(idItemPedido);
        {
            //  int idProduto = Convert.ToInt32(((TextBox)(row.Cells[2].Controls[0])).Text);
            int quantidade = Convert.ToInt32(((TextBox)(row.Cells[8].Controls[0])).Text);
            //ItemPedido itemPedido = ItemPedidoBO.FindByID(id: idItemPedido);
            //itemdido.idProduto = dados.id;
            item.Quantidade = quantidade;
            RepositorioItemPedido.Update(item);
        }

        gvItensPedido.EditIndex = -1;

        //Bind data to the GridView control.
        //BindData();
        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }

    private void BindData()
    {
        //gvItensPedido.DataSource = Session["tabelaItens"];

        gvItensPedido.DataSource = RepositorioPedido.FindBy(Codigo).Itens;
        gvItensPedido.DataBind();
        if (gvItensPedido.HeaderRow != null)
            gvItensPedido.HeaderRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#E9E4D1");
    }

    protected void TaskGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //calcular novo total
        int idItemPedido = Convert.ToInt32(e.Keys[0]);
        ItemPedido item = RepositorioItemPedido.FindBy(idItemPedido);
        RepositorioItemPedido.Delete(item);
        //cookieBO.Delete(cookie);
        PesquisarItensPedido();
        //Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }


    #endregion

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
        //MetodosFE.limparCampos(this);
        //int i = 1;
    }


    #region Edicao da lista de pedidos

    //Acionar edição da linha
    protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Set the edit index.
        gvDados.EditIndex = e.NewEditIndex;
        Pesquisar();

        GridViewRow row = gvDados.Rows[e.NewEditIndex];

        IList<System.Web.UI.WebControls.ListItem> situacoes = new List<System.Web.UI.WebControls.ListItem>();
        foreach (var item in Enum.GetValues(typeof(Pedido.situacoes)))
        {

            situacoes.Add(new System.Web.UI.WebControls.ListItem() { Value = ((int)item).ToString(), Text = item.ToString() });
        }
        //situacoes.Insert(0, new System.Web.UI.WebControls.ListItem() { Value = "", Text = "Selecione" });

        DropDownList ddlStatusPedido = (DropDownList)row.Cells[3].FindControl("ddlStatusPedido");

        ddlStatusPedido.DataSource = situacoes;
        ddlStatusPedido.DataValueField = "Value";
        ddlStatusPedido.DataTextField = "Text";
        ddlStatusPedido.DataBind();

        //Bind data to the GridView control.

    }

    //Cancelar edição da linha
    protected void gvDados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Reset the edit index.
        gvDados.EditIndex = -1;
        //Bind data to the GridView control.
        Pesquisar();
    }

    //Confirmar edição da linha
    protected void gvDados_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //Update the values.
            GridViewRow row = gvDados.Rows[e.RowIndex];
            //dt.Rows[gvItensPedido.EditIndex]["nome"] = ((string)(row.Cells[4].Text)).ToString();
            int status_pedido = Convert.ToInt32(((DropDownList)(row.Cells[3].FindControl("ddlStatusPedido"))).SelectedValue);
            int id = Convert.ToInt32(gvDados.Rows[e.RowIndex].Cells[0].Text);
            //dt.Rows[gvItensPedido.EditIndex]["valor_unitario"] = ((TextBox)(row.Cells[4].Controls[0])).Text;
            //dt.Rows[gvItensPedido.EditIndex]["qtde_produto"] = ((TextBox)(row.Cells[6].Controls[0])).Text;
            //dt.Rows[gvItensPedido.EditIndex]["valor_total"] = ((TextBox)(row.Cells[7].Controls[0])).Text;

            //Update the values.        

            Pedido pedido = RepositorioPedido.FindBy(id);
            if (pedido != null)
            {
                if (pedido.Status != status_pedido)
                {
                    pedido.Status = status_pedido;
                    RepositorioPedido.Update(pedido);

                    //if (pedido.Status == (int)Pedido.situacoes.Separando && !pedido.Cliente.PossuiCupom())
                    //    ControleCupom.CriarCupom(pedido.Cliente, pedido.Cupom);
                    //if (pedido.Status == (int)Pedido.situacoes.Separando && pedido.Cupom != null)
                    //    ControleComissao.AdicionarComissoes(pedido);

                    string email = enviaEmailStatus(pedido.Id);

                    MetodosFE.mostraMensagem("Status de pedido nº " + pedido.Id + " alterado com sucesso e aviso emitido para " + email + ".", "sucesso");
                }
            }

            gvItensPedido.EditIndex = -1;

            //Bind data to the GridView control.
            //Pesquisar("");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.Message);
        }
        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
    }
    #endregion




    protected void ExportToPDFWithFormatting(int idPedido)
    {

        Pedido pedido = RepositorioPedido.FindBy(idPedido);
        Cliente cliente = pedido.Cliente;


        //link button column is excluded from the list
        int colCount = gvItensPedido.Columns.Count - 4;



        //Create a table
        PdfPTable table = new PdfPTable(colCount);
        table.HorizontalAlignment = 0;
        table.WidthPercentage = 100;
        var colWidthPercentages = new[] { 10f, 30f, 15f, 10f, 10f, 10f, 10f, 10f };
        table.SetWidths(colWidthPercentages);
        //create an array to store column widths
        //int[] colWidths = new int[gvDados.Columns.Count];

        PdfPCell cell;
        string cellText;
        //create the header row
        for (int colIndex = 2; colIndex < colCount + 2; colIndex++)
        {
            //set the column width
            //colWidths[colIndex] = (int)gvDados.Columns[colIndex].ItemStyle.Width.Value;

            //fetch the header text
            cellText = Server.HtmlDecode(gvItensPedido.HeaderRow.Cells[colIndex].Text);

            //create a new cell with header text
            cell = new PdfPCell(new Phrase(cellText));

            //set the background color for the header cell
            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#777777"));
            if (colIndex == 6 || colIndex == 8)
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            if (colIndex == 4 || colIndex == 5)
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            if (colIndex == 7)
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //add the cell to the table. we dont need to create a row and add cells to the row
            //since we set the column count of the table to 4, it will automatically create row for
            //every 4 cells
            table.AddCell(cell);
        }

        //export rows from GridView to table
        for (int rowIndex = 0; rowIndex < gvItensPedido.Rows.Count; rowIndex++)
        {
            if (gvItensPedido.Rows[rowIndex].RowType == DataControlRowType.DataRow)
            {
                for (int j = 2; j < gvItensPedido.Columns.Count - 2; j++)
                {
                    //fetch the column value of the current row
                    cellText = Server.HtmlDecode(gvItensPedido.Rows[rowIndex].Cells[j].Text);
                    if (String.IsNullOrEmpty(cellText))
                        cellText = Server.HtmlDecode(((DataBoundLiteralControl)gvItensPedido.Rows[rowIndex].Cells[j].Controls[0]).Text.Trim());

                    //create a new cell with column value
                    cell = new PdfPCell(new Phrase(cellText));
                    if (j == 6 || j == 8)
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    if (j == 4 || j == 5)
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    if (j == 7)
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                    //Set Color of Alternating row
                    if (rowIndex % 2 != 0)
                    {
                        cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
                    }
                    else
                    {
                        cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#CCCCCC"));
                    }
                    //add the cell to the table
                    table.AddCell(cell);
                }
            }
        }

        Paragraph numeroPedido = new Paragraph("Nº do Pedido: " + idPedido);
        Paragraph nome = new Paragraph("Nome: " + cliente.Nome);
        Paragraph cpfcnpj = new Paragraph("CPF/CNPJ: " + cliente.CPF);
        Paragraph email = new Paragraph("Email: " + cliente.Email);
        Paragraph endereco = new Paragraph("Endereço: " + pedido.Endereco.Logradouro);
        Paragraph numero = new Paragraph("Número: " + pedido.Endereco.Numero);
        Paragraph complemento = new Paragraph("Complemento: " + pedido.Endereco.Complemento);
        Paragraph bairro = new Paragraph("Bairro: " + pedido.Endereco.Bairro);
        Paragraph CEP = new Paragraph("CEP: " + pedido.Endereco.CEP);
        Paragraph Estado = new Paragraph("Estado: " + pedido.Endereco.Estado.Nome);
        Paragraph Cidade = new Paragraph("Cidade: " + pedido.Endereco.Cidade.Nome);
        Paragraph Fone1 = new Paragraph("Fone 1: " + cliente.Telefone);
        //Paragraph Fone2 = null;
        //if (!String.IsNullOrEmpty(cliente.contato.fone2))
        //    Fone2 = new Paragraph("Fone 2:" + cliente.contato.fone2);
        decimal ss = 0;
        if (pedido.FormaDePagamento == 2 || pedido.FormaDePagamento == 3)
        {
            foreach (ItemPedido x in pedido.Itens)
            {
                ss += x.Produto.Preco.ValorAvista;
            }
            ss += pedido.PrecoFrete;
        }
        else
        {
            ss = pedido.GetTotalPedido();
        }

        Paragraph Total = new Paragraph("Total: R$ " + ss.ToString("N"));

        //UsuarioVO usuario = null;
        //if (pedido.idVendedor > 0)
        //    usuario = UsuarioBO.FindByID(pedido.idVendedor);
        //Paragraph Vendedor = new Paragraph("Código: " + pedido.codigo + ", Desconto: " + pedido.desconto.ToString("N") + "%, Vendedor: " + (usuario == null ? "Nenhum" : usuario.nomeCliente));
        Paragraph Frete = new Paragraph("Tipo de Frete: " + (String.IsNullOrEmpty(pedido.ModoFrete) ? "Não definido" : pedido.ModoFrete) + ", Preço do Frete: R$ " + pedido.PrecoFrete.ToString("N"));
        //Vendedor.Alignment = Element.ALIGN_RIGHT;
        Total.Alignment = Element.ALIGN_RIGHT;
        Frete.Alignment = Element.ALIGN_RIGHT;


        //Create the PDF Document
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //open the stream
        pdfDoc.Open();


        //pdfDoc.Add(titulo);
        //pdfDoc.Add(new Paragraph(" "));

        pdfDoc.Add(numeroPedido);
        pdfDoc.Add(nome);
        pdfDoc.Add(cpfcnpj);
        pdfDoc.Add(email);
        pdfDoc.Add(endereco);
        if (numero != null)
            pdfDoc.Add(numero);
        if (complemento != null)
            pdfDoc.Add(complemento);
        pdfDoc.Add(bairro);
        pdfDoc.Add(CEP);
        pdfDoc.Add(Estado);
        pdfDoc.Add(Cidade);
        pdfDoc.Add(Fone1);
        //   if (Fone2 != null)
        //     pdfDoc.Add(Fone2);
        pdfDoc.Add(new Paragraph(" "));
        //add the table to the document
        pdfDoc.Add(table);


        pdfDoc.Add(new Paragraph(" "));
        pdfDoc.Add(Total);
        pdfDoc.Add(new Paragraph(" "));
        //pdfDoc.Add(Vendedor);
        pdfDoc.Add(Frete);





        //close the document stream
        pdfDoc.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;" + "filename=Pedido.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void btnExportarPedido_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtIdPedido.Text))
            ExportToPDFWithFormatting(Convert.ToInt32(txtIdPedido.Text));
        else
            MetodosFE.mostraMensagem("Nenhum pedido selecionado.");
    }

    protected string enviaEmailStatus(int codigo)
    {
        EnvioEmailsVO envio = new EnvioEmailsVO();

        DadoVO dado = MetodosFE.getTela("Configurações de SMTP");
        Pedido pedido = RepositorioPedido.FindBy(codigo);

        string email = "";

        if (dado != null && pedido.Status != 5)
        {

            Cliente cliente = pedido.Cliente;

            email = cliente.Email;

            envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = pedido.Cliente.Email;
            envio.assuntoMensagem = Configuracoes.getSetting("NomeSite") + " - Mudança de status - Pedido Nº " + pedido.Id;

            //Dictionary<string, string> valores = new Dictionary<string, string>();

            //valores.Add("[ID]", pedido.Id.ToString());
            //valores.Add("[Nome]", pedido.Cliente.Nome);
            //valores.Add("[Valor]", pedido.GetTotalPedido().ToString("C"));
            //valores.Add("[Data]", pedido.DataPedido.ToShortDateString());
            //valores.Add("[Status]", pedido.GetStatusPedido().ToUpper());
            ////valores.Add("[Comentarios]", txtComentarios.Text);
            ////valores.Add("[NomeEmpresa]", Configuracoes.getSetting("NomeSite"));


            string mensagem = "";
            mensagem += "Seu pedido acaba de mudar de status. Confira abaixo mais detalhes:<br/><br/>";

            mensagem += "O seu pedido se encontra na seguinte situação:<br/>";

            switch (pedido.Status)
            {
                case 1:
                    mensagem += "Aguardando a confirmação de pagamento.<br/>";
                    break;
                case 2:
                    mensagem += "Separando mercadoria para envio.<br/>";
                    break;
                case 3:
                    mensagem += "Pedido enviado.<br/>";
                    break;
                case 4:
                    mensagem += "Pedido entregue.<br/>";
                    break;
                //case 5: mensagem += "Pedido cancelado.<br/>";
                //    break;
            }


            envio.conteudoMensagem = mensagem;

            bool vrecebe = EnvioEmails.envioemails(envio);

            if (!vrecebe)
                throw new Exception("Problemas ocorreram no envio de e-mail.");

            return email;
        }

        return email;
    }


    public string MontaTemplate(string template, Dictionary<string, string> valores)
    {
        string retorno = template;
        foreach (KeyValuePair<string, string> entrada in valores)
            retorno = retorno.Replace(entrada.Key, entrada.Value);
        return retorno;
    }

    #region CRIRAR EXCEL
    public void exportarExcel()
    {
        //CRIAR DATA TABLE DO PEDIDO
        DataTable table = new DataTable();

        table.Columns.Add("ID");
        table.Columns.Add("Cliente");
        table.Columns.Add("Valor Total");
        table.Columns.Add("Status");
        table.Columns.Add("Data do Pedido");

        IList<Pedido> pesquisa = RepositorioPedido.All().Where(x => x.Status != 5).ToList();

        foreach (Pedido pedido in pesquisa)
        {
            DataRow dr = table.NewRow();

            dr[0] = pedido.Id.ToString();
            dr[1] = pedido.Cliente.Nome;
            dr[2] = pedido.GetTotalPedido();
            dr[3] = pedido.GetStatusPedido();
            dr[4] = pedido.DataPedido.ToShortDateString();

            table.Rows.Add(dr);
        }

        if (table.Rows.Count > 0)
        {
            string filename = "pedidos_" + agora.Day + "-" + agora.Month + "-" + agora.Year + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = table;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }

        //string attachment = "attachment; filename=pedidos.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/vnd.ms-excel";
        //string tab = "";
        //foreach (DataColumn dc in table.Columns)
        //{
        //    Response.Write(tab + dc.ColumnName);
        //    tab = "\t";
        //}
        //Response.Write("\n");
        //int i;
        //foreach (DataRow dr in table.Rows)
        //{
        //    tab = "";
        //    for (i = 0; i < table.Columns.Count; i++)
        //    {
        //        Response.Write(tab + dr[i].ToString());
        //        tab = "\t";
        //    }
        //    Response.Write("\n");
        //}
        //Response.End();

        //Response.Clear();
        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("pedidos_" + agora.Day + "-" + agora.Month + "-" + agora.Year + ".xls", System.Text.Encoding.UTF8));

        //using (OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage())
        //{
        //    OfficeOpenXml.ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Logs");
        //    ws.Cells["A1"].LoadFromDataTable(table, true);
        //    var ms = new System.IO.MemoryStream();
        //    pck.SaveAs(ms);
        //    ms.WriteTo(Response.OutputStream);
        //}

    }
    #endregion
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            exportarExcel();
        }
        catch (Exception ex)
        {

        }
    }
}
