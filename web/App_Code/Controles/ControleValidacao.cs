using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Modelos;
using System.Web;

/// <summary>
/// Summary description for ControleValidacao
/// </summary>
public class ControleValidacao
{
    private static Repository<UsuarioVO> repUsuario
    {
        get
        {
            return new Repository<UsuarioVO>(NHibernateHelper.CurrentSession);
        }
    }
    private static Repository<Cliente> repCliente
    {
        get
        {
            return new Repository<Cliente>(NHibernateHelper.CurrentSession);
        }
    }

    public static bool validaTelefone(string telefone)
    {
        //Formato (99)9999-9999X
        //        01234567890123    
        if (telefone.Length != 13 && telefone.Length != 14)
            return false;

        for (int i = 0; i < telefone.Length; i++)
        {
            char c = telefone[i];
            switch (i)
            {
                case 0:
                    if (c != '(')
                        return false;
                    break;
                case 3:
                    if (c != ')')
                        return false;
                    break;
                case 8:
                    if (c != '-')
                        return false;
                    break;
                case 13:
                    if (c == '_')
                        return true;
                    if (c > '9' || c < '0')
                        return false;
                    break;
                default:
                    if (c > '9' || c < '0')
                        return false;
                    break;
            }
        }


        return true;
    }

    public static string converteTelefone(string telefone)
    {
        try
        {
            telefone = telefone.Insert(0, "(");
            telefone = telefone.Insert(3, ")");
            telefone = telefone.Insert(8, "-");

            return telefone;
        }
        catch (Exception ex)
        {
            throw new Exception("Telefone inválido.");
        }
    }

    public static void ContaTentativas(string local)
    {
        if (HttpContext.Current.Session[local] != null)
        {
            int tentativas = (int)HttpContext.Current.Session[local];
            if (tentativas < 5)
            {
                tentativas++;
                HttpContext.Current.Session[local] = tentativas;
            }
            else
                throw new Exception("Aguarde alguns minutos e tente novamente.");
        }
        else
        {
            HttpContext.Current.Session[local] = 0;
        }
    }

    public static string converteHora(string hora)
    {
        try
        {
            hora = hora.Insert(2, ":");
            return hora;
        }
        catch (Exception ex)
        {
            throw new Exception("Hora inválida.");
        }
    }
    public static string converteData(string data)
    {
        try
        {
            data = data.Insert(2, "/");
            data = data.Insert(5, "/");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("Data inválida.");
        }
    }

    public static bool validaCEP(string CEP)
    {
        //Formato: 99999-999
        //         012345678

        if (CEP.Length != 9)
            return false;

        for (int i = 0; i < CEP.Length; i++)
        {
            char c = CEP[i];
            switch (i)
            {
                case 5:
                    if (c != '-')
                        return false;
                    break;
                default:
                    if (c > '9' || c < '0')
                        return false;
                    break;
            }
        }

        return true;
    }
    public static bool validaCPFCNPJ(string CPFCNPJ)
    {
        //Formato: 999.999.999-99
        //         01234567890123
        CPFCNPJ = CPFCNPJ.Replace("_", "");
        if (CPFCNPJ.Length != 14 && CPFCNPJ.Length != 11)
            return false;

        for (int i = 0; i < CPFCNPJ.Length; i++)
        {
            if (CPFCNPJ[i] > '9' || CPFCNPJ[i] < '0')
                return false;
        }

        return true;
    }

    public static bool validaCNPJ(string cnpj)
    {
        if (Regex.IsMatch(cnpj, @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)"))
            return true;
        else
            return false;
    }

    public static bool validaCPF(string cpf)
    {
        if (Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)"))
            return true;
        else
            return false;
    }

    public static bool CPFUnico(string cpf)
    {
        if (validaCPF(cpf))
        {
            IList<Cliente> clientes = repCliente.FilterBy(x => x.CPF != null && x.CPF.Equals(cpf)).ToList();

            if (clientes.Count == 0)
                return true;
        }
        return false;
    }

    public static bool CNPJUnico(string cnpj)
    {
        if(validaCNPJ(cnpj))
        {
            IList<Cliente> clientes = repCliente.FilterBy(x => x.CNPJ != null && x.CNPJ.Equals(cnpj)).ToList();
            if (ControleLoginCliente.ClienteLogado())
            {
                var cliente = ControleLoginCliente.GetClienteLogado();
                clientes = clientes.Where(x => x.Id != cliente.Id).ToList();
            }
            if (clientes.Count == 0)
                return true;
        }
        return false;
    }

    public static bool validaNumero(string numero)
    {
        if (string.IsNullOrEmpty(numero))
            return false;

        for (int i = 0; i < numero.Length; i++)
        {
            char c = numero[i];
            if (c > '9' || c < '0')
                return false;
        }
        return true;
    }
    public static bool validaEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address.Equals(email);
        }
        catch
        {
            return false;
        }
    }

    public static bool validaLogin(string login)
    {
        if (repUsuario.All().Any(x => x.login.ToLower().Equals(login.ToLower())))
            return false;
        return true;
    }

    public static bool emailDisponivel(string email)
    {
        Cliente usuario = repCliente.All().Where(x => x.Email.Equals(email)).FirstOrDefault();
        if (usuario != null)
            if (ControleLoginCliente.GetClienteLogado() != null)
            {
                if (!usuario.Equals(ControleLoginCliente.GetClienteLogado()))
                    return false;
            }
            else
                return false;

        return true;
    }
}