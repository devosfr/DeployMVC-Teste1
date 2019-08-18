namespace Modelos
{
    public class PermissaoVO: ModeloBase
    {
        public virtual PaginaDeControleVO paginaDeControle { get; set; }
        public virtual UsuarioVO usuario { get; set; }

        public PermissaoVO() { }

        


    }
}


