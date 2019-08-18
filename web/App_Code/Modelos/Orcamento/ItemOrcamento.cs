namespace Modelos
{
    /// <summary>
    /// Summary description for ItemCarrinho
    /// </summary>
    public class ItemOrcamento : ModeloBase
    {
        public virtual string Referencia { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual Tamanhos Tamanho { get; set; }
        public virtual Cor Cor { get; set; }
        public ItemOrcamento()
        {
        }
    }
}