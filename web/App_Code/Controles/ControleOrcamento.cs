using Modelos;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Controle Orçamento
/// </summary>
public static class ControleOrcamento
{
    private static Repository<CarrinhoOrcamento> RepositorioCarrinhoOrcamento
    {
        get
        {
            return new Repository<CarrinhoOrcamento>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<ItemCarrinhoOrcamento> RepositorioItemCarrinhoOrcamento 
    {
        get 
        {
            return new Repository<ItemCarrinhoOrcamento>(NHibernateHelper.CurrentSession);
        }
    }

    public static int GetQuantidadeItens()
    {
        CarrinhoOrcamento carrinho = GetCarrinho();

        if (carrinho == null)
            return 0;

        return carrinho.Itens.Count;
    }

    public static decimal TotalCarrinho()
    {
        CarrinhoOrcamento carrinho = GetCarrinho();

        decimal total = 0;
        foreach (var item in carrinho.Itens)
            total += item.Valor * item.Quantidade;

        return total;
    }

    public static void SetCookie()
    {
        {
            string strCookie = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + Guid.NewGuid().ToString();

            HttpCookie cookieCod = new HttpCookie("CookieCod");
            cookieCod.Value = strCookie;

            cookieCod.Expires = DateTime.Now.AddDays(5d);
            HttpContext.Current.Response.Cookies.Add(cookieCod);

            CarrinhoOrcamento carrinho = new CarrinhoOrcamento();
            carrinho.Referencia = strCookie;
            carrinho.DataCriacao = DateTime.Now;
            RepositorioCarrinhoOrcamento.Add(carrinho);
        }
    }

    public static string GetCookie()
    {
        HttpCookie cookieCod = HttpContext.Current.Request.Cookies["CookieCod"];
        if (cookieCod == null)
        {
            return null;
        }

        return cookieCod.Value;
    }

    public static CarrinhoOrcamento GetCarrinho()
    {
        string codigo = GetCookie();
        if (codigo == null)
            return null;

        CarrinhoOrcamento carrinho = RepositorioCarrinhoOrcamento.FilterBy(x => x.Referencia.Equals(codigo)).Fetch(x => x.Itens).FirstOrDefault();
        return carrinho;
    }

    public static void limparCookieCompra()
    {
        HttpCookie cookieCod = HttpContext.Current.Response.Cookies["CookieCod"];
        if (cookieCod != null)
        {
            CarrinhoOrcamento carrinho = RepositorioCarrinhoOrcamento.FindBy(x => x.Referencia.Equals(cookieCod.Value));


            //Declaração do Cookie
            cookieCod = new HttpCookie("CookieCod");
            //Adicionando um prazo negativo para eliminar o cookie
            cookieCod.Expires = DateTime.Now.AddDays(-5d);
            //Gravando o cookie na máquina
            HttpContext.Current.Response.Cookies.Add(cookieCod);

            if (carrinho != null)
                RepositorioCarrinhoOrcamento.Delete(carrinho);
        }
    }

    public static void AdicionaItem(Produto produto, Tamanhos tamanho, Cor cor, int quantidade)
    {
        ItemCarrinhoOrcamento item = new ItemCarrinhoOrcamento();

        CarrinhoOrcamento carrinho = GetCarrinho();
        if (carrinho == null)
        {
            SetCookie();
            carrinho = GetCarrinho();
        }


        item.Produto = produto;
        item.Tamanho = tamanho;
        item.Cor = cor;
        item.Quantidade = quantidade;

        RepositorioItemCarrinhoOrcamento.Add(item);

        carrinho.Itens.Add(item);

        RepositorioCarrinhoOrcamento.Update(carrinho);


    }

    public static IList<ItemCarrinhoOrcamento> GetItensCarrinho()
    {
        CarrinhoOrcamento carrinho = GetCarrinho();



        return carrinho.Itens;
    }

    public static void ModificaItem(ItemCarrinhoOrcamento item)
    {
        if (item.Quantidade < 1)
            RepositorioItemCarrinhoOrcamento.Delete(item);
        else
            RepositorioItemCarrinhoOrcamento.Update(item);
    }

    public static void RemoveItem(ItemCarrinhoOrcamento item)
    {
        RepositorioItemCarrinhoOrcamento.Delete(item);
    }
    public static void RemoveItens(IList<ItemCarrinhoOrcamento> itensExcluidos)
    {
        RepositorioItemCarrinhoOrcamento.Delete(itensExcluidos);
    }

}