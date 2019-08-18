using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Web.Http;
using Modelos;

public class CidadeController : ApiController
{
    private Repository<Cidade> RepoCidade
    {
        get 
        {
            return new Repository<Cidade>(NHibernateHelper.CurrentSession);
        }
    }

    // GET api/<controller>
    public IEnumerable<Cidade> Get()
    {

        
        IList<Cidade> cidades = RepoCidade.All().Fetch(x=>x.Estado).OrderBy(x => x.Nome).ToList();

        return cidades;
    }
    public IEnumerable<Cidade> Get([FromUri]int id)
    {


        IList<Cidade> cidades = RepoCidade.FilterBy(x=>x.Estado.Id == id).Fetch(x => x.Estado).OrderBy(x => x.Nome).ToList();

        return cidades;
    }
}
