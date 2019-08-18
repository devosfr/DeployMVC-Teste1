using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Modelos;

public partial class _Default : System.Web.UI.Page
{

    protected Repository<Produto> RepositorioProduto
    {
        get
        {
            return new Repository<Produto>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Pagamento" + " - " + Configuracoes.getSetting("nomeSite");
        ControleLoginCliente.statusLogin();

        hiddeMensagem.Value = "false";

        Cliente cliente = ControleLoginCliente.GetClienteLogado();

        int intensCarrinho = ControleCarrinho.GetQuantidadeItens();

        Session["intensCarrinho"] = ControleCarrinho.GetItensCarrinho();

        if (intensCarrinho == 0)
        {
            Response.Redirect("~/");
        }

        if (!IsPostBack && intensCarrinho != 0)
        {
            List<DadoVO> perguntas = MetodosFE.documentos.Where(x => x.tela.nome.Equals("Perguntas Frequentes") && x.visivel).ToList();
            if (perguntas != null && perguntas.Count > 0)
            {
                repPerguntas.DataSource = perguntas.OrderBy(x => MetodosFE.verificaOrdem(x.ordem));
                repPerguntas.DataBind();
            }

            Decimal total = ControleCarrinho.TotalCarrinho();
            Decimal totalFrete = ControleFrete.ValorFreteCarrinho().Preco;
            Decimal totalPedido = total + totalFrete;
            litPagamento.Text = totalPedido.ToString("C");
            //litPagamento2.Text = (totalFrete + ControleCarrinho.TotalCarrinhoTransf()).ToString("C");
            //litPagamento3.Text = (totalFrete + ControleCarrinho.TotalCarrinhoTransf()).ToString("C");
            //litPagamento.Text = litPagamento2.Text = litPagamento3.Text = total.ToString("C");

            IList<DadoVO> informacoesDuvidas = MetodosFE.documentos.Where(x => x.tela.nome == "Ajuda" && x.visivel).ToList();

            if (informacoesDuvidas != null && informacoesDuvidas.Count > 0)
            {
                //repInformacoesPagamento.DataSourceID = String.Empty;
                //repInformacoesPagamento.DataSource = informacoesDuvidas;
                //repInformacoesPagamento.DataBind();
            }

            DadoVO texto = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Pagamento"));

            if (texto != null)
            {
                //litTitulo.Text = texto.nome;
                //litTexto.Text = texto.descricao;
            }

            //DadoVO compraFinalizadaTexto = MetodosFE.documentos.FirstOrDefault(x => x.tela.nome.Equals("Pagamento"));

            DadoVO compraFinalizadaTexto = MetodosFE.getTela("Compra Finalizada - Texto");

            if (compraFinalizadaTexto != null)
            {
                litTitulo.Text = compraFinalizadaTexto.nome;
                litTexto.Text = compraFinalizadaTexto.descricao;
            }




        }
    }
    protected void linkPagar_Click(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            //Session["intensCarrinho"];
            IList<Produto> prod = null;
            //Carrinho produto = new Carrinho();
            prod = RepositorioProduto.All().ToList();
            if (prod != null)
            {

                EnvioEmailsVO envio = new EnvioEmailsVO();

                DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

                if (dado != null)
                {
                    DadoVO dadosContato = MetodosFE.getTela("Contato");
                    string email = null;
                    if (dadosContato != null)
                        if (!String.IsNullOrEmpty(dadosContato.nome))
                            email = dadosContato.nome;

                    if (String.IsNullOrEmpty(email))
                        email = dado.referencia;

                    envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
                    envio.emailRemetente = dado.referencia;
                    envio.emailDestinatario = email;
                    envio.assuntoMensagem = "Contato do Site";
                    //envio.emailResposta = txtMail.Text;

                    Cliente cliente = ControleLoginCliente.GetClienteLogado();

                    IList<ItemCarrinho> carrinho = null;
                    carrinho = ControleCarrinho.GetItensCarrinho();

                    Endereco endereco = ControleCarrinho.GetEndereco();

                    string mensagem = "";
                    mensagem += "<strong>Dados do Cliente:</strong><br/>";
                    mensagem += "<br/>Nome: " + cliente.Nome;
                    mensagem += "<br/>E-mail: " + cliente.Email;
                    mensagem += "<br/>Telefone: " + cliente.Telefone + "<br/>";
                    if (cliente.Observacoes != null)
                    {
                        mensagem += "<br/>Comentários: " + cliente.Observacoes + "<br/>";
                    }


                    mensagem += "<br/><strong>Endereço:</strong>";
                    mensagem += "<br/>Logradouro: " + endereco.Logradouro;
                    mensagem += "<br/>Numero: " + endereco.Numero;
                    mensagem += "<br/>Cidade: " + endereco.Cidade.Nome;
                    mensagem += "<br/>Estado: " + endereco.Estado.Nome + "<br/>";
                    for (int i = 0; i < carrinho.Count; i++)
                    {

                        mensagem += "<br/><strong>Produto:</strong>";
                        mensagem += "<br/>Nome do Produto: " + carrinho[i].Produto.Nome;
                        mensagem += "<br/>Descrição: " + carrinho[i].Produto.Descricao;
                        mensagem += "<br/>Quantidade: " + carrinho[i].Quantidade;

                    }


                    envio.conteudoMensagem = mensagem;

                    bool vrecebe = EnvioEmails.envioemails(envio);
                    hiddeMensagem.Value = "true";
                    // = "Pedido enviado com sucesso!";
                    //txtNome.Text = "";
                    //txtMail.Text = "";
                    //txtFone.Text = "";
                    //txtComent.Text = "";
                    ControlePedido.FecharPedido(3);
                }
            }
        }

    }
    protected void lbPagarDeposito_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect(MetodosFE.BaseURL + "/Deposito-Banco", false);
    }
    protected void lbPagarBoleto_Click(object sender, EventArgs e)
    {
        ControlePedido.FecharPedido((int)Pedido.FormasDePagamento.Boleto);
    }

    protected void btnfecharmodal_ServerClick(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            IList<ItemCarrinho> itens = null;
            itens = ControleCarrinho.GetItensCarrinho();

            ControleCarrinho.RemoveItens(itens);

            Response.Redirect("~/Area-Do-Usuario/Meus-Pedidos");
        }
        
    }
}
