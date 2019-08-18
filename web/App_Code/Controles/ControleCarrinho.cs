using Modelos;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ControleCarrinho
/// </summary>
public static class ControleCarrinho
{
   private static Repository<Carrinho> RepositorioCarrinho
   {
      get
      {
         return new Repository<Carrinho>(NHibernateHelper.CurrentSession);
      }
   }

   private static Repository<ItemCarrinho> RepositorioItemCarrinho
   {
      get
      {
         return new Repository<ItemCarrinho>(NHibernateHelper.CurrentSession);
      }
   }

   public static int GetQuantidadeItens()
   {
      Carrinho carrinho = GetCarrinho();

      if (carrinho == null)
         return 0;

      return carrinho.Itens.Count;
   }

   public static decimal TotalCarrinho()
   {
      Carrinho carrinho = GetCarrinho();
      Cupom cupom = ControleCarrinho.GetCupom();

      decimal total = 0;
      decimal subTotal = 0;
      decimal desconto = 0;
      decimal cento = 100;

      if (cupom != null && cupom.DescontoPercentual > 0)
      {
         foreach (var item in carrinho.Itens)
            subTotal += item.Valor * item.Quantidade;

      }
      if (cupom != null && cupom.Desconto > 0)
      {
         foreach (var item in carrinho.Itens)
         {
            total += item.Valor * item.Quantidade;
         }
         total = total - cupom.Desconto;
      }
      if (cupom == null)
      {
         foreach (var item in carrinho.Itens)
            total += item.Valor * item.Quantidade;
      }

      if (subTotal > 0)
      {

         desconto = subTotal * (cupom.DescontoPercentual / cento);

         total = subTotal - desconto;

      }


      return total;
   }

   public static decimal TotalCarrinhoTransf()
   {
       Carrinho carrinho = GetCarrinho();
       Cupom cupom = ControleCarrinho.GetCupom();

       decimal total = 0;
       decimal subTotal = 0;
       decimal desconto = 0;
       decimal cento = 100;

       if (cupom != null && cupom.DescontoPercentual > 0)
       {
           foreach (var item in carrinho.Itens)
               subTotal += item.Produto.Preco.ValorAvista * item.Quantidade;

       }
       if (cupom != null && cupom.Desconto > 0)
       {
           foreach (var item in carrinho.Itens)
           {
               total += item.Produto.Preco.ValorAvista * item.Quantidade;
           }
           total = total - cupom.Desconto;
       }
       if (cupom == null)
       {
           foreach (var item in carrinho.Itens)
               total += item.Produto.Preco.ValorAvista * item.Quantidade;
       }

       if (subTotal > 0)
       {

           desconto = subTotal * (cupom.DescontoPercentual / cento);

           total = subTotal - desconto;

       }


       return total;
   }

