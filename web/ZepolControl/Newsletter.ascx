<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Newsletter.ascx.cs" Inherits="ZepolControl_Newsletter" %>


<%--formulario newsletter--%>

<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 newsletter" style="margin-bottom:15%">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 titulo-newsletter">
        Assine nossa
                            <b>NEWSLETTER</b>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 digite-seu-email-newsletter">
        <div class="input-group">          
             <input id="txtEmailNew" type="text" class="form-control" placeholder="Seu e-mail" style="height: 40px">
            <div class="input-group-btn">               
                <button id="btnEnviarNews"  class="botao" type="button" onclick="return false;">ok</button>
            </div>
        </div>
    </div>
</div>