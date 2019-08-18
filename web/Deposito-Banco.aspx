<%@ Page Language="C#" MasterPageFile="~/MasterPageCarrinho.master" AutoEventWireup="true"
    CodeFile="Deposito-Banco.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <!-- CSS Global Compulsory -->
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/shop.style.css" />
    <link rel="stylesheet" href="assets/css/style.css" />

    <!-- CSS Implementing Plugins -->
    <link rel="stylesheet" href="../assets/plugins/line-icons/line-icons.css" />
    <link rel="stylesheet" href="../assets/plugins/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/plugins/fancybox/source/jquery.fancybox.css">
    <link rel="stylesheet" href="../assets/plugins/flexslider/flexslider.css" />
    <link rel="stylesheet" href="../assets/plugins/revolution-slider/rs-plugin/css/settings.css" type="text/css" media="screen" />
    <!--[if lt IE 9]><link rel="stylesheet" href="assets/plugins/revolution-slider/rs-plugin/css/settings-ie8.css" type="text/css" media="screen"><![endif]-->
    <!-- CSS Theme -->
    <link rel="stylesheet" href="../assets/css/theme-colors/default.css" />

    <!-- CSS Customization -->
    <link rel="stylesheet" href="../assets/css/custom.css" />
    
    <link rel="stylesheet" href="../css/custom.css" type="text/css" />

    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff">
        <div class="container">
            <span class="page-name" style="color:#1b511b">Pagamento</span>
            <h1 style="color:#1b511b">Efetue o pagamento da sua compra</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" style="color:#1b511b">Home</a></li>
                <li><a href="" style="color:#1b511b">Carrinho de compras</a></li>
                <li class="active">Pagamento</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->

    <!--=== Content Medium Part ===-->
    <div class="content-md margin-bottom-30" style="background-color:#fff">
        <div class="container">
            <div class="shopping-cart">
                <div>
                    <div class="container margin-bottom-50">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-8">
                            <div class="col-sm-4 espaca-50-left text-center">
                                <div class="carrinho-light"></div>
                                <br />
                                <p class="inativo-carrinho">Carrinho de compras</p>
                            </div>
                            <div class="col-sm-4 espaca-50-left text-center">
                                <div class="cobranca-light"></div>
                                <br />
                                <p class="inativo-carrinho">Informações de cobrança</p>
                            </div>
                            <div class="col-sm-4 espaca-50-left text-center">
                                <div class="pagamento-dark"></div>
                                <br />
                                <p class="ativo-carrinho">Pagamento</p>
                            </div>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <section>
                        <div class="row">
                            <div class="col-md-6 md-margin-bottom-50">
                                <h2 class="title-type">ESCOLHA UM MÉTODO DE PAGAMENTO</h2>
                                <!-- Accordion -->
                                <div class="accordion-v2">
                                    <div class="panel-group" id="accordion">
                                        <asp:Repeater runat="server" ID="repBancos">
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#banco<%# Container.ItemIndex %>">
                                                                Depósito no <%# ((DadoVO)Container.DataItem).nome %>
                                                                <br />
                                                                 <%# ((DadoVO)Container.DataItem).descricao %>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id="banco<%# Container.ItemIndex %>" class="panel-collapse collapse in">
                                                        <div class="content margin-left-10">
                                                            <a href="#">
                                                                <img src="<%# ((DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" alt="<%# ((DadoVO)Container.DataItem).nome %>">
                                                            </a>
                                                            <span></span>
                                                            <asp:LinkButton runat="server" ID="linkSelecionar" OnClick="linkSelecionar_Click" CommandArgument="<%# ((DadoVO)Container.DataItem).Id %>">
                                                                <label class="btn-u-carrinho btn-u-sea-shop margin-left-50">
                                                                    Escolher este Banco
                                                                </label>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                </div>
                                <!-- End Accordion -->
                            </div>

                            <div class="col-md-6">
                                <!-- Accordion -->
                                <div class="accordion-v2 plus-toggle">

                                </div>
                                <!-- End Accordion -->
                            </div>
                        </div>
                        <div class="col-sm-6 pull-right">
                            <a href="<%= MetodosFE.BaseURL %>/pagamento">
                                <label class="btn-u-carrinho btn-u-sea-shop">anterior</label></a>
                        </div>
                    </section>
                </div>
            </div>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Content Medium Part ===-->
</asp:Content>
