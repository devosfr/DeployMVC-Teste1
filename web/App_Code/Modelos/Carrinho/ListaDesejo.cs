using System;
using System.Collections.Generic;


namespace Modelos
{
    /// <summary>
    /// Summary description for Pedido
    /// </summary>
    public class ListaDesejo : ModeloBase
    {
        public virtual IList<ItemListaDesejo> Itens { get; set; }

        public virtual string Referencia { get; set; }

        public virtual DateTime DataCriacao { get; set; }

        public virtual string ModoFrete { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual int ModoPagamento { get; set; }
        public virtual Cupom Cupom { get; set; }

        public ListaDesejo()
        {
            Itens = new List<ItemListaDesejo>();
            //
            // TODO: Add constructor logic here
            //
        }
    }
}