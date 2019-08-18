using System;
using System.Runtime.Serialization;
namespace Modelos
{
    [Serializable]
    [DataContract]
    public class Endereco: ModeloBase
    {
        // Atributos
        [DataMember]
        public virtual int Numero { get; set; }
        [DataMember]
        public virtual string Complemento { get; set; }
        [DataMember]
        public virtual string Bairro { get; set; }
        [DataMember]
        public virtual string Logradouro { get; set; }
        [DataMember]
        public virtual string CEP { get; set; }
        [DataMember]
        public virtual Cidade Cidade { get; set; }
        [DataMember]
        public virtual Estado Estado { get; set; }
        [DataMember]
        public virtual bool Principal { get; set; }

        public virtual Cliente Cliente { get; set; }


        public Endereco()
        { }
    }
}


