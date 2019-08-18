
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;



namespace Modelos
{
    /// <summary>
    /// Summary description for Cor
    /// </summary>
    [Serializable]
    public class Cor : ModeloBase
    {
        
        public virtual string Nome { get; set; }
        public virtual string Chave { get; set; }
        /// <summary>
        /// Código hexadecimal da cor. ex: #FFF000
        /// </summary>
        
        public virtual string Codigo { get; set; }

        
        public virtual string Imagem { get; set; }

        
        public virtual Boolean Visivel { get; set; }

        public Cor()
        {
        }

        public virtual void ExcluirArquivos()
        {
            if (!String.IsNullOrEmpty(Imagem))
            {
                if (File.Exists(uplImage.diretorioHQ + "\\" + Imagem))
                    File.Delete(uplImage.diretorioHQ + "\\" + Imagem);

                if (File.Exists(uplImage.diretorioLQ + "\\" + Imagem))
                    File.Delete(uplImage.diretorioLQ + "\\" + Imagem);
            }
        }

        public virtual string GetEnderecoImagemHQ()
        {
            return (MetodosFE.BaseURL + "/ImagensHQ/" + Imagem).ToLower();
        }
        public virtual string GetEnderecoImagemLQ()
        {
            return (MetodosFE.BaseURL + "/ImagensLQ/" + Imagem).ToLower();
        }
    }
}