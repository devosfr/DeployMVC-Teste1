using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelos;

public partial class noticias : System.Web.UI.Page
{

    Int16 numPorPaginas = 8;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Notícias " + Configuracoes.getSetting("NomeSite");

        IList<DadoVO> NotíciaTopo = null;
        NotíciaTopo = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Notícias Lista") && x.visivel).Take(1).OrderBy(x => x.data).ToList();
        if (NotíciaTopo != null)
        {
            repNotíciaTopo.DataSource = NotíciaTopo;
            repNotíciaTopo.DataBind();

        }


        IList<DadoVO> NotíciaTopoMulti = null;
        NotíciaTopoMulti = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Notícias Lista") && x.visivel).Skip(1).OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).OrderByDescending(x => x.data).ToList();
        if (NotíciaTopoMulti != null)
        {
            repNotíciaTopoMulti.DataSource = NotíciaTopoMulti.Skip((MetodosFE.GetPagerIndex() - 1) * numPorPaginas).Take(numPorPaginas); ;
            repNotíciaTopoMulti.DataBind();

        }

        litPaginacao.Text = MetodosFE.buildSitePagination(NotíciaTopoMulti.Count, numPorPaginas);

        MaintainScrollPositionOnPostBack = true;























    }











}