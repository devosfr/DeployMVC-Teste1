<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="_login" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff" >
        <div class="container">
            <h1 style="color:#1b511b">Login</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/Default.aspx" style="color:#1b511b">Home</a></li>
                <li class="active">Login</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->

    <!--=== Login ===-->
    <div class="log-reg-v3 content-md fundo-cinza" style="background-color:#fff">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                </div>

                <div class="col-md-4 login-log">
     
                        <h2 style="color:#1b511b">Acesse sua Conta</h2>

                        <div class="col-md-12 no-padding">
                            <label class="input login-input">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    <asp:TextBox runat="server" TextMode="SingleLine" ID="txtMail" name="email" placeholder="E-mail" CssClass="form-control" />
                                </div>
                            </label>
                        </div>
                        <div class="col-md-12 no-padding">
                            <label class="input login-input no-border-top">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                    <asp:TextBox runat="server" TextMode="Password" name="password" placeholder="Senha" ID="txtSenha" CssClass="form-control" />
                                </div>
                            </label>
                        </div>
                        <div class="row margin-bottom-5">

                            <div class="esqueceu col-xs-12 text-right">
                                <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/recuperar-senha">Esqueceu a senha?</a>
                            </div>
                        </div>

                        <div class="acessar col-md-12">
                            <%--<asp:Button runat="server" ID="BtnLogin" Text="ACESSAR A CONTA" CssClass="btn_AcessarConta" OnClick="BtnLogin_Click"></asp:Button>--%>
                        </div>
                    <asp:Button runat="server" ID="Button1" Text="ACESSAR A CONTA" CssClass="btn_AcessarConta" OnClick="BtnLogin_Click"></asp:Button>

                        <div class="headline-center-v2" style="margin: 30px 0px 30px 0px">
                            <span class="bordered-icon">OU</span>
                        </div>
                        <div class="cadastrar col-md-1">
                        </div>
                        <div class="cadastrar col-md-10">
                            <asp:Button runat="server" ID="Button2" Text="CADASTRAR" 
                                CssClass="btn_AcessarConta" PostBackUrl="~/Area-Do-Usuario/Cadastro">
                            </asp:Button>
                        </div>
                        <div class="cadastrar col-md-1">
                        </div>
                        <div class="alert alert-dismissible alert-danger rounded col-md-12" runat="server" id="mensagemErro">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <p class="palavras-brancas">
                                <asp:Literal runat="server" ID="litErro"></asp:Literal>
                            </p>
                        </div>

                        <div class="alert alert-dismissible alert-success rounded col-md-12" runat="server" id="mensagemSucesso">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <p class="palavras-brancas">
                                <asp:Literal runat="server" ID="litSucesso"></asp:Literal>
                            </p>
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
    <!--=== End Login ===-->






</asp:Content>
