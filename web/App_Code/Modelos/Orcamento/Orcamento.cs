using System;
using System.Collections.Generic;


namespace Modelos
{
    /// <summary>
    /// Summary description for Pedido
    /// </summary>
    public class Orcamento:ModeloBase
    {
        public enum situacoes
        {
            //[StringValue("Em Análise")]
            //EA,
            //[StringValue("Pendente")]
            //PE,
            //[StringValue("Aprovada")]
            //AP,
            //[StringValue("Negada")]
            //NE

            
            Analise = 1,
            Pendente = 2,
            Aprovada = 3,
            Negada = 4
        }


        public virtual DateTime DataOrcamento { get; set; }

        public virtual int Status { get; set; }

        public virtual ISet<ItemOrcamento> Itens { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual string ModoFrete { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual string Rastreamento { get; set; }

        public virtual decimal PrecoFrete { get; set; }

        public virtual int ModoPagamento { get; set; }

        public Orcamento()
        {
            //
            // TODO: Add constructor logic here
            //
            Itens = new HashSet<ItemOrcamento>();
        }
    }
}