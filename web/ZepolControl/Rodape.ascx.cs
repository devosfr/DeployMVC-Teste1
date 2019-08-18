using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Modelos;
using NHibernate.Linq;
using System.Web;

public partial class ZepolControl_Rodape : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        if (!IsPostBack)
        {
            IList<DadoVO> sobres = null;
            IList<DadoVO> suportes = null;
            IList<DadoVO> servicos = null;

           
            servicos = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Serviços") && x.visivel).ToList();
            if (servicos != null && servicos.Count > 0)
            {
                //repServicos.DataSource = servicos.OrderBy(x => MetodosFE.verificaOrdem(x.ordem)).ToList();
                //repServicos.DataBind();
            }

            DadoVO dado = null;

            dado = MetodosFE.getTela("Whatsapp");
            if (dado != null)
            {
                litFone2.Text = dado.nome;
                //string fone = dado.referencia.Replace("-", "").Replace(")", "").Replace("(", "").Replace("+", "").Replace(".", "").Replace(" ", "").Replace("Cliqueefale:", "");
                string fone = dado.referencia;
                linkWhats.HRef = fone;

                WhatsImg.Visible = true;
                //WhatsImgB.Visible = true;
            }

           

            DadoVO social = null;

            dado = MetodosFE.getTela("Cabeçalho");
            if (dado != null)
            {
                if (!String.IsNullOrEmpty(dado.nome))
                {
                    //divIcone.Visible = true;
                    //litFone.Text = dado.nome;
                }
                //litWhats.Text = dado.referencia;
                //Replace('-', String.Empty).Replace(')', "").Replace('(',"").Replace('+', "");
                string fone = dado.referencia.Replace("-", "").Replace(")", "").Replace("(", "").Replace("+", "").Replace(".", "").Replace(" ", "").Replace("Cliqueefale:", "");
                //linkWhats.HRef = "https://api.whatsapp.com/send?phone=" + fone;

            }


            DadoVO face = null;

            face = MetodosFE.getTela("Facebook");
            if (face != null && !String.IsNullOrEmpty(face.nome))
            {
                linkFace.Visible = true;
                linkFace.HRef = face.nome;

            }

            DadoVO Instagram = null;

            Instagram = MetodosFE.getTela("Instagram");
            if (Instagram != null && !String.IsNullOrEmpty(Instagram.nome))
            {
                linkInstagram.Visible = true;
                linkInstagram.HRef = Instagram.nome;

            }










        }
    }
}
