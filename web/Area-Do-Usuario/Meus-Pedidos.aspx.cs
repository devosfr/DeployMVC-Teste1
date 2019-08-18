using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using NHibernate.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Modelos;

public partial class _Default : System.Web.UI.Page
{
    private Repository<Pedido> RepositorioPedido
    {
        get 
        {
            return new Repository<Pedido>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<Album> RepositorioAlbum
    {
        get
        {
            return new Repository<Album>(NHibernateHelper.CurrentSession);
        }
    }
    private Repository<ImagemProduto> RepositorioImagemProduto
    {
        get
        {
            return new Repository<ImagemProduto>(NHibernateHelper.CurrentSession);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Meus Pedidos" + " - " + Configuracoes.getSetting("nomeSite");
        ControleLoginCliente.statusLogin();
        if (!IsPostBack)
        {
            Cliente cliente = ControleLoginCliente.GetClienteLogado();
            var pedidos = RepositorioPedido.FilterBy(x => x.Cliente.Id == cliente.Id && x.Status != 5).FetchMany(x => x.Itens).ThenFetch(x => x.Produto).OrderByDescending(x => x.DataPedido);

            if (pedidos != null)
            {
                repPedidos.DataSource = pedidos.ToList();
                repPedidos.ItemDataBound += repPedidos_ItemDataBound;
                repPedidos.DataBind();
            }
            
            
        }
    }

    public string GetImg(Produto prod)
    {
        string retorno = "";
        Album alb = RepositorioAlbum.FindBy(x => x.Produto.Id == prod.Id);
        if (alb != null)
        {
            IList<ImagemProduto> imgs = RepositorioImagemProduto.FilterBy(x => x.Album.Id == alb.Id).ToList();
            //retorno = MetodosFE.BaseURL + "/ImagensHQ/" + imgs.First().Nome.Replace("jgp", "jpg");
            retorno = MetodosFE.BaseURL + "/ImagensHQ/" + imgs[0].Nome;
            //retorno = "";
        }
        return retorno;
    }

    void repPedidos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Pedido dados = (Pedido)e.Item.DataItem;
            FileUpload upload = (FileUpload)e.Item.FindControl("fulArquivo");
            LinkButton botaoArquivo = (LinkButton)e.Item.FindControl("btnEnviarArquivo");
            //botaoArquivo.CommandArgument = upload.ClientID + "---" + dados.Id;
        }
    }

    protected void btnEnviarArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            string nomeArquivo = ((LinkButton)sender).CommandArgument;
            string[] dados = nomeArquivo.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);

            FileUpload fulArquivo = (FileUpload)((Control)sender).Parent.FindControl("fulArquivo");

