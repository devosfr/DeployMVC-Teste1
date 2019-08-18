
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using NHibernate.Linq;
using Modelos;
using System;


public class Pesquisa
{
    private static Repository<DadoVO> repDados 
    { 
        get 
        { 
            return new Repository<DadoVO>(NHibernateHelper.CurrentSession);
        }
    }
    public static List<RetornoPesquisa> getResultadoBusca(string busca)
    {
        IList<DadoVO> dados = repDados.All().Where(x => (x.nome != null && x.nome.Contains(busca)) || (x.referencia != null && x.referencia.Contains(busca)) || (x.resumo != null && x.resumo.Contains(busca)) || (x.descricao != null && x.descricao.Contains(busca))).ToList();
        return configuraRetorno(dados, busca);
    }

    public static List<RetornoPesquisa> configuraRetorno(IList<DadoVO> dados, string busca)
    {
        List<RetornoPesquisa> lista = new List<RetornoPesquisa>();

        foreach (DadoVO dado in dados)
        {
            RetornoPesquisa nova = new RetornoPesquisa();
            nova.nome = dado.nome.Replace(busca, "<strong>" + busca + "</strong>"); ;
            nova.resumo = Regex.Replace(dado.descricao, "<.*?>", string.Empty);
            nova.resumo = nova.resumo.Replace(busca, "<strong>" + busca + "</strong>");
            nova.endereco = getUrl(dado);
            if (nova.endereco != null)
                lista.Add(nova);
        }
        return lista;
    }

    public static string getUrl(DadoVO dado)
    {
        SegmentoPaiVO segPai = (dado.segPai);

        return getUrl(segPai.nome, dado);
    }
    public static string getUrl(string nomeSegPai, DadoVO dado)
    {
        List<String> excluidos = new List<string>();

        excluidos.Add("Cidades das Filiais");
        excluidos.Add("Animacao");

        Dictionary<string, string> enderecos = new Dictionary<string, string>();
        enderecos.Add("Noticias", MetodosFE.BaseURL + "/Texto/Noticia.aspx?ID=" + dado.Id);
        enderecos.Add("Envie seu Curriculo", MetodosFE.BaseURL + "/TrabalheConosco.aspx");
        enderecos.Add("Clippings", MetodosFE.BaseURL + "/Texto/ListaClipping.aspx");
        enderecos.Add("Clipping", MetodosFE.BaseURL + "/Texto/ListaClipping.aspx");
        enderecos.Add("Campanhas", MetodosFE.BaseURL + "/Texto/ListaCampanhas.aspx");
        enderecos.Add("Duvidas Frequentes", MetodosFE.BaseURL + "/Texto/Duvidas.aspx");
        enderecos.Add("Videos-Texto", MetodosFE.BaseURL + "/Texto/ListaVideos.aspx");
        enderecos.Add("Vagas-Texto", MetodosFE.BaseURL + "/Texto/ListaVagas.aspx");
        enderecos.Add("Localizacao", MetodosFE.BaseURL + "/Contato.aspx");

        if (enderecos.ContainsKey(nomeSegPai))
        {
            return enderecos[nomeSegPai];
        }
        else if (excluidos.Contains(nomeSegPai))
        {
            return null;
        }
        else if (nomeSegPai == "Vagas")
        {
            IList<DadoVO> dados = repDados.All().Where(x => x.segPai.Id == dado.segPai.Id && x.visivel).OrderBy(x => x.Id).ToList();
            int pos = dados.IndexOf(dado);
            int pagina = (pos + 10) / 10;

            if (pagina > 1)
            {
                return MetodosFE.BaseURL + "/Texto/ListaVagas.aspx?Pagina=" + (pagina - 1);
            }
            else return MetodosFE.BaseURL + "/Texto/ListaVagas.aspx";

        }
        else if (nomeSegPai == "Videos")
        {
            IList<DadoVO> dados = repDados.All().Where(x => x.segPai.Id == dado.segPai.Id && x.visivel).OrderBy(x => x.Id).ToList();
            int pos = dados.IndexOf(dado);
            int pagina = (pos + 10) / 10;

            if (pagina > 1)
            {
                return MetodosFE.BaseURL + "/Texto/ListaVideos.aspx?Pagina=" + (pagina - 1);
            }
            else return MetodosFE.BaseURL + "/Texto/ListaVideos.aspx";
        }
        else if (nomeSegPai == "Cargas")
        {
            IList<DadoVO> dados = repDados.All().Where(x => x.segPai.Id == dado.segPai.Id && x.visivel).OrderBy(x => x.Id).ToList();
            int pos = dados.IndexOf(dado);
            int pagina = (pos + 10) / 10;

            if (pagina > 1)
            {
                return MetodosFE.BaseURL + "/Texto/ListaCargas.aspx?Pagina=" + (pagina - 1);
            }
            else return MetodosFE.BaseURL + "/Texto/ListaCargas.aspx";
        }
        else
            return MetodosFE.BaseURL + "/Texto/Texto.aspx?Pai=" + dado.segPai.Id;

    }





}



