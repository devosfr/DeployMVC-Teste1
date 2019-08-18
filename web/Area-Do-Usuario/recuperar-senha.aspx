<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="recuperar-senha.aspx.cs" Inherits="_recuperarSenha" EnableEventValidation="false" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff">
        <div class="container">
            <a class="page-name" style="color:#1b511b">Entrar</a>
            <h1 style="color:#1b511b">Esqueci a senha</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/Default.aspx" style="color:#1b511b">Home</a></li>
                <li class="active">Esqueci a senha</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Login ===-->
    <div class="fundo-cinza" style="background-color:#fff">
        <div class="log-reg-v3 content-md">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">
                    </div>

                    <div class="col-md-6">
                        <form id="sky-form1" class="log-reg-block sky-form titulo-recuperar-senha">
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

                        </form>

                        <div class="margin-bottom-20"></div>
                        <div class="text-center ainda-nao-tem-conta" style="color:#1b511b">
                            Ainda não tem uma conta? <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Cadastro">Registre-se.</a>
                        </div>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <!--/end row-->
            </div>
            <!--/end container-->
        </div>
    </div>
    <!--=== End Login ===-->










  

</asp:Content>
