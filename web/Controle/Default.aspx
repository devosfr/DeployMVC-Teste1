<%@ Page Language="C#" MasterPageFile="Entrada.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">
    <div style="position: absolute; top: 50%; left: 50%; width: 300px; height: 360px;
        margin-top: -250px; margin-left: -150px;background:#FFFFFF;padding:15px;color:#FFF;border:solid 1px black; ">
        <div align="center" style="width:300px; height:100px;margin-bottom:15px;">
            <img src="<%= MetodosFE.BaseURL %>/assets/img/logo.jpg" style="width:auto;height:auto; max-height:120px; max-width:170px;margin-top: 8%;"/>
        </div>
        <table border="0" cellspacing="2" cellpadding="0">
            <tr>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left" style="height: 40px;">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="MensagemAlertaCliente" ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="TituloCampoCliente" style="color:white">Usuário:</span>
                </td>
                <td style="width: 180px" align="left" valign="middle">
                    <asp:TextBox ID="txtUsuario" runat="server" Width="180px" Height="18px" Style="border-radius: 3px 3px 3px 3px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span style="color:white">Senha:</span>
                </td>
                <td style="width: 180px" align="left" valign="middle">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogar">
                        <asp:TextBox ID="txtSenha" runat="server" Width="180px" Height="18px" TextMode="Password"
                            Style="border-radius: 3px 3px 3px 3px;"></asp:TextBox>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" valign="middle" style="padding-top: 5px;">
                    <asp:Button ID="btnLogar" runat="server" Text="Efetuar Login" Style="height: 35px;
padding: 8px 20px;
color: #FFF;
background-color: #498305;
border: 0px none;
cursor: pointer;
font-size: 16px;

margin-top: 10px;
margin-left: 20px;" OnClick="btnEntrar_Click"
                         />
                </td>
            </tr>
            <tr>
            <td>
            <br />
            </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" valign="middle" style="padding-top: 5px;">
                <div style="width:184px; height:50px;" align="center">
                <label style="color:#000;">Desenvolvido por </label>

                 <a  href="http://www.zepol.com.br" target="_blank" 
                            style="width: 135px; height: 28px" title="Página Inicial">
            <img src="<%= MetodosFE.BaseURL %>/images/Popup/logo.png" style="width:auto; height:auto; max-height:50px; max-width:184px;"/></a>  
            </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
