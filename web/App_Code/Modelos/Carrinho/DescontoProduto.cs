using System;

namespace Modelos
{
    //<summary>
    //Summary description for ItemPedido
    //</summary>
    public class DescontoCupom:ModeloBase
    {
        public enum TipoDesconto
        {

            Fixo = 1,
            Porcentagem = 2
        }

        /// <summary>
        /// Valor do desconto, podendo ser em reais ou uma porcentagem do preço do produto.
        /// </summary>
        public virtual decimal Desconto { get; set; }
        /// <summary>
        /// Se for fixo, o desconto será uma quantidade de dinheiro pré-estabelecida.
        /// Se for porcentagem, o desconto será uma porcentagem do preço do produto.
        /// </summary>
        public virtual int Tipo { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual Produto Produto { get; set; }

        /// <summary>
        /// Quanto que o dono do cupom irá ganhar pela venda.
        /// </summary>
        public virtual decimal Comissao { get; set; }

        public virtual Cupom Cupom { get; set; }

        public DescontoCupom()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public virtual decimal GetPrecoComDesconto(Produto produto) 
        {
            if(produto==null)
                return 0;
            if (Produto.Equals(produto))
            {
                if (Tipo == (int)TipoDesconto.Fixo)
                {
                    return produto.Preco.Valor - Desconto;
                }
                else
                    return produto.Preco.Valor - (produto.Preco.Valor * Desconto / 100);
            }

            return produto.Preco.Valor;
        }
    }
}