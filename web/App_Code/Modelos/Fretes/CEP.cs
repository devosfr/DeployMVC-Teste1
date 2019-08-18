using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Modelos
{
    /// <summary>
    /// Summary description for Pedido
    /// </summary>
    public class EnderecoCEP : ModeloBase, IEquatable<EnderecoCEP>
    {
        public virtual string CEP { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Bairro { get; set; }


        public EnderecoCEP()
        {
        }

        public virtual bool Equals(EnderecoCEP other)
        {
            if (Id == other.Id && other.Id > 0)
                return true;

            if (Cidade == null || !Cidade.Equals(other.Cidade))
                return false;

            if (Estado == null || !Estado.Equals(other.Estado))
                return false;

            if (String.IsNullOrEmpty(Logradouro) || !Logradouro.Equals(other.Logradouro))
                return false;

            if (String.IsNullOrEmpty(Bairro) || !Bairro.Equals(other.Bairro))
                return false;

            return true;
        }
    }
}