<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Default.aspx.cs" Inherits="AreaDoUsuario_Default" %>

<%@ Import Namespace="Modelos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <!--=== Breadcrumbs v4 ===-->
        <div class="breadcrumbs-v4" style="margin-bottom: 2%;">
            <div class="container">
                <h1>Minha Conta</h1>
                <ul class="breadcrumb-v4-in">
                    <li><a href="<%= MetodosFE.BaseURL%>/">Home</a></li>
                    <li class="active">Minha Conta</li>
                </ul>
            </div><!--/end container-->
        </div>
        <!--=== End Breadcrumbs v4 ===-->

    <!--=== Login ===-->
    <div class="fundo-branco padd-boto-80">
        <div class="log-reg-v3">
            <div class="container">
                <div class="col-md-3 col-xs-12 bordered-grey menu-meusdados lista-menu">
                    <ul>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/area-do-usuario/meus-dados">meus dados
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/area-do-usuario/Alterar-Senha">alterar senha
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/area-do-usuario/meus-pedidos">meus pedidos
                            </a>
                        </li>
                    </ul>
                </div>


                <div class="col-md-9 col-xs-12 pull-right">
                    <div class="col-md-10 col-xs-12">
                        <div class="col-md-12 col-xs-12 conteudo">
                            <h2>
                                <asp:Literal runat="server" ID="litTituloBoasVindas" />
                            </h2>
                            <div runat="server" id="divTexto">
                                <asp:Literal runat="server" ID="litTextoBoasVindas" />
                            </div>
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12" runat="server" id="divFinalizar">
                            Agora que você já esta registrado em nosso site, você pode finalizar sua compra, <a href="<%= MetodosFE.BaseURL %>/Carrinho">CLICANDO AQUI</a>
                        </div>
                    </div>
                </div>
                <!--/end container-->
            </div>
        </div>
    </div>
    <!--=== End Login ===-->
</asp:Content>
