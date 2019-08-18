<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="noticias.aspx.cs" Inherits="noticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--=== CONTEÚDO ===-->
    <!--NOTICIA PRINCIPAL-->
    <asp:Repeater runat="server" ID="repNotíciaTopo">
        <ItemTemplate>
            <div class="noticia-principal">
                <div class="container">
                    <div class="row">
                        <div class="margin-bottom-40"></div>
                        <!--TITULO-->
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 titulo margin-tela">
                            <h1>Notícias
                            </h1>
                        </div>
                        <!-- fim TITULO-->
                        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                            <div class="img-noticia">
                                <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" class="img-responsive" alt="" title="" />
                                <!--ver mais-->
                                <div class="botao-ver-mais-noticia margin-top-35 margin-bottom-30">
                                    <a href="<%# String.Format("{0}/detalhe-noticia/{1}", MetodosFE.BaseURL, ((Modelos.DadoVO)Container.DataItem).chave) %>" 
                                        title="" class="botao">SAIBA MAIS
                                    </a>
                                </div>
                                <!--FIM ver mais-->

                            </div>

                        </div>

                        <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 no-padding div-noticias-margin">

                            <!--TITULO-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 titulo-noticia-principal">
                                <h2>
                                    <%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                </h2>
                            </div>
                            <!--fim TITULO-->
                            <!--DATA-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 data-noticia-principal">
                                <%# ((Modelos.DadoVO)Container.DataItem).data.ToShortDateString() %>
                            </div>
                            <!--fim DATA-->
                            <!--TEXTO-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 texto margin-top-20">
                                <%# ((Modelos.DadoVO)Container.DataItem).descricao %>
                            </div>
                            <!--fim TEXTO-->
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <!--fim  NOTICIA PRINCIPAL-->


    <div class="container margin-top-20">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <hr />
            </div>
            <!--TITULO OUTRAS NOTÍCIAS-->
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 outras-noticias">
                <a title="">Outras Notícias
                </a>
            </div>
            <!--fim TITULO OUTRAS NOTÍCIAS-->

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 noticia-div-fora">

                <asp:Repeater runat="server" ID="repNotíciaTopoMulti">
                    <ItemTemplate>
                        <!--NOTICIA-->
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 noticias-itens">
                            <div class="easy-block-v1">
                                <!--IMG-->
                                <div class="tela-compacta">
                                    <a href="<%# String.Format("{0}/detalhe-noticia/{1}", MetodosFE.BaseURL, ((Modelos.DadoVO)Container.DataItem).chave) %>" title="<%# ((Modelos.DadoVO)Container.DataItem).nome %>">
                                        <img class="img-responsive" alt="" title="<%# ((Modelos.DadoVO)Container.DataItem).nome %>"
                                             src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>">
                                    </a>
                                </div>
                                <!--fim IMG-->
                                <!--TITULO-->
                                <div class="fundo-noticias margin-bottom-10">
                                    <div class="noticias-titulo tamanho-45">
                                        <a href="<%# String.Format("{0}/detalhe-noticia/{1}", MetodosFE.BaseURL, ((Modelos.DadoVO)Container.DataItem).chave) %>"
                                             title=""><%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                        </a>
                                    </div>
                                </div>
                                <!--fim TITULO-->
                                <!--DATA-->
                                <div class="fundo-cinza data">
                                    <%# ((Modelos.DadoVO)Container.DataItem).data.ToShortDateString() %>
                                </div>
                                <!--fim DATA-->
                            </div>
                        </div>
                        <!--fim NOTICIA-->
                    </ItemTemplate>
                </asp:Repeater>

                <div class="clear hidden-xs hidden-sm"></div>
                <!--DE 4em 4-->
                <div class="clear hidden-lg hidden-md"></div>
                <!--DE 2 em 2-->
                <!--PAGINAÇÃO-->
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 paginacao margin-bottom-30">
                    <div class="text-center">
                        <ul class="pagination">
                            <asp:Literal runat="server" ID="litPaginacao" />
                        </ul>
                    </div>
                </div>
                <!--fim PAGINAÇÃO-->

            </div>

        </div>
    </div>
    <!--=== fim CONTEÚDO ===-->

</asp:Content>

