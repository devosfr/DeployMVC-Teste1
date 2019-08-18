using Modelos;
using Newtonsoft.Json.Linq;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }


    protected RepositoryImport<Tamanhos> RepositorioTamanho
    {
        get
        {
            return new RepositoryImport<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Peso> RepositorioPeso
    {
        get
        {
            return new RepositoryImport<Peso>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Produto> RepositorioProduto
    {
        get
        {
            return new RepositoryImport<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<ItemCarrinho> RepositorioItemCarrinho
    {
        get
        {
            return new RepositoryImport<ItemCarrinho>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<ItemPedido> RepositorioItemPedido
    {
        get
        {
            return new RepositoryImport<ItemPedido>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Pedido> RepositorioPedido
    {
        get
        {
            return new RepositoryImport<Pedido>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Preco> RepositorioPreco
    {
        get
        {
            return new RepositoryImport<Preco>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<InformacaoProduto> RepositorioInformacao
    {
        get
        {
            return new RepositoryImport<InformacaoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Cor> RepositorioCores
    {
        get
        {
            return new RepositoryImport<Cor>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Album> RepositorioAlbum
    {
        get
        {
            return new RepositoryImport<Album>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<CampoImportacao> RepositorioCampoImportacao
    {
        get
        {
            return new RepositoryImport<CampoImportacao>(NHibernateHelper.CurrentSession);
        }
    }

    protected RepositoryImport<Tela> RepositorioTela
    {
        get
        {
            return new RepositoryImport<Tela>(NHibernateHelper.CurrentSession);
        }
    }

    private RepositoryImport<ImagemProduto> RepositorioFotoProduto
    {
        get
        {
            return new RepositoryImport<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private RepositoryImport<SegmentoProduto> RepositorioSegmento
    {
        get
        {
            return new RepositoryImport<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private RepositoryImport<SubSegmentoProduto> RepositorioSubSegmento
    {
        get
        {
            return new RepositoryImport<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    private RepositoryImport<CategoriaProduto> RepositorioCategoria
    {
        get
        {
            return new RepositoryImport<CategoriaProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<CategoriaImportacao> RepositorioCategoriaImportacao
    {
        get
        {
            return new Repository<CategoriaImportacao>(NHibernateHelper.CurrentSession);
        }
    }

    protected string EnderecoImagem
    {
        get
        {
            return "http://www.rickeletron.com.br/media/catalog/product/";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        nome = "Controle de Manutençao";
        nome2 = "Controle de Manutençao";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;

        if (!IsPostBack)
        {
            if (EmManutencao())
                litSituacao.Text = "Em Manutenção";
            else
                litSituacao.Text = "Normal";

        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }


    protected void btnMudarSituacao_Click(object sender, EventArgs e)
    {
        try
        {
            //string chave = "Manutencao";

            bool status = EmManutencao();

                salvarStatus((!status));
            

            if (EmManutencao())
                MetodosFE.mostraMensagem("Sistema agora encontra-se em manutenção", "sucesso");
            else
                MetodosFE.mostraMensagem("Sistema agora encontra-se normalizado", "sucesso");
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message + " - " + ex.InnerException);
        }
    }

    private bool EmManutencao()
    {
        if (HttpContext.Current.Application["Manutencao"] == null)
        {
            string enderecoPasta = Server.MapPath("~/userfiles/import-log/");

            if (!Directory.Exists(enderecoPasta))
                Directory.CreateDirectory(enderecoPasta);

            string nomeArquivo = "status";
            string sFileXLS = enderecoPasta + "\\" + nomeArquivo;
            if (!File.Exists(sFileXLS)) {
                File.Create(sFileXLS).Close();
                using (StreamWriter file = new StreamWriter(sFileXLS, true))
                {
                    file.Write(false.ToString());
                    file.Close();
                }
            }
                

            string status = null;
            using (StreamReader sr = new StreamReader(sFileXLS))
            {
                // Read the stream to a string, and write the string to the console.
                status = sr.ReadLine();
            }

            bool statusBool = Convert.ToBoolean(status);

            if (statusBool)
                HttpContext.Current.Application["Manutencao"] = true;
            else
                HttpContext.Current.Application["Manutencao"] = false;
        }


        bool manutencao = Convert.ToBoolean(HttpContext.Current.Application["Manutencao"]);
        return manutencao;



    }

    protected void salvarStatus(bool status)
    {
        try
        {
            HttpContext.Current.Application["Manutencao"] = status;

            string enderecoPasta = Server.MapPath("~/userfiles/import-log/");

            if (!Directory.Exists(enderecoPasta))
                Directory.CreateDirectory(enderecoPasta);

            string nomeArquivo = "status";
            string sFileXLS = enderecoPasta + "\\" + nomeArquivo;
            if (!File.Exists(sFileXLS))
                File.Create(sFileXLS).Close();

            using (StreamWriter file = new StreamWriter(sFileXLS, false))
            {
                file.Write(status.ToString());
                file.Close();
            }
        }
        catch (Exception)
        {
        }
    }
}