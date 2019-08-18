using System;

namespace Modelos
{
    public class Resposta : ModeloBase, IEquatable<Resposta>
    {

        public enum CodigoOrigem
        {
            [StringValue("Reposta do Administrador.")]
            Administrador = 1,
            [StringValue("Resposta do Cliente.")]
            Cliente = 2
        }

        public virtual int Origem { get; set; }

        public virtual string Descricao { get; set; }
        public virtual DateTime DataEnvio { get; set; }

        public virtual string GetOrigem()
        {
            foreach (var item in Enum.GetValues(typeof(CodigoOrigem)))
                if ((int)item == Origem)
                    return item.ToString();
            return null;
        }

        public Resposta()
        {
            DataEnvio = DateTime.Now;
        }

        public virtual bool Equals(Resposta other)
        {
            if (other.Id == Id && Id > 0)
                return true;

            return false;
        }
    }
}


