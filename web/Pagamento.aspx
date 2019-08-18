<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="Pagamento.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .btnPagar {
            padding: 10px 26px;
        }


        #loading {
            display: none;
            position: fixed;
            left: 0%;
            top: 0%;
            margin-left: 0;
            margin-top: 0;
            padding: 10px;
            width: 100%;
            height: 100%;
            z-index: 100;
            border: 1px solid #d0d0d0;
            vertical-align: middle;
            text-align: center;
        }

        #loading {
            background-image: url('../../images/popup/fundoLoading.png');
        }

        #loading2 {
            position: fixed;
            top: 50%;
            left: 50%;
            width: 300px;
            height: 350px;
            margin-left: -150px;
            margin-top: -125px;
            padding: 10px 10px 20px 10px;
            border-width: 2px;
            border-style: solid;
            display: block;
            text-align: center;
            background-color: White;
            z-index: 101;
        }
    </style>


    <%-- MODAL --%>
    <div class="modal" tabindex="-1" role="dialog" id="myModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="border-bottom:none">
                    <h5 class="modal-title">
                        <asp:Literal Text="" ID="litTitulo" runat="server" />
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal Text="" ID="litTexto" runat="server" /></p>
                </div>
                <div class="modal-footer" style="border-top:none">
                    <button type="button" class="btn btn-secondary btn-modal-pagamento" id="btnfecharmodal" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <input type="hidden" runat="server" id="hiddeMensagem" name="name" value="" />

    <div class="breadcrumbs-v4" style="background-color: #fff">
        <div class="container">
            <a class="page-name" style="color: #1b511b">meu carrinho</a>
            <h1 style="color: #1b511b">Veja seus itens no carrinho</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" style="color: #1b511b">Home</a></li>
                <li><a href="<%# MetodosFE.BaseURL %>/carrinho" style="color: #1b511b">Meu Carrinho</a></li>
                <li><a href="<%# MetodosFE.BaseURL %>/endereco" style="color: #1b511b">Endereço de Entrega</a></li>
                <li class="active">Pagamento</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Content Medium Part ===-->
    <div class="fundo-cinza" style="background-color: #fff">
        <div class="content-md">
            <div class="container">
                <form class="shopping-cart" action="#">
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
                                    <p class="inativo-carrinho">Endereço de Entrega</p>
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
                                <div class="col-md-6 md-margin-bottom-50 metodos-de-pagamento">
                                    <h2 class="title-type">MÉTODO DE PAGAMENTO</h2>
                                    <!-- Accordion -->
                                    <div class="accordion-v2">
                                        <div class="panel-group" id="accordion">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a>
                                                            <b class="glyphicon glyphicon glyphicon-envelope"></b> Enviar Pedido
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div>
                                                    <div style="z-index: 999999;" class="content bordas  col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                                        <!--PAGSEGURO-->
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" style="padding-bottom: 25px">
                                                            <div class="col-lg-6 col-md-5 col-sm-6 col-xs-12 imagem-pag-seguro">
                                                                <img src="<%# MetodosFE.BaseURL %>/assets/img/enviar-por-email.png" class="img-responsive" alt="Pagseguro" />
                                                            </div>
                                                            <div class="col-lg-3 col-md-4 col-sm-3 col-xs-12 valor-pag-seguro">
                                                                <asp:Literal runat="server" ID="litPagamento"></asp:Literal>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 botao-pag-seguro">
                                                                <asp:Button CssClass="btn-u-carrinho btn-u-sea-shop margin-right-10" 
                                                                    Text="Enviar" runat="server" ID="linkPagar" ClientIDMode="Static" OnClick="linkPagar_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                        <!--fim PAGSEGURO-->
                                                        <%--<!--BOLETO-->
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" style="padding-bottom: 25px">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 pagar-com">
                                                                Pagar com Boleto
                                                            </div>
                                                            <div class="col-lg-6 col-md-5 col-sm-6 col-xs-12 imagem-pag-seguro imagem-boleto">
                                                                <img src="<%# MetodosFE.BaseURL %>/assets/img/boleto.jpg" class="img-responsive" alt="" title="" />
                                                            </div>
                                                            <div class="col-lg-3 col-md-4 col-sm-3 col-xs-12 imagem-pag-seguro valor-pag-seguro valor-boleto">
                                                                <asp:Literal runat="server" ID="litPagamento2"></asp:Literal>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 botao-pag-seguro botao-boleto">
                                                                <asp:Button CssClass="btn-u-carrinho btn-u-sea-shop margin-right-10" Text="Pagar" runat="server" ID="linkBoleto" ClientIDMode="Static" OnClick="lbPagarBoleto_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                        <!--fim BOLETO-->
                                                        <!--Transferência-->
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding" style="padding-bottom: 25px">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 pagar-com">
                                                                Transferência ou Depósito Bancário
                                                            </div>
                                                            <div class="col-lg-6 col-md-5 col-sm-6 col-xs-12 imagem-pag-seguro imagem-boleto">
                                                                <img src="<%# MetodosFE.BaseURL %>/assets/img/transferencia-bancaria.png" class="img-responsive" alt="" title="" />
                                                            </div>
                                                            <div class="col-lg-3 col-md-4 col-sm-3 col-xs-12 imagem-pag-seguro valor-pag-seguro valor-boleto">
                                                                <asp:Literal runat="server" ID="litPagamento3"></asp:Literal>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 botao-pag-seguro botao-boleto">
                                                                <asp:Button CssClass="btn-u-carrinho btn-u-sea-shop margin-right-10" Text="Pagar" runat="server" ID="btnTransferencia" ClientIDMode="Static" OnClick="lbPagarDeposito_Click"></asp:Button>
                                                            </div>
                                                        </div>
                                                        <!--fim Transferência-->--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Accordion -->
                                </div>
                                <div class="col-md-6">
                                    <h2 class="title-type">PERGUNTAS FREQUENTES</h2>
                                    <div class="perguntas-frequentes">
                                        <div class="panel-group-acordion" id="accordion">
                                            <asp:Repeater runat="server" ID="repPerguntas">
                                                <ItemTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" class="<%# Container.ItemIndex == 0 ? "":"collapsed" %>" data-parent="#accordion" href="#collapse<%# Container.ItemIndex %>"><%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse<%# Container.ItemIndex %>" class="panel-collapse collapse <%# Container.ItemIndex == 0 ? "in":"" %>">
                                                            <div class="panel-body">
                                                                <%# ((Modelos.DadoVO)Container.DataItem).descricao %>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                            </div>
                            <div class="col-md-6 col-sm-12 total-price botao-voltar-pagamentos voltar-pagamentos">
                                <button onclick="window.location.href = 'javascript:window.history.go(-1)'" type="button" class="btn-u-carrinho btn-u-sea-shop margin-right-10">
                                    VOLTAR
                                </button>
                            </div>
                        </section>
                    </div>
                </form>
            </div>
            <!--/end container-->
        </div>
    </div>

    <div id="loading">
        <div id="loading2">
            <center>
                <table>
                    <tr>
                        <td align="center">Carregando...
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <img src="<%=MetodosFE.BaseURL %>/images/popup/pagseguro.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <img src="<%=MetodosFE.BaseURL %>/images/popup/loading.gif" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </div>


</asp:Content>
