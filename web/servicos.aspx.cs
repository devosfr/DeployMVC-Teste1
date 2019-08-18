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

            dados = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Serviços") && x.visivel).ToList();

            //menu
            if (dados != null && dados.Count > 0)
            {
                repMenu.DataSource = dados.OrderBy(x => MetodosFE.verificaOrdem(x.ordem));
                repMenu.DataBind();
            }

            if (Request.QueryString["q"] != null)
            {
                string chave = Request.QueryString["q"].ToString();

                dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Serviços") && x.visivel && x.chave.Equals(chave));
                if (dado != null)
                {
                    litTitulo.Text = dado.nome;
                    litTexto.Text = dado.descricao;
                  
                    liTopo.InnerText = dado.nome;
                }
                else
                    Response.Redirect("~/");
            }
            else
            {
                dado = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Serviços") && x.visivel);
                if (dado != null)
                {
                    litTitulo.Text = dado.nome;
                    litTexto.Text = dado.descricao;
                    
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
