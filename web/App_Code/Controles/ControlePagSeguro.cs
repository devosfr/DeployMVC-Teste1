using Modelos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;

/// <summary>
/// Summary description for ControlePagSeguro
/// </summary>
public class ControlePagSeguro
{
    private static Repository<Pedido> RepoPedido
    {
        get { return new Repository<Pedido>(NHibernateHelper.CurrentSession); }
    }

    private static Repository<ItemPedido> RepoItemPedido
    {
        get { return new Repository<ItemPedido>(NHibernateHelper.CurrentSession); }
    }

    public static string executarVenda(int idPedido)
    {
        Pedido pedido = RepoPedido.FindBy(idPedido);
        return executarVenda(pedido);
    }

    public static string executarVenda(Pedido pedido)
    {
        if (pedido == null)
            return null;
        try
        {
            //Dados da conta que receberá o pagamento
            DadoVO dado = MetodosFE.getTela("Dados do PagSeguro");
            if (dado == null)
                throw new Exception("Não foram especificadas as configurações de PagSeguro.");

            AccountCredentials dadosAcesso = new AccountCredentials(dado.nome, dado.referencia);

            //Endereço do cliente
            //Uol.PagSeguro.Address enderecoCliente = new Uol.PagSeguro.Address();
            Cliente cliente = pedido.Cliente;

            String telefone1 = "";

            //Dados de Telefone
            if (!String.IsNullOrEmpty(cliente.Telefone))
                telefone1 = cliente.Telefone;
            else
                telefone1 = "(51)9999-9999";

            Phone telefone = null;
            if (!String.IsNullOrEmpty(telefone1))
            {

                String[] sep = telefone1.Split(')');
                telefone1 = sep[1];
                String[] sep2 = telefone1.Split('-');
                telefone1 = sep2[0] + sep2[1];
                String ddd = sep[0].Split('(')[1];

                telefone = new Phone(ddd.Trim(), telefone1.Trim());

            }

            //Dados do Cliente
            //Sender dadosCliente = new Sender(cliente.Nome, cliente.Email, telefone);


            //Dados do Pedido
            PaymentRequest pagamento = new PaymentRequest();
            pagamento.Currency = Currency.Brl;

            pagamento.Reference = pedido.Id.ToString();

            var request = HttpContext.Current.Request;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                pagamento.RedirectUri = new Uri("http://gtiracingparts.com.br/");
            }
            else
            {
                string baseUrl = request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
                pagamento.RedirectUri = new Uri(baseUrl + "/compra-finalizada");
            }

            pagamento.Sender = new Sender("Cliente " + cliente.Nome, cliente.Email, telefone);
            pagamento.Shipping = new Shipping();
            pagamento.Shipping.ShippingType = ShippingType.NotSpecified;
            pagamento.Shipping.Cost = 0;
            pagamento.Shipping.Address = null;
            //pagamento.Sender = cliente;

            //Itens do Pedido
            ISet<ItemPedido> itensPedido = pedido.Itens;
            foreach (ItemPedido item in itensPedido)
            {
                pagamento.Items.Add(new Item(item.Produto.Referencia, item.Produto.Nome, item.Quantidade, item.Preco));
            }
            if (pedido.PrecoFrete > 0)
                pagamento.Items.Add(new Item("001", "Frete " + pedido.ModoFrete, 1, pedido.PrecoFrete));

            Uri enderecoPagamento = pagamento.Register(dadosAcesso);
            return enderecoPagamento.AbsoluteUri;
        }
        catch (PagSeguroServiceException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new Exception(exception.StatusCode.ToString() + "Erro na autorização da operação. Favor entrar em contato com a administração do site.");
                    break;

                case HttpStatusCode.BadRequest:
                    string erros = "";
                    foreach (var erro in exception.Errors)
                        erros += erro.Message + " - ";
                    throw new Exception("Má estrutura do pedido:" + erros);
                    break;

                default: break;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Problema ao requerir PagSeguro: " + ex.Message);
        }

