using Modelos;

/// <summary>
/// Summary description for Categoria
/// </summary>

	public class CategoriaImportacao: ModeloBase
{
	public virtual string Nome { get; set; }
	public virtual string CodigoImportacao { get; set; }

	public CategoriaImportacao()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}