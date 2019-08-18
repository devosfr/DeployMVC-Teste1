using System;
using System.Collections.Generic;

namespace Modelos
{
    //<summary>
    //Summary description for ItemPedido
    //</summary>
    [Serializable]
    public class Cupom : ModeloBase
    {
        public virtual Cliente Cliente { get; set; }
        public virtual IList<DescontoCupom> Descontos { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual string Codigo { get; set; }
        public virtual Cupom CupomPai { get; set; }
        public virtual decimal Desconto { get; set; }
        public virtual int DescontoPercentual { get; set; }
        public virtual DateTime Validade { get; set; }

        /// <summary>
        /// Define se o dono do cupom concordou com o contrato do sistema de bonificação e esta recebendo comissões por uso do cupom.
        /// </summary>
        
        public virtual bool Ativo { get; set; }
        /// <summary>
        /// Se verdadeiro, este cupom é o que será usado como base para todos os futuros cupons. Somente um cupom pode ser marcado como modelo. 
        /// </summary>
        public virtual bool Modelo { get; set; }
        
        public Cupom()
        {
            //
            // TODO: Add constructor logic here
            //
            Descontos = new List<DescontoCupom>();
        }

        public virtual DescontoCupom GetDesconto(Produto produto) 
        {
            if (produto == null)
                return null;

            if (Descontos.Count > 0)
            {
                foreach (var desconto in Descontos)
                {
                    if (desconto.Ativo && desconto.Produto != null)
                        if (desconto.Produto.Equals(produto))
                            return desconto;
                }
            }

            return null;
        }
    }
}