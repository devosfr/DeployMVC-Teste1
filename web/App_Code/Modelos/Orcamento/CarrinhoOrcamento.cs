using Modelos;
using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for CarrinhoOrcamento
/// </summary>
public class CarrinhoOrcamento: ModeloBase
{
    public virtual IList<ItemCarrinhoOrcamento> Itens { get; set; }

    public virtual string Referencia { get; set; }

    public virtual DateTime DataCriacao { get; set; }

	public CarrinhoOrcamento()
	{
		//
		// TODO: Add constructor logic here
		//
        Itens = new List<ItemCarrinhoOrcamento>();
	}
}