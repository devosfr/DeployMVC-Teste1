<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cabecalho.ascx.cs" Inherits="ZepolControl_DadosTexto" %>
<!-- Controles -->

<%@ Import Namespace="Modelos" %>

<div class="topo hidden-sm hidden-xs">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-md-12">
                <ul>
                    <li>
                        <a href="#" title="" target="_blank" runat="server" visible="false" id="linkFace">
                            <img src="<%# MetodosFE.BaseURL %>/assets/img/facebook-a-horta.png" class="img-responsive" alt="" title="" />
                        </a>
                    </li>
                    <li>
                        <a href="#" title="" target="_blank" runat="server" visible="false" id="linkInstagram">
                            <img src="<%# MetodosFE.BaseURL %>/assets/img/instagram-a-horta.png" class="img-responsive" alt="" title="" />
                        </a>
                    </li>
                    <li class="whats-sociais">
                        <a title="" runat="server" visible="false" id="linkWhats">
                            <asp:Literal runat="server" ID="litWhats"></asp:Literal>
                            <img src="<%# MetodosFE.BaseURL %>/assets/img/whatsapp-a-horta.png" class="img-responsive" alt="" title="" />
                        </a>
                    </li>
                    <li>
                        <div class="dropdown">
                            <a href="#" class="dropdown-toggle" type="button" style="color: #1b511b; font-weight: bold" data-toggle="dropdown">Entrar
                             <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu" id="submenuEntrar">
                                <li class="" runat="server" id="lilogin">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Login" title="">Login</a>
                                </li>
                                <li class="" runat="server" id="lilogout">
                                    <asp:Button Text="Sair" ID="logout" OnClick="Deslogar" runat="server" CssClass="btnLogout" />
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Cadastro.aspx" title="">Cadastro</a>
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Minha conta</a>
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Alterar-Senha.aspx" title="">Alterar Senha</a>
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Meus-Pedidos" title="">Meus pedidos</a>
                                </li>
                            </ul>
                        </div>

                    </li>
                    <li class="" style="height: 26px">
                        <a href="<%# MetodosFE.BaseURL %>/carrinho" title="" class="hover-carrinho">
                            <div class="qtd-topo-carrinho carrinho-topo-menu pull-right">
                                <asp:Literal Text="" ID="litCarrinho" runat="server" />
                            </div>
                            <div class="pull-right texto-carrinho" style="margin: 0px 2px 0px 10px; padding-top: 7px">
                                Carrinho
                            </div>
                            <span class="glyphicon fa fa-shopping-cart pull-right" style="padding-top: 7px"></span>
                        </a>
                    </li>
                </ul>
            </div>

        </div>
    </div>
</div>



<%--<div class="topo" id="entrar-mobile">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-md-12">
                <ul>
                    <li style="width: 100%;">
                        <div class="dropdown">
                            <a href="#" class="dropdown-toggle" type="button" style="color: #1b511b; font-weight: bold; float: left" data-toggle="dropdown">Entrar
                             <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-mobile" id="submenuEntrarMobile">
                                <li class="" runat="server" id="liloginMobile">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Login" title="">Login</a>
                                </li>
                                <li class="" runat="server" id="lilogoutMobile">
                                    <asp:Button Text="Sair" ID="Button1" OnClick="Deslogar" runat="server" CssClass="btnLogout" />
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Cadastro.aspx" title="">Cadastro</a>
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Minha conta</a>
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Alterar-Senha.aspx" title="">Alterar Senha</a>
                                </li>
                                <li class="">
                                    <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Meus-Pedidos" title="">Meus pedidos</a>
                                </li>
                            </ul>
                        </div>

                    </li>
                </ul>
            </div>

        </div>
    </div>
</div>--%>
<!-- Brand and toggle get grouped for better mobile display -->
<%--<div id="entrar-mobile">

</div>--%>



<div class="header" id="headerCabecalho">
    <!-- Navbar -->
    <div class="navbar navbar-default mega-menu" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header hidden-lg hidden-md">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-responsive-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="fa fa-bars"></span>
                </button>
                <a class="navbar-brand" href="<%# MetodosFE.BaseURL %>/">
                    <img id="logo-header" src="<%# MetodosFE.BaseURL %>/assets/img/A-Horta-logo.png" class="img-responsive" alt="Logo Empresa" title="" />
                </a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse navbar-responsive-collapse">
                <ul class="nav navbar-nav">

                    <li class="active hidden-lg hidden-md">
                        <a href="<%# MetodosFE.BaseURL %>/" title="">HOME
                        </a>
                    </li>

                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/sobre" title="">SOBRE
                        </a>
                    </li>

                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/produtos" title="">PRODUTOS
                        </a>
                    </li>

                    <li class="hidden-sm hidden-xs logo-menu">
                        <a href="<%# MetodosFE.BaseURL %>/" style="width:auto" title="">
                            <img src="<%# MetodosFE.BaseURL %>/assets/img/A-Horta-logo.png" class="img-responsive" alt="" title="" />
                        </a>
                    </li>

                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/blog" title="">BLOG
                        </a>
                    </li>

                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/contato" title="">CONTATO 
                        </a>
                    </li>
                </ul>
                <%-- MOBILE --%>
                <ul class="nav navbar-nav" id="entrar-mobile">

                    <li class="" runat="server" id="liloginMobile">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Login" title="">Login</a>
                    </li>
                    <li class="" runat="server" id="lilogoutMobile">
                        <asp:Button Text="Sair" ID="Button2" OnClick="Deslogar" runat="server" CssClass="btnLogout" />
                    </li>
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Cadastro.aspx" title="">Cadastro</a>
                    </li>
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Minha conta</a>
                    </li>
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Alterar-Senha.aspx" title="">Alterar Senha</a>
                    </li>
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Meus-Pedidos" title="">Meus pedidos</a>
                    </li>
                    <li class="">
                        <a href="<%# MetodosFE.BaseURL %>/carrinho" title="">Carrinho</a>
                    </li>
                </ul>


            </div>
            <!--/navbar-collapse-->
        </div>
    </div>
    <!-- End Navbar -->
</div>
<!--===fim CABEÇALHO===-->






