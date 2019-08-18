using Modelos;
using System.Collections.Generic;


namespace Modelos
{
    /// <summary>
    /// Summary description for Produto
    /// </summary>

    /// Summary description for Segmento
    /// </summary>
    public class SegmentoProduto : ModeloBase
    {
    
        public virtual string Nome { get; set; }
        public virtual string Chave { get; set; }
        public virtual bool Visivel { get; set; }
        public virtual string Texto { get; set; }
        public virtual int Ordem { get; set; }
        public virtual ISet<SubSegmentoProduto> SubSegmentos { get; set; }

        public SegmentoProduto()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public virtual string GetUrl()
        {
            string retorno = "";

            retorno += MetodosFE.BaseURL + "/produtos/?seg=" + this.Chave;

            return retorno;
        }

    }
}