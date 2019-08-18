<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rodape.ascx.cs" Inherits="ZepolControl_Rodape" %>


<%@ Import Namespace="Modelos" %>


<script
    src="https://code.jquery.com/jquery-3.2.1.js"
    integrity="sha256-DZAnKJ/6XZ9si04Hgrsxu/8s717jcIzLy3oi35EouyE="
    crossorigin="anonymous"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnEnviar").click(function () {
            if ($("#txtEmailNew").val()) {
                {
                    $("#btnEnviar").prop("disabled", true);
                    $('#txtEmailNew').attr('placeholder', 'Executando ...');

                    var endereco = baseUrl + "Webservices/Newsletter.asmx/enviaEmailNews";
                    $.ajax({
                        type: 'POST'
                                    , url: endereco
                                    , contentType: 'application/json; charset=utf-8'
                                    , data: "{txtEmailNew:'" + $("#txtEmailNew").val() + "'}" //Envia a nova ordem
                                    , dataType: 'json'
                                    , success: function (data) {
                                        $("#txtEmailNew").val("");
                                        $('#txtEmailNew').attr('placeholder', 'E-Mail enviado com Sucesso!');
                                        $("#btnEnviar").prop("disabled", false);
                                    }
                                    , error: function (xmlHttpRequest, status, err) {
                                        $('#txtEmailNew').attr('placeholder', 'Ocorreu erro no processo');
                                        $("#btnEnviar").prop("disabled", false);
                                    }
                    });
                }
            }
            else {
                $('#txtEmailNew').attr('placeholder', 'Preencha o campo com o seu E-mail');
            }
        });

    });
</script>


        <!--=== RODAPÉ ===-->
        <div class="rodape">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 logo-rodape">
                        <img src="<%# MetodosFE.BaseURL %>/assets/img/A-Horta-logo-rodape.png" class="img-responsive" alt="A Horta Logo" title="A Horta Logo" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <ul>
                            <li class="whats-sociais" runat="server" visible="false" id="WhatsImg">
                                <a href="#" title="" runat="server" id="linkWhats">
                                     <asp:Literal Text="" ID="litFone2" runat="server" />
                                    <img src="<%# MetodosFE.BaseURL %>/assets/img/whatsapp-a-horta.png" class="img-responsive" alt="" title="" />
                                </a>
                            </li>
                            <li>
                                <a href="#" runat="server" visible="false" id="linkFace" title="" target="_blank" >
                                    <img src="<%# MetodosFE.BaseURL %>/assets/img/facebook-a-horta.png" class="img-responsive" alt="" title="" />
                                </a>
                            </li>
                            <li>
                                <a href="#" title="" target="_blank" runat="server" visible="false" id="linkInstagram">
                                    <img src="<%# MetodosFE.BaseURL %>/assets/img/instagram-a-horta.png" class="img-responsive" alt="" title="" />
                                </a>
                            </li>
                        </ul>
                    </div>
                    <!--ZEPOL-->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 zepol">
                        <a href="http://www.zepol.com.br/" target="_blank" title="Zepol Criação de Sites">
                            <img src="<%# MetodosFE.BaseURL %>/assets/img/zepol.png" alt="Imagem da Zepol Criação de Sites" title="Zepol Criação de Sites - Logo" class="img-responsive">
                        </a>
                    </div>
                    <!--ZEPOL-->
                </div>
            </div>
        </div>
        <!--===fim RODAPÉ ===-->



