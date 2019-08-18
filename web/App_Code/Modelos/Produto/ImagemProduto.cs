

namespace Modelos
{
    /// <summary>
    /// Summary description for ImagemProduto
    /// </summary>
    public class ImagemProduto : ModeloImagem
    {
        public virtual Album Album { get; set; }

      public virtual Produto produto { get; set; }
      public ImagemProduto()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}