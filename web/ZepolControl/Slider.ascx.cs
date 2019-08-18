using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelos;

public partial class ZepolControl_Newsletter : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<DadoVO> dadosAnimacao;

        dadosAnimacao = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Animação") && x.visivel).OrderBy(x => (x.ordem)).ToList();

        if (dadosAnimacao != null && dadosAnimacao.Count > 0)
        {
            repAnimacao.DataSource = dadosAnimacao;
            repAnimacao.DataBind();
        }

        DadoVO dadosMensagemFrete;

        dadosMensagemFrete = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Banner Animação"));

        if (dadosMensagemFrete != null)
        {
            litMensagemSlider.Text = dadosMensagemFrete.descricao;
        }     
    }

    public string getValor(string valor)
    {
        string valorSemCentavos = "";

        decimal value = Decimal.Parse(valor.Replace("R$",""));

        value = decimal.Floor(value);

        return value.ToString();
    }

    public string getCentavos(string valor)
    {
        string centavos = "";

        string[] valorSeparado = valor.Split(',');

        if (valorSeparado.Length > 0)
        {
            centavos = valorSeparado[1];
        }

        return "," + centavos;
    }
}