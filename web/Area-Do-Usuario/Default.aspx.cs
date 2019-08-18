using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AreaDoUsuario_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ControleLoginCliente.statusLogin();
        if(!IsPostBack)
        {
            DadoVO dadoBoasVindas = null;

            dadoBoasVindas = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Login - Boas Vindas"));

            if (dadoBoasVindas != null)
            {
                litTituloBoasVindas.Text = dadoBoasVindas.nome;
                litTextoBoasVindas.Text = dadoBoasVindas.descricao;
            }

            int quantidadeItensCarrinho = ControleCarrinho.GetQuantidadeItens();

            if (quantidadeItensCarrinho > 0)
            {
                divFinalizar.Visible = true;
                divTexto.Visible = false;
            }
            else
            {
                divFinalizar.Visible = false;
                divTexto.Visible = true;
            }
                
        }
        
    }
}