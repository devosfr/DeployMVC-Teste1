<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="recuperar-senha2.aspx.cs" Inherits="_recuperarSenha" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>Entrar</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/">Home</a></li>
                <li class="active">Recuperar Senha</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Login ===-->
    <div class="fundo-cinza">
        <div class="log-reg-v3 content-md">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 titulo-recuperar-senha">
                        <h2 class="welcome-title">Esqueci a Senha</h2>
                    </div>
                    <div class="col-md-6 md-margin-bottom-50 titulo-recuperar-senha">
                        <div class="texto-recuperar-senha"><asp:Literal runat="server" ID="litTexto"></asp:Literal></div>
                        <br />
                    </div>
                    <div class="col-md-5">
                        <div id="sky-form1" class="log-reg-block sky-form titulo-recuperar-senha">
                            <h2 class="welcome-title"></h2>
                            <section>
                                <label class="input login-input reenviar">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                        <asp:TextBox runat="server" ID="txtMail" placeholder="E-mail registrado na loja" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <button class="btn-u btn-u-sea-shop-login btn-block margin-top-20" runat="server" id="btnEnviar" onserverclick="btnRecuperar_Click" type="submit">Reenviar dados para este e-mail</button>

                                    <div class="alert alert-dismissible alert-danger rounded margin-top-10" runat="server" id="divMensagemErro" visible="false">
                                        <button type="button" class="close" data-dismiss="alert">×</button>
                                        <p class="palavras-brancas"><asp:Literal runat="server" ID="litErro"></asp:Literal></p>
                                    </div>

                                    <div class="alert alert-dismissible alert-success rounded" runat="server" id="divMensagemSucesso" visible="false">
                                        <button type="button" class="close" data-dismiss="alert">×</button>
                                        <p class="palavras-brancas"><asp:Literal runat="server" ID="litSucesso"></asp:Literal></p>
                                    </div>
                                </label>
                            </section>
                        </div>
                        <div class="margin-bottom-20"></div>
                        <div class="text-center ainda-nao-tem-conta">
                            Ainda não tem uma conta? <a href="cadastro.html">Registre-se.</a>
                        </div>
                    </div>
                </div>
                <!--/end row-->
            </div>
            <!--/end container-->
        </div>
    </div>
</asp:Content>
