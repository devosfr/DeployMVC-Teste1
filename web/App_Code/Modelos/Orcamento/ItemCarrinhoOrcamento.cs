using Modelos;

/// <summary>
/// Summary description for ItemCarrinhoOrcamento
/// </summary>
public class ItemCarrinhoOrcamento: ModeloBase
{
	public virtual Produto Produto { get; set; }
	public virtual int Quantidade { get; set; }
	public virtual Tamanhos Tamanho { get; set; }
	public virtual Cor Cor { get; set; }
	public virtual CarrinhoOrcamento Carrinho { get; set; }
	public virtual decimal Valor { get; set; }

	public ItemCarrinhoOrcamento()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}