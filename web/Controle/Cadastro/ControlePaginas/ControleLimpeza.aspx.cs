using Modelos;
using Newtonsoft.Json.Linq;
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

public partial class Controle_Cadastro_Estado : System.Web.UI.Page
{
    public string nome { get; set; }
    public string nome2 { get; set; }


    protected RepositoryImport<Tamanho> RepositorioTamanho
    {
        get
        {
            return new RepositoryImport<Tamanho>(NHibernateHelper.CurrentSession);
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

 protected Repository<ListaDesejo> RepositorioListaDesejo
    {
        get
        {
            return new Repository<ListaDesejo>(NHibernateHelper.CurrentSession);
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
        nome = "Limpeza de Produtos";
        nome2 = "Limpeza de Produtos";
        this.MaintainScrollPositionOnPostBack = true;
        this.Title = nome;
        litTitulo.Text = nome;

        if (!IsPostBack)
        {

        }
    }

    protected virtual void Page_LoadComplete(object sender, EventArgs e)
    {
        string mensagem = MetodosFE.confereMensagem();
        litErro.Text = mensagem != null ? mensagem : "";
    }

    
    protected void btnLimparProdutos_Click(object sender, EventArgs e)
    {
        try
        {
            var transaction = NHibernateHelper.CurrentSession.BeginTransaction(IsolationLevel.ReadCommitted);
            
            var listaDesejo = RepositorioListaDesejo.All();
            RepositorioListaDesejo.Delete(listaDesejo);

            var precos = RepositorioPreco.All();
            RepositorioPreco.Delete(precos);
            
            var pesos = RepositorioPeso.All();
            RepositorioPeso.Delete(pesos);

            var informacoes = RepositorioInformacao.All();
            RepositorioInformacao.Delete(informacoes);

            var imagens = RepositorioFotoProduto.All();
            foreach (var imagem in imagens)
                imagem.ExcluirArquivos();
            RepositorioFotoProduto.Delete(imagens);

            var albuns = RepositorioAlbum.All();
            RepositorioAlbum.Delete(albuns);

            var itensCarrinho = RepositorioItemCarrinho.All();
            RepositorioItemCarrinho.Delete(itensCarrinho);

            var itensPedido = RepositorioItemPedido.All();
            RepositorioItemPedido.Delete(itensPedido);

            var pedidos = RepositorioPedido.All();
            RepositorioPedido.Delete(pedidos);

            var produtos = RepositorioProduto.All();
            RepositorioProduto.Delete(produtos);

            var cores = RepositorioCores.All();
            RepositorioCores.Delete(cores);

            var tamanhos = RepositorioTamanho.All();
            RepositorioTamanho.Delete(tamanhos);

            var categorias = RepositorioCategoria.All();
            RepositorioCategoria.Delete(categorias);

            var subsegmentos = RepositorioSubSegmento.All();
            RepositorioSubSegmento.Delete(subsegmentos);

            var segmento = RepositorioSegmento.All();
            RepositorioSegmento.Delete(segmento);

            


            transaction.Commit();

            MetodosFE.mostraMensagem("Limpeza realizada com sucesso", "sucesso");
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem(ex.Message +" - "+ ex.InnerException);
        }
    }
}