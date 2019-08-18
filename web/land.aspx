<%@ Page Language="C#" AutoEventWireup="true" CodeFile="land.aspx.cs" Inherits="land" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A Horta</title>
    <asp:Literal runat="server" ID="litMetaTags"></asp:Literal>
    <asp:Literal runat="server" ID="litGetSocial"></asp:Literal>
    <asp:Literal runat="server" ID="litAnalytics"></asp:Literal>

    <!-- Meta -->
    <meta charset="utf-8" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <!-- Favicon -->
    <link rel="shortcut icon" href="favicon.png" />

    <!-- CSS Global Compulsory -->
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/shop.style.css" />

    <!-- CSS Implementing Plugins -->
    <link rel="stylesheet" href="assets/plugins/line-icons/line-icons.css" />
    <link rel="stylesheet" href="assets/plugins/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/plugins/scrollbar/src/perfect-scrollbar.css" />
    <link rel="stylesheet" href="assets/plugins/owl-carousel/owl-carousel/owl.carousel.css" />
    <link rel="stylesheet" href="assets/plugins/revolution-slider/rs-plugin/css/settings.css" type="text/css" media="screen" />
    <!--[if lt IE 9]><link rel="stylesheet" href="assets/plugins/revolution-slider/rs-plugin/css/settings-ie8.css" type="text/css" media="screen"><![endif]-->

    <link rel="stylesheet" href="assets/plugins/sky-forms/version-2.0.1/css/custom-sky-forms.css" />

    <!-- CSS Theme -->
    <link rel="stylesheet" href="assets/css/theme-colors/default.css" />

    <!-- CSS Customization -->
    <link rel="stylesheet" href="assets/css/custom.css" />
    <link rel="stylesheet" href="assets/css/style.css" />


    <script>
        var baseUrl = "<%= MetodosFE.BaseURL %>/";

    </script>
