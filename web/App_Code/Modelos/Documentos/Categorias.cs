using System;
using System.Collections.Generic;

namespace Modelos
{



    public class CategoriaVO :ModeloBase, IEquatable<CategoriaVO>
    {
        // Atributos
        public virtual string nome { get; set; }
        public virtual string imagem { get; set; }
        [StringLength(10000)]
        public virtual string descricao { get; set; }
        public virtual string ordem { get; set; }
        public virtual string chave { get; set; }
        public virtual Tela tela { get; set; }
        public virtual IList<ImagemCategoriaVO> imagens { get; set; }
        public virtual SegmentoFilhoVO segFilho { get; set; }
        public virtual bool visivel { get; set; }


        public virtual string getPrimeiraImagemHQ()
        {
            if (!String.IsNullOrEmpty(imagem))
                return MetodosFE.BaseURL + "/ImagensHQ/" + imagem;
            else
                return uplImage.imgSemImagem;
        }



        // Propriedades
        public CategoriaVO()
        {
            imagens = new List<ImagemCategoriaVO>();
        }

        public virtual bool Equals(CategoriaVO other)
        {
            if (Id == other.Id && Id >0)
                return true;
            if (nome.Equals(other.nome) && other.segFilho.Equals(segFilho))
                return true;
            return false;
        }
    }
}


