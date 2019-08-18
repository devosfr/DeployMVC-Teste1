namespace Modelos
{
    public class PermissaoGrupoDePaginasVO: ModeloBase
    {
        public virtual GrupoDePaginasVO grupoDePaginas { get; set; }
        public virtual  UsuarioVO usuario { get; set; }
        //public virtual LojaVO loja { get; set; }

        public PermissaoGrupoDePaginasVO() { }
        
        



    }

}


