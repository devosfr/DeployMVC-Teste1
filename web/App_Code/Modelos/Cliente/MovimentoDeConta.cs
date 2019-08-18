using System;

namespace Modelos
{
    //<summary>
    //Summary description for ItemPedido
    //</summary>
    [Serializable]
    public class MovimentoDeConta:ModeloBase
    {
        public enum TipoMovimento 
        {
            Bonus = 1,
            Saque = 2
        }

        public enum StatusMovimento 
        {
            Aberto = 1,
            Confirmado = 2,
            Negado = 3
        }

        public virtual Cliente Cliente { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DescontoCupom Desconto { get; set; }
        public virtual ItemPedido ItemPedido{get;set;}

        public virtual decimal Valor { get; set; }
        public virtual decimal SaldoAtual { get; set; }
        public virtual int Tipo { get; set; }
        public virtual string Observacao { get; set; }
        public virtual int Status { get; set; }

        public MovimentoDeConta()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public virtual string GetStatus()
        {
            foreach (var item in Enum.GetValues(typeof(StatusMovimento)))
                if ((int)item == Status)
                    return item.ToString();
            return null;
        }

        public virtual string GetTipo()
        {
            foreach (var item in Enum.GetValues(typeof(TipoMovimento)))
                if ((int)item == Tipo)
                    return item.ToString();
            return null;
        }
    }
}