using Modelos;
using System;



/// <summary>
/// Summary description for Preco
/// </summary> []
public class Preco: ModeloBase
{
	public virtual string Nome { get; set; }
	public virtual decimal Valor { get; set; }
    public virtual decimal ValorSemPromocao { get; set; }
    public virtual decimal ValorAvista { get; set; }
    public virtual decimal Desconto { get; set; }
    public virtual string Texto { get; set; }
    public virtual DateTime DataVigencia { get; set; }
    public virtual Produto Produto { get; set; }
	public Preco()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}