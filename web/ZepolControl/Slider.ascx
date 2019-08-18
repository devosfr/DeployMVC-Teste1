<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Slider.ascx.cs" Inherits="ZepolControl_Newsletter" %>


<%--Slider--%>

<!--=== Slider ===-->
<div class="tp-banner-container hidden-xs hidden-sm">
    <div class="tp-banner">
        <ul>
            <!-- SLIDE -->
            <asp:Repeater runat="server" ID="repAnimacao">
                <ItemTemplate>
                    <li class="revolution-mch-2" data-transition="fade" data-slotamount="5" data-masterspeed="1000" data-title="Slide 1">
                        <!-- MAIN IMAGE -->
                        <img src="<%# MetodosFE.BaseURL %>/assets/img/slide.jpg" title="" alt="darkblurbg" data-bgfit="cover" data-bgposition="left top" data-bgrepeat="no-repeat">
                        <div class="tp-caption revolution-ch2 sft start"
                            data-x="80"
                            data-hoffset="0"
                            data-y="20"
                            data-speed="1500"
                            data-start="500"
                            data-easing="Back.easeInOut"
                            data-endeasing="Power1.easeIn"
                            data-endspeed="300">
                            <a title="">
                                <%# ((Modelos.DadoVO)Container.DataItem).descricao %>
                            </a>
                        </div>
                        <div class="tp-caption revolution-ch2 sft start"
                            data-x="20"
                            data-hoffset="0"
                            data-y="160"
                            data-speed="1500"
                            data-start="500"
                            data-easing="Back.easeInOut"
                            data-endeasing="Power1.easeIn"
                            data-endspeed="300">
                            <div class="condicoes-do-produto-slide">
                                <div class="prestacao-slide">
                                    <div class="numero-de-prestacoes">
                                        <%# ((Modelos.DadoVO)Container.DataItem).referencia %>
                                    </div>
                                    <div class="cifrao">
                                        R$
                                    </div>
                                </div>
                                <div class="valor-slide">
                                    <%# getValor(((Modelos.DadoVO)Container.DataItem).valor) %>
                                    <a class="centavos" title=""><%# getCentavos(((Modelos.DadoVO)Container.DataItem).valor) %> </a>
                                </div>
                            </div>
                        </div>
                        <div class="botao-slide-direito tp-caption text-alig-rigth revolution-ch1 sft start"
                            data-x="80"
                            data-hoffset="0"
                            data-y="210"
                            data-speed="1500"
                            data-start="500"
                            data-easing="Back.easeInOut"
                            data-endeasing="Power1.easeIn"
                            data-endspeed="300">
                            <a href="<%# ((Modelos.DadoVO)Container.DataItem).keywords %>" title="" class="botao-slide">veja mais
                            </a>
                        </div>
                        <div class="tp-caption img-slide revolution-ch1 sft start"
                            data-x="right"
                            data-hoffset="0"
                            data-y="center"
                            data-speed="1500"
                            data-start="500"
                            data-easing="Back.easeInOut"
                            data-endeasing="Power1.easeIn"
                            data-endspeed="300">
                            <img src=" <%# ((Modelos.DadoVO)Container.DataItem).getPrimeiraImagemHQ() %>" class="img-responsive" alt="" title="" />
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

<div class="informacao-slide hidden-sm hidden-xs">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-md-12">
                    <asp:Literal runat="server" ID="litMensagemSlider" />
            </div>
        </div>
    </div>
</div>
<!--=== Slider ===-->
