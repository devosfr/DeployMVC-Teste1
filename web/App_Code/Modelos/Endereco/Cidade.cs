using System;
using System.Runtime.Serialization;


namespace Modelos
{
    [DataContract]
    public class Cidade : ModeloBase, IEquatable<Cidade>
    {


        [DataMember]
        public virtual string Nome { get; set; }    
        public virtual Estado Estado { get; set; }
        public virtual string Status { get; set; }

            public Cidade()
            {
            }


            public virtual bool Equals(Cidade other)
            {
                if (Id == other.Id && Id>0)
                    return true;
                if (Nome.Equals(other.Nome) && other.Estado.Equals(Estado))
                    return true;
                return false;
            }
    }
}


