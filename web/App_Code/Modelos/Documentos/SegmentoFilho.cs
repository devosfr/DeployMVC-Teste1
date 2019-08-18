using System.Collections.Generic;

namespace Modelos
{
    public class SegmentoFilhoVO : ModeloBase
    {
        // Atributos
        public virtual string nome { get; set; }

        public virtual string chave { get; set; }
        [StringLength(10000)]
        public virtual string descricao { get; set; }
        public virtual string ordem { get; set; }
        public virtual Tela tela { get; set; }
        public virtual SegmentoPaiVO segPai { get; set; }
        public virtual bool visivel { get; set; }
        public virtual IList<ImagemSegFilhoVO> imagens { get; set; }

        //public virtual int idSegmentoPai
        //{
        //    set {
        //        segPai = new SegmentoPaiVO();
        //        segPai.id = value;
        //    }
        //}
        // Propriedades
        public SegmentoFilhoVO()
        {
            imagens = new List<ImagemSegFilhoVO>();
        }
    }
}