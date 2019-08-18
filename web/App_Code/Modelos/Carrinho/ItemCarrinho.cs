namespace Modelos
{
    /// <summary>
    /// Summary description for ItemCarrinho
    /// </summary>
    public class ItemCarrinho : ModeloBase
    {
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual string Tamanho { get; set; }
        public virtual string Nome_pers { get; set; }
        public virtual int Numero_pers { get; set; }
        public virtual Cor Cor { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual decimal Valor { get; set; }

        public ItemCarrinho()
        {
        }
    }
}