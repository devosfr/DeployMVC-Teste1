

namespace Modelos
{

    /// <summary>
    /// Summary description for Album
    /// </summary>
    public class InformacaoProduto : ModeloBase
    {
        public virtual string Nome { get; set; }
        public virtual string Texto { get; set; }
        public virtual int Ordem { get; set; }
        public virtual bool Visivel { get; set; }
        public virtual Produto Produto { get; set; }

        public InformacaoProduto()
        {

        }
    }
}