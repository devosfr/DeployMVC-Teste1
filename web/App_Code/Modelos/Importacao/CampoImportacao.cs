namespace Modelos
{

    /// <summary>
    /// Summary description for Album
    /// </summary>
    public class CampoImportacao : ModeloBase
    {
        public virtual string NomeCampo { get; set; }
        public virtual string NovoNome { get; set; }

        public CampoImportacao()
        {

        }
    }
}