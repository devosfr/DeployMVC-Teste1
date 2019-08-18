<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Cadastro.aspx.cs" Inherits="_cadastro" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="<%= MetodosFE.BaseURL %>/assets/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">

        var varmostraPJ;
        var varmostraPF;

        window.onload = function () {

            document.getElementById("cadastroPJ").style.display = "none";
            var Erro = document.getElementById('Erro');
            var ErroPJ = document.getElementById('ErroPJ');
            var Sucesso = document.getElementById('Sucesso');
            var SucessoPJ = document.getElementById('SucessoPJ');

            if (Erro.innerText != "") {
                document.getElementById("cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "block";
            }

            if (ErroPJ.innerText != "") {
                document.getElementById("cadastroPJ").style.display = "block";
                document.getElementById("cadastroPF").style.display = "none";
            }
            if (Sucesso.innerText == "Enviado com Suscesso!") {
                document.getElementById("cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "block";
            }

            if (SucessoPJ.innerText == "Enviado com Suscesso!") {
                document.getElementById("cadastroPF").style.display = "none";
                document.getElementById("cadastroPJ").style.display = "block";
            }

        }
        function mostraPJ(valor) {
            varmostraPJ = true;
            varmostraPF = false;
            if (valor == "2"){
                document.getElementById("cadastroPF").style.display = "none";
                document.getElementById("cadastroPJ").style.display = "";
                document.getElementById("radios0").checked = false;
                document.getElementById("radios1").checked = true;
            }
            
        }
        function mostraPF(valor) {
            varmostraPJ = false;
            varmostraPF = true;
            if (valor == "1") {
                document.getElementById("cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "";
                document.getElementById("radios0").checked = true;
                document.getElementById("radios1").checked = false;
            }
            
        }
        
        $('#msnErro, #msnSucesso').ready(function () {

            var msnErro = $('#msnErro');
            var msnSucesso = $('#msnSucesso');

            if (msnErro != undefined) {

                document.getElementById("cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "block";
                if (varmostraPF == true) {
                    document.getElementById("cadastroPJ").style.display = "none";
                    document.getElementById("cadastroPF").style.display = "block";
                }
                if (varmostraPJ == true) {
                    document.getElementById("cadastroPJ").style.display = "block";
                    document.getElementById("cadastroPF").style.display = "none";
                }


            }else if (msnSucesso != undefined) {

                document.getElementById("cadastroPJ").style.display = "none";
                document.getElementById("cadastroPF").style.display = "block";
                if (varmostraPF == true) {
                    document.getElementById("cadastroPJ").style.display = "none";
                    document.getElementById("cadastroPF").style.display = "block";
                }
                if (varmostraPJ == true) {
                    document.getElementById("cadastroPJ").style.display = "block";
                    document.getElementById("cadastroPF").style.display = "none";
                }


            }


        });

        

    </script>

    <asp:HiddenField runat="server" ID="emailEnviado" />



    <!--=== Breadcrumbs v4 ===-->
    <div class="breadcrumbs-v4" style="background-color:#fff">
        <div class="container">
            <a class="page-name" title="" style="color:#1b511b">Cadastro</a>
            <h1 style="color:#1b511b">Cadastre-se</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/Default.aspx" title="" style="color:#1b511b">Home</a></li>
                <li class="active" title="">Cadastro</li>
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
                    <div class="col-md-6 md-margin-bottom-50 registro-esquerdo">
                        <h2 class="welcome-title">
                            <asp:Literal runat="server" ID="litTitulo"></asp:Literal></h2>
                        <div class="registro-esquerdo-texto" style="color:#808080">
                            <asp:Literal runat="server" ID="litTexto"></asp:Literal>
                        </div>
                    </div>



                    <div class="col-md-6 registro-direito">
                        <div class="col-sm-12">
                            <form class="form-horizontal">
                                <fieldset>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label class="radio-inline" for="radios-0">
                                                <input type="radio" onclick="mostraPF(this.value)" name="radios" id="radios-0" value="1" checked />
                                                <span style="color:#666">Física</span> 
                                            </label>
                                            <label class="radio-inline" for="radios-1">
                                                <input type="radio" onclick="mostraPJ(this.value)" name="radios" id="radios-1" value="2" />
                                                <span style="color:#666">Pessoa Jurídica</span>
                                            </label>
                                        </div>
                                    </div>

                                </fieldset>
                            </form>
                        </div>


                        <div id="sky-form4" class="log-reg-block sky-form" style="margin-top:15px">

                            <div class="login-input reg-input" id="cadastroPF">
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
                                                    <asp:TextBox runat="server" ID="txtNascimento" CssClass="form-control DATA" placeholder="DD/MM/AAAA"></asp:TextBox>
                                                </label>
                                            </section>
                                    </div>

                                </div>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" name="textinput" placeholder="Telefone" ID="txtFonePF" CssClass="form-control FONE" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" name="textinput" placeholder="Whattsap" ID="txtWhatsPF" CssClass="form-control WHATS" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" name="textinput" placeholder="Email" ID="txtMailPF" CssClass="form-control" />
                                        </label>
                                    </section>

                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Senha" ID="txtSenhaPF" CssClass="form-control" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                            <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Confirmação de Senha" ID="txtConfirmaPF" CssClass="form-control" />
                                        </label>
                                    </section>
                                <%--<button class="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" type="submit">Registrar</button>--%>
                                <asp:Button CssClass="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" ID="BtnCadastro" Text="Registrar" OnClick="cadastroPF_ServerClick" ></asp:Button>

                            </div>

                            <div class="login-input reg-input" id="cadastroPJ">
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
                                    <div class="col-sm-6">
                                        <section>
                                                <label class="input">
                                                    <asp:TextBox runat="server" ID="txtDataCadastroDJ" CssClass="form-control DATA" placeholder="Data de Cadastro"></asp:TextBox>
                                                </label>
                                            </section>

                                    </div>

                                </div>
                                <section>
                                        <label class="input cadastroCampos">
                                           <asp:TextBox runat="server" ID="txtFonePJ" CssClass="form-control FONE cadastroCampos" placeholder="Telefone" maxlength="26"></asp:TextBox>
                                        </label>
                                    </section>
                                <section>
                                        <label class="input cadastroCampos">
                                           <asp:TextBox runat="server" name="textinput" placeholder="Whatsapp" ID="txtWhatsappPJ" CssClass="form-control WHATS" maxlength="26" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input cadastroCampos">
                                           <asp:TextBox runat="server" name="textinput" placeholder="E-mail" ID="txtMailPJ" CssClass="form-control cadastroCampos" />

                                        </label>
                                    </section>

                                <section>
                                        <label class="input">
                                           <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Senha" ID="txtSenhaPJ" CssClass="form-control input-md cadastroCampos" />
                                        </label>
                                    </section>
                                <section>
                                        <label class="input">
                                          <asp:TextBox runat="server" TextMode="Password" name="textinput" placeholder="Confirmação de Senha" ID="txtConfirmaPJ" CssClass="form-control input-md cadastroCampos" />
                                        </label>
                                    </section>
                                <asp:Button CssClass="btn-u btn-u-sea-shop-login btn-block margin-bottom-20" runat="server" ID="LinkButton1" Text="REGISTRAR" OnClick="cadastroPJ_ServerClick"></asp:Button>
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

                        <div class="margin-bottom-20"></div>
                        <div class="text-center possui" style="color:#666">Já possui uma conta? <a href="<%#MetodosFE.BaseURL %>/Area-Do-Usuario/Login.aspx" title="">Entre</a></div>
                    </div>
                </div>
                <!--/end row-->
            </div>
            <!--/end container-->
        </div>
    </div>
    <!--=== End Login ===-->











</asp:Content>
