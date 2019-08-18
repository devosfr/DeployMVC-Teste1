
using System.Collections.Generic;
using System.Linq;


namespace Modelos
{

    /// <summary>
    /// Summary description for Album
    /// </summary>
    public class Album : ModeloBase
    {
        public virtual string Nome { get; set; }
        public virtual Cor Cor { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual IList<ImagemProduto> Imagens { get; set; }
        public virtual ImagemProduto Primeira { get; set; }
        public virtual bool Principal { get; set; }
        public virtual bool Ativo { get; set; }

        public Album()
        {
            //
            // TODO: Add constructor logic here
            //
            Imagens = new List<ImagemProduto>();
        }

        public virtual void ExcluirImagens()
        {
            foreach (var imagem in Imagens)
                imagem.ExcluirArquivos();
        }

        public virtual IList<ImagemProduto> GetImagensOrdenadas()
        {
            return Imagens.OrderBy(x => x.Ordem).ThenBy(x => x.Nome).ToList();
        }
    }
}