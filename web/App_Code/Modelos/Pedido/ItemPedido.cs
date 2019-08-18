namespace Modelos
{
    /// <summary>
    /// Summary description for ItemPedido
    /// </summary>
    public class ItemPedido:ModeloBase
    {
        public virtual Produto Produto { get; set; }
        public virtual string Tamanho { get; set; }
        public virtual Cor Cor { get; set; }
        public virtual float Peso { get; set; }
        public virtual decimal PrecoOriginal { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual DescontoCupom Desconto { get; set; }

        public virtual decimal GetTotal() 
        {
            return Preco * Quantidade;
        }

        public ItemPedido()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}