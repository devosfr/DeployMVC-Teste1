<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cabecalho.ascx.cs" Inherits="ZepolControl_DadosTexto" %>
<!-- Controles -->


<!-- CSS -->
<style>
    .cabecalho {
        height: 70px;
        width: 100%;
        min-width: 600px;
        position: relative;
        background: #7A7A7A;
        z-index: 5;
        -webkit-box-sizing: border-box;
        -moz-box-sizing: border-box;
        box-sizing: border-box;
        padding: 0 20px;
        overflow: visible;
        position: absolute;
        top: 0;
        left: 0;
        z-index: 130;
    }

    .menuCabecalho {
        -webkit-box-sizing: border-box;
        -moz-box-sizing: border-box;
        box-sizing: border-box;
        padding: 0 20px;
        height: 30px;
        position: absolute;
        bottom: 0;
        left: 0;
        width: 100%;
        background: #315189;
        text-align: left;
        overflow: visible;
        margin:0;
    }

        .menuCabecalho > li {
            float: left;
            height: 30px;
            position: relative;
            text-align: left;
            background: #315189;
        }

            .menuCabecalho > li > a {
                font: normal 14px Arial;
                color: white;
                text-decoration: none;
                display: block;
                padding: 0 5px;
                height: 30px;
                line-height: 30px;
            }



            .menuCabecalho > li > ul {
                text-align: center;
                position: absolute;
                left: 0;
                top: 30px;
                display: none;
                min-width: 100%;
                padding-bottom: 77px;
            }

            .menuCabecalho > li:hover > ul, .menuCabecalho > li.over > ul {
                display: block;
            }

            .menuCabecalho > li > ul > li {
                height: 30px;
                text-align: left;
                background: #315189;
                width: auto;
                min-width: 100%;
                float: left;
            }

                .menuCabecalho > li > ul > li:hover {
                    -webkit-box-sizing: border-box;
                    -moz-box-sizing: border-box;
                    box-sizing: border-box;
                    background: #FFF;
                    border: 1px solid #315189;
                }


                .menuCabecalho > li > ul > li > a {
                    font: normal 14px Arial;
                    color: white;
                    text-decoration: none;
                    display: block;
                    padding: 0 5px;
                    height: 30px;
                    line-height: 30px;
                    white-space: nowrap;
                }

                .menuCabecalho > li > ul > li:hover > a {
                    background: #FFF;
                    color: #315189;
                }

    .imagemLogotipo {
        position: absolute;
        height: 30px;
        width: 120px;
        padding: 5px;
        border: 1px solid black;
        background: white;
        line-height: 100px;
        text-align: center;
        border-radius: 5px;
        -moz-box-sizing: content-box;
        -webkit-box-sizing: content-box;
        box-sizing: content-box;
    }

        .imagemLogotipo > img {
            max-width: 105px;
            max-height: 30px;
            height: auto;
            width: auto;
            vertical-align: middle;
        }
</style>
<!-- JS -->

<!-- Html do Cabecalho -->
<asp:Literal ID="litErro" runat="server"></asp:Literal>
<div class="cabecalho">
    <div class="imagemLogotipo">
        <img src="<%= MetodosFE.BaseURL %>/assets/img/logo.jpg" />
    </div>
    <a href="http://zepol.com.br" target="_blank">
        <img src="<%= MetodosFE.BaseURL %>/images/Popup/logotipoBrancoMenor.png" style="position: absolute; top: 9px; right: 59px; max-height: 25px; height: auto; width: auto;" />
    </a>
    <asp:LinkButton ID="lkbSair" runat="server"
        OnClick="lkbSair_Click"> <img src="<%= MetodosFE.BaseURL %>/Controle/comum/img/imgSair.png" style="top: 20px;position: absolute;left: 320px;" /></asp:LinkButton>
    <asp:Repeater runat="server" ID="repMenuAreas">
        <HeaderTemplate>
            <ul class="menuCabecalho">
        </HeaderTemplate>
        <ItemTemplate>
            <li><a>
                <%# ((Modelos.GrupoDePaginasVO)Container.DataItem).nome %>
            </a>
                <ul>
                    <asp:Repeater runat="server" DataSource='<%# paginas.Where(x=>x.grupoDePaginas!=null && x.grupoDePaginas.Id == ((Modelos.GrupoDePaginasVO)Container.DataItem).Id).OrderBy(x=>x.ordem) %>'>
                        <ItemTemplate>
                            <li>
                                <a href="<%# String.Format("{0}",((Modelos.PaginaDeControleVO)Container.DataItem).pagina)%>">
                                    <%# ((Modelos.PaginaDeControleVO)Container.DataItem).nome%>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
