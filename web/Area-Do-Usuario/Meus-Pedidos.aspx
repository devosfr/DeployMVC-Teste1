<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Meus-Pedidos.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff">
        <div class="container">
            <h1 style="color:#1b511b">Meus Pedidos</h1>
            <ul class="breadcrumb-v4-in lista-area-do-usuario">
                <li><a href="<%# MetodosFE.BaseURL %>/" title="" style="color:#1b511b">Home</a></li>
                <span> > </span>
                <li><a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="" style="color:#1b511b">Minha Conta</a></li>
                <span> > </span>
                <li class="active">Meus Pedidos</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Login ===-->
    <%--<asp:UpdatePanel runat="server" ID="upt">
        <ContentTemplate>--%>
    <style>
        .xx {
            display: block;
    width: 100%;
    height: 34px;
    padding: 6px 12px;
    font-size: 14px;
    line-height: 1.42857143;
        }
    </style>
    <div class="log-reg-v3 content-md" style="background-color:#fff">
        <div class="container" style="min-height: 400px !important">
            <div class="col-md-2 col-xs-12 bordered-grey menu-meusdados">
                <ul id="menu-meusdados-custom">
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Meus Dados
                        </a>
                    </li>
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/alterar-senha.aspx" title="">Alterar Senha
                        </a>
                    </li>
                    <li class="active">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-pedidos.aspx" title="">Meus Pedidos
                        </a>
                    </li>
                </ul>
                <section class="" style="border-top: 1px solid gainsboro;padding: 0 !important;" runat="server" id="divComprovante" visible="false">
                    <asp:HiddenField runat="server" ID="hdn" />
                    <h4 class="xx">Enviar comprovante
                        <asp:Literal runat="server" ID="litPedido"></asp:Literal></h4>
                    <asp:FileUpload runat="server" ID="fulArquivo" Width="100%" CssClass="form-control" style="border:none !important;padding-top:10px;margin-bottom:10px;" />
                    <asp:Button runat="server" OnClick="btnUpload_Click" Width="75" Style="margin-bottom: 5px;" ID="btnUpload" Text="Enviar" CssClass="btn btn-success center-block" />
                </section>
                <h4 style="color: green">
                    <asp:Literal runat="server" ID="litMensagem"></asp:Literal></h4>
            </div>

            <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12 acordiao-pedidos">
                <div class="log-reg-block meusorcamentos-acordiao">
                    <!--CABEÇALHO MEUS ORÇAMENTOS-->
                    <div class="panel-group acc-v1 hidden-xs">
                        <div class="panel panel-default">
                            <div class="panel-heading-pedidos menu-pedidos col-lg-12">
                                <h4 class="panel-title">
                                    <a class="accordion-toggle" data-toggle="collapse" title="" id="titulos-meus-pedidos">
                                        <h3 class="meus-orcamentos hidden-xs">Pedido</h3>
                                        <h3 class="data-titulo hidden-xs" >Data</h3>
                                        <h3 class="status hidden-xs">Status</h3>
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                    <!--CABEÇALHO MEUS ORÇAMENTOS-->

                    <!--ACORDIÃO FUNDO ESCURO-->
                    <asp:Repeater runat="server" ID="repPedidos">
                        <ItemTemplate>
                            <div class="panel-group acc-v1" id="accordion-<%# Container.ItemIndex %>">
                                <div class="panel panel-default lista-meus-pedidos">
                                    <!--Abrir acordião-->
                                    <div class="panel-heading-pedidos <%# Container.ItemIndex%2 == 0 ? "cor-escura":"cor-clara" %> col-lg-12">
                                        <h4 class="panel-title">
                                            <a class="accordion-toggle"  id="lista-pedidos" data-toggle="collapse" data-parent="#accordion-<%# Container.ItemIndex %>" href="#<%# Container.ItemIndex %>" title="">
                                                <b class="pedido">PEDIDO:</b> 
                                                <b class="numero-pedido"><%# ((Modelos.Pedido)Container.DataItem).Id %></b>
                                                <b class="data"><%# ((Modelos.Pedido)Container.DataItem).DataPedido.ToShortDateString() %></b>
                                                <b class="enviado"><%# ((Modelos.Pedido)Container.DataItem).GetStatusPedido() %></b>
                                                <b class="ver-pedido" style="color:#6ebf50">VER ESTE PEDIDO</b>
                                            </a>
                                        </h4>
                                    </div>
                                    <!--Abrir acordião-->
                                    <!--Conteúdo acordião-->
                                    <div id="<%# Container.ItemIndex %>" class="panel-collapse collapse no-padding">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12 meus-pedidos-interno-titulo hidden-sm hidden-xs" style="border:1px solid #000">
                                                    <h4 class="panel-title bordas" >
                                                        <a title="meus-pedidos-titulo-niveldois" id="meus-pedidos-titulo-niveldois">
                                                            <b class="produto">PRODUTO</b>
                                                            <b class="valor">Valor</b>
                                                            <b class="quantidade padding-right-20 cor-qtd">Quantidade</b>
                                                            <b class="total">Total</b>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding:0px;">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bordas-laterais no-padding" style="padding:0px;">
                                                        <asp:Repeater runat="server" DataSource="<%# ((Modelos.Pedido)Container.DataItem).Itens.OrderBy(x => x.Produto.Nome) %>" >
                                                            <ItemTemplate>
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 produto<%# Container.ItemIndex%2 == 0 ? "1":"2" %>-fundo-cor produto-meus-pedidos">
                                                                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 img-meus-pedidos">
                                                                        <img class="img-responsive" src="<%# GetImg(((Modelos.ItemPedido)Container.DataItem).Produto) %>" title="" alt="" />
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 descricao-produto-meus-pedidos">
                                                                        <h2><%# ((Modelos.ItemPedido)Container.DataItem).Produto.Nome %></h2>
                                                                        <h3><%# ((Modelos.ItemPedido)Container.DataItem).Produto.Descricao %>
                                                                        </h3>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 quantidade-meuspedidos">
                                                                        <h5 class="qtd-mobile hidden-lg hidden-md">Valor
                                                                        </h5>
                                                                        <h4><%# ((Modelos.ItemPedido)Container.DataItem).Preco.ToString("C") %></h4>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 quantidade-meuspedidos">
                                                                        <h5 class="qtd-mobile hidden-lg hidden-md">Quantidade
                                                                        </h5>
                                                                        <h3><%# ((Modelos.ItemPedido)Container.DataItem).Quantidade.ToString() %>
                                                                        </h3>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 quantidade-meuspedidos totalgeral">
                                                                        <h5 class="qtd-mobile hidden-lg hidden-md">Total
                                                                        </h5>
                                                                        <h6><%# ((Modelos.ItemPedido)Container.DataItem).GetTotal().ToString("C") %>
                                                                        </h6>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bordas-laterais rastreando no-padding">
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 rasteiro no-padding" <%# String.IsNullOrEmpty(((Modelos.Pedido)Container.DataItem).Rastreamento) ? "style='display:none'":"" %>>
                                                                <ul>
                                                                    <li>CÓDIGO RASTREIO 
                                                                    </li>
                                                                    <li class="codigo"><%# ((Modelos.Pedido)Container.DataItem).Rastreamento %>
                                                                    </li>
                                                                </ul>
                                                            </div>

                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 pedido-despachado no-padding" <%# ((Modelos.Pedido)Container.DataItem).GetFormaDePagamento().Contains("Deposito") ? "":"style='display:none'" %>>
                                                                <asp:LinkButton runat="server" ID="btnEnviarComprovante" OnCommand="lbEnviarComprovante_Command" CommandArgument="<%# ((Modelos.Pedido)Container.DataItem).Id %>" Text="Enviar Comprovante de Depósito"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 total-geral">
                                                            Total Geral <b><%# ((Modelos.Pedido)Container.DataItem).GetTotalPedido().ToString("C") %></b>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <!--Conteúdo acordião-->
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <!--ACORDIÃO FUNDO CLARO-->
                    <!-- End Accordion v1 -->
                </div>
            </div>
            <!--/end container-->
        </div>
    </div>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
