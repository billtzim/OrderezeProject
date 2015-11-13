<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OrderezeImagePanel.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="labelName" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="textboxName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="labelDescription" runat="server" Text="Description"></asp:Label>
        <asp:TextBox ID="textboxDescription" runat="server"></asp:TextBox>
        <br />

        <asp:FileUpload ID="FileUploadControl" runat="server" />
        <br />
        <asp:Button ID="buttonUpload" runat="server" Text="Add Image" OnClick="uploadImage" />
        <br />
        <asp:GridView ID="fileView" AutoGenerateColumns="False" DataKeyNames="id" runat="server">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="description" HeaderText="Description" />
                <asp:HyperLinkField HeaderText="Link" DataTextField="Name" DataNavigateUrlFields="imagepath" />
                <asp:ImageField DataAlternateTextField="description" DataImageUrlField="imagepath" HeaderText="image preview">
                    <ItemStyle Height="50px" Width="50px"></ItemStyle>
                </asp:ImageField>
                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text='<%# Eval("id") %>' CommandName="DeleteImageById" CausesValidation="false" ID="LinkButton1" OnClick="DeleteButton_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br />
    </div>
    </form>
</body>
</html>
