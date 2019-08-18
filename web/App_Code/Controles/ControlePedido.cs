using Modelos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using NHibernate.Linq;

/// <summary>
/// Summary description for ControlePedido
/// </summary>
public class ControlePedido
{
    private static Repository<Pedido> RepositorioPedido
    {
        get
        {
            return new Repository<Pedido>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Tamanhos> RepositorioTamanhos
    {
        get
        {
            return new Repository<Tamanhos>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<ItemPedido> RepositorioItemPedido
    {
        get
        {
            return new Repository<ItemPedido>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Endereco> RepoEndereco
    {
        get
        {
            return new Repository<Endereco>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Estoque> RepoEstoque
    {
        get
        {
            return new Repository<Estoque>(NHibernateHelper.CurrentSession);
        }
    }

    public static DateTime agora = DateTime.Now;

    public static void FecharPedido(int modoPagamento = 0, int idBanco = 0)
    {
        ControleLoginCliente.statusLogin();

        Cliente cliente = ControleLoginCliente.GetClienteLogado();

        IList<ItemCarrinho> itens = ControleCarrinho.GetItensCarrinho();

        if (itens == null || itens.Count == 0)
        {
            throw new Exception("Não é possível finalizar um pedido sem itens.");
        }

        Endereco endereco = ControleCarrinho.GetEndereco();

        string codigoFrete = ControleCarrinho.GetOpcaoFrete();

        if (endereco == null)
            throw new Exception("Endereço de entrega não foi especificado.");

        if (String.IsNullOrEmpty(codigoFrete))
            throw new Exception("Forma de envio não foi espeficicada.");

        Pedido pedido = new Pedido();

        pedido.Cliente = cliente;
        pedido.DataPedido = DateTime.Now;
        pedido.Itens = new HashSet<ItemPedido>();

        Cupom cupom = ControleCarrinho.GetCupom();

        foreach (ItemCarrinho item in itens)
        {
            ItemPedido itemPedido = new ItemPedido();

            DescontoCupom desconto = null;
            if (cupom != null)
                desconto = cupom.GetDesconto(item.Produto);

            itemPedido.Produto = item.Produto;
            itemPedido.Peso = item.Produto.Peso.Valor;
            if (modoPagamento == 2 || modoPagamento == 3)
                itemPedido.PrecoOriginal = item.Produto.Preco.ValorAvista;
            else
                itemPedido.PrecoOriginal = item.Produto.Preco.Valor;


            if (desconto != null)
            {
                itemPedido.Preco = desconto.GetPrecoComDesconto(item.Produto);
                itemPedido.Desconto = desconto;
            }
            else
                itemPedido.Preco = item.Produto.Preco.Valor;


            itemPedido.Quantidade = item.Quantidade;
            itemPedido.Tamanho = item.Tamanho;
            itemPedido.Cor = item.Cor;

            RepositorioItemPedido.Add(itemPedido);
            pedido.Itens.Add(itemPedido);

            Estoque estoque = new Estoque();
            estoque.Produto = item.Produto;
            estoque.Quantidade = item.Quantidade-(item.Quantidade*2);
            estoque.Data = agora;
            estoque.Tipo = "S";
            estoque.Tamanho = RepositorioTamanhos.FindBy(x => x.Nome.Equals(item.Tamanho));

            RepoEstoque.Add(estoque);
        }

        FreteDTO frete = ControleFrete.ValorFreteCarrinho();

        pedido.ModoFrete = frete.Nome;
        pedido.PrecoFrete = frete.Preco;

        Endereco enderecoPedido = new Endereco();
        enderecoPedido.Bairro = endereco.Bairro;
        enderecoPedido.CEP = endereco.CEP;
        enderecoPedido.Cidade = endereco.Cidade;
        enderecoPedido.Numero = endereco.Numero;
        enderecoPedido.Complemento = endereco.Complemento;
        enderecoPedido.Estado = endereco.Estado;
        enderecoPedido.Logradouro = endereco.Logradouro;

        RepoEndereco.Add(enderecoPedido);

        pedido.Cupom = cupom;
        pedido.Endereco = enderecoPedido;
        pedido.Status = (int)Pedido.situacoes.Aguardando;
        pedido.FormaDePagamento = modoPagamento;

        if (modoPagamento == 2)
        {
            DadoVO dado = MetodosFE.documentos.FirstOrDefault(x => x.Id == idBanco && x.tela.nome.Equals("Bancos para Depósito"));

            pedido.BancoDeposito = dado.nome;
            pedido.InformacoesDeposito = dado.descricao;
        }

        RepositorioPedido.Add(pedido);


        IList<Produto> produtos = new List<Produto>();

        foreach (var item in pedido.Itens)
        {
            var produto = item.Produto;
            produto.NumeroVendas += item.Quantidade;
            produtos.Add(produto);
        }

       

        RepositorioProduto.Update(produtos);

        switch (modoPagamento)
        {
            case 1:
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    HttpContext.Current.Response.Redirect(MetodosFE.BaseURL + "/compra-finalizada", false);
                }
                else
                {
                    string linkPagSeguro = ControlePagSeguro.executarVenda(pedido);

                    HttpContext.Current.Response.Redirect(linkPagSeguro, false);
                }
                break;
            case 3:
                EnviarEmailBoleto(pedido);
                //HttpContext.Current.Response.Redirect(MetodosFE.BaseURL + "/compra-finalizada", false);
                ControleCarrinho.limparCookieCompra();
                break;
            case 2:
                HttpContext.Current.Response.Redirect(MetodosFE.BaseURL + "/compra-deposito-finalizada/" + idBanco, false);
                break;
            default: break;
        }


    }

    public static void EnviarEmailBoleto(Pedido pedido)
    {

        EnvioEmailsVO envio = new EnvioEmailsVO();

        DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

        if (dado != null)
        {
            DadoVO dadosContato = MetodosFE.getTela("E-mail - Boleto");
            string email = null;
            if (dadosContato != null)
                if (!String.IsNullOrEmpty(dadosContato.referencia))
                    email = dadosContato.referencia;

            if (String.IsNullOrEmpty(email))
                email = dado.referencia;

            envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = email;
            envio.assuntoMensagem = "Solicitação de Boleto - Pedido " + pedido.Id;
            envio.emailResposta = pedido.Cliente.Email;



            Dictionary<string, string> valores = new Dictionary<string, string>();

            string mensagem = "";
            mensagem += "<br/>Foi solicitado boleto para pagamento para o seguinte pedido: <br/>";
            mensagem += "<br/>Pedido: " + pedido.Id;
            mensagem += "<br/>Cliente: " + pedido.Cliente.Nome;
            mensagem += "<br/>E-mail: " + pedido.Cliente.Email;



            envio.conteudoMensagem = mensagem;

            bool vrecebe = EnvioEmails.envioemails(envio);

            if (!vrecebe)
            {
                throw new Exception("Ocorreram problemas no envio do e-mail. Tente mais tarde.");
            }
        }
        else
            throw new Exception("Problemas ocorreram na configuração de E-mail.");

    }

    public static void EnviarEmail2(Pedido pedido)
    {
        try
        {


            EnvioEmailsVO envio = new EnvioEmailsVO();

            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            if (dado != null)
            {
                DadoVO dadosContato = MetodosFE.getTela("Contato");
                string email = null;
                if (dadosContato != null)
                    if (!String.IsNullOrEmpty(dadosContato.referencia))
                        email = dadosContato.referencia;

                if (String.IsNullOrEmpty(email))
                    email = dado.referencia;

                envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
                envio.emailRemetente = dado.referencia;
                envio.emailDestinatario = pedido.Cliente.Email;
                envio.assuntoMensagem = "Solicitação de Boleto - Pedido " + pedido.Id;
                envio.emailResposta = email;



                Dictionary<string, string> valores = new Dictionary<string, string>();

                string mensagem = "";
                mensagem += "<br/>Foi solicitado boleto para pagamento para o seguinte pedido: <br/>";
                mensagem += "<br/>Pedido: " + pedido.Id;
                mensagem += "<br/>Cliente: " + pedido.Cliente.Nome;
                mensagem += "<br/>E-mail: " + pedido.Cliente.Email;



                envio.conteudoMensagem = mensagem;

                bool vrecebe = EnvioEmails.envioemails(envio);

                if (!vrecebe)
                {
                    throw new Exception("Ocorreram problemas no envio do e-mail. Tente mais tarde.");
                }


            }
            else
                throw new Exception("Problemas ocorreram na configuração de E-mail.");
        }
        catch (Exception ex)
        {

        }
    }

    public static IList<ItemPorClienteDTO> BuscaComprasPorData(DateTime? dataInicio, DateTime? dataFim, string nomeComprador, Cliente donoCupom)
    {

        IList<ItemPedido> itensComprados = RepositorioItemPedido.FilterBy(x =>
            x.Desconto != null &&
            x.Pedido.Cupom.Cliente.Id == donoCupom.Id &&
            (dataInicio == null || x.Pedido.DataPedido >= dataInicio) &&
            (dataFim == null || x.Pedido.DataPedido <= dataFim) &&
            (String.IsNullOrEmpty(nomeComprador) || x.Pedido.Cliente.Nome.ToUpper().Contains(nomeComprador.ToUpper()))
            ).Fetch(x => x.Desconto).Fetch(x => x.Pedido).ToList();



        IList<Cliente> compradores = itensComprados.Select(x => x.Pedido.Cliente).Distinct().ToList();

        IList<ItemPorClienteDTO> itensPorCliente = new List<ItemPorClienteDTO>();
        foreach (var comprador in compradores)
        {
            ItemPorClienteDTO novoItem = new ItemPorClienteDTO();
            novoItem.Comprador = comprador;
            novoItem.Itens = itensComprados.Where(x => x.Pedido.Cliente.Id == comprador.Id).OrderBy(x => x.Pedido.DataPedido).ToList();
            itensPorCliente.Add(novoItem);
        }


        return itensPorCliente;
    }
}