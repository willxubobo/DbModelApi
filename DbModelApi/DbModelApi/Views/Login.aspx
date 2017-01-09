<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DbModelApi.Views.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    用户名：<input type="text" id="txtuname"/><br/>
        密码：<asp:TextBox ID="txtpwd" runat="server" TextMode="Password"></asp:TextBox><br />
        <input type="button" id="btnlogin" value="登录" onclick="login()"/>
    </div>
    </form>
</body>
</html>
