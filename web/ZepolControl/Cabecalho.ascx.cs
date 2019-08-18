using System;
using System.Data;
using System.Linq;
using NHibernate.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Modelos;
using System.Web;
using HtmlAgilityPack;

public partial class ZepolControl_DadosTexto : System.Web.UI.UserControl
{
    protected Repository<SegmentoProduto> RepositorioSegmento
    {
        get
        {
            return new Repository<SegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<SubSegmentoProduto> RepositorioSubSegmentoProduto
    {
        get
        {
            return new Repository<SubSegmentoProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<SegmentoPaiVO> RepositorioSegmentoPaiVO
    {
        get
        {
            return new Repository<SegmentoPaiVO>(NHibernateHelper.CurrentSession);
        }
    }

    protected Repository<DadoVO> RepositorioDadoVO
    {
        get
        {
            return new Repository<DadoVO>(NHibernateHelper.CurrentSession);
        }
    }

    protected string getImg(string texto)
    {
        var img = HtmlNode.CreateNode(texto.Replace("<p>", "").Replace("</p>", ""));
        var src = img.Attributes["src"].Value;
        return src;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session.Timeout = 120;


            Cliente cliente = ControleLoginCliente.GetClienteLogado();

            if (cliente != null || cliente != null)
            {
                lilogout.Visible = true;
                lilogoutMobile.Visible = true;
                lilogin.Visible = false;
                liloginMobile.Visible = false;
            }
            else
            {
                lilogout.Visible = false;
                lilogoutMobile.Visible = false;
                lilogin.Visible = true;
                liloginMobile.Visible = true;
            }

            if (ControleLoginCliente.GetClienteLogado() != null)
            {
                //linkLogin.Visible = false;
                //linkLogout.Visible = true;
                //liCadastro.Visible = false;
                //litEntrar.Text = "Sair";
            }
            else
            {
                //linkLogin.Visible = true;
                //linkLogout.Visible = false;
                //liCadastro.Visible = true;
                //litEntrar.Text = "Entrar";
            }

            DadoVO social = null;

            social = MetodosFE.getTela("Facebook");
            if (social != null && !String.IsNullOrEmpty(social.nome))
            {
                linkFace.Visible = true;
                linkFace.HRef = social.nome;

            }


            DadoVO Instagram = null;

            Instagram = MetodosFE.getTela("Instagram");
            if (Instagram != null && !String.IsNullOrEmpty(Instagram.nome))
            {
                linkInstagram.Visible = true;
                linkInstagram.HRef = Instagram.nome;

            }


            DadoVO dado = null;

            dado = MetodosFE.getTela("Whatsapp");
            if (dado != null)
            {
          
                if (dado.referencia != null)
                {
                    linkWhats.Visible = true;
                    string whats = dado.nome.Replace("(", "").Replace(")", "").Replace(" ", "");
                    string part1 = whats.Substring(0, 2);
                    string part2 = whats.Replace("54", "");
                    litWhats.Text = part1 + " | " + part2;
                    //litWhats.Text = dado.nome;

                    //string fone = dado.referencia.Replace("-", "").Replace(")", "").Replace("(", "").Replace("+", "").Replace(" ", "").Replace("Cliqueefale:", "");
                    string fone = dado.referencia;
                    linkWhats.HRef = fone;
                }
                

            }

            //SegmentoPaiVO seg1 = null;
            //SegmentoPaiVO seg2 = null;

            //IList<SegmentoPaiVO> segs = RepositorioSegmentoPaiVO.FilterBy(x => x.tela.nome.Equals("Toda Loja - Menu") && x.visivel).ToList();
            //if (segs != null && segs.Count > 0) 
            //{
            //    if (segs.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).First() != null) 
            //    {
            //        seg1 = segs.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).First();
            //        litTitulo1.Text = seg1.nome;
            //        repMenu1.DataSource = RepositorioDadoVO.FilterBy(x => x.visivel && x.segPai.Id == seg1.Id && !x.destaque.Equals("D")).ToList();
            //        repMenu1.DataBind();
            //        repMenuDestaque1.DataSource = RepositorioDadoVO.FilterBy(x => x.visivel && x.segPai.Id == seg1.Id && x.destaque.Equals("D")).ToList();
            //        repMenuDestaque1.DataBind();
            //        divMenu1.Visible = true;
            //    }

            //    if (segs.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).Skip(1).First() != null)
            //    {
            //        seg2 = segs.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).Skip(1).First();
            //        litTitulo2.Text = seg2.nome;
            //        repMenu2.DataSource = RepositorioDadoVO.FilterBy(x => x.visivel && x.segPai.Id == seg2.Id && !x.destaque.Equals("D")).ToList();
            //        repMenu2.DataBind();
            //        repMenuDestaque2.DataSource = RepositorioDadoVO.FilterBy(x => x.visivel && x.segPai.Id == seg2.Id && x.destaque.Equals("D")).ToList();
            //        repMenuDestaque2.DataBind();
            //        divMenu2.Visible = true;
            //    }
            //}


            //DadoVO img = MetodosFE.getTela("Imagem - Menu");
            //if (img != null) 
            //{
            //    imgMenu.Src = img.getPrimeiraImagemHQ();
            //    linkImg.HRef = img.nome;
            //}



            IList<DadoVO> sobres = null;
            IList<DadoVO> suportes = null;
            IList<DadoVO> servicos = null;


            sobres = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Sobre a Empresa") && x.visivel).ToList();
            if (sobres != null && sobres.Count > 0)
            {
                //repSobre.DataSource = sobres.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                //repSobre.DataBind();


                //repSobreMobile.DataSource = sobres.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                //repSobreMobile.DataBind();

            }

            suportes = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Suporte ao Cliente") && x.visivel).ToList();
            if (suportes != null && suportes.Count > 0)
            {
                //repSuporte.DataSource = suportes.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                //repSuporte.DataBind();
            }

            servicos = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Serviços") && x.visivel).ToList();
            if (servicos != null && servicos.Count > 0)
            {
                //repServicos.DataSource = servicos.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                //repServicos.DataBind();
            }


            litCarrinho.Text = ControleCarrinho.GetQuantidadeItens().ToString();


            populaMenus();



        }
    }


    protected void Deslogar(object sender, EventArgs e)
    {
        ControleLoginCliente.Logout();
        //lilogout.Visible = false;
        //lilogin.Visible = true;
        Response.Redirect("~/");
    }


    protected List<string> getEsportes(Produto prod)
    {
        List<string> retorno = new List<string>();

        retorno = prod.Esporte.Split(',').ToList();

        return retorno;
    }

    public void populaMenus()
    {
        ////Lista de menus para os segmentos e infantil

        //List<SegmentoProduto> segs_menu = null;
        //segs_menu = RepositorioSegmento.All().ToList();
        //if (segs_menu != null && segs_menu.Count > 0)
        //{

        //    repCriancas.DataSource = segs_menu.OrderBy(x => x.Nome);
        //    repCriancas.DataBind();
        //}

        List<SegmentoProduto> segs = null;
        segs = RepositorioSegmento.All().Where(x => x.Visivel && !x.Nome.Equals("#REF!")).ToList();
        if (segs != null && segs.Count > 0)
        {
            //repSegs.DataSource = segs.OrderBy(x => x.Ordem);
            //repSegs.DataBind();

            //repSegsMobile.DataSource = segs.OrderBy(x => x.Ordem);
            //repSegsMobile.DataBind();


        }

    }

    public List<SubSegmentoProduto> getSubSegmentos(SegmentoProduto prod)
    {
        List<SubSegmentoProduto> retorno = null;
        retorno = RepositorioSubSegmentoProduto.FilterBy(x => x.Segmento.Id == prod.Id).ToList();
        if (retorno != null && retorno.Count > 0)
        {
            return retorno.OrderBy(x => x.Ordem).ToList();
        }
        else
        {
            retorno = null;
            return retorno;
        }
    }


    public string esconde(int index)
    {
        if (index > 6)
        {
            return "hidden-lg hidden-md hidden-sm hidden-xs";
        }
        else
        {
            return "";
        }
    }
    protected void linkLogout_Click(object sender, EventArgs e)
    {
        ControleLoginCliente.Logout();
        Response.Redirect("~/");
    }
}

