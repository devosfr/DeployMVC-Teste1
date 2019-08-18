<%@ Page Language="C#" MasterPageFile="~/MasterPageCarrinho.master" AutoEventWireup="true"
    CodeFile="TextoDeposito.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--=== Conteudo ===-->
    <div class="container ">
        <div class="col-md-12 col-xs-12 margin-top-50">
            <div class="col-md-12 col-xs-12 conteudo">
                <h1><asp:Literal runat="server" ID="litTitulo"></asp:Literal></h1>
                <div class="hidden-lg hidden-md"><br /><br /></div>
                <asp:Literal runat="server" ID="litTexto"></asp:Literal>
                
            </div>
        </div>


    </div>
    <!--/end container-->


    <div class="margin-bottom-60"></div>

    <!--=== Conteudo ===-->
</asp:Content>
