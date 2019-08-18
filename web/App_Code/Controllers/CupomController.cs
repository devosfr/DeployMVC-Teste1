using Modelos;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class CupomController : ApiController
{
   private static Repository<Cupom> RepositorioCupom
   {
      get
      {
         return new Repository<Cupom>(NHibernateHelper.CurrentSession);
      }
   }

   // GET api/<controller>/5
   public CupomDTO Get()
   {
      CupomDTO cupom = new CupomDTO();
      Cupom cupomCompra = ControleCarrinho.GetCupom();

      if (cupomCompra != null)
      {
         if (cupomCompra.DescontoPercentual > 0)
         {
            cupom.descontoPercentual = cupomCompra.DescontoPercentual;
         }
         else
         {
            cupom.desconto = cupomCompra.Desconto;
         }

         cupom.validade = cupomCompra.Validade;
         cupom.Codigo = cupomCompra.Codigo;
      }
      return cupom;
   }



   // POST api/<controller>
   public void Post(CupomDTO cupom)
   {
      Cupom cumpomDesconto = ControleCupom.GetCupom(cupom.Codigo);

      try
      {
         if (cumpomDesconto == null || !cumpomDesconto.Ativo)
         {
            throw new Exception(" Cupom inválido ou vencido");
         }
         if (cumpomDesconto != null && !cumpomDesconto.Ativo)
         {
            throw new Exception(" Cupom inválido ou vencido");
         }

         DateTime dataCupom = cumpomDesconto.Validade.Date;
         DateTime dataAtual = DateTime.Now.Date;


         int dataValidadeCupom = DateTime.Compare(dataCupom, dataAtual);

         if (cumpomDesconto != null && (dataValidadeCupom == 0 || dataValidadeCupom == 1))
            ControleCarrinho.SetCupom(cumpomDesconto.Codigo);
         else
            throw new Exception(" Cupom inválido ou vencido");
      }
      catch (Exception ex)
      {
         var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
         {
            Content = new StringContent(ex.Message),
            ReasonPhrase = ex.Message
         };
         throw new HttpResponseException(message);
      }
   }

}

