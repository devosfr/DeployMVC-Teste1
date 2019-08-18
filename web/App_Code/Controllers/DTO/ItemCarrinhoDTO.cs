using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for ItemCarrinhoDTO
/// </summary>
[DataContract]
public class ItemCarrinhoDTO
{
    [DataMember]
    public virtual int Id { get; set; }
    [DataMember]
    public virtual string Referencia { get; set; }
    [DataMember]
    public virtual string NomeProduto { get; set; }
    [DataMember]
    public virtual string DescricaoProduto { get; set; }
    [DataMember]
    public virtual string ImagemProduto { get; set; }
    [DataMember]
    public virtual decimal PrecoProduto { get; set; }
    [DataMember]
    public virtual int Quantidade { get; set; }
    [DataMember]
    public virtual string Tamanho { get; set; }
    [DataMember]
    public virtual string Cor { get; set; }
    [DataMember]
    public virtual string CodigoCor { get; set; }
    [DataMember]
    public virtual decimal Desconto { get; set; }

    public ItemCarrinhoDTO()
    {
    }

    private Repository<Album> repoAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    public ItemCarrinhoDTO(ItemCarrinho item)
    {
        Id = item.Id;
        NomeProduto = item.Produto.Nome + " - Ref. " + item.Produto.Referencia;
        DescricaoProduto = item.Produto.Descricao.CortarTextoLimpo(50);

        List<Album> albuns = repoAlbum.FilterBy(x => x.Produto.Id == item.Produto.Id).ToList();

        if (albuns != null && albuns.Count > 0)
        {
            IList<ItemCarrinho> itensDoCarrinho = ControleCarrinho.GetItensCarrinho();

            ItemCarrinho itemDoCarrinho = itensDoCarrinho.FirstOrDefault(x => x.Id == item.Id);

            Album album = albuns.First();

            if (album != null)
            {
                IList<ImagemProduto> imagensAlbum = album.Imagens;

                if (imagensAlbum != null && imagensAlbum.Count > 0)
                {
                    ImagemProduto = imagensAlbum[0].GetEnderecoImagemLQ();
                }
            }
        }

        PrecoProduto = item.Produto.Preco.Valor;
        Desconto = item.Produto.Preco.Valor - item.Valor;
        Quantidade = item.Quantidade;

        if (item.Tamanho != null)
        {
            Tamanho = item.Tamanho;
        }

        if (item.Cor != null)
        {
            Cor = item.Cor.Nome;
            CodigoCor = item.Cor.Codigo;
        }

    }

    public ItemCarrinhoDTO(ItemCarrinhoOrcamento item)
    {
        Id = item.Id;
        NomeProduto = item.Produto.Nome;
        DescricaoProduto = item.Produto.Descricao.CortarTextoLimpo(200);
        List<Album> albuns = repoAlbum.FilterBy(x => x.Produto.Id == Id).ToList();
        ImagemProduto = albuns.First().Imagens.First().Nome;
        PrecoProduto = item.Valor;
        Quantidade = item.Quantidade;
        Tamanho = Convert.ToString(item.Tamanho);

        if (Cor != null)
        {
            Cor = item.Cor.Nome;
            CodigoCor = item.Cor.Codigo;
        }
    }
}