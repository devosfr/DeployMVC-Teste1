using Modelos;
using System;


/// <summary>
/// Summary description for Marca
/// </summary>
public class Marca: ModeloBase
{
    public virtual string Nome {get;set;}
    public virtual Boolean Visivel { get; set; }
    public virtual ImagemMarca Imagem { get; set; }

	public Marca()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}