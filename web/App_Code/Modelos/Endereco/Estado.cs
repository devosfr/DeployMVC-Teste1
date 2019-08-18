using System;
using System.Runtime.Serialization;

namespace Modelos
{
    [DataContract]
    public class Estado: ModeloBase, IEquatable<Estado>
    {
        [DataMember]
        public virtual string Nome { get; set; }
        [DataMember]
        public virtual string Sigla { get; set; }

        public Estado()
        { }


        public virtual bool Equals(Estado other)
        {
            if (Id == other.Id&&Id>0)
                return true;
            if (Nome.Equals(other.Nome))
                return true;
            return false;
        }
    }
}


