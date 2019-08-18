using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProdutoView
/// </summary>
public class ProdutoView
{
    public string Nome { get; set; }
    public string PrecoDe { get; set; }
    public string PrecoPor { get; set; }
    public string Link { get; set; }
    public string Indisponivel { get; set; }
    public string Imagem { get; set; }
    public Dictionary<string,string> Destaques { get; set; }

	public ProdutoView()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}