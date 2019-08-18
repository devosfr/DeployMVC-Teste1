<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="detalhe-blog.aspx.cs" Inherits="detalhe_noticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <link rel="stylesheet" href="../css/custom.css" />


    <!--===CONTEÚDO===-->
    <div class="fundo-telas" style="background-image: url('<%# MetodosFE.BaseURL %>/assets/img/fundo-blog.jpg');" data-speed="1.5">
        <div class="container padding-telas">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 fundo-conteudo padding-produtos no-padding">
                    <!--TITULO-->
                    <div class="titulo-blog col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 titulo text-center">
                            <h1>Blog
                            </h1>
                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim TITULO-->
                    <!--NOTICIA PRINCIPAL-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding noticia-principal-margin">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                           <%-- <div class="hidden-lg hidden-md col-sm-12 col-xs-12 img-noticia-principal" >
                                <img src="#" id="imgNoticia" alt="" runat="server" />
                            </div>--%>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="titulo-noticia-principal">
                                    <h2>
                                        <asp:Literal Text="text" ID="liDetalheLancamentosNome" runat="server" />
                                    </h2>
                                </div>
                                <div class="data-noticia-principal">
                                    <asp:Literal Text="text" ID="liDetalheLancamentosData" runat="server" />
                                </div>
                                <div class="texto-noticia-principal">
                                    <asp:Literal Text="text" ID="liDetalheLancamentosDescricao" runat="server" />
                                </div>
                                <div class="compartilhamento-blog">
                                    <ul>
                                        <li>
                                            <a href="#" runat="server" visible="false" id="linkFace" title="" target="_blank">
                                                <img src="<%# MetodosFE.BaseURL %>/assets/img/facebook.png" class="img-responsive" alt="" title="" />
                                            </a>
                                        </li>

                                  
                                        <li>
                                            <a href="#" title="" runat="server" id="linkGoogle" target="_blank">
                                                <img src="<%# MetodosFE.BaseURL %>/assets/img/google.png" class="img-responsive" alt="" title="" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#" title="" target="_blank" runat="server" visible="false" id="linkTwitter">
                                                <img src="<%# MetodosFE.BaseURL %>/assets/img/twitter.png" class="img-responsive" alt="" title="" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 hidden-sm hidden-xs img-noticia-principal">
                                <img src="#" id="imgNoticia" alt="" runat="server" style="width:100%"/>
                            </div>
                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim NOTICIA PRINCIPAL-->
                </div>
            </div>
        </div>
    </div>
    <!--===fim CONTEÚDO===-->






</asp:Content>

