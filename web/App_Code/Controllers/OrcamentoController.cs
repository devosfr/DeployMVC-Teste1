using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class OrcamentoController : ApiController
{

    // GET api/<controller>
    public IEnumerable<ItemCarrinhoDTO> Get()
    {
        IList<ItemCarrinhoDTO> itens = new List<ItemCarrinhoDTO>();

        foreach (var item in ControleOrcamento.GetItensCarrinho())
        {
            itens.Add(new ItemCarrinhoDTO(item));
        }
        return itens;
    }

    // POST api/<controller>
    public void Post(IEnumerable<ItemCarrinhoDTO> Carrinho)
    {
        if (Carrinho != null)
        {
            IList<ItemCarrinhoOrcamento> itensCarrinho = ControleOrcamento.GetItensCarrinho();

            IList<ItemCarrinhoOrcamento> itensExcluidos = itensCarrinho.Where(x => !Carrinho.Any(y => y.Id == x.Id)).ToList();

            foreach (var item in Carrinho)
            {
                var itemCarrinhoDoBanco = itensCarrinho.FirstOrDefault(x => x.Id == item.Id);
                if (itemCarrinhoDoBanco != null)
                {

                    if (itemCarrinhoDoBanco.Quantidade != item.Quantidade)
                    {
                        itemCarrinhoDoBanco.Quantidade = item.Quantidade;
                        ControleOrcamento.ModificaItem(itemCarrinhoDoBanco);
                    }

                }
            }

            ControleOrcamento.RemoveItens(itensExcluidos);


        }
    }
}