        return null;
    }

    //public static string executarVenda(Pedido pedido)
    //{
    //    if (pedido == null)
    //        return null;
    //    try
    //    {
    //        //Dados da conta que receberá o pagamento
    //        DadoVO dado = MetodosFE.getTela("Dados do PagSeguro");
    //        if (dado == null)
    //            throw new Exception("Não foram especificadas as configurações de PagSeguro.");

    //        AccountCredentials dadosAcesso = new AccountCredentials(dado.nome, dado.referencia);

    //        //Endereço do cliente
    //        //Uol.PagSeguro.Address enderecoCliente = new Uol.PagSeguro.Address();
    //        Cliente cliente = pedido.Cliente;

    //        //Dados de Telefone
    //        String telefone1 = "(51)0000-0000";

    //        Phone telefone = null;
    //        if (!String.IsNullOrEmpty(telefone1))
    //        {

    //            String[] sep = telefone1.Split(')');
    //            telefone1 = sep[1];
    //            String[] sep2 = telefone1.Split('-');
    //            telefone1 = sep2[0] + sep2[1];
    //            String ddd = sep[0].Split('(')[1];

    //            telefone = new Phone(ddd.Trim(), telefone1.Trim());

    //        }

    //        //Dados do Cliente
    //        Sender dadosCliente = new Sender(cliente.Nome, cliente.Email, new Phone("00", "888888888"));

    //        //Dados do Pedido
    //        PaymentRequest pagamento = new PaymentRequest();
    //        pagamento.Currency = Currency.Brl;

    //        pagamento.Reference = pedido.Id.ToString();

    //        var request = HttpContext.Current.Request;

    //        if (!System.Diagnostics.Debugger.IsAttached)
    //        {
    //            pagamento.RedirectUri = new Uri("http://disfarcefantasias.com.br");
    //        }
    //        else
    //        {
    //            string baseUrl = request.Url.Scheme + "://" + request.Url.Authority +
    //                request.ApplicationPath.TrimEnd('/') + "/";
    //            pagamento.RedirectUri = new Uri(baseUrl + "/compra-finalizada");
    //        }

    //        pagamento.Sender = new Sender("Cliente " + cliente.Nome, cliente.Email, telefone);
    //        pagamento.Shipping = new Shipping();
    //        pagamento.Shipping.ShippingType = ShippingType.NotSpecified;
    //        pagamento.Shipping.Cost = 0;
    //        pagamento.Shipping.Address = null;
    //        //pagamento.Sender = cliente;

    //        //Itens do Pedido
    //        ISet<ItemPedido> itensPedido = pedido.Itens;
    //        foreach (ItemPedido item in itensPedido)
    //        {
    //            pagamento.Items.Add(new Item(item.Produto.Referencia, item.Produto.Nome, item.Quantidade, item.Preco));

    //        }
    //        if (pedido.PrecoFrete > 0)
    //            pagamento.Items.Add(new Item("001", "Frete: " + pedido.ModoFrete, 1, pedido.PrecoFrete));

    //        Uri enderecoPagamento = pagamento.Register(dadosAcesso);
    //        return enderecoPagamento.AbsoluteUri;
    //    }
    //    catch (PagSeguroServiceException exception)
    //    {
    //        switch (exception.StatusCode)
    //        {
    //            case HttpStatusCode.Unauthorized:
    //                throw new Exception(exception.StatusCode.ToString() + "Erro na autorização da operação. Favor entrar em contato com a administração do site.");
    //                break;

    //            case HttpStatusCode.BadRequest:
    //                string erros = "";
    //                foreach (var erro in exception.Errors)
    //                    erros += erro.Message + " - ";
    //                throw new Exception("Má estrutura do pedido:" + erros);
    //                break;

    //            default: break;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Problema ao requerir PagSeguro: " + ex.Message);
    //    }

    //    return null;
    //}
}