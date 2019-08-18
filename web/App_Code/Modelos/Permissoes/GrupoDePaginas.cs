using System.Collections.Generic;

namespace Modelos
{
    public class GrupoDePaginasVO: ModeloBase
    {
        public virtual string nome { get; set; }
        public virtual int ordem { get; set; }
        public virtual IList<PaginaDeControleVO> paginas { get; set; }
        public virtual IList<PermissaoGrupoDePaginasVO> permissoes { get; set; }

        public GrupoDePaginasVO() 
        {
            paginas = new List<PaginaDeControleVO>();
            permissoes = new List<PermissaoGrupoDePaginasVO>();
        }



    }

}



