<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="servicos.aspx.cs" Inherits="_contato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumbs-v4">
        <div class="container">
            <h1>Serviços</h1>
            <ul class="breadcrumb-v4-in">
                <li><a href="<%# MetodosFE.BaseURL %>/" title="">Home</a></li>
                <li class="active" title="" runat="server" id="liTopo"></li>
            </ul>
        </div>
        <!--/end container-->
    </div>
    <!--=== End Breadcrumbs v4 ===-->
    <!--=== Content Medium Part ===-->
    <div class="padding-central-de-ajuda">
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 menu-ajuda">
                    <ul>
                        <asp:Repeater runat="server" ID="repMenu">
                            <ItemTemplate>
                                <li class="<%# getAtivo(((Modelos.DadoVO)Container.DataItem).chave, Container.ItemIndex) %>">
                                    <a href="<%# MetodosFE.BaseURL + "/servicos?q=" + ((Modelos.DadoVO)Container.DataItem).chave %>" title="">
                                        <span>»</span> <%# ((Modelos.DadoVO)Container.DataItem).nome %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 menu-ajuda">
                    <div class="titulo-ajuda">
                        <h2><asp:Literal runat="server" id="litTitulo"></asp:Literal>
                        </h2>
                    </div>
                    <div class="texto-ajuda">
                        <asp:Literal runat="server" id="litTexto"></asp:Literal>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
