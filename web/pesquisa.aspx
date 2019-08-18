<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pesquisa.aspx.cs" Inherits="_pesquisa" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .indisponivel2 {
            background-color: red !important;
            opacity: 0.5;
        }
    </style>
    <div class="container">
     <center><h2 style="padding-top:30px;padding-bottom:40px">Resultados da pesquisa: </h2></center>
        <br />
<div runat="server" id="div"></div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
            <!--PRODUTOS-->

              <div class="illustration-v2 col-lg-9 col-md-12 col-sm-12 col-xs-12 no-padding margin-bottom-40">                              
                  
                    <asp:Repeater runat="server" ID="repProd">
                                    <ItemTemplate>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 produto-item margin-top-30">
                                            <div class="product-img col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                                <a href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title="">
                                                    <img class="full-width img-responsive imgProdutos" src="<%# GetImg(((Modelos.Produto)Container.DataItem)) %>"
                                                         alt="<%# (((Modelos.Produto)Container.DataItem)).Nome %>" title="<%# (((Modelos.Produto)Container.DataItem)).Nome %>"></a>

                                                <a class="product-review <%# isVisible(((Modelos.Produto)Container.DataItem)) %> <%# sobEncomenda(((Modelos.Produto)Container.DataItem), 0) %> <%# ((Modelos.Produto)Container.DataItem).Novidade ? "novidade":"" %>  <%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "indisponivel":"" %>" href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title=""><%# ((Modelos.Produto)Container.DataItem).Novidade ? "NOVIDADE":"" %>  <%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "indisponível":"" %><%# sobEncomenda(((Modelos.Produto)Container.DataItem), 1) %></a>
                                                <%# ((Modelos.Produto)Container.DataItem).GetDescontoDiv() %>
                                                <a class="product-review link-produto<%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "-indisponivel":"" %>-itens" href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title=""></a>
                                                <a class="add-to-cart" href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title=""><i class="fa"></i>Veja mais detalhes do produto</a>
                                            </div>
                                            <div class="product-description product-description-brd">
                                                <br />
                                                <div class="overflow-h margin-bottom-5">
                                                    <div class="col-lg-9" style="width: 100%; margin-top: 6px; padding-left: 0">
                                                        <h4 class="title-price tamanho-50">
                                                            <a href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title=""><%# ((Modelos.Produto)Container.DataItem).Nome %></a>

                                                        </h4>
                                                    </div>
                                                    <div class="product-price">
                                                       <div style='font-size:14px; color:#ccc;text-decoration:line-through; text-align:left; font-weight:bold'>
                                                De R$ <%# ((Modelos.Produto)Container.DataItem).Preco.ValorSemPromocao %>
                                            </div>
                                            <div class="title-price precoDefalut" style="font-size:24px">R$ <%# ((Modelos.Produto)Container.DataItem).Preco.Valor %></div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>







           <%-- <div class="illustration-v2 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <asp:Repeater runat="server" ID="">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-6 produtos-itens produtos-tela-produtos">
                            <!--IMG-->
                            <div style="height:194px;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12 produtos no-padding">
                                <div class="product-img">
                                    <a href="" title="">
                                        <img style="height:194px;"  class="full-width img-responsive" src="" alt="" title=""></a>
                                    <a class="product-review <%# isVisible(((Modelos.Produto)Container.DataItem)) %> <%# sobEncomenda(((Modelos.Produto)Container.DataItem), 0) %> <%# ((Modelos.Produto)Container.DataItem).Novidade ? "novidade":"" %>  <%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "indisponivel":"" %>" href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title=""><%# ((Modelos.Produto)Container.DataItem).Novidade ? "NOVIDADE":"" %>  <%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "indisponível":"" %><%# sobEncomenda(((Modelos.Produto)Container.DataItem), 1) %></a>
                                    <%# ((Modelos.Produto)Container.DataItem).GetDescontoDiv() %>
                                </div>
                            </div>
                            <!--fim IMG-->
                            <!--NOME DO PRODUTO-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tamanho-44 nome-do-produto<%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "-indisponivel":"" %>-itens no-padding">
                                <%# ((Modelos.Produto)Container.DataItem).Nome %>
                            </div>
                            <!--fim NOME DO PRODUTO-->
                            <!--VALOR-->
                            <%# ((Modelos.Produto)Container.DataItem).GetPreco() %>
                            <!--fim VALOR-->
                            <!--LINK-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 link-produto<%# ((Modelos.Produto)Container.DataItem).Indisponivel ? "-indisponivel":"" %>-itens no-padding">
                                <a href="<%# ((Modelos.Produto)Container.DataItem).GetUrl() %>" title="">Veja Mais
                                </a>
                            </div>
                            <!--fim LINK-->
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>
            <!--fim PRODUTOS-->





        </div>
    </div>
</asp:Content>
