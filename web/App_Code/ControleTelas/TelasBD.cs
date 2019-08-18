using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;

/// <summary>
/// Summary description for TelasBD
/// </summary>
public static class TelasBD
{

    private static Repository<GrupoDePaginasVO> repoGrupoPaginas 
    {
        get 
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private static Repository<PaginaDeControleVO> repoPaginasControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }

    public static void criaPaginasAdministracao() 
    {
        IList<PaginaDeControleVO> paginasContrucao = repoPaginasControle.FilterBy(x => x.construcao).ToList();

        List<String> paginas = new List<String>();

        //paginas.Add("Blog");
        paginas.Add("Telas Personalizadas");
        paginas.Add("Tela de Seg. Pai");
        paginas.Add("Tela de Seg. Filho");
        paginas.Add("Tela de Categoria");
        paginas.Add("Tela de Produtos");
        paginas.Add("Importação de Telas");
        paginas.Add("Limpeza de Produtos");
        paginas.Add("Demais Telas");
        paginas.Add("Organizador");
        paginas.Add("Situação Manutenção");

        string end = MetodosFE.BaseURL + "/Controle/Cadastro/ControlePaginas/";

        Dictionary<string, string> enderecoPaginas = new Dictionary<string, string>();
        enderecoPaginas.Add("Telas Personalizadas", end + "Paginas.aspx");
        enderecoPaginas.Add("Tela de Categoria", end + "PaginaCategoria.aspx");
        enderecoPaginas.Add("Tela de Seg. Filho", end + "PaginaSegFilho.aspx");
        enderecoPaginas.Add("Tela de Seg. Pai", end + "PaginaSegPai.aspx");
        enderecoPaginas.Add("Tela de Produtos", end + "PaginaProdutos.aspx");
        enderecoPaginas.Add("Limpeza de Produtos", end + "ControleLimpeza.aspx");
        enderecoPaginas.Add("Demais Telas", end + "PaginaDemais.aspx");
        enderecoPaginas.Add("Organizador", end + "Organizador.aspx");
        enderecoPaginas.Add("Situação Manutenção", end + "ControleManutencao.aspx");
        enderecoPaginas.Add("Importação de Telas", end + "ImportacaoTelas.aspx");


        GrupoDePaginasVO grupo = repoGrupoPaginas.FindBy(x => x.nome == "Controle de Páginas Fixas");

        if (grupo == null) { 
            grupo = new GrupoDePaginasVO() { nome = "Controle de Páginas Fixas", ordem = Int32.MaxValue };
            repoGrupoPaginas.Add(grupo);
        }



        foreach(string nome in paginas)
        {
            PaginaDeControleVO pagina = paginasContrucao.FirstOrDefault(x => x.nome == nome && x.construcao);
            if(pagina== null)
                repoPaginasControle.Add(new PaginaDeControleVO() { nome = nome, pagina = enderecoPaginas[nome], grupoDePaginas = grupo, fixa = true, construcao = true });
        }




    }


}