<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login2.aspx.cs" Inherits="_login" EnableEventValidation="false" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>Entrar</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/">Home</a></li>
                <li class="active">Login</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Login ===-->
    <div class="log-reg-v3 content-md fundo-cinza">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                </div>

                <div class="col-md-4 login-log">
                    <div id="sky-form1" class="log-reg-block sky-form">
                        <h2>Acesse sua Conta</h2>

                        <div class="col-md-12 no-padding">
                            <label class="input login-input">
                                <div class="input-group icone-formulario">
                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    <asp:TextBox runat="server" ID="txtMail" placeholder="E-mail" CssClass="form-control"></asp:TextBox>
                                </div>
                            </label>
                        </div>
                        <div class="col-md-12 no-padding">
                            <label class="input login-input no-border-top">
                                <div class="input-group icone-formulario">
                                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                     <asp:TextBox runat="server" ID="txtSenha" TextMode="Password" placeholder="Senha" CssClass="form-control"></asp:TextBox>
                                </div>
                            </label>
                        </div>
                        <div class="row margin-bottom-5">

                            <div class="esqueceu col-xs-12 text-right">
                                <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/recuperar-senha.aspx">Esqueceu a senha?</a>
                            </div>
                        </div>

                        <div class="acessar col-md-12 no-padding">
                            <asp:LinkButton runat="server" ID="btnLogin" OnClick="btnEntrar_Click" Text="ACESSAR A CONTA"></asp:LinkButton>
                        </div>

                        <div class="headline-center-v2">
                            <span class="bordered-icon">OU</span>
                        </div>
                        <div class="cadastrar col-md-1 hidden-sm hidden-xs">
                        </div>
                        <div class="cadastrar col-md-10 no-padding ">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/cadastro.aspx">CADASTRAR </a>
                        </div>
                        <div class="cadastrar col-md-1 hidden-sm hidden-xs">
                        </div>
                        <div class="alert alert-dismissible alert-danger rounded col-md-12" runat="server" id="divMensagemErro" visible="false">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <p class="palavras-brancas"><asp:Literal runat="server" ID="litErro"></asp:Literal></p>
                        </div>

                        <div class="alert alert-dismissible alert-success rounded col-md-12" runat="server" id="divMensagemSucesso" visible="false">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <p class="palavras-brancas"><asp:Literal runat="server" ID="litSucesso"></asp:Literal></p>
                        </div>



                    </div>

                    <div class="margin-bottom-20"></div>
                </div>

                <div class="col-md-4">
                </div>
            </div>
            <!--/end row-->
        </div>
        <!--/end container-->
    </div>
</asp:Content>
