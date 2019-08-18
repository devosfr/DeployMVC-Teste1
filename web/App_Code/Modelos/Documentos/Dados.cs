using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Modelos
{

    public class DadoVO : ModeloBase
    {
        // Atributos
        public virtual SegmentoPaiVO segPai { get; set; }
        public virtual SegmentoFilhoVO segFilho { get; set; }
        public virtual CategoriaVO categoria { get; set; }
        public virtual Tela tela { get; set; }
        public virtual string referencia { get; set; }
        public virtual DateTime data { get; set; }
        public virtual string nome { get; set; }
        [StringLength(5000)]
        public virtual string keywords { get; set; }
        public virtual string chave { get; set; }
        public virtual string destaque { get; set; }
        [StringLength(10000)]
        public virtual string descricao { get; set; }
        [StringLength(10000)]
        public virtual string resumo { get; set; }
        public virtual string valor { get; set; }
        public virtual string ordem { get; set; }
        [StringLength(10000)]
        public virtual string meta { get; set; }



        public virtual Boolean visivel { get; set; }

        public virtual IList<ImagemDadoVO> listaFotos { get; set; }



        //public virtual int idSegmentoPai
        //{
        //    set
        //    {
        //        segPai = new SegmentoPaiVO();
        //        segPai.id = value;
        //    }
        //}
        //public virtual int idSegmentoFilho
        //{
        //    set
        //    {
        //        segFilho = new SegmentoFilhoVO();
        //        segFilho.id = value;
        //    }
        //}
        //public virtual int idCategoria
        //{
        //    set
        //    {
        //        if (value == 0)
        //            categoria = null;
        //        else
        //        {
        //            categoria = new CategoriaVO();
        //            categoria.id = value;
        //        }
        //    }
        //}


        public DadoVO()
        {
            listaFotos = new List<ImagemDadoVO>();
        }

        // Propriedades

        public virtual IList<ImagemDadoVO> getImagensOrdenadas() 
        {
            return listaFotos.OrderBy(x => x.Ordem).ThenBy(x=>x.Nome).ToList();
        }
        public virtual string getPrimeiraImagemLQ()
        {
            if (listaFotos.Count > 0)
            {
                listaFotos = listaFotos.OrderBy(x => x.Ordem).ToList();
                return MetodosFE.BaseURL + "/ImagensLQ/" + listaFotos[0].Nome;
            }
            else
                return uplImage.imgSemImagem;
        }
        public virtual string getPrimeiraImagemHQ()
        {
            if (listaFotos.Count > 0)
            {
                listaFotos = listaFotos.OrderBy(x => x.Ordem).ToList();
                return MetodosFE.BaseURL + "/ImagensHQ/" + listaFotos[0].Nome;
            }

            else
                return uplImage.imgSemImagem;
        }


    }
}


