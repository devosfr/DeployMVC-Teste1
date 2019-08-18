<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="sobre.aspx.cs" Inherits="_contato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <!--===CONTEÚDO===-->
    <div class="fundo-telas" style="background-image: url('assets/img/fundo-sobre.jpg');" data-speed="1.5">
        <div class="container padding-telas">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 fundo-conteudo no-padding">


                    <!--TITULO-->
                    <div class="titulo-blog col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 titulo text-center">
                            <h1>
                                <asp:Literal Text="" ID="litsobreConteudoTitulo" runat="server" />
                            </h1>
                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim TITULO-->
                    <!--TEXTO E IMG-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                           
                            <asp:Literal Text="" ID="litsobreConteudoDescr" runat="server" />
                            <%--<div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 img-sobre">
                                <asp:Repeater runat="server" ID="repRepListaFoto">
                                    <ItemTemplate>
                                        <img src="<%# ((Modelos.ImagemDadoVO)Container.DataItem).GetEnderecoImagemHQ() %>"
                                            class="img-responsive" alt=""
                                            title="" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>--%>

                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim TEXTO E IMG-->
                    <!--GALERIA-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 galeria no-padding">
                            <asp:Repeater runat="server" ID="repRepListaFotos">
                                <ItemTemplate>
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-6 img-galeria">
                                        <a  class="thumbnail fancybox-button zoomer" data-rel="fancybox-button" title="Imagem 1" href="<%# ((Modelos.ImagemDadoVO)Container.DataItem).GetEnderecoImagemHQ() %>">
                                            <div class="overlay-zoom">
                                                <img alt="" src="<%# ((Modelos.ImagemDadoVO)Container.DataItem).GetEnderecoImagemHQ() %>"
                                                     class="img-responsive imgMiniatura" title="">
                                                <span class="zoom-icon"></span>
                                            </div>
                                        </a>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim GALERIA-->
                    <!--INSTAGRAM-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 instagram-feed no-padding">
                        <div class="col-lg-2 col-md-2 col-sm-1 hidden-xs indi-instagram">
                            <img src="assets/img/instagram-indi.png" class="img-responsive" alt="" title="" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12 titulo-instagram">
                            <h3>Instagram
                            </h3>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 img-instagram">
                            <!-- LightWidget WIDGET -->
                            <script src="//lightwidget.com/widgets/lightwidget.js"></script>
                            <asp:Literal Text="" ID="litIntagramLista" runat="server" />
                        </div>
                    </div>
                    <!--fim INSTAGRAM-->
                </div>
            </div>
        </div>
    </div>
    <!--===fim CONTEÚDO===-->







</asp:Content>
