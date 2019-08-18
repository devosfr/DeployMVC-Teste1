using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Modelos;

public partial class Blog : System.Web.UI.Page
{


    Int16 numPorPaginas = 8;

    private string GeraHtmlHiddenMad()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class='clear hidden-lg hidden-md'></div>");
        return sb.ToString();
    }

    private string GeraHtmlHiddenSm()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class='lear hidden-xs hidden-sm'></div>");
        return sb.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        IList<DadoVO> Noticia = null;
        Noticia = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Publicações")).OrderByDescending(x => x.data).ToList();

        if (Noticia != null && Noticia.Count > 0)
        {

            repNoticiaPrincipal.DataSource = Noticia.Take(1);
            repNoticiaPrincipal.DataBind();

            repNoticia.DataSource = Noticia.Skip(1).Skip((MetodosFE.GetPagerIndex() - 1) * numPorPaginas).Take(numPorPaginas);
            repNoticia.DataBind();
        }



        litPaginacao.Text = MetodosFE.buildSitePagination(Noticia.Count, numPorPaginas);

        MaintainScrollPositionOnPostBack = true;

    }
}