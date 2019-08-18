using Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for ControleFrete
/// </summary>
public class ControleFrete
{
    private static Repository<Endereco> RepositorioEndereco
    {
        get
        {
            return new Repository<Endereco>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<OpcaoFreteLocalidade> RepositorioOpcoesLocalidade
    {
        get
        {
            return new Repository<OpcaoFreteLocalidade>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<EnderecoCEP> RepositorioCEP
    {
        get
        {
            return new Repository<EnderecoCEP>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Cidade> RepositorioCidade
    {
        get
        {
            return new Repository<Cidade>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Estado> RepositorioEstado
    {
        get
        {
            return new Repository<Estado>(NHibernateHelper.CurrentSession);
        }
    }

    private static Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    //40010 - SEDEX Varejo
    //40045 - SEDEX a Cobrar Varejo
    //40215 - SEDEX 10 Varejo
    //40290 - SEDEX Hoje Varejo
    //41106 - PAC Varejo

    public enum TipoFrete { SEDEX = 40010, SEDEX_A_COBRAR = 40045, SEDEX10 = 40215, SEDEXHoje = 40290, PAC = 41106 };

    public enum FormatoFrete { FORMATO_CAIXA = 1, FORMATO_ROLO = 2, FORMATO_ENVELOPE = 3 };

    public static void LimpaOpcaoFrete()
    {
    }

    public static IList<FreteDTO> GetOpcoesFrete(String cepCliente)
    {
        //if (modoEnvio == null)
        //    return null;

        float peso = 0;

        decimal total = 0;

        IList<ItemCarrinho> colecaoItens = ControleCarrinho.GetItensCarrinho();

        decimal somaAltura = 0, somaLargura = 0, somaComprimento = 0;

        decimal maiorAltura = 0, maiorLargura = 0, maiorComprimento = 0;

        if (colecaoItens.Count > 0)
        {
            //decimal padding = 0.00m;

            foreach (ItemCarrinho linha in colecaoItens)
            {
                float pesoUnidade = linha.Quantidade * linha.Produto.Peso.Valor;
                somaAltura += linha.Produto.Peso.Altura * linha.Quantidade;
                somaLargura += linha.Produto.Peso.Largura * linha.Quantidade;
                somaComprimento += linha.Produto.Peso.Profundidade * linha.Quantidade;

                maiorAltura = Math.Max(maiorAltura, linha.Produto.Peso.Altura);
                maiorLargura = Math.Max(maiorLargura, linha.Produto.Peso.Largura);
                maiorComprimento = Math.Max(maiorComprimento, linha.Produto.Peso.Profundidade);

                total += linha.Valor * linha.Quantidade;

                peso += pesoUnidade;
            }
        }

        decimal[] somatorios = new decimal[] { somaAltura, somaLargura, somaComprimento };
        decimal somaMinina = somatorios.Min();
        IList<FreteDTO> valoresCorreio = null;

        if (somaAltura == somaMinina)
            valoresCorreio = ValorFreteCorreios(cepCliente, peso, maiorComprimento, somaAltura, maiorLargura);
        else if (somaComprimento == somaMinina)
            valoresCorreio = ValorFreteCorreios(cepCliente, peso, somaComprimento, maiorAltura, maiorLargura);
        else
            valoresCorreio = ValorFreteCorreios(cepCliente, peso, maiorComprimento, maiorAltura, somaLargura);

        IList<FreteDTO> valoresTransporte = OpcoesLocalidade(cepCliente, peso, total);

        valoresCorreio = valoresCorreio.Concat(valoresTransporte).ToList();
        
        return valoresCorreio;
    }

    public static IList<FreteDTO> GetOpcoesFrete(String cepCliente, int idProduto, int idTamanho)
    {
        //if (modoEnvio == null)
        //    return null;

        Produto produto = RepositorioProduto.FindBy(idProduto);

        float peso = produto.Peso.Valor;

        decimal valor = produto.Preco.Valor;

        IList<FreteDTO> valoresCorreio = ValorFreteCorreios(cepCliente, peso, produto.Peso.Profundidade, produto.Peso.Altura, produto.Peso.Largura);

        IList<FreteDTO> valoresTransporte = OpcoesLocalidade(cepCliente, peso, valor);

        valoresCorreio = valoresCorreio.Concat(valoresTransporte).ToList();
        
        return valoresCorreio;
    }

    public static FreteDTO ValorFreteCarrinho()
    {
        Cliente cliente = ControleLoginCliente.GetClienteLogado();

        string opcaoFrete = ControleCarrinho.GetOpcaoFrete();

        Endereco endereco = ControleCarrinho.GetEndereco();

        if (String.IsNullOrEmpty(opcaoFrete))
            throw new Exception("Não foi escolhida nenhuma opção de frete para este pedido.");
        if (endereco == null)
            throw new Exception("Não foi escolhida nenhuma opção de endereço para este pedido.");

        if (opcaoFrete.Contains("C"))
        {
            IList<FreteDTO> opcoes = GetOpcoesFrete(endereco.CEP);

            FreteDTO frete = opcoes.FirstOrDefault(x => x.Codigo.Equals(opcaoFrete));

            return frete;
        }
        else
        {
            int idOpcao = Convert.ToInt32(opcaoFrete.Replace("L", ""));
            OpcaoFreteLocalidade item = RepositorioOpcoesLocalidade.FindBy(idOpcao);
            FreteDTO frete = new FreteDTO() { Nome = item.Nome, Preco = item.Preco, Codigo = "L" + item.Id.ToString() };
            return frete;
        }

        return null;
    }

    public static int[] GetDimensoesEnvio()
    {
        return null;
    }

    public static decimal calculaFrete(string contrato, string senha, string tipoEnvio, string cepOrigem, string cepDestino, string VlPeso, int tipoFormato, decimal VlComprimento, decimal VlAltura, decimal VlLargura, decimal VlDiametro, string aviso)
    {
        try
        {
            //SistemaCorreio.cServico teste = new SistemaCorreio.cServico
            ControleCorreio.CalcPrecoPrazoWS teste = new ControleCorreio.CalcPrecoPrazoWS();

            ControleCorreio.cResultado resultado = teste.CalcPreco(contrato, senha, tipoEnvio, cepOrigem.Replace("-", ""), cepDestino.Replace("-", ""), VlPeso, (int)tipoFormato, VlComprimento, VlAltura, VlLargura, VlDiametro, "N", 0, aviso);

            decimal valor = Convert.ToDecimal(resultado.Servicos[0].Valor);
            //resposta.ErroFrete = "valor tipo " + (int)dados.FormatoTipo + " " + ((int)dados.TipoEnvio).ToString() + " " + dados.CepOrigem.Replace("-", "") + resultado.Servicos[0].Valor;
            return valor;
        }
        catch (Exception ex)
        {
            throw new Exception("Problema ao calcular frete: " + ex.Message + ":" + ex.InnerException);
        }
    }

    public static List<decimal> calculaFreteCorreio(string contrato, string senha, string tipoEnvio, string cepOrigem, string cepDestino, string VlPeso, int tipoFormato, decimal VlComprimento, decimal VlAltura, decimal VlLargura, decimal VlDiametro, string aviso)
    {
        try
        {
            //SistemaCorreio.cServico teste = new SistemaCorreio.cServico
            ControleCorreio.CalcPrecoPrazoWS teste = new ControleCorreio.CalcPrecoPrazoWS();

            ControleCorreio.cResultado resultado = teste.CalcPrecoPrazo(contrato, senha, tipoEnvio, cepOrigem.Replace("-", ""), cepDestino.Replace("-", ""), VlPeso, (int)tipoFormato, VlComprimento, VlAltura, VlLargura, VlDiametro, "N", 0, aviso);

            List<decimal> valores = new List<decimal>();
            foreach (var item in resultado.Servicos)
                valores.Add(Convert.ToDecimal(item.Valor));

            foreach (var item in resultado.Servicos) 
            {
                valores.Add(Convert.ToDecimal(item.PrazoEntrega));
            }

            return valores;
        }
        catch (Exception ex)
        {
            throw new Exception("Problema ao calcular frete: " + ex.Message + ":" + ex.InnerException);
        }
    }

    public static List<FreteDTO> ValorFreteCorreios(String CEPDestino, float peso, decimal comprimento = 0, decimal altura = 0, decimal largura = 0, decimal diametro = 0)
    {
        DadoVO dado = MetodosFE.getTela("Informações de Correio");
        string contrato = null;
        string senha = null;
        string AvisoRecebimento = "N";
        //if (dado != null)

        contrato = dado.nome;
        senha = dado.referencia;
        AvisoRecebimento = dado.valor;


        List<FreteDTO> itens = new List<FreteDTO>();

        string modoEnvio = "";
        modoEnvio += ((int)TipoFrete.SEDEX).ToString() + "," + ((int)TipoFrete.SEDEX10).ToString() + "," + ((int)TipoFrete.PAC).ToString();

        //Scope.TipoEnvio = CalcularFrete.TipoEnvio.SEDEX_SEM_CONTRATO;
        string CEPOrigem = dado.ordem;
        if (!ControleValidacao.validaCEP(CEPDestino))
            throw new Exception("CEP de destino inválido");

        CEPOrigem = CEPOrigem.Replace("-", "");

        CEPDestino = CEPDestino.Replace("-", "");

        string VlPeso = null;
        VlPeso = peso.ToString().Replace(',', '.');

        decimal VlComprimento = 0;
        if (comprimento == 0)
            VlComprimento = 16;
        else
            VlComprimento = comprimento;

        decimal VlAltura;
        if (altura == 0)
            VlAltura = 11;
        else
            VlAltura = altura;

        decimal VlLargura;
        if (largura == 0)
            VlLargura = 11;
        else
            VlLargura = largura;

        decimal VlDiametro = 0;
        //if (diametro == 0)
        //    VlDiametro = 5;
        //else
        //    VlDiametro = diametro;

        List<decimal> valores = calculaFreteCorreio(contrato, senha, modoEnvio, CEPOrigem, CEPDestino, VlPeso, (int)FormatoFrete.FORMATO_CAIXA, VlComprimento, VlAltura, VlLargura, VlDiametro, AvisoRecebimento);

        if (valores[0] > 0)
            itens.Add(new FreteDTO() { Nome = "SEDEX", Preco = valores[0], Prazo = Convert.ToInt32(valores[3]), Codigo = "C40010" });
        if (valores[1] > 0)
            itens.Add(new FreteDTO() { Nome = "SEDEX 10", Preco = valores[1], Prazo = Convert.ToInt32(valores[4]), Codigo = "C40215" });
        if (valores[2] > 0)
            itens.Add(new FreteDTO() { Nome = "PAC", Preco = valores[2], Prazo = Convert.ToInt32(valores[5]), Codigo = "C41106" });

        //if (MetodosFE.getFreteCorreio(MetodosFE.CORREIO_SEDEX) && peso <= 30)
        //{
        //    if (valores[0] > 0)
        //        itens.Add(new ListItem("SEDEX", valores[0].ToString()));
        //}
        //if (MetodosFE.getFreteCorreio(MetodosFE.CORREIO_SEDEX10) && peso <= 10)
        //{
        //    if (valores[1] > 0)
        //        itens.Add(new ListItem("SEDEX10", valores[1].ToString()));

        //}

        //if (MetodosFE.getFreteCorreio(MetodosFE.CORREIO_PAC) && peso <= 30)
        //{
        //    if (valores[2] > 0)
        //        itens.Add(new ListItem("PAC", valores[2].ToString()));
        //}

        return itens;
    }

    public static IList<FreteDTO> OpcoesLocalidade(string CEP, float peso, decimal valorTotal)
    {

        EnderecoCEP endereco = BuscaEnderecoCEP(CEP);

        var pesquisa = RepositorioOpcoesLocalidade.FilterBy(x =>
            (x.Bairro == null && x.Estado == null && x.Cidade == null && x.Ativo)
            || (x.Estado.Id == endereco.Estado.Id && x.Cidade.Id == endereco.Cidade.Id && x.Bairro.ToLower().Equals(endereco.Bairro) && x.Ativo)
            || (x.Estado.Id == endereco.Estado.Id && x.Cidade.Id == endereco.Cidade.Id && x.Ativo)
            || (x.Estado.Id == endereco.Estado.Id && x.Cidade == null)
            ).OrderBy(x => x.Preco).ToList();

        IList<OpcaoFreteLocalidade> opcoes = new List<OpcaoFreteLocalidade>();

        foreach (var item in pesquisa)
        {
            var opcao = opcoes.FirstOrDefault(x => x.Nome.Equals(item.Nome) && x.Preco < item.Preco);
            if (opcao == null)
                opcoes.Add(item);
        }

        List<FreteDTO> opcoesExibicao = new List<FreteDTO>();

        foreach (var item in opcoes)
            opcoesExibicao.Add(new FreteDTO() { Nome = item.Nome, Prazo = item.Prazo,  Preco = item.Preco, Codigo = "L" + item.Id.ToString() });

        return opcoesExibicao;
    }

    public static EnderecoCEP BuscaEnderecoCEP(string CEP)
    {

        EnderecoCEP endereco = RepositorioCEP.FindBy(x => x.CEP.Equals(CEP));

        if (endereco != null)
            return endereco;


        string uf = "";
        string cidadeString = "";
        string bairro = "";
        string tipo = "";
        string logradouro = "";
        string resultado = "0";
        string resultado_txt = "CEP não encontrado";

        //Cria um DataSet  baseado no retorno do XML
        DataSet ds = new DataSet();
        ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + CEP.Replace("-", "").Trim() + "&formato=xml");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                resultado = ds.Tables[0].Rows[0]["resultado"].ToString();
                switch (resultado)
                {
                    case "1":
                        uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                        cidadeString = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                        bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                        tipo = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                        logradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                        resultado_txt = "CEP completo";
                        break;
                    case "2":
                        uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                        cidadeString = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                        bairro = "";
                        tipo = "";
                        logradouro = "";
                        resultado_txt = "CEP  único";
                        break;
                    default:
                        uf = "";
                        cidadeString = "";
                        bairro = "";
                        tipo = "";
                        logradouro = "";
                        resultado_txt = "CEP não  encontrado";
                        return null;
                }

                endereco = new EnderecoCEP();
                Estado estado = RepositorioEstado.FindBy(x => x.Sigla.ToLower().Equals(uf.ToLower()));

                if (estado == null)
                    return null;

                endereco.Estado = estado;

                string cidadePesquisa = MetodosFE.RemoverAcentos(cidadeString).ToUpper();
                Cidade cidade = RepositorioCidade.FindBy(x => x.Estado.Id == estado.Id && x.Nome.Equals(cidadePesquisa));

                if (cidade == null)
                    return null;

                endereco.Cidade = cidade;

                endereco.Logradouro = MetodosFE.RemoverAcentos(tipo + " " + logradouro).ToLower();
                endereco.Bairro = MetodosFE.RemoverAcentos(bairro).ToLower();

                RepositorioCEP.Add(endereco);//.Insert(endereco);

                return endereco;
            }
        }

        return null;

        //Exemplo do retorno da  WEB
        //<?xml version="1.0"  encoding="iso-8859-1"?>
        //<webservicecep>
        //<uf>RS</uf>
        //<cidade>Porto  Alegre</cidade>
        //<bairro>Passo  D'Areia</bairro>
        //<tipo_logradouro>Avenida</tipo_logradouro>
        //<logradouro>Assis Brasil</logradouro>
        //<resultado>1</resultado>
        //<resultado_txt>sucesso - cep  completo</resultado_txt>
        //</webservicecep>
    }
}