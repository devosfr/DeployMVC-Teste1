<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Compra-Finalizada.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--=== Conteudo ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff">
        <div class="container">
            <h1 style="color:#1b511b">PEDIDO CONFIRMADO</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" style="color:#1b511b">Home</a></li>
                <li><a href="<%# MetodosFE.BaseURL %>/carrinho.aspx" style="color:#1b511b">Meu Carrinho</a></li>
                <li class="active">Confirmação do pedido</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Pedido aprovado ===-->
    <div class="fundo-cinza" style="background-color:#fff">
        <div class="log-reg-v3 content-md">
            <div class="container">
                <div class="col-md-3 col-xs-12 ">
                </div>
                <div class="col-md-8 col-xs-12 pull-right">
                    <div class="row">
                        <div class="col-md-12 margin-bottom-50">
                            <h2 class="atualiza">
                                <asp:Literal runat="server" ID="litNome"></asp:Literal></h2>
                            <div class="texto-ajuda">
                                <asp:Literal runat="server" ID="litTexto"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: center;">
                        <button type="button" class="btn-u-carrinho btn-u-sea-shop" onclick="location.href='<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Meus-Pedidos.aspx'" 
                            >Meus Pedidos</button>
                    </div>
                </div>
                <!--/end container-->
            </div>
        </div>
    </div>
</asp:Content>
