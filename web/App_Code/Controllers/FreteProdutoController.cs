using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using Modelos;

public class FreteProdutoController : ApiController
{

    private static Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }


    // GET api/<controller>
    public IEnumerable<FreteDTO> Get([FromUri]int idProduto, int idTamanho, string cep)
    {
        try
        {
            IList<FreteDTO> lista = ControleFrete.GetOpcoesFrete(cep, idProduto, idTamanho);

            return lista;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }



    //// GET api/<controller>
    //public IEnumerable<FreteDTO> Get([FromUri]int idProduto, int idTamanho, string cep)
    //{
    //    try
    //    {


    //        Produto produto = RepositorioProduto.FindBy(idProduto);

    //        float peso = produto.Peso.Valor;

    //        decimal valor = produto.Preco.Valor;

    //        IList<FreteDTO> valoresCorreio = ControleFrete.ValorFreteCorreios(cep, peso, produto.Peso.Profundidade, produto.Peso.Altura, produto.Peso.Largura);

    //        IList<FreteDTO> valoresTransporte = ControleFrete.OpcoesLocalidade(cep, peso, valor);

    //        valoresCorreio = valoresCorreio.Concat(valoresTransporte).ToList();

    //        //IList<FreteDTO> lista = ControleFrete.GetOpcoesFrete(cep, idProduto, idTamanho);

    //        return valoresCorreio;
    //    }
    //    catch (Exception ex)
    //    {
    //        //var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
    //        //{
    //        //    Content = new StringContent(ex.Message),
    //        //    ReasonPhrase = ex.Message
    //        //};
    //        //throw new HttpResponseException(message);
    //        throw new Exception(ex.Message);
    //    }
    //}



}
