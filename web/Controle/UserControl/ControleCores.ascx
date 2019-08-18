<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControleCores.ascx.cs" Inherits="ZepolControl_DadosTexto" %>
<!-- Controles -->



<!-- CSS -->
<!-- JS -->
<!-- Html do Cabecalho -->

<asp:Literal runat="server" ID="litErro"></asp:Literal>
<asp:Label ID="lblTitulo" runat="server" Text="Cores:" CssClass="nomeCampo"></asp:Label>


<div>

    <div style="float: left; width: 300px;">
        <asp:CheckBoxList runat="server" ID="cblCores" Width="100%"></asp:CheckBoxList>
        <div>

            <asp:Panel ID="Panel1" DefaultButton="btnAdicionarDetalhe" runat="server">
                <asp:TextBox ID="txtNovoDetalhe" runat="server" Style="width: 100%; margin-top: 20px;" />
            </asp:Panel>

            <asp:ImageButton ID="btnAdicionarDetalhe" OnClick="btnAdicionarDetalhe_Click"  ImageUrl="../comum/img/BotoesList/btnAdicionar.png"
                runat="server" />
        </div>
    </div>
    <div style="clear: both;"></div>
</div>
