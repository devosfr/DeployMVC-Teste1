<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Cupons.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Import Namespace="Modelos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {
				    var secao = $('#hfSecao').val();
				    if (secao != "") {
				        if (secao == "C") {
				            $('#divDados').show();
				            $('#tituloCadastro').css('color', '#FFF');
				            $('#divLista').hide();

				        }
				        else {
				            $('#divDados').hide();
				            $('#divLista').show();
				            $('#tituloBusca').css('color', '#FFF');
				        }
				    }
				    else if (window.location.href.indexOf("Codigo") != -1) {
				        $('#divDados').show();

				        $('#divLista').hide();
				        $('#tituloBusca').css('display', 'none');

				    }
				    else {
				        $('#divDados').hide();
				        $('#divLista').show();
				        $('#tituloBusca').css('color', '#FFF');
				    }
				    $(".decimal").maskMoney({ showSymbol: false, decimal: ",", thousands: "" });

				    $('#tituloCadastro').click(function () {
				        $('#divLista').stop().slideToggle(function () {
				            if ($('#divLista').is(':visible')) {
				                $('#tituloBusca').css('color', '#FFF');
				                $('#hfSecao').val("");
				            }

				            else
				                $('#tituloBusca').css('color', 'rgb(122, 122, 122)');


				        });
				        $('#divDados').stop().slideToggle(function () {
				            if ($('#divDados').is(':visible')) {
				                $('#tituloCadastro').css('color', '#FFF');
				                $('#hfSecao').val("C");
				            }

				            else
				                $('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
				        });
				    });
				    $('#tituloBusca').click(function () {
				        $('#divLista').stop().slideToggle(function () {
				            if ($('#divLista').is(':visible')) {
				                $('#tituloBusca').css('color', '#FFF');
				                $('#hfSecao').val("");
				            }
				            else {
				                $('#tituloBusca').css('color', 'rgb(122, 122, 122)');
				            }
				        });
				        $('#divDados').stop().slideToggle(function () {
				            if ($('#divDados').is(':visible')) {
				                $('#hfSecao').val("C");
				                $('#tituloCadastro').css('color', '#FFF');
				            }
				            else {
				                $('#tituloCadastro').css('color', 'rgb(122, 122, 122)');
				                $('#hfSecao').val("");
				            }
				        });
				    });

				});


    </script>
    <asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina">
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>

    <h1 id="tituloCadastro" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">

        <ul>
            <li>
                <span class="nomeCampo">Código:</span>
                <asp:TextBox ID="txtCodigo" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>

                <asp:Button ID="btnGerarCodigo" runat="server" Text="Gerar Código do Cupom"
                    OnClick="btnGerarCodigo_Click" CssClass="EstiloBotao" />
            </li>

            <li style="margin-bottom: 12px;">
                <span class="nomeCampo">Ativo:</span>
                <asp:DropDownList ID="ddlAtivo" runat="server" Width="220px">
                    <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </li>
            Tipo de desconto
            <li runat="server" id="liDescontos">
                <li style="margin-left: 62px;">

                    <label for="chkFixo">
                        <input type="radio" id="chkFixo" name="chkDesconto" />
                        Fixo
                    </label>
                    <label for="chkPercentual">
                        <input type="radio" id="chkPercentual" name="chkDesconto" />
                        Percentual
                    </label>

                </li>
                <ul class="clearfix">
                     
                  
                       <li style="margin-left: 46px;" id="descontoRs" >
                        <span class="nomeCampo">Desconto em R$:</span>
                        <asp:TextBox runat="server" ID="txtDesconto" CssClass="decimal"></asp:TextBox>
                    </li>
                    <li style="margin-left: 46px; margin-top: 5px;" id="descontoPercentual">
                        <span class="nomeCampo">Percentual %:</span>
                        <asp:TextBox runat="server" ID="txtDescontoPercentual" CssClass=""></asp:TextBox>
                    </li>

                     <li style="margin-left: 17px; margin-top: 5px;">
                        <span class="nomeCampo">Data Validade:</span>
                        <asp:TextBox runat="server" ID="txtValidade" CssClass="Data"></asp:TextBox>
                    </li>
                  
                </ul>
            </li>
        </ul>
        <div>
            <asp:Button ID="btnAlterar" runat="server" Text="Alterar"
                OnClick="btnAlterar_Click" CssClass="EstiloBotao" />

            <asp:Button ID="btnSalvar" runat="server" Text="Inserir" CssClass="EstiloBotao"
                OnClick="btnSalvar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="EstiloBotao"
                OnClick="btnCancelar_Click" />

        </div>
    </div>
    <h1 id="tituloBusca" class="TituloSecao">Busca
    </h1>
    <div id="divLista" runat="server" clientidmode="Static">
        <ul class="listaCampoBusca">
            <%--            <li>
                <span class="nomeCampo">ID:</span>
                <asp:TextBox ID="txtIDBusca" runat="server" Width="63px"></asp:TextBox></li>--%>
            <li>
                <span class="nomeCampo">Busca:</span>
                <asp:TextBox ID="txtBuscaNome" runat="server" Width="220px"
                    AutoCompleteType="Disabled"></asp:TextBox>

            </li>

        </ul>
        <asp:Button ID="btnPesquisar" CssClass="EstiloBotao" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click"
            CausesValidation="False" />
        <div class="idFiltro clearfix">
            <h2>Resultado da pesquisa</h2>
            <div class="idResultado">
                <asp:GridView ID="gvObjeto" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" DataKeyNames="id" CellPadding="4" PageSize="50"
                    EmptyDataText="Não existe dados." ForeColor="Black" GridLines="Both"
                    AllowSorting="True" OnSorting="gvObjeto_Sorting" OnRowDeleting="gvObjeto_RowDeleting" OnRowEditing="gvObjeto_RowEditing"
                    OnPageIndexChanging="gvObjeto_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True"
                            SortExpression="Id" Visible="True">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Código" SortExpression="Codigo">
                            <ItemTemplate>
                                <%# ((Cupom)Container.DataItem).Codigo%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valido até" SortExpression="Valido até">
                            <ItemTemplate>
                                <%# ((Cupom)Container.DataItem).Validade.ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desconto Fixo" SortExpression="Desconto Fixo">
                            <ItemTemplate>
                                <%# ((Cupom)Container.DataItem).Desconto  %>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Desconto Percentual" SortExpression="Desconto Percentual">
                            <ItemTemplate>
                                <%# ((Cupom)Container.DataItem).DescontoPercentual%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo">
                            <ItemTemplate>
                                <%# ((Cupom)Container.DataItem).Ativo ? "Sim" : "Não"%> 
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ButtonType="Image"
                            EditImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" ShowEditButton="True"
                            UpdateImageUrl="~/Controle/comum/img/BotoesGrid/icoEditar.jpg" HeaderStyle-BorderWidth="0px"
                            ItemStyle-BorderWidth="0px" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCancel" CausesValidation="False" ImageUrl="~/Controle/comum/img/BotoesGrid/icoExcluir.jpg"
                                    OnClientClick="return confirm('Deseja mesmo apagar este cupom?')" CommandName="Delete"></asp:ImageButton>
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
</asp:Content>

