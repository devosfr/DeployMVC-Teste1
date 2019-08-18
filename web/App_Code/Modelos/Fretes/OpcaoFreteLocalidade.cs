using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Modelos
{
    /// <summary>
    /// Summary description for Pedido
    /// </summary>
    public class OpcaoFreteLocalidade : ModeloBase, IEquatable<OpcaoFreteLocalidade>
    {

        public virtual string Nome { get; set; }

        //Endereço
        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Bairro { get; set; }
        public virtual int Prazo { get; set; }

        //Requisitos para opcao
        public virtual decimal ValorMinimoCompra { get; set; }
        public virtual float PesoMaximo { get; set; }
        public virtual bool Ativo { get; set; }

        
        //Preço da opção
        public virtual Decimal Preco { get; set; }



        public OpcaoFreteLocalidade()
        {

        }

        public virtual bool Equals(OpcaoFreteLocalidade other)
        {
            if (Id == other.Id && other.Id > 0)
                return true;

            //if (Cidade == null || !Cidade.Equals(other.Cidade))
            //    return false;

            //if (Estado == null || !Estado.Equals(other.Estado))
            //    return false;

            //if (String.IsNullOrEmpty(Logradouro) || !Logradouro.Equals(other.Logradouro))
            //    return false;

            //if (String.IsNullOrEmpty(Bairro) || !Bairro.Equals(other.Bairro))
            //    return false;

            return true;
        }
    }
}