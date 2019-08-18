using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


namespace Modelos
{
    /// <summary>
    /// Summary description for ObjUplFoto
    /// </summary>
    public class UploadTela : ModeloBase
    {
        public virtual int QtdeFotos { get; set; }
        public virtual int TamFotoGrW { get; set; }
        public virtual int TamFotoPqW { get; set; }
        public virtual int TamFotoGrH { get; set; }
        public virtual int TamFotoPqH { get; set; }
        public virtual int Configuracao { get; set; }
        public virtual int Qualidade { get; set; }
        public virtual string Cor { get; set; }

        public UploadTela()
        {
            QtdeFotos = 5;
            TamFotoGrW = 700;
            TamFotoPqW = 120;
            TamFotoGrH = 500;
            TamFotoPqH = 90;
            Qualidade = 80;
            Cor = null;
            Configuracao = 0;

        }
    }
}