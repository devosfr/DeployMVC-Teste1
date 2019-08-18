<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="Importacao.aspx.cs" Inherits="Controle_Cadastro_Estado" Title="Acabamentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        $(document).ready(
				function () {
				    $('#divDados').show();
				    $('#tituloCadastro').css('color', '#FFF');
				});
    </script>

    <asp:HiddenField ID="hfSecao" ClientIDMode="Static" runat="server" />
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <h1 class="TituloPagina">
        <asp:Literal runat="server" ID="litTitulo"></asp:Literal>
    </h1>

    <h1 id="tituloCadastro" style="color: #FFF;" class="TituloSecao">Cadastro
    </h1>
    <div id="divDados" runat="server" clientidmode="Static">
        <div>

            <ul>
                <li>
                    <div>
                        <span>Campo</span>
                        <asp:TextBox runat="server" ID="txtNomeCampo"></asp:TextBox><br />
                        <span>Novo Nome</span>
                        <asp:TextBox runat="server" ID="txtNomeNovo"></asp:TextBox><br />
                        <asp:Button runat="server" ID="btnAdicionarCampo" OnClick="btnAdicionarCampo_Click" CssClass="EstiloBotao" Text="Adicionar" />
                    </div>
                    <asp:GridView ID="gvCampos" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                        DataKeyNames="Id" CellPadding="4" EmptyDataText="Não existe dados."
                        ForeColor="Black" GridLines="Vertical" OnRowDeleting="gvInformacoes_RowDeleting"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <FooterStyle BackColor="#CCCC99" />
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Codigo" InsertVisible="False"
                                SortExpression="Id" />
                            <asp:BoundField DataField="NomeCampo" HeaderText="Nome Campo"
                                SortExpression="Nome" />
                            <asp:BoundField DataField="NovoNome" HeaderText="Novo Nome"
                                SortExpression="Texto" />
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
                </li>
                <li>
                    <asp:Button runat="server" ID="btnImportar" Text="Importar" CssClass="EstiloBotao" OnClick="btnImportar_Click1" />
                </li>
                                <li>

                                    <span>Atacado?</span>

                                    <asp:CheckBox runat="server" ID="cbAtacado" />
                </li>
                <li>
                    <asp:Button runat="server" ID="btnInserirImagens" Text="Inserir Imagens nos Produtos" OnClick="btnInserirImagens_Click" CssClass="EstiloBotao" />
                </li>

                <li style="width: 600px; position: relative;">
                    <asp:GridView ID="gvCSV" runat="server" CellPadding="3" GridLines="Vertical"
                        Width="700px" Style="font-family: 'Arial'; font-size: 12px"
                        BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                        BorderWidth="0px" HeaderStyle-BorderWidth="0px" RowStyle-BorderWidth="0px" EditRowStyle-BorderWidth="0px"
                        EnableModelValidation="True">
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#F8F8FB" />
                    </asp:GridView>
                </li>
                <li>
                    <asp:FileUpload runat="server" ID="fulSiteMap" />
                </li>
                <li>
                    <asp:Button runat="server" ID="btnCarregar" Text="Clique para carregar" CssClass="EstiloBotao" OnClick="btnCarregar_Click" />
                </li>
                <li>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="btnApagarCarga" Text="Apagar Dados Importados" OnClick="btnApagarCarga_Click"  CssClass="EstiloBotao" />
                    <
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

