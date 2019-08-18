using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Modelos;

public class EnderecoController : ApiController
{
    private Repository<Endereco> RepoEndereco
    {
        get
        {
            return new Repository<Endereco>(NHibernateHelper.CurrentSession);
        }
    }

    // GET api/<controller>
    public IEnumerable<Endereco> Get()
    {
        Cliente cliente = ControleLoginCliente.GetClienteLogado();

        IList<Endereco> enderecos = RepoEndereco.FilterBy(x => x.Cliente.Id == cliente.Id).Fetch(x => x.Cidade).Fetch(x => x.Estado).ToList();


        return enderecos;
    }

    // POST api/<controller>
    public void Post(Endereco endereco)
    {

        IList<Endereco> enderecosLista = RepoEndereco.All().ToList();

        for (int i = 0; i < enderecosLista.Count; i++)
        {
            if (enderecosLista[i].Id == endereco.Id)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Este Endereço já foi Cadastrado"),
                    ReasonPhrase = "Este Endereço já foi Cadastrado"
                };
                throw new HttpResponseException(message);
            }

        }

        try
        {

            Cliente cliente = ControleLoginCliente.GetClienteLogado();
            endereco.Cliente = cliente;

            if (!ControleValidacao.validaCEP(endereco.CEP))
                throw new Exception("CEP inválido.");

            if (endereco.Numero == 0)
                throw new Exception("Número inválido.");

            if (endereco.Estado == null)
                throw new Exception("Estado inválido.");
            if (endereco.Cidade == null)
                throw new Exception("Cidade inválida.");

            if (endereco.Id != 0)
            {
                Endereco enderecoBD = RepoEndereco.FindBy(x => x.Id == endereco.Id && x.Cliente.Id == cliente.Id);

                enderecoBD.CEP = endereco.CEP;
                enderecoBD.Bairro = endereco.Bairro;
                enderecoBD.Cidade = endereco.Cidade;
                enderecoBD.Numero = endereco.Numero;
                enderecoBD.Complemento = endereco.Complemento;
                enderecoBD.Logradouro = endereco.Logradouro;

                RepoEndereco.Update(enderecoBD);
            }
            else
            {
                IList<Endereco> enderecos = RepoEndereco.FilterBy(x => x.Cliente.Id == cliente.Id).Fetch(x => x.Cidade).Fetch(x => x.Estado).ToList();
                if (enderecos.Count == 0)
                    endereco.Principal = true;
                else
                    endereco.Principal = false;



                RepoEndereco.Add(endereco);
            }
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

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
        try
        {
            Cliente cliente = ControleLoginCliente.GetClienteLogado();
            Endereco enderecoBD = RepoEndereco.FindBy(x => x.Id == id && x.Cliente.Id == cliente.Id);
            if (enderecoBD == null)
                throw new Exception("Endereço não encontrado.");
            if (enderecoBD.Principal)
                throw new Exception("Este é seu endereço principal. Não pode ser excluído.");
            //if (enderecoBD.Equals(ControleCarrinho.GetEndereco()))
            //    ControleCarrinho.SetEndereco(null);

            RepoEndereco.Delete(enderecoBD);
            ControleCarrinho.SetEndereco(null);

            
        }        catch (Exception ex)

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
