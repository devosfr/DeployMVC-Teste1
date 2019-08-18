using System;
using System.Collections.Generic;


namespace Modelos
{
    /// <summary>
    /// Summary description for Pedido
    /// </summary>
    public class Carrinho : ModeloBase
    {
        public virtual IList<ItemCarrinho> Itens { get; set; }

        public virtual string Referencia { get; set; }

        public virtual DateTime DataCriacao { get; set; }

        public virtual string ModoFrete { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual int ModoPagamento { get; set; }
        public virtual Cupom Cupom { get; set; }

        public Carrinho()
        {
            Itens = new List<ItemCarrinho>();
            //
            // TODO: Add constructor logic here
            //
        }
    }
}