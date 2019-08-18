using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Modelos;

namespace Modelos
{
    /// <summary>
    /// Summary description for ObjTela
    /// </summary>
    public class Tela:ModeloBase
    {

        public virtual string nome { get; set; }
        public virtual string nomeFixo { get; set; }
        public virtual Boolean multiplo { get; set; }
        public virtual UploadTela upload { get; set; }
        public virtual PaginaDeControleVO pagina { get; set; }
        public virtual IList<CampoTela> campos { get; set; }
        public virtual IList<DadoVO> dados { get; set; }
        public virtual IList<SegmentoPaiVO> segmentosPai { get; set; }
        public virtual IList<SegmentoFilhoVO> segmentosFilho { get; set; }
        public virtual IList<CategoriaVO> categorias { get; set; }
        

        public Tela()
        {
            campos = new List<CampoTela>();
        }
    }
}
