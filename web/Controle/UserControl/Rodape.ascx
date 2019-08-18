<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rodape.ascx.cs" Inherits="ZepolControl_DadosTexto" %>
<%--Controles--%>

<!-- CSS -->
<style type="text/css">
    .rodape {
        width: 100%;
        min-width:600px;
        height: 77px;
        position: absolute;
        left: 50%;
        transform: translate(-50%, 0%);
        bottom: 0;
        clear: both;
        z-index: 5;
    }
</style>
<!-- HTML do Rodape -->
<div class="rodape">

    <span style="display: block; position: absolute; left: 20px; bottom: 10px; font: normal 12px Arial; color: #FFF;">Zepol © 2013 – <%= DateTime.Now.Year %>. Todos os direitos reservados. 
    </span>

    <a href="http://www.zepol.com.br" target="_blank">
        <img src="<%= MetodosFE.BaseURL %>/images/Popup/logotipoBrancoMenor.png" style="height: 30px; width: auto; position: absolute; right: 20px; top: 18px;" />
    </a>


</div>
