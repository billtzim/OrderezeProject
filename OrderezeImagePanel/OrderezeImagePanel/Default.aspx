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
        <asp:Button ID="buttonUpload" runat="server" Text="Add Image" OnClick="uploadBlobImage" />
        <br />
        <asp:GridView ID="fileView" AutoGenerateColumns="False" DataKeyNames="id" runat="server"
            OnRowCommand="RowCommandHandler">
            <Columns>
                <asp:ButtonField Text="Delete" CommandName="DeleteItem" />
                <asp:HyperLinkField HeaderText="Link" DataTextField="FileName" DataNavigateUrlFields="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
            </Columns>
        </asp:GridView>
    <br />
    </div>
    </form>
</body>
</html>
