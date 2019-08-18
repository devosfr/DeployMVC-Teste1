<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uplCor.ascx.cs" Inherits="controle_uplLogo" %>
<script language="javascript" type="text/javascript">
    // <!CDATA[

    function arquivo_onclick() {

    }

    // ]]>
</script>
<style>
    #controleCores
    {
        position: relative;
        overflow: hidden;
    }
    #controleCores > div:first-child
    {
        float: left;
        width: 50%;
        min-width:400px;
        position: relative;
    }
    #controleCores > div:first-child > div
    {
        line-height: 30px;
        margin: 5px 0;
        position: relative;
    }
    #controleCores > div:first-child > div *
    {
        vertical-align: middle;
    }
    
    #controleCores > div:nth-child(2)
    {
        float:left;
        width:50%;
        min-width:400px;
        height: 100%;
    }
    #overlay
    {
        position: absolute;
        left: 0;
        top: 0;
        margin: 0;
        width: 100%;
        height: 100%;
        z-index: 100;
        vertical-align: middle;
        text-align: center;
        background-image: url('<%=MetodosFE.BaseURL %>/images/popup/fundoLoading.png');
    }
    #overlay > img
    {
        max-width: 100%;
        max-height: 100%;
    }
    
    #listaImagens{}
    #listaImagens>li
    {
        position:relative;
        width:100%;
        text-align:center;
        
        }
    #listaImagens>li>img:first-child
    {
        max-width:100%;
        max-height:100%;
        margin:5px 0;
    }
        #listaImagens>li>img:nth-child(2)
    {
        max-width:30px;
        max-height:30px;
        position:absolute;
        top:5px;
        right:5px;
        
    }
</style>


<script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        carregaImagens();

        $("#mulitplefileuploader").uploadFile({
            url: '<%= MetodosFE.BaseURL%>/Controle/Cadastro/UploadCor.ashx',
            autoSubmit: true,
            multiple: true,
            dragDrop: true,
            showDone: false,
            //returnType: 'json',
            showStatusAfterSuccess:false,
            showProgress: true,
            allowedTypes: 'gif,jpg,png,bmp',
            formData: {
                'idProduto': '<%= Codigo %>',
                'nomeTela': '<%= nomeTela %>',
                'TamWidthG': '<%= TamFotoGrW %>',
                'TamWidthP': '<%= TamFotoPqW %>',
                'TamHeightG': '<%= TamFotoGrH %>',
                'TamHeightP': '<%= TamFotoPqH %>',
                'QtdFotos': '<%= QtdeFotos  %>',
                'Qualidade': '<%= Qualidade  %>',
                'Cor': '<%= Cor %>',
                'Configuracao': '<%= Configuracao  %>'

            },
            onSuccess: function (files, data, xhr) {
                if (data.indexOf('Erro:') != -1)
                    $("#errorMSG").html($("#errorMSG").html() + (data) + "<br/>");
                carregaImagens();
            },
            onError: function (files, status, errMsg) {
                alert('O arquivo não pode ser carregado: ' + JSON.stringify(files));
            },
            afterUploadAll: function (files, data, xhr) {
                if (data.indexOf('Erro:') != -1)
                    alert(data);
            }
        });
        $(document).on("click", ".botaoExcluirImagem", function () {
            excluirImagem($(this).attr('rel'));
        });


    });

    function excluirImagem(idFoto) {
        $.ajax({
            type: 'POST'
                , url: "<%=MetodosFE.BaseURL %>/Webservices/ControleImagensCor.asmx/excluirImagemDados"
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
                , data: "{'idImagem':'" + idFoto + "'}" //Envia a nova ordem
                , success: function (data, status) {
                    carregaImagens();
                    $("#divUpload").show();
                }
                , error: function (xmlHttpRequest, status, err) {
                    alert(xmlHttpRequest);
                    alert(status);
                    alert(err);
                }
        });
    }


    function carregaImagens() {

            $.ajax({
                type: 'POST'
                , url: "<%=MetodosFE.BaseURL %>/Webservices/ControleImagensCor.asmx/getImagensDados"
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
                , data: "{'idDado':'" + <%= Codigo %> + "'}" //Envia a nova ordem
                , success: function (data, status) {
                    $("#listaImagens").html(data.d);
                    //$(document).on("click", ".botaoExcluirImagem", function () {
                    //    excluirImagem($(this).attr('rel'));
                    //});

                }
                , error: function (xmlHttpRequest, status, err) {
                    alert(xmlHttpRequest);
                    alert(status);
                    alert(err);
                }
            });

    }


    $(window).load(
    function () {

    }
);
</script>

<asp:Literal runat="server" ID="litMensagem" ClientIDMode="Static">

</asp:Literal>
<div id="divFotoMensagem" style="padding:10px 0px 20px; font-family:Tahoma; font-size:12px;">
		Você pode enviar até <%= QtdeFotos.ToString() %> fotos (em JPG, GIF, PNG, BMP com resoluções preferencialmente em torno de <%= TamFotoGrW.ToString() %> x 
        <%= TamFotoGrH.ToString() %>) que apresente seu 
        produto.&nbsp;
	</div>
<div id="controleCores" class="CoresControl">
    <div>
        
        <div id="divUpload" style="margin: 20px 0;">
            <div id="mulitplefileuploader">Upload</div>
        </div>
        <div id="errorMSG">

        </div>
    </div>
    <div>
        <ul id="listaImagens" >
            <asp:Repeater runat="server" ID="repImagens">
                <ItemTemplate>
                    <li id="<%# ((Modelos.Cor)Container.DataItem).Id %>">
                        <img src=" <%# String.Format("{0}/ImagensLQ/{1}",MetodosFE.BaseURL, ((Modelos.Cor)Container.DataItem).Imagem) %>" > 
                        <img src="<%= String.Format("{0}/Controle/comum/img/close-button.png",MetodosFE.BaseURL) %>" class="botaoExcluirImagem" rel="<%# ((Modelos.Cor)Container.DataItem).Id %>" >
                            </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div style="clear: both;">
    </div>

</div>
<asp:Label runat="server" ID="lblMensagem"></asp:Label>
<script type="text/javascript">


</script>
