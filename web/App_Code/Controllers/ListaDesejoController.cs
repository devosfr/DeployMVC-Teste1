using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Modelos;

public class ListaDesejoController : ApiController
{

    // GET api/<controller>
    public IEnumerable<ItemListaDesejoDTO> Get()
    {
        IList<ItemListaDesejoDTO> itens = new List<ItemListaDesejoDTO>();

        foreach (var item in ControleListaDesejos.GetItensLista())
        {
            itens.Add(new ItemListaDesejoDTO(item));
        }
        return itens;
    }

    // POST api/<controller>
    public void Post(IEnumerable<ItemListaDesejoDTO> ListaDesejo)
    {
        if (ListaDesejo != null)
        {
            IList<ItemListaDesejo> itensLista = ControleListaDesejos.GetItensLista();

            IList<ItemListaDesejo> itensExcluidos = itensLista.Where(x => !ListaDesejo.Any(y => y.Id == x.Id)).ToList();

            foreach (var item in ListaDesejo)
            {
                var itemlistaDoBanco = itensLista.FirstOrDefault(x => x.Id == item.Id);
                if (itemlistaDoBanco != null)
                {

                    if (itemlistaDoBanco.Quantidade != item.Quantidade)
                    {
                        itemlistaDoBanco.Quantidade = item.Quantidade;
                        ControleListaDesejos.ModificaItem(itemlistaDoBanco);
                    }

                }
            }

            ControleListaDesejos.RemoveItens(itensExcluidos);


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
