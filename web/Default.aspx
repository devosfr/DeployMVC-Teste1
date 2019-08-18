<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!--=== Slider ===-->
    <div class="tp-banner-container hidden-xs hidden-sm">
        <div class="tp-banner">
            <ul>
                <asp:Repeater runat="server" ID="repSlider">
                    <ItemTemplate>
                        <!-- SLIDE -->
                        <li class="revolution-mch-2" data-transition="fade" data-slotamount="5" data-masterspeed="1000" data-title="Slide 1">
                            <!-- MAIN IMAGE -->
                            <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>"
                                title="<%# ((Modelos.DadoVO)Container.DataItem).descricao %>" alt="<%# ((Modelos.DadoVO)Container.DataItem).descricao %>"
                                data-bgfit="cover" data-bgposition="center" data-bgrepeat="no-repeat">
                            <div class="tp-caption sft start"
                                data-x="center"
                                data-hoffset="0"
                                data-y="center"
                                data-speed="1500"
                                data-start="500"
                                data-easing="Back.easeInOut"
                                data-endeasing="Power1.easeIn"
                                data-endspeed="300">
                                <div class="texto-slide">
                                    <%# ((Modelos.DadoVO)Container.DataItem).descricao %>
                                    <div class="link-slide">
                                        <a href="<%# ((Modelos.DadoVO)Container.DataItem).referencia %>" title="">SAIBA MAIS >
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <!-- END SLIDE -->

                    </ItemTemplate>
                </asp:Repeater>


            </ul>
            <div class="tp-bannertimer tp-bottom">
            </div>
        </div>
    </div>
    <!--=== fim Slider ===-->

    <div class="container padding-home">
        <div class="row">

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 fundo-conteudo no-padding">
                <!--VANTAGENS DE CONSUMIR ORGÂNICO-->
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 consumir-organico">
                    <h2>
                        <asp:Literal Text="" runat="server" ID="lithomeTitulo" />
                    </h2>
                    <ul>

                        <asp:Repeater runat="server" ID="rephomeContItens">
                            <ItemTemplate>
                                <li>
                                    <div class="etiqueta-organico">
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/etiqueta-organicos.png" class="img-responsive" alt="" title="" />
                                        <h3><%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                        </h3>
                                    </div>
                                    <div class="img-organico">
                                        <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" class="img-responsive" 
                                            alt="<%# ((Modelos.DadoVO)Container.DataItem).descricao %>" title="<%# ((Modelos.DadoVO)Container.DataItem).descricao %>" />
                                    </div>
                                    <div class="texto-organico">
                                        <%# ((Modelos.DadoVO)Container.DataItem).descricao %>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ul>
                </div>
                <!--fim VANTAGENS DE CONSUMIR ORGÂNICO-->
                <!--BLOG-->
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 blog padding-blog-home no-padding">
                    <!--TITULO BLOG-->
                    <div class="titulo-blog col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <h2>
                                Destaques do Blog
                            </h2>
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
                                        <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" class="img-responsive" alt="<%# ((Modelos.DadoVO)Container.DataItem).nome %>"
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
                                        <a href="<%# String.Format("{0}/detalhe-blog/{1}", MetodosFE.BaseURL, ((Modelos.DadoVO)Container.DataItem).chave) %>" title="Leia mais">Leia mais
                                            <img src="<%# MetodosFE.BaseURL %>/assets/img/leia-mais.png" class="img-responsive" alt="Leia mais" title="Leia mais" />
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

    <!--===fim CONTEÚDO===-->





    <script src="<%# MetodosFE.BaseURL %>/assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">

        //var preco = document.getElementsByClassName('precoDefalut');

        //var list = [];
        //for (var i = 0; i < preco.length; i++) {

        //    list[i] = preco[i].innerText.replace(",",".");

        //    document.getElementsByClassName('precoDefalut')[i].innerText = list[i];

        //}






    </script>

</asp:Content>