   public static Carrinho SetCookie()
   {
      {
         string strCookie = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + Guid.NewGuid().ToString();

         HttpCookie cookieCod = new HttpCookie("CookieCod");
         cookieCod.Value = strCookie;

         cookieCod.Expires = DateTime.Now.AddDays(5d);
         HttpContext.Current.Response.Cookies.Add(cookieCod);

         Carrinho carrinho = new Carrinho();
         carrinho.Referencia = strCookie;
         carrinho.DataCriacao = DateTime.Now;
         RepositorioCarrinho.Add(carrinho);

         return carrinho;
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

   public static string GetOpcaoFrete()
   {
      Carrinho carrinho = GetCarrinho();
      if (carrinho == null)
         return null;
      return carrinho.ModoFrete;
   }

   public static void SetOpcaoFrete(string opcao)
   {
      Carrinho carrinho = GetCarrinho();
      if (carrinho != null)
      {
         carrinho.ModoFrete = opcao;
         RepositorioCarrinho.Update(carrinho);
      }
   }

   public static Carrinho GetCarrinho()
   {
      string codigo = GetCookie();
      if (codigo == null)
         return null;

      Carrinho carrinho = RepositorioCarrinho.FilterBy(x => x.Referencia.Equals(codigo)).Fetch(x => x.Cupom).FetchMany(x => x.Itens).ThenFetch(x => x.Produto).ThenFetch(x => x.Peso).Fetch(x => x.Endereco).AsEnumerable().FirstOrDefault();
      return carrinho;
   }

   public static void limparCookieCompra()
   {
      HttpCookie cookieCod = HttpContext.Current.Response.Cookies["CookieCod"];
      if (cookieCod != null)
      {
         Carrinho carrinho = RepositorioCarrinho.FindBy(x => x.Referencia.Equals(cookieCod.Value));


         //Declaração do Cookie
         cookieCod = new HttpCookie("CookieCod");
         //Adicionando um prazo negativo para eliminar o cookie
         cookieCod.Expires = DateTime.Now.AddDays(-5d);
         //Gravando o cookie na máquina
         HttpContext.Current.Response.Cookies.Add(cookieCod);

         if (carrinho != null)
            RepositorioCarrinho.Delete(carrinho);
      }
   }

   public static void AdicionaItem(Produto produto, string tamanho, Cor cor, int quantidade, string nome_pers, int num_pers)
   {
      ItemCarrinho item = new ItemCarrinho();

      Carrinho carrinho = GetCarrinho();
        if (carrinho == null)
        {
            carrinho = SetCookie();
        }

        item.Produto = produto;
      item.Tamanho = tamanho;
      if (!String.IsNullOrEmpty(nome_pers + num_pers.ToString())) 
      {
          item.Nome_pers = nome_pers;
          item.Numero_pers = num_pers;
      }
      item.Cor = cor;
      item.Quantidade = quantidade;
      RepositorioItemCarrinho.Add(item);
      carrinho.Itens.Add(item);
        if (carrinho != null)
        {
            RepositorioCarrinho.Update(carrinho);
        }
      
   }

   public static Cupom GetCupom()
   {
      return GetCarrinho().Cupom;
   }

   public static void SetCupom(string codigo)
   {
      Carrinho carrinho = GetCarrinho();

      if (carrinho != null)
      {
         Cupom cupom = ControleCupom.GetCupom(codigo);
         if (cupom == null)
            throw new Exception("Cupom inválido.");
         carrinho.Cupom = cupom;
         RepositorioCarrinho.Update(carrinho);
      }

   }

   public static IList<ItemCarrinho> GetItensCarrinho()
   {
      //Carrinho carrinho = GetCarrinho();



      return GetDescontos();
   }

   public static void ModificaItem(ItemCarrinho item)
   {
      if (item.Quantidade < 1)
         RepositorioItemCarrinho.Delete(item);
      else
         RepositorioItemCarrinho.Update(item);
   }

   public static void RemoveItem(ItemCarrinho item)
   {
      RepositorioItemCarrinho.Delete(item);
   }

   public static void RemoveItens(IList<ItemCarrinho> itensExcluidos)
   {
      Carrinho carrinho = GetCarrinho();

      IList<ItemCarrinho> itens = RepositorioItemCarrinho.FilterBy(x => x.Carrinho.Id == carrinho.Id).ToList();

      itens = itens.Where(x => !itensExcluidos.Any(y => y.Id == x.Id)).ToList();

      carrinho.Itens.Clear();

      foreach (var item in itens)
         carrinho.Itens.Add(item);

      RepositorioCarrinho.Update(carrinho);
   }

   public static void SetEndereco(Endereco endereco)
   {
      Carrinho carrinho = GetCarrinho();
      carrinho.Endereco = endereco;

      RepositorioCarrinho.Update(carrinho);
   }

   public static Endereco GetEndereco()
   {
      Carrinho carrinho = GetCarrinho();
      return carrinho.Endereco;
   }

   public static IList<ItemCarrinho> GetDescontos()
   {
      Carrinho carrinho = GetCarrinho();
      if (carrinho == null)
      {
         return new List<ItemCarrinho>();
      }

      IList<ItemCarrinho> itens = carrinho.Itens.ToList();

      //Cliente cliente = ControleLoginCliente.GetClienteAtualizado;

      if (carrinho.Cupom != null)
      {
         if (carrinho.Cupom.Ativo)
         {
            Cupom cupom = carrinho.Cupom;

            foreach (var item in itens)
            {
               DescontoCupom desconto = cupom.GetDesconto(item.Produto);
               if (desconto != null)
               {
                  if (desconto.Tipo == (int)DescontoCupom.TipoDesconto.Fixo)
                     item.Valor = item.Produto.Preco.Valor - desconto.Desconto;
                  else
                     item.Valor = item.Produto.Preco.Valor - (item.Produto.Preco.Valor * desconto.Desconto / 100);
               }
               else
                  item.Valor = item.Produto.Preco.Valor;
            }
         }
         else
         {
            foreach (var item in itens)
            {
               item.Valor = item.Produto.Preco.Valor;
            }
         }
      }
      else
      {

         foreach (var item in itens)
         {

            item.Valor = item.Produto.Preco.Valor;

         }
      }


      return itens;


   }



}