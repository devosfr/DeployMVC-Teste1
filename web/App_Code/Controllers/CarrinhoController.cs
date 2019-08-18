using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Modelos;

public class CarrinhoController : ApiController
{

    // GET api/<controller>
    public IEnumerable<ItemCarrinhoDTO> Get()
    {
        IList<ItemCarrinhoDTO> itens = new List<ItemCarrinhoDTO>();

        foreach (var item in ControleCarrinho.GetItensCarrinho())
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
            IList<ItemCarrinho> itensCarrinho = ControleCarrinho.GetItensCarrinho();

            IList<ItemCarrinho> itensExcluidos = itensCarrinho.Where(x => !Carrinho.Any(y => y.Id == x.Id)).ToList();

            foreach (var item in Carrinho)
            {
                var itemCarrinhoDoBanco = itensCarrinho.FirstOrDefault(x => x.Id == item.Id);
                if (itemCarrinhoDoBanco != null)
                {

                    if (itemCarrinhoDoBanco.Quantidade != item.Quantidade)
                    {
                        itemCarrinhoDoBanco.Quantidade = item.Quantidade;
                        ControleCarrinho.ModificaItem(itemCarrinhoDoBanco);
                    }

                }
            }
            ControleCarrinho.RemoveItens(itensExcluidos);
        }
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
}
