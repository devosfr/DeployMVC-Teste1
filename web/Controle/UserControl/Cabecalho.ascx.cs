using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq.Dynamic;
using Modelos;

public partial class ZepolControl_DadosTexto : System.Web.UI.UserControl
{

    private Repository<UsuarioVO> repoUsuario
    {
        get
        {
            return new Repository<UsuarioVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<GrupoDePaginasVO> repoGrupoPaginas
    {
        get
        {
            return new Repository<GrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PermissaoGrupoDePaginasVO> repoPermissaoGrupoPaginas
    {
        get
        {
            return new Repository<PermissaoGrupoDePaginasVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PermissaoVO> repoPermissao
    {
        get
        {
            return new Repository<PermissaoVO>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<PaginaDeControleVO> repoPaginaDeControle
    {
        get
        {
            return new Repository<PaginaDeControleVO>(NHibernateHelper.CurrentSession);
        }
    }
    protected IList<PaginaDeControleVO> paginas { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                TelasBD.criaPaginasAdministracao();

                UsuarioVO usuario = ControleLogin.GetUsuarioLogado();
                if (usuario != null)
                {

                    List<PermissaoGrupoDePaginasVO> lista = new List<PermissaoGrupoDePaginasVO>();
                    List<GrupoDePaginasVO> listaGrupos = new List<GrupoDePaginasVO>();

                    if (usuario.tipo == "AA")
                    {
                        listaGrupos = repoGrupoPaginas.All().ToList();
                    }
                    else
                    {
                        lista = repoPermissaoGrupoPaginas.FilterBy(x => x.usuario != null && x.usuario.Id == usuario.Id).ToList();

                        foreach (PermissaoGrupoDePaginasVO grupoP in lista)
                        {
                            listaGrupos.Add(grupoP.grupoDePaginas);
                        }
                    }
                   listaGrupos =  listaGrupos.OrderBy(x => x.ordem).ToList();


                    if (usuario.tipo != "AA")
                    {
                        IList<PermissaoVO> permissoes = repoPermissao.FilterBy(x => x.usuario.Id == usuario.Id).ToList();

                        paginas = repoPaginaDeControle.FilterBy(x => x.grupoDePaginas != null).ToList();

                        paginas = paginas.Where(x => permissoes.Any(y => y.paginaDeControle != null && y.paginaDeControle.Id == x.Id) && listaGrupos.Any(z => x.grupoDePaginas != null && z.Id == x.grupoDePaginas.Id)).ToList();
                    }
                    else
                        paginas = repoPaginaDeControle.FilterBy(x=>x.grupoDePaginas!=null).ToList();

                    paginas = paginas.OrderBy(x => x.grupoDePaginas.nome).ToList();

                    //            var listaRepeater = paginas.GroupBy(p => p.grupoDePaginas, p => p,
                    //(grupo, lP) => new { grupoDePaginas = grupo.nome, listaDePaginas = lP.ToList() });

                    repMenuAreas.DataSource = listaGrupos;
                    repMenuAreas.DataBind();

                    string endereco = Page.Request.RawUrl.ToLower();

                    if (usuario.tipo != "AA")
                    {
                        if (!(Page.AppRelativeVirtualPath.ToLower() == "~/controle/login.aspx"))
                        {
                            bool permissao = false;
                            foreach (PaginaDeControleVO pagina in paginas)
                            {
                                if (endereco.Contains(pagina.pagina.ToLower()))
                                {
                                    permissao = true;
                                    break;
                                }
                            }
                            if (permissao == false)
                            {
                                Response.Redirect(MetodosFE.BaseURL + "/Controle/Login.aspx");
                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
    }


    protected void lkbSair_Click(object sender, EventArgs e)
    {
        ControleLogin.Logout();
        HttpContext.Current.Session["PaginaLoginGerenciador"] = null;
        Response.Redirect("~/Controle/Default.aspx");
    }

    protected string BaseURL
    {
        get
        {
            try
            {
                return string.Format("http://{0}{1}",
                                     HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                                     (VirtualFolder.Equals("/")) ? string.Empty : VirtualFolder);
            }
            catch
            {
                // This is for design time
                return null;
            }
        }
    }

    /// <summary>
    /// Returns the name of the virtual folder where our project lives
    /// </summary>
    private static string VirtualFolder
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }

}
