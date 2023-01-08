<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario14-2_ActualizarDatos.aspx.cs" Inherits="Formulario14_2_ActualizarDatos" %>

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
                        <asp:TextBox ID="codEmpleado" runat="server" />
                        <asp:Button ID="botonBuscar" runat="server" Text="Buscar" OnClick="botonBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td>Nombre</td>
                    <td>
                        <asp:TextBox ID="nombreEmpleado" runat="server" /></td>
                </tr>
                <tr>
                    <td>Apellido</td>
                    <td>
                        <asp:TextBox ID="apellidoEmpleado" runat="server" /></td>
                </tr>
                <tr>
                    <td>Puesto</td>
                    <td>
                        <asp:TextBox ID="puestoEmpleado" runat="server" /></td>
                </tr>
                <tr>
                    <td>Fecha alta</td>
                    <td>
                        <asp:TextBox ID="fechaAlta" runat="server" /></td>
                </tr>
                <tr>
                    <td>Salario</td>
                    <td>
                        <asp:TextBox ID="salario" runat="server" /></td>
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
