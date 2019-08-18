<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Cadastro2.aspx.cs" Inherits="_cadastro" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>Entrar</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" title="">Home</a></li>
                <li class="active" title="">Cadastro</li>
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
                    <div class="col-md-6 md-margin-bottom-50 registro-esquerdo">
                        <h2 class="welcome-title">
                            <asp:Literal runat="server" ID="litTitulo"></asp:Literal></h2>
                        <div class="registro-esquerdo-texto">
                            <asp:Literal runat="server" ID="litTexto"></asp:Literal>
                        </div>
                    </div>
                    <script type="text/javascript">
                        function mostraPJ() {
                            document.getElementById("cadastroPF").style.display = "none";
                            document.getElementById("cadastroPJ").style.display = "";
                            document.getElementById("radios-0").checked = false;
                            document.getElementById("radios-1").checked = true;
                        }
                        function mostraPF() {
                            document.getElementById("cadastroPJ").style.display = "none";
                            document.getElementById("cadastroPF").style.display = "";
                            document.getElementById("radios-0").checked = true;
                            document.getElementById("radios-1").checked = false;
                        }
                    </script>
                    <div class="col-md-6 registro-direito">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <fieldset>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="radio-inline" for="radios-0">
                                                <input type="radio" onclick="mostraPF()" name="radios" id="radios-0" value="1" checked="checked" />
                                                Pessoa Física
                                            </label>
                                            <label class="radio-inline" for="radios-1">
                                                <input type="radio" onclick="mostraPJ()" name="radios" id="radios-1" value="2" />
                                                Pessoa Jurídica
                                            </label>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>
                        </div>
                        <section id="cadastroPF">
                            <div id="sky-form4" class="log-reg-block sky-form">
                                <div class="login-input reg-input">
                                    <div class="row">
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
                                        <div class="col-sm-12">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" TextMode="Password" ID="txtSenhaPF" CssClass="form-control" placeholder="Senha"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-12">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" TextMode="Password" ID="txtConfirmaPF" CssClass="form-control" placeholder="Confirmação de Senha"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                    </div>
                                </div>
                                <button class="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" id="cadastroPF" onserverclick="cadastroPF_ServerClick">CADASTRAR</button>
                            </div>
                            <div class="margin-bottom-20"></div>
                            <div class="text-center possui">Já possui uma conta? <a href="<%#MetodosFE.BaseURL %>/Area-Do-Usuario/Login.aspx" title="">Entre</a></div>
                        </section>
                        <section id="cadastroPJ">
                            <div id="sky-form4" class="log-reg-block sky-form">
                                <div class="login-input reg-input">
                                    <div class="row">
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
                                        <div class="col-sm-12">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" TextMode="Password" ID="txtSenhaPJ" CssClass="form-control" placeholder="Senha"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                        <div class="col-sm-12">
                                            <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" TextMode="Password" ID="txtConfirmaPJ" CssClass="form-control" placeholder="Confirmação de Senha"></asp:TextBox>
                                                </label>
                                            </section>
                                        </div>
                                    </div>
                                </div>
                                <button class="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" id="cadastroPJ" onserverclick="cadastroPJ_ServerClick">CADASTRAR</button>
                            </div>
                            <div class="margin-bottom-20"></div>
                            <div class="text-center possui">Já possui uma conta? <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/Login.aspx" title="">Entre</a></div>
                        </section>
                        <div class="alert alert-dismissible alert-danger rounded" runat="server" id="divMensagemErro" visible="false">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <p class="palavras-brancas">
                                <asp:Literal runat="server" ID="litErro"></asp:Literal></p>
                        </div>
                        <div class="alert alert-dismissible alert-success rounded" runat="server" id="divMensagemSucesso" visible="false">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <p class="palavras-brancas">
                                <asp:Literal runat="server" ID="litSucesso"></asp:Literal></p>
                        </div>
                    </div>
                </div>
                <!--/end row-->
            </div>
            <!--/end container-->
        </div>
    </div>
    <!--=== End Login ===-->
</asp:Content>
