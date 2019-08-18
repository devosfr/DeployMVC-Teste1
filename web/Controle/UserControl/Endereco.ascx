<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Endereco.ascx.cs" Inherits="ZepolControl_DadosTexto" %>
<!-- Controles -->
<%@ Import Namespace="Modelos" %>


<!-- CSS -->
<!-- JS -->
<!-- Html do Cabecalho -->

<ul>
<asp:Repeater runat="server" ID="repEnderecos">
    <ItemTemplate>
        <li>
            <label >
                <%# ((Endereco)Container.DataItem).Logradouro %>, <%# ((Endereco)Container.DataItem).Numero %><%# !String.IsNullOrEmpty(((Endereco)Container.DataItem).Complemento) ? ((Endereco)Container.DataItem).Complemento : "" %><br />
                Bairro <%# ((Endereco)Container.DataItem).Bairro %><br />
                CEP <%# ((Endereco)Container.DataItem).CEP %>, <%# ((Endereco)Container.DataItem).Cidade.Nome %> - <%# ((Endereco)Container.DataItem).Estado.Sigla %><br />
            </label>
        </li>
    </ItemTemplate>
</asp:Repeater>
</ul>