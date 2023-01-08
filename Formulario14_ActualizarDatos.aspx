<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario14_ActualizarDatos.aspx.cs" Inherits="Formulario14_ActualizarDatos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1">
                <tr>
                    <td>Código</td>
                    <td>
                        <asp:TextBox ID="codPersona" runat="server" />
                        <asp:Button ID="botonBuscar" runat="server" Text="Buscar" OnClick="botonBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td>D.N.I.</td>
                    <td>
                        <asp:TextBox ID="dniPersona" runat="server" /></td>
                </tr>
                <tr>
                    <td>Nombre</td>
                    <td>
                        <asp:TextBox ID="nomPersona" runat="server" /></td>
                </tr>
                <tr>
                    <td>Fecha Nac</td>
                    <td>
                        <asp:TextBox ID="fnPersona" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="BotonActualizar" runat="server" Text="Actualizar" OnClick="BotonActualizar_Click" />
                        <asp:Label ID="Mensaje" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
