<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="404.aspx.cs" Inherits="Erros_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- CSS Page Style -->    
    <link rel="stylesheet" href="<%= MetodosFE.BaseURL %>/assets/css/pages/page_404_error.css">
    <!--=== Content Part ===-->
    <div class="container content">		
        <!--Error Block-->
        <div class="row bg-color-white rounded-2x">
            <div class="col-md-8 col-md-offset-2">
                <div class="error-v1">
                    <span class="error-v1-title">404</span>
                    <span>Pagina não encontrada!</span>
                    <p>Não encontramos esse endereço no site. Verifique a URL e tente novamente.</p>
                    <a class="btn-u btn-bordered" href="<%= MetodosFE.BaseURL %>/">Ir para Home</a>
                </div>
            </div>
        </div>
        <!--End Error Block-->
    </div>	
    <!--=== End Content Part ===-->

</asp:Content>
