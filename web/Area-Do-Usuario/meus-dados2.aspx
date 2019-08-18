<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="meus-dados2.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>Minha Conta</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/">Home</a></li>
                <li class="active">Minha Conta</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <div class="fundo-cinza">
        <div class="log-reg-v3 content-md">
            <div class="container">
                <div class="col-md-2 col-xs-12 bordered-grey menu-meusdados">
                    <ul>
                        <li class="active">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Meus dados
                            </a>
                        </li>
                        <li class="">
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
                <div class="col-md-7 col-xs-12  no-padding">
                    <div class="col-md-10 col-xs-12 no-padding">
                        <div id="sky-form4" class="log-reg-block sky-form">
                            <h2 class="atualiza">Atualização de cadastro</h2>

                            <div class="login-input reg-input">
                                <div class="row">
                                    <section runat="server" id="divPF" visible="false">
                                        <div class="col-sm-6">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtNomePF" CssClass="form-control" placeholder="Nome"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-6">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtCPF" CssClass="form-control CPF" placeholder="CPF"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-6">
                                            <section>
                                                <asp:DropDownList runat="server" ID="ddlGenero" CssClass="input form-control">
                                                    <asp:ListItem Value="">Gênero</asp:ListItem>
                                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                </asp:DropDownList>
                                            </section>
                                        </div>
                                        <div class="col-sm-6">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtNascimento" CssClass="form-control Data" placeholder="Data Nascimento"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-6">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtFonePF" CssClass="form-control Telefone" placeholder="Telefone"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-6">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtWhatsPF" CssClass="form-control Telefone" placeholder="Whatsapp"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-12">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtMailPF" CssClass="form-control" placeholder="E-mail"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                    </section>

                                    <section runat="server" id="divPJ" visible="false">
                                        <div class="col-sm-8">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtRazaoSocial" CssClass="form-control" placeholder="Razão Social"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-4">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtCNPJ" CssClass="form-control CNPJ" placeholder="CNPJ"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-4">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtInscricaoEstadual" CssClass="form-control" placeholder="Inscrição Estadual"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-4">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtFonePJ" CssClass="form-control Telefone" placeholder="Telefone"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-4">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtWhatsappPJ" CssClass="form-control Telefone" placeholder="Whatsapp"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-12">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtMailPJ" CssClass="form-control" placeholder="E-mail"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                    </section>
                                </div>
                            </div>

                            <button class="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" onserverclick="btnAtualizar_Click" type="submit">Atualizar dados</button>
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