</head>
<body style="background-color: #1d1d1d;">
    <form id="form1" runat="server">

        <!--TOPO-->
        <div class="baner-topo" style="background-image: url('<%= MetodosFE.BaseURL %>/assets/img/fundo-menu-topo.jpg'); height: 120px;">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="margin-top: 30px;">
                        <img id="logo-header" src="<%# MetodosFE.BaseURL %>/assets/img/n1-sports-logo.png" title="" alt="Logo" class="img-responsive " />
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12" style="margin-top: 20px; text-align: right;">
                        <!--socias-->
                        <div class="col-lg-7 col-md-6 col-sm-3 hidden-xs">
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-5 hidden-xs sociais-info-rodape">
                            <ul>
                                <li class="" runat="server" id="liFace2" visible="false">
                                    <a href="#" runat="server" visible="false" id="linkFace2" target="_blank" title="">
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/facebook-n1-sports-rodape.png" class="img-responsive" alt="" title="" />
                                    </a>
                                </li>
                                <li class="" runat="server" id="liInsta2" visible="false">
                                    <a href="#" runat="server" visible="false" id="linkInstagram2" target="_blank" title="">
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/instagram-n1-sports-rodape.png" class="img-responsive" alt="" title="" />
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="hidden-lg hidden-md hidden-sm col-xs-12 margin-top-20">
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12 text-center">
                            <!--fim socias-->
                            <a href="#" id="lkfonewhats" runat="server" target="_blank" class="btn btnland"><strong>
                                <span style="font-size: 12px;">Clique aqui e fale com a gente</span><br />
                                <asp:Literal runat="server" ID="litfoneTopo"></asp:Literal></strong><img src="<%= MetodosFE.BaseURL %>/assets/img/whatts-land.png" style="height: 22px; width: 22px; margin-left: 8px;" />
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--TOPO-->
        <!--FORMULARIO-->
        <div class="formulario">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 titulo-carrossel">
                        <h2><span style="color: #F9c300;">
                            <asp:Literal runat="server" ID="litTitulo"></asp:Literal></span>
                        </h2>
                        <span style="color: #FFFFFF;">
                            <asp:Literal runat="server" ID="litTexto"></asp:Literal>
                        </span>
                    </div>
                    <!--FORMULARIO-->
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding formulario-kemp">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-20">
                                <sapn style="font-size: 16px; color: #333333;">Cadastre-se e fique por dentro das novidades N1 Sports!
                                </sapn>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 icone-formulario no-padding">
                                    <img src="<%= MetodosFE.BaseURL %>/assets3/img/icone-nome.jpg" class="img-responsive" alt="" title="" />
                                </div>
                                <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 padd-left">
                                    <div class="form-horizontal">
                                        <fieldset>
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <asp:TextBox runat="server" ID="txtNome" CssClass="form-control input-md" placeholder="Seu Nome"></asp:TextBox>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 icone-formulario no-padding">
                                    <img src="<%= MetodosFE.BaseURL %>/assets3/img/icone-envelope.jpg" class="img-responsive" alt="" title="" />
                                </div>
                                <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 padd-left">
                                    <div class="form-horizontal">
                                        <fieldset>
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <asp:TextBox runat="server" ID="txtMail" CssClass="form-control input-md" placeholder="Seu E-mail para contato"></asp:TextBox>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:Button runat="server" OnClick="Button1_Click" ID="btnEnviar" CssClass="btn btn-block btnland" Text="Enviar" />
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 mensagem">
                                <asp:Literal runat="server" ID="litSucesso"></asp:Literal>
                                <span>
                                    <asp:Literal runat="server" ID="litErro"></asp:Literal>
                                </span>
                            </div>

                        </div>


                    </div>
                    <!--fim FORMULARIO-->
                </div>
            </div>
        </div>
        <!--fim FORMULARIO-->
        <div class="container" style="margin-bottom: 80px; border: 10px solid #000000;">
            <div class="row">
                <!--=== Slider ===-->
                <div class="tp-banner-container">
                    <div class="tp-banner">
                        <ul>
                            <asp:Repeater runat="server" ID="repSlider">
                                <ItemTemplate>
                                    <!-- SLIDE -->
                                    <li class="revolution-mch-2" data-transition="fade" data-slotamount="5" data-masterspeed="1000" data-title="Slide 1">
                                        <!-- MAIN IMAGE -->
                                        <img src="<%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" title="" alt="darkblurbg" data-bgfit="cover" data-bgposition="center" data-bgrepeat="no-repeat">
                                    </li>
                                    <!-- END SLIDE -->
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div class="tp-bannertimer tp-bottom">
                        </div>
                    </div>
                </div>
                <!--=== Slider ===-->
            </div>
        </div>

        <!--INFORMAÇÕES RODAPÉ-->
        <div class="footer-v4">
            <div class="footer">
                <div class="container">
                    <div class="row">
                        <div class="margin-top-20"></div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 informacoes-rodape no-padding">
                            <!--LOGO-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 logo-rodape">
                                <img src="<%# MetodosFE.BaseURL %>/assets/img/logo-n1-sports-rodape.png" class="img-responsive" alt="" title="" />
                            </div>
                            <!--fim LOGO-->
                            <!--INFO-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 info-rodape">
                                <asp:Literal runat="server" ID="litInfoRodapeBaixo"></asp:Literal>
                            </div>
                            <!--fim INFO-->
                            <!--FONES-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 telefone-info-rodape">
                                <ul>
                                    <li>
                                        <asp:Literal runat="server" ID="litFone2"></asp:Literal>
                                    </li>
                                </ul>
                                <ul class="whats hidden-lg hidden-md">
                                    <li>
                                        <a href="#" runat="server" target="_blank" class="whats" id="linkWhats2">
                                            <span style="color:#fbe17c;">Atendimento por WhatsApp: <asp:Literal runat="server" ID="litWhats2"></asp:Literal></span>
                                        </a>
                                    </li>
                                    <li>
                                        <img src="<%# MetodosFE.BaseURL %>/assets/img/n1-sports-whatsapp-rodape.png" />
                                    </li>
                                </ul>
                            </div>
                            <!--fim FONES-->
                            <!--socias-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 sociais-info-rodape">
                                <ul>
                                    <li class="" runat="server" id="liFace" visible="false">
                                        <a href="#" runat="server" visible="false" id="linkFace" target="_blank" title="">
                                            <img src="<%# MetodosFE.BaseURL %>/assets/img/facebook-n1-sports-rodape.png" class="img-responsive" alt="" title="" />
                                        </a>
                                    </li>
                                    <li class="" runat="server" id="liInsta" visible="false">
                                        <a href="#" runat="server" visible="false" id="linkInstagram" target="_blank" title="">
                                            <img src="<%# MetodosFE.BaseURL %>/assets/img/instagram-n1-sports-rodape.png" class="img-responsive" alt="" title="" />
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <!--fim socias-->

                            <!--ZEPOL-->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 zepol">
                                <a href="http://www.zepol.com.br/" target="_blank" title="Zepol Criação de Sites">
                                    <img src="<%# MetodosFE.BaseURL %>/assets/img/zepol.png" alt="Imagem da Zepol Criação de Sites" title="Zepol Criação de Sites - Logo" class="img-responsive" />
                                </a>
                            </div>
                            <!--fim ZEPOL-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--fim INFORMAÇÕES RODAPÉ-->


        <!-- JS Global Compulsory -->

        <script src="https://code.jquery.com/jquery-3.2.1.js" integrity="sha256-DZAnKJ/6XZ9si04Hgrsxu/8s717jcIzLy3oi35EouyE=" crossorigin="anonymous"></script>

        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-maskmoney/3.0.2/jquery.maskMoney.min.js" type="text/javascript"></script>

        <script src="https://code.angularjs.org/1.4.1/angular.min.js"></script>
        <script src="https://code.angularjs.org/1.4.1/angular-resource.min.js"></script>
        <script src="https://code.angularjs.org/1.4.1/angular-animate.min.js"></script>
        <script src="https://code.angularjs.org/1.4.1/i18n/angular-locale_pt-br.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/js/ngMask.min.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/js/Angular/keepr.js"></script>

        <script src="<%= MetodosFE.BaseURL %>/js/Angular/controleCarrinho.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/js/Angular/controleListaDesejo.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/js/Angular/controleOrcamento.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/js/Angular/controleEndereco.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/js/Angular/controleSimulacaoFrete.js"></script>

        <!-- JS Global Compulsory -->
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/jquery/jquery.min.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/jquery/jquery-migrate.min.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/bootstrap/js/bootstrap.min.js"></script>


        <!-- JS Implementing Plugins -->
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/back-to-top.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/owl-carousel/owl-carousel/owl.carousel.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/scrollbar/src/jquery.mousewheel.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/scrollbar/src/perfect-scrollbar.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/plugins/jquery.parallax.js"></script>
        <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/plugins/revolution-slider/rs-plugin/js/jquery.themepunch.tools.min.js"></script>
        <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/plugins/revolution-slider/rs-plugin/js/jquery.themepunch.revolution.min.js"></script>

        <!--DOT DOT-->
        <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/js/jquery.dotdotdot.min.js"></script>
        <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/js/dotdotConfig.js"></script>
        <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/js/dotdotConfig.min.js"></script>
        <!--DOT DOT-->
        <!-- JS Customization -->
        <script src="<%= MetodosFE.BaseURL %>/assets/js/custom.js"></script>
        <!-- JS Page Level -->
        <script src="<%= MetodosFE.BaseURL %>/assets/js/shop.app.js"></script>
        <script src="<%= MetodosFE.BaseURL %>/assets/js/plugins/owl-carousel.js"></script>
        <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/js/plugins/revolution-slider.js"></script>

        <script>
            jQuery(document).ready(function () {
                App.init();
                App.initParallaxBg();
                OwlCarousel.initOwlCarousel();
                RevolutionSlider.initRSfullWidth();
            });
        </script>
        <script src="<%# MetodosFE.BaseURL %>/js/jquery.inputmask.min.js" type="text/javascript"></script>
        <script>
            $(document).ready(function () {
                $(".Telefone").inputmask('(99)9999-9999[9]');

                $(".CPF").inputmask('999.999.999-99');

                $(".Data").inputmask('99/99/9999');

                $(".CNPJ").inputmask('99.999.999/9999-99');
            });
        </script>
    </form>
</body>
</html>
