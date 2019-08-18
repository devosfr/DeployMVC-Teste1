using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Modelos;

public class EstadoController : ApiController
{
    private Repository<Estado> RepoEstado
    {
        get 
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }

    // GET api/<controller>
    public IEnumerable<Estado> Get()
    {
        IList<Estado> estados = RepoEstado.All().OrderBy(x=>x.Nome).ToList();

        return estados;
    }

}
