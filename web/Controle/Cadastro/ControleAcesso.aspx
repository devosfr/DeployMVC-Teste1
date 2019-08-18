<%@ Page Language="C#" MasterPageFile="~/Controle/GerenciadorNovo.master" AutoEventWireup="true" CodeFile="ControleAcesso.aspx.cs" Inherits="Controle_Cadastro_Usuario" Title="Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="Server">
    <script type="text/javascript">
        var TREEVIEW_ID = "<%=tvPermissoes.ClientID%>"; //the ID of the TreeView control
        //the constants used by GetNodeIndex()
        var LINK = 0;
        var CHECKBOX = 1;

        //this function is executed whenever user clicks on the node text
        function ToggleCheckBox(senderId)
        {
            var nodeIndex = GetNodeIndex(senderId, LINK);
            var checkBoxId = TREEVIEW_ID + "n" + nodeIndex + "CheckBox";
            var checkBox = document.getElementById(checkBoxId);
            checkBox.checked = !checkBox.checked;

            ToggleChildCheckBoxes(checkBox);
            //ToggleParentCheckBox(checkBox);
        }

        //checkbox click event handler
        function checkBox_Click(eventElement) {
            ToggleChildCheckBoxes(eventElement.target);
            //ToggleParentCheckBox(eventElement.target);
        }

        //returns the index of the clicked link or the checkbox
        function GetNodeIndex(elementId, elementType) {
            var nodeIndex;
            if (elementType == LINK) {
                nodeIndex = elementId.substring((TREEVIEW_ID + "t").length);
            }
            else if (elementType == CHECKBOX) {
                nodeIndex = elementId.substring((TREEVIEW_ID + "n").length, elementId.indexOf("CheckBox"));
            }
            return nodeIndex;
        }

        //checks or unchecks the nested checkboxes
        function ToggleChildCheckBoxes(checkBox) {
            var postfix = "n";
            var childContainerId = TREEVIEW_ID + postfix + GetNodeIndex(checkBox.id, CHECKBOX) + "Nodes";
            var childContainer = document.getElementById(childContainerId);
            if (childContainer) {
                var childCheckBoxes = childContainer.getElementsByTagName("input");
                for (var i = 0; i < childCheckBoxes.length; i++) {
                    childCheckBoxes[i].checked = checkBox.checked;
                }
            }
        }

        //unchecks the parent checkboxes if the current one is unchecked
        function ToggleParentCheckBox(checkBox) {
            if (checkBox.checked == false) {
                var parentContainer = GetParentNodeById(checkBox, TREEVIEW_ID);
                if (parentContainer) {
                    var parentCheckBoxId = parentContainer.id.substring(0, parentContainer.id.search("Nodes")) + "CheckBox";
                    if ($get(parentCheckBoxId) && $get(parentCheckBoxId).type == "checkbox") {
                        $get(parentCheckBoxId).checked = false;
                        ToggleParentCheckBox($get(parentCheckBoxId));
                    }
                }
            }
        }

        //returns the ID of the parent container if the current checkbox is unchecked
        function GetParentNodeById(element, id) {
            var parent = element.parentNode;
            if (parent == null) {
                return false;
            }
            if (parent.id.search(id) == -1) {
                return GetParentNodeById(parent, id);
            }
            else {
                return parent;
            }
        }
    </script>
    <asp:Literal runat="server" ID="litErro"></asp:Literal>
    <ul>
        <li>
            <label>Usuários</label>
            <asp:DropDownList runat="server" ID="ddlUsuario"
                OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <asp:TreeView ID="tvPermissoes" CollapseImageUrl="~/Controle/comum/img/ic_menos.gif" ExpandImageUrl="~/Controle/comum/img/ic_mais.gif" runat="server" ShowCheckBoxes="All">
            </asp:TreeView>

        </li>
        <li>
            <asp:Button runat="server" ID="btnAtualizar" Text="Atualizar"
                OnClick="btnAtualizar_Click" />
        </li>
        <li>
            <asp:Label runat="server" ID="lblMensagem"></asp:Label>
        </li>
    </ul>
    <style type="text/css">
        .style1 {
            color: #000000;
            font-size: 18px;
        }

        .style2 {
            font-family: Arial;
            font-size: 12px;
        }
    </style>

    <script type="text/javascript">
        var links = document.getElementsByTagName("a");
        for (var i = 0; i < links.length; i++) {
            if (links[i].className == TREEVIEW_ID + "_0") {
                links[i].href = "javascript:ToggleCheckBox(\"" + links[i].id + "\");";
            }
        }

        var checkBoxes = document.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox") {
                $addHandler(checkBoxes[i], "click", checkBox_Click);
            }
        }
    </script>
</asp:Content>

