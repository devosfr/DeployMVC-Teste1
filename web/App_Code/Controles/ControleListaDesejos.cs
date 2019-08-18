using Modelos;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ControleCarrinho
/// </summary>
/// 
public static class ControleListaDesejos
{
    private static Repository<ListaDesejo> RepositorioListaDesejo
    {
        get
        {
            return new Repository<ListaDesejo>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<ItemListaDesejo> RepositorioItemListaDesejo
    {
        get
        {
            return new Repository<ItemListaDesejo>(NHibernateHelper.CurrentSession);
        }
    }

    public static int GetQuantidadeItens()
    {
        ListaDesejo listaDesejo = GetLista();

        if (listaDesejo == null)
            return 0;

        return listaDesejo.Itens.Count;
    }

    public static decimal TotalCarrinho()
    {
        ListaDesejo listaDesejo = GetLista();

        decimal total = 0;
        foreach (var item in listaDesejo.Itens)
            total += item.Valor * item.Quantidade;

        return total;
    }

    public static ListaDesejo SetCookie()
    {
        {
            string strCookie = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + Guid.NewGuid().ToString();

            HttpCookie cookieCod = new HttpCookie("CookieCodLista");
            cookieCod.Value = strCookie;

            cookieCod.Expires = DateTime.Now.AddDays(5d);
            HttpContext.Current.Response.Cookies.Add(cookieCod);

            ListaDesejo lista = new ListaDesejo();
            lista.Referencia = strCookie;
            lista.DataCriacao = DateTime.Now;
            RepositorioListaDesejo.Add(lista);

            return lista;
        }
    }

    public static string GetCookie()
    {
        HttpCookie cookieCod = HttpContext.Current.Request.Cookies["CookieCodLista"];
        if (cookieCod == null)
        {
            return null;
        }

        return cookieCod.Value;
    }

    public static ListaDesejo GetLista()
    {
        string codigo = GetCookie();

        if (codigo == null)
            return null;

        ListaDesejo lista = RepositorioListaDesejo.FilterBy(x => x.Referencia.Equals(codigo)).Fetch(x => x.Cupom).FetchMany(x => x.Itens).ThenFetch(x => x.Produto).ThenFetch(x => x.Peso).Fetch(x => x.Endereco).AsEnumerable().FirstOrDefault();
        return lista;
    }

    public static void AdicionaItem(Produto produto, Tamanhos tamanho, Cor cor, int quantidade)
    {
        ItemListaDesejo item = new ItemListaDesejo();

        ListaDesejo listaDesejo = GetLista();

        if (listaDesejo == null)
        {
            listaDesejo = SetCookie();
        }

        item.Produto = produto;
        item.Tamanho = tamanho;
        item.Cor = cor;
        item.Quantidade = quantidade;

        RepositorioItemListaDesejo.Add(item);

        listaDesejo.Itens.Add(item);

        RepositorioListaDesejo.Update(listaDesejo);


    }

    public static IList<ItemListaDesejo> GetItensLista()
    {
        //Carrinho carrinho = GetCarrinho();



        return GetDescontos();
    }

    public static void ModificaItem(ItemListaDesejo item)
    {
        if (item.Quantidade < 1)
            RepositorioItemListaDesejo.Delete(item);
        else
            RepositorioItemListaDesejo.Update(item);
    }

    public static void RemoveItem(ItemListaDesejo item)
    {
        RepositorioItemListaDesejo.Delete(item);
    }

    //public static void RemoveItemPorId(ItemListaDesejo item)
    //{
    //    RepositorioItemListaDesejo.Delete(item);
    //}

    public static void RemoveItens(IList<ItemListaDesejo> itensExcluidos)
    {
        ListaDesejo listaDesejos = GetLista();

        IList<ItemListaDesejo> itens = RepositorioItemListaDesejo.FilterBy(x => x.ListaDesejo.Id == listaDesejos.Id).ToList();

        itens = itens.Where(x => !itensExcluidos.Any(y => y.Id == x.Id)).ToList();

        listaDesejos.Itens.Clear();

        foreach (var item in itens)
            listaDesejos.Itens.Add(item);

        RepositorioListaDesejo.Update(listaDesejos);
    }

    public static IList<ItemListaDesejo> GetDescontos()
    {
        ListaDesejo listaDesejo = GetLista();

        if (listaDesejo == null)
        {
            return new List<ItemListaDesejo>();
        }

        IList<ItemListaDesejo> itens = listaDesejo.Itens.ToList();

        foreach (var item in itens)
        {

            item.Valor = item.Produto.Preco.Valor;

        }

        return itens;

    }

}