<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fundo.ascx.cs" Inherits="ZepolControl_DadosTexto" %>
<!-- Controles -->
<%--<%@ Register Src="~/ZepolControl/MenuMaster.ascx" TagName="MenuMaster" TagPrefix="uc5" %>--%>
<!-- CSS -->
<style>
div.TopoFundo
{
    background-image: url("<%=MetodosFE.BaseURL%>/Controle/comum/img/fundoTopo.png");     
    background-position: center bottom;     
    background-repeat:repeat;
    border: 0 none; /*float: left;*/
    position: absolute;
    top: 0px;
    text-align:center;
    height: 70px;
    width: 100%;
    z-index: 1;
}
div.AnimacaoFundo
{
    background-image: url("<%=MetodosFE.BaseURL%>/images/fundoAnimacao.jpg");   
    position: absolute;
    top: 101px;
    height: 430px;
    width: 100%;
    z-index: 1;
}
div#BaseFundo
{
    background-image: url("<%=MetodosFE.BaseURL%>/Controle/comum/img/fundoRodape.png");
    background-repeat: repeat;
    border: 0 none; /*float: left;*/
    position: absolute;
    bottom: 0;
    height: 77px;
    width: 100%;
    z-index: 1;
}
</style>

<!-- Html do Cabecalho -->

<div id="TopoFundo" class="TopoFundo"></div>

<div id="BaseFundo"></div>

