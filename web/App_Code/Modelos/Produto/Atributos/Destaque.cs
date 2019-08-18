using Modelos;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Summary description for Destaque
/// </summary>
namespace Modelos
{
    public class Destaque : ModeloBase
    {
        public virtual string Nome { get; set; }
        public virtual string Chave { get; set; }
        public virtual Boolean Visivel { get; set; }
        public virtual int Prioridade { get; set; }
        public virtual string Imagem { get; set; }

        public Destaque()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public virtual string GetEnderecoImagem()
        {
            return (MetodosFE.BaseURL + "/ImagensLQ/" + Imagem).ToLower();
        }

        public virtual void ExcluirArquivos()
        {
            if (File.Exists(uplImage.diretorioLQ + "\\" + Imagem))
                File.Delete(uplImage.diretorioLQ + "\\" + Imagem);
        }
    }
}