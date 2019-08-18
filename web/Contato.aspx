<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Contato.aspx.cs" Inherits="_contato" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!--===CONTEÚDO===-->
    <div class="fundo-telas" style="background-image: url('assets/img/fundo-contato.jpg');" data-speed="1.5">
        <div class="container padding-telas">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 fundo-conteudo padding-produtos no-padding">
                    <!--TITULO-->
                    <div class="titulo-blog col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 titulo text-center">
                            <h1>Contato
                            </h1>
                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim TITULO-->
                    <!--CONTATO-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding noticia-principal-margin">
                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 info-contato">
                                <asp:Literal runat="server" ID="litInfos"></asp:Literal>
                            </div>
                            <!--FORMULARIO-->
                            <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 no-padding">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 formulario-contato">

                                    <fieldset>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="txtNome" CssClass="form-control input-md" placeholder="Nome"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="txtFone" CssClass="form-control input-md FONE" placeholder="Fone"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="txtMail" CssClass="form-control input-md" placeholder="E-mail"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12 botao-contato">
                                                <asp:Button runat="server" OnClick="Button1_Click" ID="btnEnviar" CssClass="botao" Text="Enviar" />
                                            </div>
                                            <div class="mensagem col-md-12">
                                                <asp:Literal runat="server" ID="litSucesso"></asp:Literal>
                                                <span>
                                                    <asp:Literal runat="server" ID="litErro"></asp:Literal>
                                                </span>
                                            </div>
                                        </div>

                                    </fieldset>


                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 formulario-contato">

                                    <fieldset>
                                        <div class="form-group">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom-10 ">
                                                <asp:TextBox runat="server" ID="txtComent" placeholder="Comentários" CssClass="form-control rounded" TextMode="MultiLine" Rows="9"></asp:TextBox>
                                            </div>
                                        </div>
                                    </fieldset>

                                </div>
                            </div>
                            <!--fim FORMULARIO-->
                        </div>

                        <div class="col-lg-1 hidden-md hidden-sm hidden-xs">
                        </div>
                    </div>
                    <!--fim CONTATO-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 google-maps1">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 google-maps">
                            <div class="google" title="">
                                <asp:Literal runat="server" ID="litMap"></asp:Literal>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!--===fim CONTEÚDO===-->



  
</asp:Content>
