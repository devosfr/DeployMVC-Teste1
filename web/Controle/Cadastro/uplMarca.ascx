<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uplMarca.ascx.cs" Inherits="controle_uplLogo" %>
<script language="javascript" type="text/javascript">
    // <!CDATA[

    function arquivo_onclick() {

    }

    // ]]>
</script>
<style>
    #controleCores
    {
        position: relative;
        overflow: hidden;
    }
    #controleCores > div:first-child
    {
        float: left;
        width: 50%;
        min-width:400px;
        position: relative;
    }
    #controleCores > div:first-child > div
    {
        line-height: 30px;
        margin: 5px 0;
        position: relative;
    }
    #controleCores > div:first-child > div *
    {
        vertical-align: middle;
    }
    
    #controleCores > div:nth-child(2)
    {
        float:left;
        width:50%;
        min-width:400px;
        height: 100%;
    }
    #overlay
    {
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
    #overlay > img
    {
        max-width: 100%;
        max-height: 100%;
    }
    
    #listaImagens{}
    #listaImagens>li
    {
        position:relative;
        width:100%;
        text-align:center;
        
        }
    #listaImagens>li>img:first-child
    {
        max-width:100%;
        max-height:100%;
        margin:5px 0;
    }
        #listaImagens>li>img:nth-child(2)
    {
        max-width:30px;
        max-height:30px;
        position:absolute;
        top:5px;
        right:5px;
        
    }
</style>

<asp:Literal runat="server" ID="litMensagem" ClientIDMode="Static">

</asp:Literal>
<div id="divFotoMensagem" style="padding:10px 0px 20px; font-family:Tahoma; font-size:12px;">
		Você pode enviar até <%= QtdeFotos.ToString() %> fotos (em JPG, GIF, PNG, BMP com resoluções preferencialmente em torno de <%= TamFotoGrW.ToString() %> x 
        <%= TamFotoGrH.ToString() %>) que apresente esta 
        marca.&nbsp;
	</div>
<div id="controleCores" class="CoresControl">
    <div>
        
        <div id="divUpload" style="margin: 20px 0;">
            <asp:FileUpload runat="server" ID="fulMarca" />
            <asp:Button runat="server" CssClass="subir" Text="Enviar Imagem" ID="btnSubir" OnClick="btnSubir_Click" />
        </div>
        <div id="errorMSG">

        </div>
    </div>
    <div>
        <ul id="listaImagens" >
            <asp:Repeater runat="server" ID="repImagens">
                <ItemTemplate>
                    <li id="<%# ((Modelos.ImagemMarca)Container.DataItem).Id %>">
                        <img class="img-responsive" style="width: 80px" src=" <%# ((Modelos.ImagemMarca)Container.DataItem).Nome %>" > 
                        <asp:LinkButton runat="server" ID="linkExcluir" OnCommand="linkExcluir_Command" CommandArgument="<%# ((Modelos.ImagemMarca)Container.DataItem).Id %>"><img src="<%= String.Format("{0}/Controle/comum/img/close-button.png",MetodosFE.BaseURL) %>"></asp:LinkButton>
                            </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div style="clear: both;">
    </div>

</div>
<asp:Label runat="server" ID="lblMensagem"></asp:Label>
<script type="text/javascript">


</script>
