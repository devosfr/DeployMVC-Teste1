<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Alterar-Senha2.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>Alterar Senha</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" title="">Home</a></li>
                <li><a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Minha Conta</a></li>
                <li class="active" title="">Alterar Senha</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Login ===-->
    <div class="fundo-cinza">
        <div class="log-reg-v3 content-md">
            <div class="container">
                <div class="col-md-2 col-xs-12 bordered-grey menu-meusdados">
                    <ul>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Meus dados
                            </a>
                        </li>
                        <li class="active">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/alterar-senha.aspx" title="">Alterar Senha
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-pedidos.aspx" title="">Meus Pedidos
                            </a>
                        </li>
                    </ul>
                </div>

                <div class="col-md-2"></div>
                <div class="col-md-7 col-xs-12 no-padding">
                    <div class="col-md-10 col-xs-12 no-padding">
                        <div id="sky-form4" class="log-reg-block sky-form">
                            <h2 class="atualiza">Alterar senha</h2>
                            <div class="login-input reg-input">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <section>
                                            <label class="input">
                                                <asp:TextBox placeholder="Senha Atual" TextMode="Password" CssClass="form-control" runat="server" ID="txtSenhaAtual"></asp:TextBox>
                                            </label>
                                        </section>
                                    </div>
                                    <div class="col-sm-6">
                                        <section>
                                            <label class="input">
                                                <asp:TextBox placeholder="Senha Nova" TextMode="Password" CssClass="form-control" runat="server" ID="txtSenhaNova"></asp:TextBox>
                                            </label>
                                        </section>
                                    </div>
                                    <div class="col-sm-6">
                                        <section>
                                            <label class="input">
                                                 <asp:TextBox placeholder="Confirmar" TextMode="Password" CssClass="form-control" runat="server" ID="txtConfirmarSenha"></asp:TextBox>
                                            </label>
                                        </section>
                                    </div>
                                </div>
                            </div>
                            <button class="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" id="btnAlterar" onserverclick="btnAlterarSenha_Click" type="submit">Alterar senha</button>

                            <div class="alert alert-dismissible alert-danger rounded" runat="server" id="divMensagemErro" visible="false">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <p class="palavras-brancas"><asp:Literal runat="server" ID="litErro"></asp:Literal></p>
                            </div>

                            <div class="alert alert-dismissible alert-success rounded" runat="server" id="divMensagemSucesso" visible="false">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <p class="palavras-brancas"><asp:Literal runat="server" ID="litSucesso"></asp:Literal></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-1"></div>
                <!--/end container-->
            </div>
        </div>
    </div>
</asp:Content>

