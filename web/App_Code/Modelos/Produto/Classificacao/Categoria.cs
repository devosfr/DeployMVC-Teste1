using System.Collections.Generic;
using System.Linq;

namespace Modelos
{

    /// <summary>
    /// Summary description for Categoria
    /// </summary>

    public class CategoriaProduto : ModeloBase
    {
        public virtual string Nome { get; set; }
        public virtual string Chave { get; set; }

        public virtual bool Visivel { get; set; }
        public virtual int Ordem { get; set; }
        public virtual string CodigoImportacao { get; set; }
        public virtual SubSegmentoProduto SubSegmento { get; set; }

        public CategoriaProduto()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}