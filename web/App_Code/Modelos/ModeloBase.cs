using System;
using System.Runtime.Serialization;


namespace Modelos
{
    [Serializable]
    [DataContract]
    public abstract class ModeloBase: IEquatable<ModeloBase>
    {
        [DataMember]
        public virtual int Id { get; set; }

        public virtual bool Equals(ModeloBase other)
        {
            if (other.Id == Id && Id > 0)
                return true;
            return false;
        }
    }
}