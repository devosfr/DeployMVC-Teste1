using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;
using System.Data.OleDb;

public partial class Controle_Importacao_Categorias : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }

    //private string sFileXLSX;
    private string sFileXLS { get; set; }
    //private string strConnXLSX;
    private string strConnXLS { get; set; }

    private string nomeTabela { get; set; }

    private string caminhoBase { get; set; }

    protected Repository<CategoriaImportacao> RepositorioCategoriaImportacao
    {
        get
        {
            return new Repository<CategoriaImportacao>(NHibernateHelper.CurrentSession);
        }
    }

    protected UploadTela objUpload { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Importação de Categorias";
        nome2 = "Importação de Categorias";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;

        //Caminho do arquivo xlsx
        //string sFileXLSX = Server.MapPath("~\\exemplo.xlsx");
        //Caminho do arquivo xls
        sFileXLS = HttpContext.Current.ApplicationInstance.Server.MapPath("~/userfiles/importacaoCategoria.xls");
        //Conexão com o XLSX para versões 2007 e 2010
        //string strConnXLSX = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + sFileXLSX + "';Extended Properties=Excel 12.0;";
        //Conexão com o XLS para versões 2003, XP..
        strConnXLS = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + sFileXLS + "';Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";

        if (!IsPostBack)
        {

        }
    }


    protected void btnCarregar_Click(object sender, EventArgs e)
    {
        try
        {

            caminhoBase = HttpContext.Current.Server.MapPath("~/userfiles/importacaoCategoria.xls");

            //File 

            if (fulSiteMap.HasFile)
            {
                bool bValido = false;

                string fileExtension = System.IO.Path.GetExtension(fulSiteMap.FileName).ToLower();
                foreach (string ext in new string[] { ".xls" })
                {
                    if (fileExtension == ext)
                        bValido = true;
                }
                if (!bValido)
                    throw new Exception("Extensão inválida de arquivo.");
            }

            fulSiteMap.SaveAs(caminhoBase);



            MetodosFE.mostraMensagem("Arquivo carregado.", "sucesso");



        }
        catch (Exception ex)
        {
            litErro.Text = ex.Message;
        }
    }

    protected string getNomeTabela()
    {

        OleDbConnection objConn = null;
        System.Data.DataTable dt = null;

        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConnXLS))
            {

                //Criando o OleDbCommand com o SQL e a conexão

                //Abrindo a conexão
                conn.Open();
                //Executando o UPDATE
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //Fechando a conexão
                conn.Close();
            }


            if (dt == null)
            {
                return null;
            }

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;

            // Add the sheet name to the string array.
            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            // Loop through all of the sheets if you want too...
            for (int j = 0; j < excelSheets.Length; j++)
            {
                // Query each excel sheet.
            }

            return excelSheets[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            // Clean up.
            if (objConn != null)
            {
                objConn.Close();
                objConn.Dispose();
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }

    }

    protected void criarGridView(string filePath)
    {
        //FileInfo file = new FileInfo(filePath);
        string erros = "";
        try
        {
            nomeTabela = getNomeTabela();
            DataTable tbl = new DataTable("MyTable");
            using (OleDbConnection con =
                    new OleDbConnection(strConnXLS))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + nomeTabela + "]", con))
                {
                    con.Open();

                    //conn.Open();
                    //Executando o UPDATE
                    cmd.ExecuteNonQuery();

                    OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                    oleAdapter.SelectCommand = cmd;
                    tbl = new DataTable("");
                    oleAdapter.FillSchema(tbl, SchemaType.Source);
                    oleAdapter.Fill(tbl);
                    //Fechando a conexão
                    con.Close();
                }
            }
            // Using a DataTable to process th


            IList<CategoriaImportacao> categorias = RepositorioCategoriaImportacao.All().ToList();

            RepositorioCategoriaImportacao.Delete(categorias);




            foreach (DataRow row in tbl.Rows)
            {
                try
                {
                    //row[""].ToString();

                    string nomeCategoria = row["Nome"].ToString();
                    string idCategoria = (row["ID"].ToString());


                    //CategoriaImportacao categoria = RepositorioCategoriaImportacao.FindBy(x => x.Nome.Equals(nomeCategoria));

                    //if (categoria == null)

                    CategoriaImportacao categoria = new CategoriaImportacao();
                    categoria.Nome = nomeCategoria;
                    categoria.CodigoImportacao = idCategoria;


                    //if (categoria.Id == 0)
                    RepositorioCategoriaImportacao.Add(categoria);
                    //else
                    //  RepositorioCategoriaImportacao.Update(categoria);



                }
                catch (Exception ex)
                {
                    erros += "Categoria ref. " + row["nome"].ToString() + " : " + ex.Message + "<br/>";
                }
            }







            if (!String.IsNullOrEmpty(erros))
                MetodosFE.mostraMensagem(erros);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnImportar_Click1(object sender, EventArgs e)
    {
        try
        {
            criarGridView(caminhoBase);
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }
    }
    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }


}
