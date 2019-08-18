using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;

/// <summary>
/// Summary description for ItemPorClienteDTO
/// </summary>
public class ItemPorClienteDTO
{
    public IList<ItemPedido> Itens { get; set; }

    public Cliente Comprador { get; set; }

	public ItemPorClienteDTO()
	{
        Itens = new List<ItemPedido>();
		//
		// TODO: Add constructor logic here
		//
	}

    public decimal GetTotal()
    {
        decimal total = 0;

        foreach (var item in Itens)
        {
            total += item.Desconto.Comissao * item.Quantidade;
        }

        return total;
    }
}