<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Meus-Dados.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<%@ Import Namespace="Modelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">



        $('#msnErro').ready(function () {

            var msnErro = $('#msnErro');

            if (msnErro[0].innerText != undefined) {

                document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "none";
            }

        });




        window.onload = function () {

            document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "none";
            var Erro = document.getElementById('Erro');
            var ErroPJ = document.getElementById('ErroPJ');
            var Sucesso = document.getElementById('Sucesso');
            var SucessoPJ = document.getElementById('SucessoPJ');

            if (Erro.innerText != "") {
                document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "block";
            }

            if (ErroPJ.innerText != "") {
                document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "block";
                document.getElementById("cadastroPF").style.display = "none";
            }
            if (Sucesso.innerText == "Enviado com Suscesso!") {
                document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "block";
            }

            if (SucessoPJ.innerText == "Enviado com Suscesso!") {
                document.getElementById("cadastroPF").style.display = "none";
                document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "block";
            }

        }
        function mostraPJ(valor) {

            document.getElementById("cadastroPF").style.display = "none";
            document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "";
            document.getElementById("radios0").checked = false;
            document.getElementById("radios1").checked = true;
        }
        function mostraPF(valor) {

            document.getElementById("ctl00_ContentPlaceHolder1_cadastroPJ").style.display = "none";
            document.getElementById("cadastroPF").style.display = "";
            document.getElementById("radios0").checked = true;
            document.getElementById("radios1").checked = false;
        }
    </script>

    <asp:HiddenField runat="server" ID="emailEnviado" />


    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color: #fff">
        <div class="container">

            <h1 style="color: #1b511b">Minha Conta</h1>
            <ul class="breadcrumb-v4-in lista-area-do-usuario">
                <li><a href="<%# MetodosFE.BaseURL %>/Default.aspx" style="color: #1b511b">Home</a></li>
                <span>> </span>
                <li class="active">Minha Conta</li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Login ===-->
    <div class="fundo-cinza" style="background-color: #fff">
        <div class="log-reg-v3 content-md">
            <div class="container">
                <div class="col-md-3 col-xs-12 bordered-grey menu-meusdados">
                    <ul id="menu-meusdados-custom">
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-dados.aspx" title="">Meus Dados
                            </a>
                        </li>
                        <li class="">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/alterar-senha.aspx" title="">Alterar Senha
                            </a>
                        </li>
                        <li class="active">
                            <a href="<%# MetodosFE.BaseURL %>/Area-Do-Usuario/meus-pedidos.aspx" title="">Meus Pedidos
                            </a>
                        </li>
                    </ul>
                </div>

                <div class="col-md-8 col-xs-12 pull-right no-padding">
                    <div class="col-md-10 col-xs-12 no-padding">
                        <h2 class="atualiza" style="color: #1b511b">Atualização de cadastro</h2>
                        <div class="col-sm-12 no-padding">
                            <form class="form-horizontal">
                                <fieldset>
                                    <%--  <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="radio-inline" for="radios-0">
                                                <input type="radio" onclick="mostraPF(this.value)" name="radios" id="radios-0" value="1" checked />
                                                Pessoa Física
                                            </label>
                                            <label class="radio-inline" for="radios-1">
                                                <input type="radio" onclick="mostraPJ(this.value)" name="radios" id="radios-1" value="2" />
                                                Pessoa Jurídica
                                            </label>
                                        </div>
                                    </div>--%>
                                </fieldset>
                            </form>
                        </div>


                        <div id="sky-form4" class="log-reg-block sky-form">

                            <div class="login-input reg-input" id="cadastroPF">
                                <div class="row">


                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                     <asp:TextBox runat="server" ID="txtNomePF" CssClass="form-control meusDadosCamposMenores" placeholder="Nome"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>
                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtCPF" CssClass="form-control CPF meusDadosCamposMenores" placeholder="CPF"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>

                                    <div class="col-sm-6">
                                        <section>
                                            <asp:DropDownList runat="server" ID="ddlGenero" CssClass="input form-control meusDadosCamposMenores">
                                                    <asp:ListItem Value="">Gênero</asp:ListItem>
                                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                </asp:DropDownList>
                                            </section>
                                    </div>
                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtNascimento" CssClass="form-control DATA meusDadosCamposMenores" placeholder="DD/MM/AAAA"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>

                                </div>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" name="textinput" placeholder="Telefone" ID="txtFonePF" CssClass="form-control FONE meusDadosCampos" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" name="textinput" placeholder="Whattsap" ID="txtWhatsPF" CssClass="form-control WHATS meusDadosCampos" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" name="textinput" placeholder="Email" ID="txtMailPF" CssClass="form-control meusDadosCampos" />
                                        </label>
                                    </section>

                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Senha" ID="txtSenhaPF" CssClass="form-control meusDadosCampos" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Confirmação de Senha" ID="txtConfirmaPF" CssClass="form-control meusDadosCampos" />
                                        </label>
                                    </section>

                                <asp:Button CssClass="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" ID="BtnCadastro"
                                    Text="Registrar" OnClick="btnAtualizar_Click"></asp:Button>

                            </div>

                            <div class="login-input reg-input" runat="server" id="cadastroPJ">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtRazaoSocial" CssClass="form-control" placeholder="Razão Social"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>
                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtCNPJ" CssClass="form-control CNPJ" placeholder="CNPJ"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>
                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtInscricaoEstadual" CssClass="form-control" placeholder="Inscrição Estadual"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>

                                </div>
                                <section>
                                        <label class="input">
                                           <asp:TextBox runat="server" ID="txtFonePJ" CssClass="form-control FONE" placeholder="Telefone"></asp:TextBox>
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                           <asp:TextBox runat="server" name="textinput" placeholder="Whatsapp" ID="txtWhatsappPJ" CssClass="form-control WHATS" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                           <asp:TextBox runat="server" name="textinput" placeholder="E-mail" ID="txtMailPJ" CssClass="form-control" />

                                        </label>
                                    </section>

                                <section>
                                        <label class="input">
                                           <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Senha" ID="txtSenhaPJ" CssClass="form-control input-md" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                          <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Confirmação de Senha" ID="txtConfirmaPJ" CssClass="form-control input-md" />
                                        </label>
                                    </section>
                                <asp:Button CssClass="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" ID="LinkButton1" Text="REGISTRAR PJ" OnClick="btnAtualizar_Click"></asp:Button>
                            </div>



                            <div class="alert alert-dismissible alert-danger rounded" runat="server" id="divMensagemErro" visible="false">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <p class="palavras-brancas" id="msnErro">
                                    <asp:Literal runat="server" ID="litErro"></asp:Literal>
                                </p>
                            </div>
                            <div class="alert alert-dismissible alert-success rounded" runat="server" id="divMensagemSucesso" visible="false">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <p class="palavras-brancas" id="msnSucesso">
                                    <asp:Literal runat="server" ID="litSucesso"></asp:Literal>
                                </p>
                            </div>


                        </div>
                    </div>
                </div>
                <!--/end container-->
            </div>
        </div>
    </div>
    <!--=== End Login ===-->



    <%-- <div class="breadcrumbs-v4">
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
    </div>--%>
</asp:Content>