            if (fulArquivo.HasFile)
            {
                //String body = Templates.loadWithData("emailTrabalheConosco.htm", Encoding.UTF8, data);
                // bool retorno = MailSender.SendMailMessage(fromEmail, toEmail, null, null, "Mensagem de contato", body, new List<Attachment> { curriculoAnexo });

                bool bValido = false;

                string fileExtension = System.IO.Path.GetExtension(fulArquivo.FileName).ToLower();
                foreach (string ext in new string[] { ".bmp", ".jpg", ".png", ".gif" })
                {
                    if (fileExtension == ext)
                    {
                        bValido = true;
                        break;
                    }
                }
                if (!bValido)
                    throw new Exception("Extensão inválida de arquivo.");

                string vNomeArquivo = (fulArquivo.PostedFile.FileName);
                fulArquivo.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~/userfiles/Boletos/") + "\\" + dados[1] + "_Pedido_" + vNomeArquivo);

                Pedido pedido = RepositorioPedido.FindBy(Convert.ToInt32(dados[1]));

                pedido.ComprovanteDeposito = MetodosFE.BaseURL + "/userfiles/Boletos/" + dados[1] + "_Pedido_" + vNomeArquivo;

                RepositorioPedido.Update(pedido);

                enviaEmail(pedido.Id);

                MetodosFE.mostraMensagem("Imagem carregada com sucesso.", "sucesso");

                Cliente cliente = ControleLoginCliente.GetClienteLogado();
                var pedidos = RepositorioPedido.FilterBy(x => x.Cliente.Id == cliente.Id).FetchMany(x => x.Itens).ThenFetch(x => x.Produto).OrderByDescending(x => x.DataPedido);
                repPedidos.DataSource = pedidos;
                repPedidos.DataBind();

            }
        }
        catch (Exception ex)
        {
            MetodosFE.mostraMensagem("Problemas ao carregar imagem:" + ex.Message);
        }
    }

    public void enviaEmail(int id)
    {
        EnvioEmailsVO envio = new EnvioEmailsVO();

        DadoVO dado = MetodosFE.getTela("Configurações de SMTP");

        if (dado != null)
        {
            DadoVO dadosContato = MetodosFE.getTela("E-mail - Depósito");
            string email = null;
            if (dadosContato != null)
                if (!String.IsNullOrEmpty(dadosContato.referencia))
                    email = dadosContato.referencia;

            if (String.IsNullOrEmpty(email))
                email = dado.referencia;

            envio.nomeRemetente = Configuracoes.getSetting("NomeSite");
            envio.emailRemetente = dado.referencia;
            envio.emailDestinatario = email;
            envio.assuntoMensagem = "Envio de comprovante de depósito: pedido " + id;
            //envio.emailResposta = txtEmail.Text;

            string mensagem = "";
            mensagem = "Foi enviada a imagem do comprovante de depósito do pedido " + id + " pelo cliente. É possível conferir no gerenciador, em Administração > Pedidos";



            envio.conteudoMensagem = mensagem;

            bool vrecebe = EnvioEmails.envioemails(envio);

        }

    }

    protected string cortaTexto(string txt) 
    {
        string retorno = "";

        if (txt.Length >= 87)
        {
            retorno = txt.Substring(0, 85) + "...";
        }
        else
            retorno = txt;

        return retorno;
    }

    protected void lbImagemDeposito_Click(object sender, EventArgs e)
    {
        string nomeArquivo = ((LinkButton)sender).CommandArgument;
        int idPedido = Convert.ToInt32(nomeArquivo);
        Pedido pedido = RepositorioPedido.FindBy(idPedido);

        string extensao = System.IO.Path.GetExtension(pedido.ComprovanteDeposito).ToLower();

        string contentType = "";

        switch (extensao)
        {
            case ".jpeg": contentType = "image/jpeg"; break;
            case ".jpg": contentType = "image/jpeg"; break;
            case ".png": contentType = "image/png"; break;
            case ".gif": contentType = "image/gif"; break;
            case ".bmp": contentType = "image/bmp"; break;
        }

        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=Deposito" + extensao);
        Response.TransmitFile(pedido.ComprovanteDeposito);
        Response.End();
    }


    protected void lbEnviarComprovante_Command(object sender, CommandEventArgs e)
    {
        divComprovante.Visible = true;
        hdn.Value = e.CommandArgument.ToString();
        litPedido.Text = "Nº pedido: "  + hdn.Value;
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        bool bValido = false;
        int idpedido = Convert.ToInt32(hdn.Value);

        string fileExtension = System.IO.Path.GetExtension(fulArquivo.FileName).ToLower();
        foreach (string ext in new string[] { ".bmp", ".jpg", ".png", ".gif" })
        {
            if (fileExtension == ext)
            {
                bValido = true;
                break;
            }
        }
        if (!bValido)
            throw new Exception("Extensão inválida de arquivo.");

        string vNomeArquivo = (fulArquivo.PostedFile.FileName);
        fulArquivo.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~/userfiles/Comprovantes/") + "\\" + idpedido + "_Pedido_" + vNomeArquivo);

        Pedido pedido = RepositorioPedido.FindBy(idpedido);

        pedido.ComprovanteDeposito = MetodosFE.BaseURL + "/userfiles/Comprovantes/" + idpedido + "_Pedido_" + vNomeArquivo;

        RepositorioPedido.Update(pedido);

        enviaEmail(pedido.Id);

        MetodosFE.mostraMensagem("Imagem carregada com sucesso.", "sucesso");

        Cliente cliente = ControleLoginCliente.GetClienteLogado();

        var pedidos = RepositorioPedido.FilterBy(x => x.Cliente.Id == cliente.Id).FetchMany(x => x.Itens).ThenFetch(x => x.Produto).OrderByDescending(x => x.DataPedido);

        repPedidos.DataSource = pedidos;
        repPedidos.DataBind();

        litMensagem.Text = "Comprovante enviado com sucesso!";
    }
}
