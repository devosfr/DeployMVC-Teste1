using System.Collections.Generic;

namespace Modelos
{
    public class SegmentoPaiVO : ModeloBase
    {
        // Atributos
        public virtual string nome { get; set; }

        public virtual string chave { get; set; }
        public virtual bool visivel { get; set; }
        [StringLength(10000)]
        public virtual string descricao { get; set; }
        public virtual string ordem { get; set; }
        public virtual Tela tela { get; set; }
        public virtual IList<ImagemSegPaiVO> imagens { get; set; }

        public SegmentoPaiVO()
        {
            imagens = new List<ImagemSegPaiVO>();
        }
    }
}