using Modelos;
//using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using Excel;
using ExcelLibrary;

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{

    private Repository<Produto> repoProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<SegmentoProduto> repoSeg
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<SubSegmentoProduto> repoSubSeg
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Preco> repoPreco
    {
        get
        {
            return new Repository<Preco>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Peso> repoPeso
    {
        get
        {
            return new Repository<Peso>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Cor> repoCor
    {
        get
        {
            return new Repository<Cor>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Album> repoAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Marca> repoMarca
    {
        get
        {
            return new Repository<Marca>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<Estoque> repoEstoque
    {
        get
        {
            return new Repository<Estoque>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<InformacaoProduto> repoInformacaoProduto
    {
        get
        {
            return new Repository<InformacaoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private Repository<ImagemProduto> repoImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private string Erros { get; set; }

    //private string sFileXLSX;
    private string SFileXLS { get; set; }

    //private string strConnXLSX;
    private string StrConnXLS { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "Importação - " + Configuracoes.getSetting("nomeSite");



        SFileXLS = HttpContext.Current.ApplicationInstance.Server.MapPath("~/userfiles/importacao.xlsx");

        IList<ImagemProduto> imagens = new List<ImagemProduto>();
        imagens = repoImagemProduto.All().ToList();
        if (imagens.Count > 0)
        {
            gvImagens.DataSource = imagens.OrderBy(x => x.Nome);
            gvImagens.DataBind();
        }

    }

    protected void btnCarregar_Click(object sender, EventArgs e)
    {
        try
        {
            string caminhoBase = SFileXLS;

            //File

            if (fulSiteMap.HasFile)
            {
                bool bValido = false;

                string fileExtension = Path.GetExtension(fulSiteMap.FileName).ToLower();
                foreach (string ext in new string[] { ".xlsx" })
                {
                    if (fileExtension == ext)
                        bValido = true;
                }
                if (!bValido)
                    throw new Exception("Extensão inválida de arquivo.");
            }

            fulSiteMap.SaveAs(caminhoBase);

            MetodosFE.mostraMensagem("Arquivo carregado.", "sucesso");

            btnImportar.Visible = true;

            btnCarregar.Visible = false;
            fulSiteMap.Visible = false;
            //litLog.Text = "Arquivo Carregado: '" + fulSiteMap.FileName + "'";
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message);
        }

        getNome();


    }

    protected void gvImagens_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ImagemProduto dado = repoImagemProduto.FindBy(Convert.ToInt32(gvImagens.DataKeys[e.RowIndex].Value));
            dado.ExcluirArquivos();
            dado.Album.Imagens = new List<ImagemProduto>();
            repoAlbum.Update(dado.Album);
            repoImagemProduto.Delete(dado);
            MetodosFE.mostraMensagem("Registro de " + gvImagens.Columns[1].HeaderText + " \"" + dado.Nome + "\" excluído com sucesso", "sucesso");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }

        //gvDados.DataBind();
        //Pesquisar();
        Response.Redirect(Request.RawUrl);
    }

    protected void apagar_Command(object sender, CommandEventArgs e)
    {
        try
        {
            ImagemProduto dado = repoImagemProduto.FindBy(x => x.Nome.Equals(e.CommandArgument.ToString()));
            dado.ExcluirArquivos();
            dado.Nome = null;
            repoImagemProduto.Delete(repoImagemProduto.FindBy(x => x.Id == dado.Id));

            MetodosFE.mostraMensagem("Registro de " + gvImagens.Columns[1].HeaderText + " \"" + dado.Nome + "\" excluído com sucesso", "sucesso");
        }
        catch (Exception er)
        {
            MetodosFE.mostraMensagem(er.GetType() + " " + er.Message);
        }

        //gvDados.DataBind();
        //Pesquisar();
        Response.Redirect(Request.RawUrl);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (fulImagens.HasFile)
        {
            dynamic fileUploadControl = fulImagens;
            foreach (HttpPostedFile uploadedFile in fileUploadControl.PostedFiles)
            {
                ImagemProduto orf = new ImagemProduto();
                orf.Nome = uploadedFile.FileName;
                repoImagemProduto.Add(orf);
                uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/ImagensHQ/"), uploadedFile.FileName));
                uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/ImagensLQ/"), uploadedFile.FileName));
            }
            Response.Redirect(Request.RawUrl);
        }
    }

    public static DataTable GetDataTableFromExcel(string path, string nome, bool hasHeader = true)
    {
        DataTable tbl = new DataTable();
        try
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.FirstOrDefault(x => x.Name.Contains(nome));
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    OfficeOpenXml.ExcelRange wsRow = null;
                    if (nome.Equals("CarrosselCor"))
                        wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column - 1];
                    else
                        wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];

                    if (nome.Equals("Caracteristicas"))
                        wsRow = ws.Cells[rowNum, 1, rowNum, 3];

                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        if (cell.Start.Column != null && row[cell.Start.Column - 1] != null)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                }

                return tbl;
            }
        }
        catch (Exception ex)
        {
            string e = ex.Message;
            return null;
        }
    }

    public List<string> getNome()
    {
        try
        {
            List<string> nomes = new List<string>();

            string path = SFileXLS;

            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets;

                foreach (OfficeOpenXml.ExcelWorksheet tab in ws)
                {
                    nomes.Add(tab.Name);
                }
            }
            return nomes;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void ImportarPlanilha()
    {
        string progress = "";

        Session.Timeout = 30;

        Server.ScriptTimeout = 2500;

        IList<string> tabelas = getNome();

        try
        {
            int i = 0;
            foreach (string tabela in tabelas)
            {
                if (tabela.ToLower().Contains("Plan1"))
                {
                    continue;
                }

                DataTable planilha = new DataTable("myTable");

                planilha = GetDataTableFromExcel(SFileXLS, tabela, true);

                if (tabela.ToLower().Contains("entrada"))
                {
                    try
                    {
                        ImportaMercadoria(planilha);
                        progress += "Produtos (Base) Importados!<br/>";
                    }
                    catch (Exception ex)
                    {
                        progress += ex.Message + "<br/>";
                    }
                }

                if (tabela.ToLower().Contains("caracteristicas"))
                {
                    try
                    {
                        ImportaCaracteristicas(planilha);
                        progress += "Caracteristicas Importadas!<br/>";
                    }
                    catch (Exception ex)
                    {
                        progress += ex.Message + "<br/>";
                    }
                }

                if (tabela.ToLower().Contains("estoque"))
                {
                    try
                    {
                        AtualizaEstoque(planilha);
                        progress += "Estoque Atualizado!<br/>";
                    }
                    catch (Exception ex)
                    {
                        progress += ex.Message + "<br/>";
                    }
                }

                if (tabela.ToLower().Contains("cor-foto"))
                {
                    try
                    {
                        ImportaCor(planilha);
                        progress += "Cor-foto importados!<br/>";
                    }
                    catch (Exception ex)
                    {
                        progress += ex.Message + "<br/>";
                    }
                }

                if (tabela.ToLower().Contains("carrosselcor"))
                {
                    try
                    {
                        ImportaCarrossel(planilha);
                        progress += "Carrossel importados!<br/>";
                    }
                    catch (Exception ex)
                    {
                        progress += ex.Message + "<br/>";
                    }
                }

                i++;
            }

            litLog.Text = progress;

            if (!string.IsNullOrEmpty(Erros))
            {
                MetodosFE.mostraMensagem("Importação realizada com erros. Configura o log.");
                //salvaLogErro(Erros);
            }
            else
            {
                progress += "Importação realizada com sucesso!";
            }

        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }

    public void ImportaSegmentos(string r, string segP, string segF)
    {
        Produto prod = repoProduto.FindBy(x => x.Referencia.Equals(r));
        SegmentoProduto segPai = new SegmentoProduto();
        if (prod != null)
        {
            if (!String.IsNullOrEmpty(segP))
            {
                segPai = new SegmentoProduto();
                segPai = repoSeg.FindBy(x => x.Nome.Equals(segP));
                if (segPai == null)
                {
                    segPai = new SegmentoProduto();
                    segPai.Nome = segP;
                    segPai.Visivel = true;
                    segPai.Chave = segP.ToSeoUrl();
                    repoSeg.Add(segPai);
                }
                prod.Segmentos = new List<SegmentoProduto>();
                prod.Segmentos.Add(segPai);
            }

            if (!String.IsNullOrEmpty(segF))
            {
                SubSegmentoProduto segFilho = new SubSegmentoProduto();
                segFilho = repoSubSeg.FindBy(x => x.Nome.Equals(segF));
                if (segFilho == null)
                {
                    segFilho = new SubSegmentoProduto();
                    segFilho.Nome = segF;
                    segFilho.Chave = segF.ToSeoUrl();
                    segFilho.Visivel = true;
                    segFilho.Segmento = segPai;
                    repoSubSeg.Add(segFilho);
                }
                prod.SubSegmentos = new List<SubSegmentoProduto>();
                prod.SubSegmentos.Add(segFilho);
            }
            repoProduto.Update(prod);
        }
    }

    #region tabela Entrada
    public void ImportaMercadoria(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = null;
                produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {
                    #region edita_produto

                    produto.Visivel = true;

                    produto.Referencia = row[0].ToString();

                    produto.Nome = row[1].ToString();

                    produto.Chave = (produto.Nome + produto.Referencia).ToSeoUrl();

                    produto.Descricao = row[2].ToString();

                    if (row[16].ToString().ToLower().Equals("s"))
                    {
                        produto.Destaque = true;
                    }
                    else
                        produto.Destaque = false;


                    if (!String.IsNullOrEmpty(row[5].ToString()))
                    {
                        Marca marca = new Marca();
                        if (!repoMarca.All().Any(x => x.Nome.Equals(row[5].ToString())))
                        {
                            marca.Nome = row[5].ToString();
                            marca.Visivel = true;
                            repoMarca.Add(marca);
                        }
                        else
                        {
                            marca = repoMarca.FindBy(x => x.Nome.Equals(row[5].ToString()));
                        }

                        produto.Marca = marca;
                    }

                    int comEstoque;
                    if (int.TryParse(row[12].ToString(), out comEstoque))
                    {
                        produto.Prazo_com_estoque = comEstoque;
                        comEstoque = 0;
                    }

                    int semEstoque;
                    if (int.TryParse(row[13].ToString(), out semEstoque))
                    {
                        produto.Prazo_sem_estoque = semEstoque;
                        semEstoque = 0;
                    }

                    if (row[14].ToString().ToLower().Equals("s"))
                    {
                        produto.Novidade = true;
                    }
                    else
                        produto.Novidade = false;

                    if (row[15].ToString().ToLower().Equals("s"))
                    {
                        produto.Personalizavel = true;
                    }
                    else
                        produto.Personalizavel = false;

                    repoProduto.Add(produto);
                    #endregion

                    if (!String.IsNullOrEmpty(row[3].ToString()) && !String.IsNullOrEmpty(row[4].ToString()))
                        ImportaSegmentos(produto.Referencia, row[3].ToString(), row[4].ToString());
                    else
                        throw new Exception("Segmentos vazios, ref: " + produto.Referencia);

                    ImportaPrecos(produto.Referencia, row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString());

                    ImportaPeso(produto.Referencia, row[10].ToString(), row[11].ToString());

                    continue;
                }
                else
                {
                    Produto novoProduto = new Produto();

                    #region cadastra_novo

                    novoProduto.Visivel = true;

                    novoProduto.Referencia = row[0].ToString();

                    novoProduto.Nome = row[1].ToString();

                    novoProduto.Chave = (novoProduto.Nome + novoProduto.Referencia).ToSeoUrl();

                    novoProduto.Descricao = row[2].ToString();

                    if (row[16].ToString().ToLower().Equals("s"))
                    {
                        novoProduto.Destaque = true;
                    }
                    else
                        novoProduto.Destaque = false;


                    if (!String.IsNullOrEmpty(row[5].ToString()))
                    {
                        Marca marca = new Marca();
                        if (!repoMarca.All().Any(x => x.Nome.Equals(row[5].ToString())))
                        {
                            marca.Nome = row[5].ToString();
                            marca.Visivel = true;
                            repoMarca.Add(marca);
                        }
                        else
                        {
                            marca = repoMarca.FindBy(x => x.Nome.Equals(row[5].ToString()));
                        }

                        novoProduto.Marca = marca;
                    }

                    int comEstoque;
                    if (int.TryParse(row[12].ToString(), out comEstoque))
                    {
                        novoProduto.Prazo_com_estoque = comEstoque;
                        comEstoque = 0;
                    }

                    int semEstoque;
                    if (int.TryParse(row[13].ToString(), out semEstoque))
                    {
                        novoProduto.Prazo_sem_estoque = semEstoque;
                        semEstoque = 0;
                    }

                    if (row[14].ToString().ToLower().Equals("s"))
                    {
                        novoProduto.Novidade = true;
                    }
                    else
                        novoProduto.Novidade = false;

                    if (row[15].ToString().ToLower().Equals("s"))
                    {
                        novoProduto.Personalizavel = true;
                    }
                    else
                        novoProduto.Personalizavel = false;

                    repoProduto.Add(novoProduto);
                    #endregion

                    if (!String.IsNullOrEmpty(row[3].ToString()) && !String.IsNullOrEmpty(row[4].ToString()))
                        ImportaSegmentos(novoProduto.Referencia, row[3].ToString(), row[4].ToString());
                    else
                        throw new Exception("Segmentos vazios, ref: " + produto.Referencia);

                    ImportaPrecos(novoProduto.Referencia, row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString());

                    ImportaPeso(novoProduto.Referencia, row[10].ToString(), row[11].ToString());

                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public void ImportaPrecos(string referencia, string precoCheio, string precoRemarcado, string precoVista, string desconto)
    {
        Produto produto = repoProduto.FindBy(x => x.Referencia.Equals(referencia));
        if (produto != null)
        {
            Preco preco = new Preco();
            preco.Produto = produto;

            decimal valorVista;
            if (decimal.TryParse(precoVista.Replace("R$", ""), out valorVista))
                preco.ValorAvista = valorVista;
            else
                preco.ValorAvista = 0;

            decimal remarcado;
            if (decimal.TryParse(precoVista.Replace("R$", ""), out remarcado))
                preco.Valor = remarcado;
            else
                preco.Valor = 0;

            decimal tmp;
            if (decimal.TryParse(desconto.Replace("%", ""), out tmp))
            {
                preco.Desconto = Convert.ToDecimal(desconto.Replace("%", ""));
            }
            else
                preco.Desconto = 0;

            decimal cheio;
            if (decimal.TryParse(precoCheio.Replace("R$", ""), out cheio))
                preco.ValorSemPromocao = cheio;
            else
                preco.ValorSemPromocao = 0;

            repoPreco.Add(preco);
            produto.Preco = preco;
            repoProduto.Update(produto);
        }
    }

    public void ImportaPeso(string referencia, string peso, string dims)
    {
        Produto produto = repoProduto.FindBy(x => x.Referencia.Equals(referencia));

        if (produto != null)
        {

            string[] dimensoesDivididas = null;

            if (!String.IsNullOrEmpty(dims.ToString()))
            {
                dimensoesDivididas = dims.ToString().Split('x');
            }

            Peso Peso = new Peso();
            Peso.Valor = float.Parse(peso);
            Peso.Produto = produto;
            repoPeso.Add(Peso);

            if (dimensoesDivididas != null)
            {
                Decimal altura = Convert.ToDecimal(dimensoesDivididas[0]);
                Decimal largura = Convert.ToDecimal(dimensoesDivididas[1]);
                Decimal profundidade = Convert.ToDecimal(dimensoesDivididas[2]);
                Peso.Altura = altura;
                Peso.Largura = largura;
                Peso.Profundidade = profundidade;
            }

            produto.Peso = Peso;
            repoProduto.Update(produto);

        }
    }
    #endregion

    #region tabela Cor-Foto

    public void ImportaCor(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {

                    string nomeCor = row[1].ToString();

                    //COR
                    if (String.IsNullOrEmpty(nomeCor))
                        throw new Exception("Cor vazia no produto: " + produto.Nome + " - Cod.: " + produto.Referencia);

                    Cor cor = null;

                    cor = repoCor.FindBy(x => x.Nome.ToLower().Equals(nomeCor.ToLower()));
                    if (cor == null)
                    {
                        cor = new Cor();
                        cor.Nome = nomeCor;
                        cor.Visivel = true;
                        cor.Codigo = row[2].ToString();
                        repoCor.Add(cor);
                    }

                    if (produto.Cores == null)
                        produto.Cores = new List<Cor>();

                    produto.Cores.Add(cor);

                    Album alb = new Album();

                    List<Album> albuns = repoAlbum.FilterBy(x => x.Produto.Id == produto.Id).ToList();
                    if (albuns != null && albuns.Count > 0) 
                    {
                        alb = albuns.First();
                        alb.Cor = cor;
                        alb.Ativo = true;
                        alb.Produto = produto;
                        repoAlbum.Update(alb);
                    }
                    else
                    {
                        alb.Cor = cor;
                        alb.Ativo = true;
                        alb.Produto = produto;
                        repoAlbum.Add(alb);
                    }

                    if (!String.IsNullOrEmpty(row[3].ToString()))
                    {
                        string nome = row[3].ToString();

                        if (!nome.Contains(".jpg"))
                            nome += ".jpg";

                        ImagemProduto img = new ImagemProduto();

                        img = repoImagemProduto.FindBy(x => x.Nome.Equals(nome));
                        if (img == null)
                        {
                            img = new ImagemProduto();

                            img.Nome = nome;
                            img.Album = alb;
                            repoImagemProduto.Add(img);
                        }

                        img.Album = alb;

                        if (alb.Imagens == null)
                        {
                            alb.Imagens = new List<ImagemProduto>();
                        }

                        repoImagemProduto.Update(img);
                    }

                    repoProduto.Update(produto);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    #endregion

    #region tabela-Genero

    public void ImportaGenero(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = null;
                produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {
                    if (!String.IsNullOrEmpty(row[1].ToString()))
                    {
                        List<string> generos = produto.GetGeneros();

                        if (!generos.Any(x => x.Equals(row[1].ToString())))
                        {
                            generos.Add(row[1].ToString().Replace(" ", ""));
                        }

                        string mont = "";

                        int i = 0;

                        foreach (string gen in generos)
                        {
                            if (i == 0)
                                mont += ("," + gen).Replace(",", "");

                            else
                                mont += "," + gen;

                            i++;
                        }
                        produto.Genero = mont;

                        repoProduto.Update(produto);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    #endregion

    #region tabela-Caracteristicas

    public void ImportaCaracteristicas(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = null;
                produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {
                    if (!String.IsNullOrEmpty(row[1].ToString()))
                    {
                        List<InformacaoProduto> infos = produto.Informacoes.ToList();

                        if (!infos.Any(x => x.Nome.Equals(row[1].ToString())))
                        {
                            InformacaoProduto info = new InformacaoProduto();
                            info.Nome = row[1].ToString();
                            info.Produto = produto;
                            info.Texto = row[2].ToString();
                            repoInformacaoProduto.Add(info);
                            infos.Add(info);

                            if (produto.Informacoes == null)
                                produto.Informacoes = new List<InformacaoProduto>();

                            produto.Informacoes.Add(info);
                        }
                        repoProduto.Update(produto);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    #endregion

    #region tabela-Esportes

    public void ImportaEsportes(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = null;
                produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {
                    if (!String.IsNullOrEmpty(row[1].ToString()))
                    {
                        List<string> esportes = produto.GetEsportes();
                        if (!esportes.Any(x => x.Equals(row[1].ToString())))
                        {
                            esportes.Add(row[1].ToString());
                        }

                        string mont = "";

                        int i = 0;

                        foreach (string sport in esportes)
                        {
                            if (i == 0)
                                mont += ("," + sport).Replace(",", "");

                            else
                                mont += "," + sport;

                            i++;
                        }
                        produto.Esporte = mont;
                    }
                    repoProduto.Update(produto);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    #endregion

    #region tabela-Estoque

    public void AtualizaEstoque(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = null;
                produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {
                    string tam = row[1].ToString();
                    int qnt = Convert.ToInt32(row[2].ToString());
                    DateTime data = DateTime.Now;
                    Estoque est = new Estoque();
                    est.Produto = produto;
                    est.Quantidade = qnt;
                    est.Data = data;
                    est.Tamanho = tam;
                    repoEstoque.Add(est);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    #endregion

    #region tabela-CarroselCor

    public void ImportaCarrossel(DataTable planilha)
    {
        foreach (DataRow row in planilha.Rows)
        {
            try
            {
                if (string.IsNullOrEmpty(row[0].ToString()))
                    continue;

                if (row[0].ToString().ToLower().Equals("referencia"))
                    continue;

                Produto produto = null;
                produto = repoProduto.FindBy(x => x.Referencia.Equals(row[0].ToString()));
                if (produto != null)
                {
                    if (!String.IsNullOrEmpty(row[1].ToString()))
                    {
                        List<string> cores = produto.GetCarrossel();
                        if (!cores.Any(x => x.Equals(row[1].ToString())))
                        {
                            cores.Add(row[1].ToString());
                        }

                        string mont = "";

                        int i = 0;

                        foreach (string refe in cores)
                        {
                            if (i == 0)
                                mont += ("," + refe).Replace(",", "");

                            else
                                mont += "," + refe;

                            i++;
                        }
                        produto.Carrossel = mont;
                    }
                    repoProduto.Update(produto);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    #endregion

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        ImportarPlanilha();
    }

    protected void btnInserirImagens_Click(object sender, EventArgs e)
    {

    }
}