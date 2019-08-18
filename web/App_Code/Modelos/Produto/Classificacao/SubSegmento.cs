using Modelos;
using System.Collections.Generic;


/// <summary>
/// Summary description for SubSegmento
/// </summary>
public class SubSegmentoProduto: ModeloBase
{
	public virtual string Nome { get; set; }
    public virtual string Chave { get; set; }
    public virtual string CodigoImportacao { get; set; }
    public virtual bool Visivel { get; set; }
    public virtual int Ordem { get; set; }
    public virtual SegmentoProduto Segmento { get; set; }
	public virtual ISet<CategoriaProduto> Categorias { get; set; }
	public SubSegmentoProduto()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public virtual string GetUrl()
    {
        string retorno = "";

        retorno += MetodosFE.BaseURL + "/produtos/?subseg=" + this.Chave;

        return retorno;
    }

}