<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CidadeEstado.ascx.cs"
    Inherits="ZepolControl_DadosTexto" %>
<!-- Controles -->
<%--<%@ Register Src="~/ZepolControl/MenuMaster.ascx" TagName="MenuMaster" TagPrefix="uc5" %>--%>
<!-- CSS -->
<script type="text/javascript">
    $(document).ready(function () {
        // get cidades//
        $("#<%=ddlEstado.ClientID%>").bind("keyup change", function (e) {
                $.ajax({
                    type: 'POST'
                , url: "<%= MetodosFE.BaseURL %>/Webservices/Location.asmx/getCityByState"
                , contentType: 'application/json; charset=utf-8'
                , dataType: 'json'
                , data: "{'stateID':'" + $("#<%=ddlEstado.ClientID%>").val() + "'}" //Envia a nova ordem
                , success: function (data, status) {
                    $("#hfEstado").val($("#<%=ddlEstado.ClientID%>").val());
                    $("#<%=ddlCidade.ClientID%>").html(data.d);
                    $("#hfCidade").val('');
                }
                , error: function (xmlHttpRequest, status, err) {
                    alert($("#<%=ddlEstado.ClientID%>").val()); //No caso de ocorrer algum erro.
                    alert(xmlHttpRequest);
                    alert(status);
                    alert(err);
                }
                });
            });

            if ($("#hfEstado").val() != "") {

                $.ajax({
                    type: 'POST'
                    , url: "<%= MetodosFE.BaseURL %>/Webservices/Location.asmx/getCityByState"
                    , contentType: 'application/json; charset=utf-8'
                    , dataType: 'json'
                    , data: "{'stateID':'" + $("#hfEstado").val() + "'}" //Envia a nova ordem
                    , success: function (data, status) {
                        $("#<%=ddlCidade.ClientID%>").html(data.d);
                        $("#<%=ddlCidade.ClientID%>").val($("#hfCidade").val());
                    }
                    , error: function (xmlHttpRequest, status, err) {
                        alert($("#<%=ddlEstado.ClientID%>").val()); //No caso de ocorrer algum erro.
                        alert(xmlHttpRequest);
                        alert(status);
                        alert(err);
                    }
                });

                
            }

            <%-- verifica valor no combo e muda o campo hidden com id da cidade
                foi usado HiddenField para não ter que fazer uso de um ajaxToolKit devido ao DropDownList
                não aceitar alterações em tempo de execução --%>
            $("#<%=ddlCidade.ClientID%>").bind("keyup change", function (e) {
                $("#hfCidade").val($(this).val());
            });
        });
</script>
<asp:HiddenField ID="hfCidade" Value="" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hfEstado" Value="" runat="server" ClientIDMode="Static" />
<li>
    <asp:Label runat="server" Text="Estado:" CssClass="nomeCampo"></asp:Label>
    <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="true" Style="width: 144px;">
    </asp:DropDownList>
    <asp:Label ID="Label1" runat="server" Text="Cidade:" CssClass="nomeCampo" Style="width: 61px;"></asp:Label>
    <asp:DropDownList ID="ddlCidade" runat="server" AppendDataBoundItems="true" Style="width: 389px;">
    </asp:DropDownList>
</li>

<!-- Html do Cabecalho -->
