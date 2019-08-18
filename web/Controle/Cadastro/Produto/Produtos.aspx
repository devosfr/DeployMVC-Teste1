<%@ Page Title="" Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master"
    AutoEventWireup="true" CodeFile="Produtos.aspx.cs" Inherits="Controle_Cadastro_produtos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/Controle/Cadastro/uplProdutoEnch.ascx" TagName="uplLogoProd" TagPrefix="uc1" %>
<%@ Register Src="~/Controle/UserControl/ControleCores.ascx" TagPrefix="uc1" TagName="ControleCores" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">
    <style>
        #divDados {
            display: none;
        }

        #divDescricao {
            display: none;
        }

        #divDetalhes {
            display: none;
        }

        #divCores {
            display: none;
        }

        #divBusca {
            display: none;
        }

        /*
        .tituloProduto
        {
            color: White;
            background: white;
            border: 1px solid black;
            cursor: pointer;
            margin: 10px 0;
        }
        .listaLinha
        {
            list-style-type: none;
        }
        .listaLinha > li
        {
            list-style-type: none;
        }
        .listaLinha > li > span
        {
            display: inline-block;
            width: 200px;
            text-align: right;
        }
        .listaLinha > li > submit[type="text"]
        {
            width: 200px;
            text-align: right;
        }*/
        .mensagemErro {
            border: 1px solid white;
            background: #660000;
            min-height: 30px;
            padding: 4px 15px;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            display: none;
        }

        .boxSize {
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.13.4/jquery.mask.min.js"></script>
    <script>
        $('.decimal4').mask("##.000", { reverse: true });
    </script>
    <script type="text/javascript">
        $(document).ready(
                function () {
                    if (window.location.href.indexOf("Codigo") != -1) {
                        $('#divDados').show();
                        $('#divDados>div').each(function () {

                            $(this).hide();
                        });
                        if ($("#hfSecao").val() != "") {

                            $($("#hfSecao").val()).slideDown();
                        }
                        else {
                            //$('#divInformacoes').show();
                        }
                        $('#divLista').hide();
                        $('#tituloBusca').css('display', 'none');
                    }
                    else {
                        $("#hfSecao").val("");
                        $('#divDados').hide();

                        $('#divDados>div').each(function () {

                            $(this).hide();
                        });
                        $('#divDados>h1.tituloProduto').each(function () {
                            $(this).hide();
                        });
                        $('#divLista').show();
                        $('#tituloBusca').css('color', '#FFF');
                    }
                    $('#h1Informacoes').click(function () {
                        $("#hfSecao").val('#divInformacoes');
                        escondeDivs();
                        $('#divInformacoes').stop().slideDown();
                    });
                    $('#h1Descricao').click(function () {
                        $("#hfSecao").val('#divDescricao');
                        escondeDivs();
                        $('#divDescricao').stop().slideDown();
                    });
                    $('#h1Detalhes').click(function () {
                        $("#hfSecao").val('#divDetalhes');
                        escondeDivs();
                        $('#divDetalhes').stop().slideDown();
                    });
                    $('#h1Cores').click(function () {
                        $("#hfSecao").val('#divCores');
                        escondeDivs();
                        $('#divCores').stop().slideDown();
                    });
                    $('#h1Informacoes2').click(function () {
                        $("#hfSecao").val('#divInformacoes2');
                        escondeDivs();
                        $('#divInformacoes2').stop().slideDown();
                    });
                    $('#h1Busca').click(function () {

                        escondeDivs();
                        $('#divBusca').stop().slideDown();
                    });
                    $('#tituloCadastro').click(function () {
                        $('#divLista').stop().slideToggle(function () {
                            if ($('#divLista').is(':visible'))
                                $('#tituloBusca').css('color', '#FFF');

                            else
                                $('#tituloBusca').css('color', 'rgb(122, 122, 122)');
                        });
                        $('#divDados').stop().slideToggle(function () {
                            if ($('#divDados').is(':visible'))
                                $('#tituloCadastro').css('color', '#FFF');

                            else
                                $('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
                        });
                    });
                    $('#tituloBusca').click(function () {
                        $('#divLista').stop().slideToggle(function () {
                            if ($('#divLista').is(':visible'))
                                $('#tituloBusca').css('color', '#FFF');

                            else
                                $('#tituloBusca').css('color', 'rgb(122, 122, 122)');
                        });
                        $('#divDados').stop().slideToggle(function () {
                            if ($('#divDados').is(':visible'))
                                $('#tituloCadastro').css('color', '#FFF');

                            else
                                $('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
                        });
                    });
                    $('#h1DimensoesMinimas').click(function () {

                    });
                });

        function escondeDivs() {
            $('#divDados>div').each(function () {
                $(this).stop().slideUp();
            });
        }
        function mostraDivs() {
            $('#divDados>div').each(function () {
                $(this).stop().slideDown();
            });
        }
    </script>

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hfSecao" />
    <div>
        <%--Nome e ID do Produto--%>
    </div>
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <strong><span style="color: #000000; font-family: Arial">
            <asp:Label ID="lblProdutos" runat="server"></asp:Label></span> </strong>
        <br />
        <br />
        <ul class="listaLinha">
            <li>
                <span>ID:</span>
                <asp:TextBox ID="txtIdDados" runat="server" Width="100px" AutoPostBack="True" Enabled="False"></asp:TextBox>
            </li>
            <li>
                <span>Nome:</span>
                <asp:TextBox ID="txtNome" runat="server" Width="391px" AutoCompleteType="Disabled"></asp:TextBox>
            </li>
            <li>
                <asp:Button ID="btnSalvar" CausesValidation="true" runat="server" Text="Criar Produto"
                    OnClick="btnSalvar_Click" CssClass="EstiloBotao" />
            </li>
        </ul>
        <h1 class="tituloProduto" id="h1Informacoes">Dados</h1>
        <div id="divInformacoes">
            <ul class="listaLinha">
                <li>
                    <span>Referência:</span>
                    <asp:TextBox ID="txtreferencia" runat="server" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                </li>
                <li>
                    <span>Visível:</span>
                    <asp:CheckBox runat="server" ID="chkVisivel" />
                </li>
                <li>
                    <span>Indisponível:</span>
                    <asp:CheckBox runat="server" ID="chkIndisponivel" />
                </li>
                <%--<li>
                    <span>Destaque:</span>
                    <asp:CheckBox runat="server" ID="chkDestaque" />
                </li>--%>
                 <li>
                    <span>Marca:</span>
                    <asp:DropDownList runat="server" ID="ddlMarca" />
                </li>
                <li>
                    <span>Segmentos:</span>
                    <div>
                        <asp:TreeView CollapseImageUrl="~/Controle/comum/img/ic_menos.gif" NoExpandImageUrl="~/Controle/comum/img/tvSubItemAtivo.gif" ExpandImageUrl="~/Controle/comum/img/ic_mais.gif" runat="server" ShowCheckBoxes="All" ID="tvSegmentos"></asp:TreeView>
                    </div>
                </li>
                <li>
                    <span>Tamanhos:</span>
                    <div>
                        <ul>
                            <asp:Repeater runat="server" ID="repTamahos">
                                <ItemTemplate>
                                    <li>
                                        <asp:CheckBox runat="server" ID="chkTamanho" AutoPostBack="true" OnCheckedChanged="chjTamanho_CheckedChanged" Text="<%# ((Modelos.Tamanhos)Container.DataItem).Nome %>" />
                                        <asp:HiddenField runat="server" ID="hdnTam" Value="<%# ((Modelos.Tamanhos)Container.DataItem).Id %>" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
        <h1 class="tituloProduto" id="h1Descricao">Descrições</h1>
        <div id="divDescricao">
            <ul class="listaLinha">
                <%--   <li runat="server" id="divResumo">
                    <span>Resumo</span>
                    <br />
                    <CKEditor:CKEditorControl ID="txtResumo" EnterMode="BR" BasePath="~/ckeditor/" Width="100%" Height="250px" runat="server"></CKEditor:CKEditorControl>
                </li>--%>
                <li>
                    <span>Descrição</span>
                    <br />
                    <CKEditor:CKEditorControl ID="txtDescricao" EnterMode="BR" BasePath="~/ckeditor/" Width="100%" Height="350px" runat="server"></CKEditor:CKEditorControl>
                </li>


                <li id="liGridInformacoesDado" runat="server" visible="false">
                    <div>
                        <div style="margin-bottom: 5px;">
                            <span>
                                <asp:Label ID="lblGridInformacoesDado" runat="server" /></span>
                        </div>
                        <br />
                        <%--<div>
                            <div style="margin-bottom: 5px;">
                                <span style="padding-right: 35px;">Característica</span><asp:TextBox runat="server" ID="txtNomeInformacao"></asp:TextBox><br />
                            </div>
                            <asp:Label ID="labelColunaVisivel" runat="server" Text="Visivel ?" Style="padding-right: 25px;" />
                            <asp:DropDownList ID="ddlColunaVisivel" runat="server" Width="100px">
                                <asp:ListItem Selected="True" Value="true">Sim</asp:ListItem>
                                <asp:ListItem Value="false">Não</asp:ListItem>
                            </asp:DropDownList>
                            <br />

                            <div style="margin-top: 15px;">
                                <span>Descrição da Característica</span>
                            </div>
                            <CKEditor:CKEditorControl ID="txtDescricaoInformacao" EnterMode="BR" BasePath="~/ckeditor/" Width="600px" Height="350px" runat="server"></CKEditor:CKEditorControl>
                            <asp:Button runat="server" ID="btnAdicionarInformacao" CssClass="EstiloBotao" Text="Adicionar" OnClick="btnAdicionarInformacao_Click" />
                            <asp:Button runat="server" ID="btnCancelarInformacao" CssClass="EstiloBotao" Text="Cancelar" OnClick="btnCancelarInformacao_Click" />
                        </div>--%>

<%--                        <asp:GridView ID="gvInformacoes" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                            DataKeyNames="Id" CellPadding="4" EmptyDataText="Não existe dados."
                            ForeColor="Black" GridLines="Vertical" OnRowDeleting="gvInformacoes_RowDeleting"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="#CCCC99" />
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Codigo" InsertVisible="False"
                                    SortExpression="Id" />
                                <asp:BoundField DataField="Nome" HeaderText="Característica"
                                    SortExpression="Nome" />
                                <asp:BoundField DataField="Texto" HeaderText="Descrição da Característica"
                                    SortExpression="Texto" />
                                <asp:TemplateField HeaderText="Visível">
                                    <ItemTemplate>
                                        <span><%# ((Modelos.InformacaoProduto)Container.DataItem).Visivel ? "Sim" : "Não"  %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnEditarInformacao" Text="Editar" OnClick="btnEditarInformacao_Click" CommandArgument="<%# ((Modelos.InformacaoProduto)Container.DataItem).Id %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                            OnClientClick="return confirm('Realmente deseja excluir este produto, junto com suas imagens e demais informações relacionadas?')"
                                            CommandName="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>--%>
                    </div>
                </li>

            </ul>
        </div>

        <h1 class="tituloProduto" id="h1Detalhes">Detalhes</h1>
        <div id="divDetalhes">

            <div runat="server" id="divTamanho" style="display: none;">
                <span>Tamanhos:</span>
                <div>
                    <asp:TreeView CollapseImageUrl="~/Controle/comum/img/ic_menos.gif" NoExpandImageUrl="~/Controle/comum/img/tvSubItemAtivo.gif" ExpandImageUrl="~/Controle/comum/img/ic_mais.gif" runat="server" ShowCheckBoxes="All" ID="treViewTamanhos"></asp:TreeView>
                </div>
            </div>
            <div>
                <span>Peso:</span>
                <asp:TextBox runat="server" CssClass="decimal4" ID="txtPeso"></asp:TextBox>
            </div>
            <div runat="server" id="divDepara">
                <span>Preço "De" (Para promoções):</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtPrecoDe"></asp:TextBox>
            </div>
            <div>
                <span>Preço:</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtPreco"></asp:TextBox>
            </div>
            <%--<div>
                <span>Preço à vista:</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtPrecoAvista"></asp:TextBox>
            </div>
            <div>
                <span>Desconto (%):</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtDeconto"></asp:TextBox>
            </div>--%>
            <%--<div>
                <span>Texto abaixo do preço:</span>
                <asp:TextBox runat="server" ID="txtTextoPreco"></asp:TextBox>
            </div>--%>
            <h1 class="tituloProduto" id="h1DimensoesMinimas" style="width: 13%;">Ver Dimensões Mínimas   &nbsp&nbsp &nbsp &nbsp&nbsp&nbsp +</h1>
            <div id="divDimensoesMinimas">
                <table class="conteudo-tabela">
                    <tbody>
                        <tr>
                            <th style="background: #336699; color: #fff; font-size: 12px; font-weight: bold; padding-top: 5px; padding-bottom: 5px; padding-left: 10px; }">Pacote e Caixa&nbsp;</th>
                            <th style="background: #336699; color: #fff; font-size: 12px; font-weight: bold; padding-top: 5px; padding-bottom: 5px; padding-left: 10px; }">Mínimo</th>
                            <th style="background: #336699; color: #fff; font-size: 12px; font-weight: bold; padding-top: 5px; padding-bottom: 5px; padding-left: 10px; }">Máximo</th>
                        </tr>
                        <tr>
                            <td><strong>Comprimento (C)&nbsp;</strong></td>
                            <td>16 cm</td>
                            <td>105&nbsp;cm</td>
                        </tr>
                        <tr>
                            <td><strong>Largura (L)</strong></td>
                            <td>11 cm</td>
                            <td>105&nbsp;cm</td>
                        </tr>
                        <tr>
                            <td><strong>Altura (A)</strong></td>
                            <td>2 cm</td>
                            <td>105 cm</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div>
                <span>Altura:</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtAltura"></asp:TextBox>
            </div>
            <div>
                <span>Largura:</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtLargura"></asp:TextBox>
            </div>
            <div>
                <span>Profundidade:</span>
                <asp:TextBox runat="server" CssClass="decimal" ID="txtProfundidade"></asp:TextBox>
            </div>
        </div>

        <h1 class="tituloProduto" id="h1Cores">Cores/Imagens</h1>
        <div id="divCores">
            <uc1:uplLogoProd ID="uplLogoProd1" runat="server" />
        </div>
        <p>
            <asp:Button ID="btnAlterar" CausesValidation="true" runat="server" CssClass="EstiloBotao" Text="Alterar"
                OnClick="btnAlterar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao" OnClick="btnCancelar_Click"
                CausesValidation="False" />
        </p>
    </div>
    <h1 id="tituloBusca" class="TituloSecao">Busca
    </h1>
    <div id="divLista" runat="server" clientidmode="Static">
        <asp:Panel runat="server" DefaultButton="btnPesquisar">
            <ul class="listaCampoBusca">
                <li>
                    <asp:Label ID="Label4" runat="server" CssClass="nomeCampo" Text="ID"></asp:Label><asp:TextBox runat="server" ID="txtBuscaID"></asp:TextBox></li>
                <li>
                    <span class="nomeCampo">Nome</span><asp:TextBox runat="server" ID="txtBuscaNome"></asp:TextBox>

                </li>
                <li>
                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="EstiloBotao" OnClick="btnPesquisar_Clique"
                        CausesValidation="False" />
                </li>
            </ul>
        </asp:Panel>

        <div class="idFiltro clearfix">
            <h2>Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvDados" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="Id" PageSize="1000" CellPadding="4" EmptyDataText="Não existe dados."
                    ForeColor="Black" GridLines="Vertical" OnRowDeleting="gvDados_RowDeleting" AllowSorting="True"
                    OnSorting="gvDados_Sorting" OnRowDataBound="gvDados_RowDataBound" OnPageIndexChanging="gvDados_PageIndexChanging"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Codigo" InsertVisible="False" ReadOnly="True"
                            SortExpression="Id" />
                        <asp:TemplateField HeaderText="Referência">
                            <ItemTemplate>
                                <%# ((Modelos.Produto)Container.DataItem).Referencia %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nome" HeaderText="Nome" InsertVisible="False" ReadOnly="True"
                            SortExpression="Nome" />
                        <asp:TemplateField HeaderText="Tamanhos">
                            <ItemTemplate>
                                <%# getTamanhos(((Modelos.Produto)Container.DataItem)) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cor">
                            <ItemTemplate>
                                <%# getCores(((Modelos.Produto)Container.DataItem)) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hplEditar" runat="server" NavigateUrl='<%# Eval("Id", "Produtos.aspx?Codigo={0}") %>'>
                                    <asp:Image ID="imgEditar" ToolTip="Editar" ImageUrl="~/Controle/comum/img/icoEditar.jpg"
                                        runat="server" />
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Realmente deseja excluir este produto, junto com suas imagens e demais informações relacionadas?')"
                                    CommandName="Delete"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
