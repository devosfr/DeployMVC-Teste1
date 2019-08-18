using System;
using System.Collections.Generic;


namespace Modelos
{
    /// <summary>
    /// Summary description for Pedido
    /// </summary>
    public class Pedido : ModeloBase
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

            [StringValue("Aguardando Confirmação de Pagamento")]
            Aguardando = 1,
            [StringValue("Separando Mercadoria para Envio")]
            Separando = 2,
            [StringValue("Pedido Enviado")]
            Enviado = 3,
            [StringValue("Pedido Entregue")]
            Entregue = 4,
            [StringValue("Pedido Cancelado")]
            Cancelado = 5
        }

        public enum FormasDePagamento
        {
            [StringValue("Pagamento através do sistema do PagSeguro")]
            PagSeguro = 1,
            [StringValue("Depósito em conta bancária")]
            Deposito = 2,
            [StringValue("Requisição de boleto para pagamento")]
            Boleto = 3
        }


        public virtual DateTime DataPedido { get; set; }

        public virtual int Status { get; set; }

        public virtual ISet<ItemPedido> Itens { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual string ModoFrete { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual decimal PrecoFrete { get; set; }

        public virtual string Rastreamento { get; set; }

        public virtual int FormaDePagamento { get; set; }

        public virtual string ComprovanteDeposito { get; set; }

        public virtual Cupom Cupom { get; set; }

        public virtual bool Comissionado { get; set; }

        public virtual string GetStatusPedido()
        {
            foreach (var item in Enum.GetValues(typeof(situacoes)))
                if ((int)item == Status)
                    return item.ToString();
            return null;
        }

        public virtual string GetFormaDePagamento()
        {
            foreach (var item in Enum.GetValues(typeof(FormasDePagamento)))
            {

                if ((int)item == FormaDePagamento)
                {
                    if(FormaDePagamento == 2)
                        return item.ToString() + "-" + BancoDeposito;
                    return item.ToString();
                }
            }
            return null;
        }

        public virtual string InformacoesDeposito { get; set; }

        public virtual string BancoDeposito { get; set; }

        public virtual decimal GetTotalPedido()
        {

         decimal total = 0;
         decimal cento = 100;

         foreach (var item in Itens)
         {
            total += item.GetTotal();
         }

         Cupom cupom = Cupom;

         if (cupom != null && cupom.DescontoPercentual > 0)
         {
            decimal descontoPercentual = (cupom.DescontoPercentual / cento);

            descontoPercentual = total * descontoPercentual;

            total = total - descontoPercentual;

         }
         if (cupom != null && cupom.Desconto > 0)
         {
            decimal descontoFixo = cupom.Desconto;

            total = total - descontoFixo;
         }

         total += PrecoFrete;
         return total;
      }

        public Pedido()
        {
            //
            // TODO: Add constructor logic here
            //
            Itens = new HashSet<ItemPedido>();
        }
    }
}