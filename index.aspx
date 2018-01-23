<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MyChatApp.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign up</title>
    <link rel="stylesheet" href="css/indexaspx.css" />
    
</head>
<body>
     <center><img src="media/logo.jpg" alt="logo" style="height: 150px; width: 150px;"></center>
     <center><h2>Let's get some information before we chat</h2></center>
    <center>
        <form id="form1" runat="server">
            <div id="login">
                <p>Name:</p>
                <asp:TextBox ID="tbxName" runat="server" Width="240px" ></asp:TextBox><br /><br />
                <p>Email:</p>
                <asp:TextBox ID="TbxEmail" runat="server" Width="240px"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" Width="70px" />  <br /><br />
                <asp:Label ID="lblSaved" runat="server" Text="" ForeColor="Green" Font-Size="Larger"></asp:Label>
            </div>
        </form>
    </center>
  
</body>
</html>
