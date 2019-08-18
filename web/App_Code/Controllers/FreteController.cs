using Modelos;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

public class FreteController : ApiController
{
    private Repository<Endereco> RepoEndereco
    {
        get
        {
            return new Repository<Endereco>(NHibernateHelper.CurrentSession);
        }
    }

    // GET api/<controller>/5
    public IEnumerable<FreteDTO> Get([FromUri]int id)
    {
        Cliente cliente = ControleLoginCliente.GetClienteLogado();
        Endereco endereco = RepoEndereco.FindBy(x => x.Id == id && x.Cliente.Id == cliente.Id);

        string cep = endereco.CEP;

        IList<FreteDTO> opcoes = ControleFrete.GetOpcoesFrete(cep);

        ControleCarrinho.SetEndereco(endereco);

        return opcoes;

    }

    public void Post(FreteDTO frete)
    {
        ControleCarrinho.SetOpcaoFrete(frete.Codigo);
    }


}
