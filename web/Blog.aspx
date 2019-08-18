<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
                    <asp:Repeater ID="repNoticiaPrincipal" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding noticia-principal-margin">
                                <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                                </div>
                                <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 img-noticia-principal">
                                        <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" class="img-responsive" alt="<%# ((Modelos.DadoVO)Container.DataItem).nome %>"
                                            title="<%# ((Modelos.DadoVO)Container.DataItem).nome %>" />
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="titulo-noticia-principal">
                                            <h2><%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                            </h2>
                                        </div>
                                        <div class="data-noticia-principal">
                                            <%# ((Modelos.DadoVO)Container.DataItem).data.ToShortDateString() %>
                                        </div>
                                        <div class="texto-noticia-principal">
                                            <%# ((Modelos.DadoVO)Container.DataItem).resumo %>
                                        </div>
                                        <div class="ver-mais-detalhe-blog">
                                            <a href="<%# String.Format("{0}/detalhe-blog/{1}", MetodosFE.BaseURL, 
    ((Modelos.DadoVO)Container.DataItem).chave) %>"
                                                title="">Leia Mais
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/leia-mais.png" />
                                            </a>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>

                    <!--fim NOTICIA PRINCIPAL-->
                    <!--BLOG-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 blog padding-blog-home no-padding">
                        <!--TITULO BLOG-->
                        <div class="titulo-blog col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                            </div>
                            <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                                <h3>Veja outras publicações
                                </h3>
                                <hr />
                            </div>

                            <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                            </div>
                        </div>
                        <!--fim TITULO BLOG-->
                        <!--BLOGS-->
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                            </div>

                            <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                                <asp:Repeater runat="server" ID="repNoticia">
                                    <ItemTemplate>
                                        <!--BLOG-->
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 blogs no-padding">
                                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12 img-noticia-blog">
                                                <a href="<%# String.Format("{0}/detalhe-blog/{1}", MetodosFE.BaseURL, ((Modelos.DadoVO)Container.DataItem).chave) %>" title="">
                                                    <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>"
                                                        class="img-responsive" alt="<%# ((Modelos.DadoVO)Container.DataItem).nome %>"
                                                        title="<%# ((Modelos.DadoVO)Container.DataItem).nome %>" />
                                                </a>
                                            </div>
                                            <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                                                <div class="titulo-noticia-blog">
                                                    <%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                                </div>
                                                <div class="data-noticia-blog">
                                                    <%# ((Modelos.DadoVO)Container.DataItem).data.ToShortDateString() %>
                                                </div>
                                                <div class="texto-noticia-blog">
                                                    <%# ((Modelos.DadoVO)Container.DataItem).resumo %>
                                                </div>
                                                <div class="link-noticia-blog">
                                                    <a href="<%# String.Format("{0}/detalhe-blog/{1}", MetodosFE.BaseURL, ((Modelos.DadoVO)Container.DataItem).chave) %>" title="">Leia mais
                                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/leia-mais.png" class="img-responsive" alt="Leia Mais" title="Leia Mais" />
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                        <!--fim BLOG-->

                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>

                            <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                            </div>
                        </div>
                        <!--fim BLOGS-->


                    </div>
                    <!--fim BLOG-->



                    <!--PAGINAÇÃO-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 paginacao margin-bottom-30">
                        <div class="text-center">
                            <ul class="pagination">
                                <asp:Literal runat="server" ID="litPaginacao" />
                            </ul>
                        </div>
                    </div>
                    <!--fim PAGINAÇÃO-->

                    <%--<!--PAGINAÇÃO-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-30">

                        <div class="paginacao">
                            <ul class="">
                                <li class="">
                                    <a href="#" title="">< Back
                                    </a>
                                </li>

                                <li class="active">
                                    <a href="#" title="">1
                                    </a>
                                </li>

                                <li class="">
                                    <a href="#" title="">2
                                    </a>
                                </li>


                                <li class="">
                                    <a href="#" title="">3
                                    </a>
                                </li>

                                <li class="">
                                    <a href="#" title="">Next >
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!--fim PAGINAÇÃO-->--%>
                </div>
            </div>
        </div>
    </div>
    <!--===fim CONTEÚDO===-->




</asp:Content>

