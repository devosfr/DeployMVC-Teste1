using System.Collections.Generic;

namespace Modelos
{
    public class PaginaDeControleVO : ModeloBase
    {

        public virtual string nome { get; set; }
        public virtual string pagina { get; set; }
        public virtual int ordem { get; set; }
        public virtual bool fixa { get; set; }
        public virtual bool construcao { get; set; }
        public virtual GrupoDePaginasVO grupoDePaginas { get; set; }
        public virtual IList<PermissaoVO> permissoes { get; set; }


        public PaginaDeControleVO() 
        {
            permissoes = new List<PermissaoVO>();
        }





    }


}



