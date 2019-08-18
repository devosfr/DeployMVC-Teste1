using System;
using System.Linq;
using System.Web;
using Modelos;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// Summary description for ControleCupom
/// </summary>
public static class ControleCupom
{

    private static Repository<Cupom> RepositorioCupom
    {
        get
        {
            return new Repository<Cupom>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<DescontoCupom> RepositorioDesconto
    {
        get
        {
            return new Repository<DescontoCupom>(NHibernateHelper.CurrentSession);
        }
    }

    public static void CriarCupom(Cliente cliente, Cupom cupomPai = null)
    {

        if (cliente == null)
            throw new Exception("É preciso definir um cliente para receber o cupom.");

        Cupom novoCupom = CriarCopiaDoModelo();

        if (cupomPai != null)
            novoCupom.CupomPai = cupomPai;

        while (true)
        {
            string hash = GetSHA1Hash(DateTime.Now.ToString());

            string codigo = hash.CortarTexto(6).ToUpper();

            if (RepositorioCupom.All().Count(x => x.Codigo.Equals(codigo)) == 0)
            {
                novoCupom.Codigo = codigo;
                break;
            }

        }

        novoCupom.Cliente = cliente;
        novoCupom.DataCriacao = DateTime.Now;

        RepositorioCupom.Add(novoCupom);

        EmitirAvisoCriacaoCupom(novoCupom);
    }

    public static string GetSHA1Hash(string input)
    {
        //SHA1 hash
        SHA1 SHA1Hash = SHA1.Create();

        // Convert the input string to a byte array and compute the hash.
        byte[] data = SHA1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    public static Cupom CriarCopiaDoModelo()
    {
        Cupom modelo = RepositorioCupom.FindBy(x => x.Modelo);

        if (modelo == null)
            throw new Exception("Nenhum modelo de cupom foi definido ainda.");

        Cupom cupom = new Cupom();

        cupom.Ativo = true;


        foreach (var desconto in modelo.Descontos)
        {
            DescontoCupom copiaDesconto = new DescontoCupom();

            copiaDesconto.Ativo = desconto.Ativo;
            copiaDesconto.Comissao = desconto.Comissao;
            copiaDesconto.Tipo = desconto.Tipo;
            copiaDesconto.Produto = desconto.Produto;
            copiaDesconto.Desconto = desconto.Desconto;

            RepositorioDesconto.Add(copiaDesconto);
            cupom.Descontos.Add(copiaDesconto);
        }

        return cupom;

    }

    public static void EmitirAvisoCriacaoCupom(Cupom cupom)
    {
        try
        {
            DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

            EnvioEmailsVO envio = new EnvioEmailsVO();
            envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = cupom.Cliente.Email;
            envio.assuntoMensagem = "Seu novo código de Cupom da " + Configuracoes.getSetting("NomeSite");

            DadoVO texto = MetodosFE.getTela("E-mail - Cupom");

            //Atribui ao método Body a texto da mensagem
            string v_recebe = "";


            v_recebe = texto.descricao;
            v_recebe = v_recebe.Replace("[Codigo]", cupom.Codigo);

            envio.conteudoMensagem = v_recebe;


            bool recebeu = EnvioEmails.envioemails(envio);

            if (recebeu)
            {
                //return true;
            }
            else
            {
                throw new Exception("Ocorreram problemas no envio do e-mail. Tente mais tarde.");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public static Cupom GetCupom(string codigo)
    {
        if (codigo == null)
            return null;

        Cupom cupom = RepositorioCupom.FindBy(x => x.Codigo.Equals(codigo));



        return cupom;
    }



}