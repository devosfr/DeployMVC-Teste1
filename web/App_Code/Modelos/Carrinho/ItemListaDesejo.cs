namespace Modelos
{
    /// <summary>
    /// Summary description for ItemCarrinho
    /// </summary>
    public class ItemListaDesejo : ModeloBase
    {
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual Tamanhos Tamanho { get; set; }
        public virtual Cor Cor { get; set; }
        public virtual ListaDesejo ListaDesejo { get; set; }
        public virtual decimal Valor { get; set; }

        public ItemListaDesejo()
        {
        }
    }
}