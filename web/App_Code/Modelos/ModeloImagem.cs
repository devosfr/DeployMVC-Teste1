using System;
using System.IO;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for ModeloImagem
/// </summary>
///      
namespace Modelos
{
    [DataContract]
    public abstract class ModeloImagem: IEquatable<ModeloImagem>
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual string Nome { get; set; }
        [DataMember]
        public virtual int Ordem { get; set; }

        // Propriedades

        public virtual string GetEnderecoImagemHQ()
        {
            return (MetodosFE.BaseURL + "/ImagensHQ/" + Nome).ToLower();
        }
        public virtual string GetEnderecoImagemLQ()
        {
            return (MetodosFE.BaseURL + "/ImagensLQ/" + Nome).ToLower();
        }

        public virtual void ExcluirArquivos()
        {
            if (File.Exists(uplImage.diretorioHQ + "\\" + Nome))
                File.Delete(uplImage.diretorioHQ + "\\" + Nome);

            if (File.Exists(uplImage.diretorioLQ + "\\" + Nome))
                File.Delete(uplImage.diretorioLQ + "\\" + Nome);
        }

        public virtual bool Equals(ModeloImagem other)
        {
            if (other.Id == Id && Id != 0)
                return true;
            return false;
        }
    }
}