﻿<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="ImportacaoTelas.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {
				    $('#divDados').show();
				    $('#tituloCadastro').css('color', '#FFF');
				});


    </script>

    <asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina">
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>

    <h1 id="tituloCadastro" style="color:#FFF;" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <div>

            <ul>
                <li>
                    <a runat="server" id="linkXML"></a>
                </li>
                <li>
                    <asp:FileUpload runat="server" ID="fulSiteMap" />
                </li>
                <li>
                    <asp:Button runat="server" ID="btnCarregar" Text="Clique para carregar" CssClass="EstiloBotao" OnClick="btnCarregar_Click" />
                </li>

            </ul>
        </div>
    </div>
</asp:Content>

