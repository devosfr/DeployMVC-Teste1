
using System.Collections.Generic;
using System.Linq;
using System;


namespace Modelos
{

    /// <summary>
    /// Summary description for Estoque
    /// </summary>
    public class Estoque : ModeloBase
    {
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual Tamanhos Tamanho { get; set; }
        public virtual string Tipo { get; set; }
        public virtual DateTime Data { get; set; }

        public Estoque()
        {
            
        }
    }
}