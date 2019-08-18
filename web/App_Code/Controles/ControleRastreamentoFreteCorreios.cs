using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Modelos;
using RDev.Correios.Helpers;

namespace RDev.Correios
{
    public class RastreamentoFreteCorreios
    {
        public RetornoRastreamento ConsultaRastreioCorreios(string codigo)
        {
            RetornoRastreamento rastreamento = new RetornoRastreamento();
            try
            {
                // Aplica campos de parâmetros
                HttpWebResponse retorno = RetornoWebRequest(codigo);
                // Lê o objeto e faz a atribuição à variável
                StreamReader stream = new StreamReader(retorno.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
                string dados = stream.ReadToEnd();
                // Transforma em um documento HTML - HtmlAgilityPack
                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(dados);
                // Captura apenas a tabela contendo os dados do envio
                HtmlNode tabela = html.DocumentNode.SelectSingleNode("//table");
                if (tabela != null)
                {
                    HtmlNodeCollection HtmlNode = tabela.SelectNodes("//tr");
                    // Nesse caso você pode capturar as colunas e linhas e trabalhar conforme desejar
                    // Apenas fiz o loop para remover a formatação inicial da tabela
                    string htmlTable = "<table border='1' align='center' width='100%' width='100%'>";
                    // Extrai as linhas da tabela
                    htmlTable += "<tr>" + HtmlNode[0].InnerHtml.Replace("#CC0000", "#1E90FF").Replace("td", "th") +
                                 "</tr>";
                    HtmlNode.RemoveAt(0);
                    foreach (HtmlNode linha in HtmlNode)
                        htmlTable += "<tr>" + linha.InnerHtml + "</tr>";
                    htmlTable += "</table>";
                    // Exibe a tabela de rastreamento
                    rastreamento.Html = (htmlTable);
                    rastreamento.DataSet = HtmlTableParser.ParseDataSet(htmlTable);
                    rastreamento.ListObject = CheckOrder(codigo);
                    rastreamento.Sucesso = true;
                }
                else
                {
                    rastreamento.Sucesso = false;
                    rastreamento.Html =
                        ("<br /><br /> O nosso sistema não possui dados sobre o objeto informado. Se o objeto foi postado recentemente, é natural que seus rastros não tenham ingressado no sistema, nesse caso, por favor, tente novamente mais tarde. Adicionalmente, verifique se o código digitado está correto: <b>" +
                         codigo + "</b>");
                }
                // Finaliza objetos
                stream.Close();
                retorno.Close();
            }
            catch (Exception)
            {
                rastreamento.Sucesso = false;
                rastreamento.Html =
                    ("<br /><br /> O nosso sistema não possui dados sobre o objeto informado. Se o objeto foi postado recentemente, é natural que seus rastros não tenham ingressado no sistema, nesse caso, por favor, tente novamente mais tarde. Adicionalmente, verifique se o código digitado está correto: <b>" +
                     codigo + "</b>");
            }
            return rastreamento;
        }

        private static HttpWebResponse RetornoWebRequest(string codigo)
        {
            string parametros = "?P_COD_UNI=" + codigo + "&P_LINGUA=001&P_TIPO=001";
            // Cria o objeto de requisição
            WebRequest requisicao =
                WebRequest.Create("http://websro.correios.com.br/sro_bin/txect01$.QueryList" + parametros);
            // Realiza a requisição
            HttpWebResponse retorno = (HttpWebResponse)requisicao.GetResponse();
            return retorno;
        }

        public List<TrackingPackageStep> CheckOrder(String code)
        {
            string parametros = "?P_COD_UNI=" + code + "&P_LINGUA=001&P_TIPO=001";
            // Cria o objeto de requisição
            HttpWebRequest request =
                (HttpWebRequest)
                WebRequest.Create("http://websro.correios.com.br/sro_bin/txect01$.QueryList" + parametros);

            var response = (HttpWebResponse)request.GetResponse();

            using (var responseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF7))
            {
                var html = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();

                if (Regex.IsMatch(html, @"<[Bb][^>]*>Objetos\s*não\s*Encontrados<\/[^>]*>"))
                    return null;

                if (Regex.IsMatch(html,
                                  @"[Oo]\s*[Nn][Oo][Ss][Ss][Oo]\s*[Ss][Ii][Ss][Tt][Ee][Mm][Aa]\s*[Nn][Ãã][Oo]\s*[Pp][Oo][Ss][Ss][Uu][Ii]\s*[Dd][Aa][Dd][Oo][Ss]\s*[Ss][Oo][Bb][Rr][Ee]\s*[Oo]\s*[Oo][Bb][Jj][Ee][Tt][Oo]\s*[Ii][Nn][Ff][Oo][Rr][Mm][Aa][Dd][Oo]"))
                    return null;

                var lineMatch = Regex.Match(html,
                                            "<[Tt][Rr][^>]*>\\s*<[Tt][Dd]\\s*(rowspan|colspan)=\\\"?(\\d)\\\"?\\s*[^>]*>\\s*(.*?)\\s*<\\/[Tt][Dd]>\\s*<\\/[Tt][Rr]>");
                var steps = new List<TrackingPackageStep>();

                while (lineMatch.Success)
                {
                    var content = lineMatch.Groups[3].Value;
                    var contentMatch = Regex.Match(content,
                                                   "(\\d{1,2}\\/\\d{1,2}\\/\\d{1,4}\\s*\\d{1,2}\\:\\d{1,2})\\s*<[^>]*>\\s*<[^>]*>([^<]*)<[^>]*>\\s*<[^>]*>\\s*<[^>]*>([^<]*)<[^>]*>");

                    if (contentMatch.Success)
                    {
                        string description = contentMatch.Groups[2].Value;
                        while (description.IndexOf("  ") >= 0)
                            description = description.Replace("  ", " ");

                        steps.Add(new TrackingPackageStep
                        {
                            Date = DateTime.Parse(contentMatch.Groups[1].Value),
                            Name = contentMatch.Groups[3].Value,
                            Description = description
                        });
                    }
                    else
                        steps.Last().Description += " " + content;

                    lineMatch = lineMatch.NextMatch();
                }

                response.Close();

                return steps;
            }
        }
    }
}