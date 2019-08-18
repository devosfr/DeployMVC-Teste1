

namespace Modelos
{
    /// <summary>
    /// Summary description for ImagemMarca
    /// </summary>
    public class ImagemMarca : ModeloImagem
    {
        public virtual Marca Marca { get; set; }

        public ImagemMarca()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}