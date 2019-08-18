<%@ Page Language="C#" MasterPageFile="~/MasterPageCarrinho.master" AutoEventWireup="true"
    CodeFile="duvidas.aspx.cs" Inherits="_Dicas" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <link rel="stylesheet" href="../css/custom.css" />

    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>central de ajuda</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" title="">Home</a></li>
                <li class="active" title="">Central de Ajuda</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Content Medium Part ===-->
    <div class="padding-central-de-ajuda">
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 menu-ajuda">
                    <ul>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/quem-somos" title="">Sobre a Empresa
                            </a>
                        </li>
                        <li class="active">
                            <a href="<%# MetodosFE.BaseURL %>/duvidas" title="">Como Comprar
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/como-pagar" title="">Como Pagar
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/politica-privacidade" title="">Política de Privacidade
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/troca-devolucoes" title="">Trocas e Devoluções
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/formas-pagamento" title="">Forma de Pagamento
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="col-lg-1 col-md-1">
                </div>
                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 menu-ajuda">
                    <div class="titulo-ajuda">
                        <h2><asp:Literal runat="server" ID="litTitulo"></asp:Literal>
                        </h2>
                    </div>
                    <div class="texto-ajuda">
                        <asp:Literal runat="server" ID="litTexto"></asp:Literal>    
                    </div>
                </div>
                <div class="col-lg-1 col-md-1">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
