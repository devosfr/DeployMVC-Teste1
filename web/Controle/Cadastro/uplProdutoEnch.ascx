<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uplProdutoEnch.ascx.cs" Inherits="controle_uplLogo" %>

<%@ Import Namespace="Modelos" %>
<script language="javascript" type="text/javascript">
    // <!CDATA[

    function arquivo_onclick() {

    }

    // ]]>
</script>
<style>
    #controleCores {
        position: relative;
        overflow: hidden;
    }

        #controleCores > div:first-child {
            float: left;
            width: 50%;
            min-width: 400px;
            position: relative;
        }

            #controleCores > div:first-child > div {
                line-height: 30px;
                margin: 5px 0;
                position: relative;
            }

                #controleCores > div:first-child > div * {
                    vertical-align: middle;
                }

        #controleCores > div:nth-child(2) {
            float: left;
            width: 50%;
            min-width: 400px;
            height: 100%;
        }

    #overlay {
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

        #overlay > img {
            max-width: 100%;
            max-height: 100%;
        }

    #listaImagens {
    }

        #listaImagens > li {
            position: relative;
            width: 100%;
            text-align: center;
        }

            #listaImagens > li > img:first-child {
                max-width: 100%;
                max-height: 100%;
                margin: 5px 0;
            }

            #listaImagens > li > img:nth-child(2) {
                max-width: 30px;
                max-height: 30px;
                position: absolute;
                top: 5px;
                right: 5px;
            }
</style>

<asp:Literal runat="server" ID="litMensagem" ClientIDMode="Static">

</asp:Literal>
<div id="divFotoMensagem" style="padding: 10px 0px 20px; font-family: Tahoma; font-size: 12px;">
    Você pode enviar até <%= QtdeFotos.ToString() %> fotos (em JPG, GIF, PNG, BMP com resoluções preferencialmente em torno de <%= TamFotoGrW.ToString() %> x 
        <%= TamFotoGrH.ToString() %>) que apresente seu 
        produto.&nbsp;
</div>
<div class="clearfix">
    <div style="float: left; width: 50%; min-width: 400px;">
        <div>
            <h2>Cores
            </h2>

            <ul class="clearfix">
                <li style="width: 200px; float: left;">
                    <ul>

                        <asp:Repeater runat="server" ID="lbCores">
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton runat="server" ID="linkCor" OnCommand="linkCor_Command" CommandArgument="<%# ((Modelos.Album)Container.DataItem).Id %>" Text="<%# ((Modelos.Album)Container.DataItem).Cor.Nome %>"></asp:LinkButton> | <asp:LinkButton runat="server" ID="lbExcluir" OnCommand="lbExcluir_Command" CommandArgument="<%# ((Modelos.Album)Container.DataItem).Id %>" ForeColor="Red">X</asp:LinkButton>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
                <li style="width: 200px; float: left;">
                    <ul>
                        <li>
                            <asp:Button runat="server" ID="btnAdicionar" Width="105px" OnClick="btnAdicionar_Click" CssClass="EstiloBotao" Text="Adicionar" /><asp:DropDownList runat="server" ID="ddlCores">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdn" />

    <div runat="server" id="divUpload" visible="false" class="CoresControl" style="float: left; width: 50%; min-width: 400px;">
        <h1 id="tituloAlbum"></h1>
        <div>
            <h2 runat="server" id="litCor"></h2>
            <div style="margin: 20px 0;">
                <asp:FileUpload runat="server" Width="286px" ID="fulImagens" />                 
                <asp:Button runat="server" ID="btnSubir" style="float:right" OnClick="btnSubir_Click" Text="Enviar Imagem" />
            </div>
            <div id="errorMSG">
            </div>
        </div>
        <div>
            <ul id="listaImagens">
                <asp:Repeater runat="server" ID="repImagens">
                    <ItemTemplate>
                        <li id="<%# ((Modelos.ModeloImagem)Container.DataItem).Id %>">
                            <img style="width:100px;height:100px;" src="/ImagensHQ/<%# ((Modelos.ImagemProduto)Container.DataItem).Nome %>">
                            <asp:LinkButton runat="server" ID="linkExcluirImg" OnCommand="linkExcluirImg_Command" CommandArgument="<%# ((Modelos.ImagemProduto)Container.DataItem).Id %>"><img src="<%= String.Format("{0}/Controle/comum/img/close-button.png",MetodosFE.BaseURL) %>"></asp:LinkButton>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div style="clear: both;">
        </div>
    </div>

    <asp:Label runat="server" ID="lblMensagem"></asp:Label>
</div>
