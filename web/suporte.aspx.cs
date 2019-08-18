using System;
using System.Collections.Generic;
using System.Web.UI;
using Modelos;
using System.Linq;

public partial class _contato : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MaintainScrollPositionOnPostBack = true;

        if (!IsPostBack)
        {
            Page.Title = "Suporte - " + Configuracoes.getSetting("NomeSite");

            IList<DadoVO> dados = null;
            DadoVO dado = null;
            DadoVO dadoLista = null;
            dados = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Sobre a Empresa Lista") && x.visivel).ToList();
            
            //menu
            if (dados != null && dados.Count > 0)
            {
                //repMenu.DataSource = dados.OrderBy(x => MetodosFE.verificaOrdem(x.ordem));
                //repMenu.DataBind();
            }

            IList<DadoVO> sobres = null;
            sobres = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Sobre a Empresa") && x.visivel).ToList();
            if (sobres != null && sobres.Count > 0)
            {
                repSobre.DataSource = sobres.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                repSobre.DataBind();
            }

            IList<DadoVO> suportes = null;
            suportes = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Suporte ao Cliente") && x.visivel).ToList();
            if (suportes != null && suportes.Count > 0)
            {
                repSuporte.DataSource = suportes.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                repSuporte.DataBind();
            }

            if (Request.QueryString["q"] != null)
            {
                string chave = Request.QueryString["q"].ToString();

                dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Suporte ao Cliente") && x.visivel && x.chave.Equals(chave));
                dadoLista = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Sobre a Empresa Lista") && x.visivel && x.chave.Equals(chave));

                if (chave == "contato0")
                {
                    Response.Redirect("~/Contato");
                }


                if (dadoLista != null)
                {
                    litTitulo.Text = dadoLista.nome;
                    litTexto.Text = dadoLista.descricao;
                    img.Src = dadoLista.getPrimeiraImagemHQ();
                    if (img.Src.Contains("SemImagem.jpg"))
                    {
                        img.Visible = false;
                    }
                    liTopo.InnerText = dadoLista.nome;
                }


                if (dado != null)
                {
                    litTitulo.Text = dado.nome;
                    litTexto.Text = dado.descricao;                    
                    img.Src = dado.getPrimeiraImagemHQ();
                    if (img.Src.Contains("SemImagem.jpg"))
                    {
                        img.Visible = false;
                    }
                    liTopo.InnerText = dado.nome;
                }
       
            }
            else
            {
                dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Suporte ao Cliente") && x.visivel);
                if (dado != null)
                {
                    litTitulo.Text = dado.nome;
                    litTexto.Text = dado.descricao;
                    img.Src = dado.getPrimeiraImagemHQ();
                    if (img.Src.Contains("SemImagem.jpg"))
                    {
                        img.Visible = false;
                    }
                    liTopo.InnerText = dado.nome;
                }
                else
                    Response.Redirect("~/");
            }
        }
    }

    public string getAtivo(string chave, int index)
    {
        if (Request.QueryString["q"] != null)
        {
            if (Request.QueryString["q"].ToString().Equals(chave))
            {
                return "active";
            }
            else
            {
                return "";
            }
        }
        else
        {
            if (index == 0)
            {
                return "active";
            }
            else
            {
                return "";
            }
        }
    }
}
