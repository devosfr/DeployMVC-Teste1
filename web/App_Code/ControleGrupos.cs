using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using System.Web.Services;
using Modelos;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

/// <summary>
/// Summary description for ControleGrupos
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ControleGrupos : System.Web.Services.WebService
{

    //public ControleGrupos () {

    //    //Uncomment the following line if using designed components 
    //    //InitializeComponent(); 
    //}
    private Repository<GrupoDePaginasVO> repoGrupoPaginas
    {
        get
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PaginaDeControleVO> repoPaginasControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String getGrupos()
    {
        ControleLogin.statusLoginGerenciador();

        IList<GrupoDePaginasVO> grupos = repoGrupoPaginas.All().FetchMany(x => x.paginas).OrderBy(x=>x.ordem).ToList();

        IList<GrupoDePaginaJS> gruposJS = new List<GrupoDePaginaJS>();
        GrupoDePaginaJS grupo = null;
        foreach (var item in grupos)
        {
            if (item.nome == "Controle de Páginas Fixas")
                continue;
            grupo = new GrupoDePaginaJS();
            grupo.id = item.Id;
            grupo.nome = item.nome;
            grupo.editing = false;
            grupo.type = "group";
            grupo.ordem = item.ordem;
            grupo.adicionarPagina(item.paginas);
            gruposJS.Add(grupo);
        }

        IList<PaginaDeControleVO> paginas = repoPaginasControle.FilterBy(x => x.grupoDePaginas == null).OrderBy(x => x.nome).ToList();

        grupo = new GrupoDePaginaJS();
        grupo.id = 0;
        grupo.nome = "Páginas Sem Grupo";
        grupo.type = "Sem Paginas";
        //grupo.ordem = item.ordem;
        grupo.adicionarPagina(paginas);
        gruposJS.Add(grupo);


        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(gruposJS);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string setGrupos(IList<GrupoDePaginaJS> gruposJS)
    {
        ControleLogin.statusLoginGerenciador();

        IList<GrupoDePaginasVO> gruposEditados = repoGrupoPaginas.FilterBy(x=>x.nome!= "Controle de Páginas Fixas").ToList();

        GrupoDePaginaJS grupoSem = gruposJS.FirstOrDefault(x=>x.type=="Sem Paginas");

        foreach (PaginaJS pag in grupoSem.paginas)
        {
            PaginaDeControleVO pag2 = repoPaginasControle.FindBy(pag.id);
            pag2.grupoDePaginas = null;
            repoPaginasControle.Update(pag2);
        }

        int ordemGrupos = 0;
        foreach (var grupo in gruposJS)
        {
            if (grupo.nome == "Páginas Sem Grupo")
                continue;

            ordemGrupos++;
            GrupoDePaginasVO grupoControle = null;

            if (grupo.id == 0)
            {
                GrupoDePaginasVO grupoTeste = repoGrupoPaginas.FindBy(x => x.nome == grupo.nome);

                int cont = 1;   

                if (grupoTeste != null)
                    while (grupoTeste != null)
                    {
                        grupo.nome = grupo.nome + "-" + cont++;
                        grupoTeste = repoGrupoPaginas.FindBy(x => x.nome == grupo.nome);
                    }

                grupoControle = new GrupoDePaginasVO() { nome = grupo.nome, ordem = ordemGrupos };
                repoGrupoPaginas.Add(grupoControle);
            }
            else
            {
                grupoControle = repoGrupoPaginas.FindBy(x => x.Id == grupo.id);
                grupoControle.ordem = ordemGrupos;

                if (grupoControle.nome != grupo.nome)
                {
                    GrupoDePaginasVO grupoTeste = repoGrupoPaginas.FindBy(x => x.nome == grupo.nome && x.Id != grupo.id);

                    int cont = 1;
                    if (grupoTeste != null)
                        while (grupoTeste != null)
                        {
                            grupo.nome = grupo.nome + "-" + cont++;
                            grupoTeste = repoGrupoPaginas.FindBy(x => x.nome == grupo.nome && x.Id != grupo.id);
                        }

                    grupoControle.nome = grupo.nome;
                }

                repoGrupoPaginas.Update(grupoControle);
            }

            grupoControle.ordem = ordemGrupos;

            gruposEditados.Remove(grupoControle);



            int ordemPagina = 0;
            foreach (var pagina in grupo.paginas)
            {
                ordemPagina++;
                PaginaDeControleVO paginaControle = repoPaginasControle.FindBy(x => x.Id == pagina.id);
                if (paginaControle != null)
                {
                    paginaControle.grupoDePaginas = grupoControle;
                    paginaControle.ordem = ordemPagina;
                    if(!paginaControle.fixa)
                    paginaControle.pagina = MetodosFE.BaseURL + "/Controle/Cadastro/" + paginaControle.grupoDePaginas.Id + "/" + paginaControle.Id;
                    repoPaginasControle.Update(paginaControle);

                }
            }
        }

        foreach (var grupoExcluido in gruposEditados)
            repoGrupoPaginas.Delete(grupoExcluido);

        //JavaScriptSerializer js = new JavaScriptSerializer();

        return getGrupos();
    }


}

public class GrupoDePaginaJS
{
    public int id { get; set; }
    public string nome { get; set; }
    public int ordem { get; set; }
    //public string type { get { return "group"; } }
    public bool editing { get; set; }
    public string type { get; set; }
    public IList<PaginaJS> paginas { get; set; }

    public GrupoDePaginaJS()
    {
        paginas = new List<PaginaJS>();
    }

    public void adicionarPagina(IList<PaginaDeControleVO> paginas)
    {

        if (paginas != null) {
            paginas = paginas.OrderBy(x => x.ordem).ToList();
            foreach (var item in paginas)
                adicionarPagina(item);
        }
            
    }

    public void adicionarPagina(PaginaDeControleVO pagina)
    {
        paginas.Add(new PaginaJS() { id = pagina.Id, nome = pagina.nome, type = "category", ordem = pagina.ordem });
    }
}

public class PaginaJS
{
    public int id { get; set; }
    public string nome { get; set; }
    public string type { get; set; }
    //public string type { get { return "category"; } }
    public int ordem { get; set; }
}
