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
public class ItemListaDesejoDTO
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
    [DataMember]
    public virtual string LinkProduto { get; set; }
    [DataMember]
    public virtual int IdProduto { get; set; }

    public ItemListaDesejoDTO()
    {
    }

    private Repository<Album> repoAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }

    public ItemListaDesejoDTO(ItemListaDesejo item )
    {
        Id = item.Id;
        IdProduto = item.Produto.Id;
        NomeProduto = item.Produto.Nome + " - ref. "+ item.Produto.Referencia;
        DescricaoProduto = item.Produto.Descricao.CortarTextoLimpo(50);

        List<Album> albuns = repoAlbum.FilterBy(x => x.Produto.Id == Id).ToList();

        if (albuns != null && albuns.Count > 0)
        {
            IList<ItemListaDesejo> itensDoCarrinho = ControleListaDesejos.GetItensLista();

            ItemListaDesejo itemDoCarrinho = itensDoCarrinho.FirstOrDefault(x => x.Id == item.Id);

            IList<Album> album = albuns.Where(x => x.Ativo && x.Imagens.Count > 0 && x.Nome.Equals(itemDoCarrinho.Cor.Nome)).OrderBy(x => x.Principal).ThenBy(x => x.Imagens.Count == 0).ThenBy(x => x.Cor.Id).ToList();

            if (album != null && album.Count > 0)
            {
                IList<ImagemProduto> imagensAlbum = album[0].Imagens;

                if (imagensAlbum != null && imagensAlbum.Count > 0)
                {
                    ImagemProduto = imagensAlbum[0].GetEnderecoImagemLQ();
                }
            }
        }

        LinkProduto = String.Format("{0}/produto/{1}", MetodosFE.BaseURL, item.Produto.Chave);
        PrecoProduto = item.Produto.Preco.Valor;
        Desconto = item.Produto.Preco.Valor - item.Valor;
        Quantidade = item.Quantidade;
                
        if (item.Tamanho != null)
        {
            Tamanho = Convert.ToString(item.Tamanho.Nome);            
        }

        if (item.Cor != null) 
        {
            Cor = item.Cor.Nome;
            CodigoCor = item.Cor.Codigo;
        }
        
    }

    //public ItemCarrinhoDTO(ItemCarrinhoOrcamento item)
    //{
    //    Id = item.Id;
    //    NomeProduto = item.Produto.Nome;
    //    DescricaoProduto = item.Produto.Descricao.CortarTextoLimpo(200);
    //    ImagemProduto = item.Produto.GetPrimeiraImagemLQ();
    //    PrecoProduto = item.Valor;
    //    Quantidade = item.Quantidade;
    //  Tamanho = Convert.ToString(item.Tamanho);

    //  if (Cor != null)
    //    {
    //        Cor = item.Cor.Nome;
    //        CodigoCor = item.Cor.Codigo;
    //    }
    //}
}