﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario1.aspx.cs" Inherits="Formulario1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="futbol" />
                <asp:ListItem Value="Baloncesto" />
            </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
    </form>
</body>
</html>
