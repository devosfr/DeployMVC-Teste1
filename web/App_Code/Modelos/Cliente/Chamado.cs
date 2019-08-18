using System;
using System.Collections.Generic;

namespace Modelos
{
    public class Chamado: ModeloBase, IEquatable<Chamado>
    {
        public enum CodigoStatus
        {
            [StringValue("Aguardando resposta do ADM.")]
            AguardandoAtendimento = 1,
            [StringValue("Aguardando resposta do cliente.")]
            AguardandoCliente = 2,
            [StringValue("Chamado encerrado pelo cliente.")]
            Fechado= 3
        }

        public virtual string Assunto { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual string Codigo { get; set; }

        public virtual string NomeProduto { get; set; }

        public virtual string Modelo { get; set; }

        public virtual string Serie { get; set; }

        public virtual string NF { get; set; }

        public virtual DateTime DataCompra { get; set; }

        public virtual string Descricao { get; set; }


        public virtual int Status { get; set; }
        public virtual DateTime DataSolicitacao { get; set; }
        public virtual DateTime? DataFechamento { get; set; }

        public virtual IList<Resposta> Respostas { get; set; }
       
        public Chamado() 
        {
            Respostas = new List<Resposta>();
        }



        public virtual bool Equals(Chamado other)
        {
            if (other.Id == Id && Id > 0)
                return true;
            if (other.Assunto.Equals(Assunto))
                return true;

            return false;
        }

        public virtual DateTime GetDataUltimaResposta() 
        {
            if (Respostas.Count > 0)
                return Respostas[Respostas.Count - 1].DataEnvio;

            return DataSolicitacao;
        }
        public virtual string GetOrigemUltimaResposta()
        {
            if (Respostas.Count > 0)
                return Respostas[Respostas.Count - 1].GetOrigem();

            return null;
        }


        public virtual string GetStatus()
        {
            foreach (var item in Enum.GetValues(typeof(CodigoStatus)))
                if ((int)item == Status)
                    return item.ToString();
            return null;
        }
    }
}


