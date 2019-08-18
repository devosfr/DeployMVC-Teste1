using System;
using System.Collections.Generic;

namespace Modelos
{

    [Serializable]
    public class Cliente : ModeloBase, IEquatable<Cliente>
    {
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
        public virtual string CPF { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string Genero { get; set; }

        public virtual string nomeProduto { get; set; }
        public virtual string numeroSerieProduto { get; set; }
        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime? DataDesativacao { get; set; }
        public virtual string Observacoes { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Whatsapp { get; set; }
        public virtual string Email { get; set; }


        public virtual string Senha { get; set; }

        public virtual IList<Endereco> Enderecos { get; set; }

        public virtual IList<MovimentoDeConta> Conta { get; set; }


        /// <summary>
        /// Define se o cliente concordou com o contrato do sistema de bonificação e receberá comissões pelas vendas.
        /// </summary>
        public virtual bool ParticipanteBonificacao { get; set; }
        public virtual DateTime DataParticipacaoBonificacao { get; set; }

        public Cliente()
        {
            Enderecos = new List<Endereco>();
            Conta = new List<MovimentoDeConta>();
        }

        public virtual bool Equals(Cliente other)
        {
            if (other.Id == Id && Id > 0)
                return true;
            if (other.Nome.Equals(Nome))
                return true;

            return false;
        }

        public virtual decimal GetSaldoAtual()
        {
            decimal total = 0;

            foreach (MovimentoDeConta movimento in Conta)
            {
                if (movimento.Status == (int)MovimentoDeConta.StatusMovimento.Confirmado)
                {
                    if (movimento.Tipo == (int)MovimentoDeConta.TipoMovimento.Bonus)
                        total += movimento.Valor;
                    else
                        total -= movimento.Valor;
                }
            }

            return total;
        }
    }
}


