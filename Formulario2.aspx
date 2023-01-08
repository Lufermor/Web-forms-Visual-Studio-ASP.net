<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario2.aspx.cs" Inherits="Formulario2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="deporte" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected="True" Text="-- Seleccionar categoría --" 
                    Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="Pulsar" />
        </div>
    </form>
</body>
</html>
