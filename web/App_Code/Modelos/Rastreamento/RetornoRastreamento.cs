using System.Collections.Generic;
using System.Data;

namespace Modelos
{
    public class RetornoRastreamento
    {
        public DataSet DataSet { get; set; }
        public string Html { get; set; }
        public List<TrackingPackageStep> ListObject { get; set; }
        public bool Sucesso { get; set; }
    }
}
