using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Modelos;

namespace Modelos
{
    /// <summary>
    /// Summary description for ObjCampo
    /// </summary>
    public class CampoTela : ModeloBase
    {

        public virtual string destino {get;set;}
        public virtual string nome {get; set; }
        public virtual string classe {get; set; }
        //public virtual Tela tela { get; set; }

        public CampoTela()
        {
        }
    }
}