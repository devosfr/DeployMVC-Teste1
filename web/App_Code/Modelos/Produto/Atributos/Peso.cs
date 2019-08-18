using System.Linq;

namespace Modelos
{
    /// <summary>
    /// Summary description for Peso
    /// </summary>
    public class Peso : ModeloBase
    {
        public virtual Tamanhos Tamanho { get; set; }
        public virtual float Valor { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual decimal Altura { get; set; }
        public virtual decimal Largura { get; set; }
        public virtual decimal Profundidade { get; set; }

        public Peso()
        {
        }

    }
}