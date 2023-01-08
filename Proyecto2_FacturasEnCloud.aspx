<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Proyecto2_FacturasEnCloud.aspx.cs" Inherits="Proyecto2_FacturasEnCloud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <%-- Web form que muestra por pantalla los datos de un xml que se lee, estos datos 
         se muestran en forma de tabla por medio de un elemento GridView--%>
    <form id="form1" runat="server">
        <div>
            <%-- En este DropDownList se despliegan las opciones de estado de factura,
                 al seleccionar una opción se llama al evento FiltersChanged que realiza 
                 el filtrado de los datos del xml y recarga la página.
                 El valor por defecto no realiza ningún filtrado, e indica 
                 al usuario que seleccione un valor --%>
            <asp:DropDownList ID="desEstadoFactura" runat="server"
                OnSelectedIndexChanged="FiltersChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Text="-- Seleccionar estado de Factura --" 
                    Value="0" />
            </asp:DropDownList>
            <%-- En este DropDownList se despliegan las opciones de productos,
                 al seleccionar una opción se llama al evento FiltersChanged que realiza 
                 el filtrado de los datos del xml y recarga la página.
                 El valor por defecto no realiza ningún filtrado, e indica 
                 al usuario que seleccione un valor --%>
            <asp:DropDownList ID="desProductos" runat="server"
                OnSelectedIndexChanged="FiltersChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Text="-- Seleccionar producto --" 
                    Value="0" />
            </asp:DropDownList>
            <%-- En este DropDownList se despliegan las opciones de monedas,
                 al seleccionar una opción se llama al evento FiltersChanged que realiza 
                 el filtrado de los datos del xml y recarga la página.
                 El valor por defecto no realiza ningún filtrado, e indica 
                 al usuario que seleccione un valor --%>
            <asp:DropDownList ID="desMoneda" runat="server"
                OnSelectedIndexChanged="FiltersChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Text="-- Seleccionar moneda --" 
                    Value="0" />
            </asp:DropDownList><br />
            <%-- El siguiente Label1 tiene la función de enseñarle al usuario
                 un mensaje cuando se realiza con éxito una modificación en la base de datos.
                 Por defecto no se muestra por pantalla y sólo se ve 
                 cuando no hay items en la selección, cambiando su valor 
                 de Visible desde el evento--%>
            <asp:Label ID="LabelRegistroModificado" runat="server" Text="Label" Visible="false">
                <br /><br />Registro modificado con éxito
            </asp:Label>
            <%-- El siguiente Div tiene la función de desplegar varios TextBox,
                 donde el usuario puede escribir datos para modificar registros
                 en la base de datos
                 por defecto no se muestra por pantalla y sólo se ve 
                 cuando el usuario hace click en un registro, 
                 cambiando su valor de Visible desde el evento--%>
            <div ID="DivModificaciones1" runat="server"  visible="false">
                <br />
                FechaCobro: <asp:TextBox ID="TextBoxFechaCobro1" runat="server"></asp:TextBox>
                EstadoFactura: <asp:TextBox ID="TextBoxEstadoFactura1" runat="server"></asp:TextBox>
                TelefonoCliente: <asp:TextBox ID="TextBoxTelefonoCliente1" runat="server"></asp:TextBox>
                addressCliente: <asp:TextBox ID="TextBoxaddressCliente1" runat="server"></asp:TextBox><br />
                CPCliente: <asp:TextBox ID="TextBoxCPCliente1" runat="server"></asp:TextBox>
                PaisCliente: <asp:TextBox ID="TextBoxPaisCliente1" runat="server"></asp:TextBox>
                CiudadCliente: <asp:TextBox ID="TextBoxCiudadCliente1" runat="server"></asp:TextBox>
                correoCliente: <asp:TextBox ID="TextBoxcorreoCliente1" runat="server"></asp:TextBox><br />
                Producto: <asp:TextBox ID="TextBoxProducto1" runat="server"></asp:TextBox>
                ImporteBruto: <asp:TextBox ID="TextBoxImporteBruto1" runat="server"></asp:TextBox>
                Moneda: <asp:TextBox ID="TextBoxMoneda1" runat="server"></asp:TextBox>
                <asp:Button ID="Modificar1" runat="server" Text="Modificar" OnClick="Modificar1_Click" />
                <asp:Button ID="Eliminar1" runat="server" Text="Eliminar" /><br />
            </div>
            <%-- El siguiente Label1 tiene la función de enseñarle al usuario
                 un mensaje cuando la selección realizada no obtiene resultados,
                 por defecto no se muestra por pantalla y sólo se ve 
                 cuando no hay items en la selección, cambiando su valor 
                 de Visible desde el evento--%>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false">
                <br /><br />No extiste ningún elemento que coincida con la búsqueda
            </asp:Label>
            <%-- El siguiente GridView es el item central del web form, es donde se muestran 
                 los datos del xml leído, los muestra en forma de tabla.--%>
            <asp:GridView ID="GridView1" runat="server" 
                AlternatingRowStyle-BackColor="Gainsboro"
                AutoGenerateColumns="true" HorizontalAlign="Center"
                HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"></asp:GridView>
            <%-- El siguiente botón tiene la función de hacer visible el div con los textBoxes
                 para añadir un registro a la tabla.--%>
            <asp:Button ID="AddRegistro1" runat="server" Text="Añadir registro" Visible="true" OnClick="AddRegistro1_Click"/>
            <%-- El siguiente Div tiene la función de desplegar varios TextBox,
                 donde el usuario puede escribir datos para añadir un nuevo registro
                 en la base de datos
                 por defecto no se muestra por pantalla y sólo se ve 
                 cuando el usuario hace click en el botón AddRegistro1, 
                 cambiando su valor de Visible desde el evento--%>
            <div ID="DivAddRegistro1" runat="server"  visible="false">
                <br />
                nFactura: <asp:TextBox ID="TextBoxnFactura2" runat="server"></asp:TextBox>
                nombreMiEmpresa: <asp:TextBox ID="TextBoxnombreMiEmpresa2" runat="server"></asp:TextBox>
                fechaFactura: <asp:TextBox ID="TextBoxfechaFactura2" runat="server"></asp:TextBox>
                CIFCliente: <asp:TextBox ID="TextBoxCIFCliente2" runat="server"></asp:TextBox>
                nombreCliente: <asp:TextBox ID="TextBoxnombreCliente2" runat="server"></asp:TextBox><br />
                FechaCobro: <asp:TextBox ID="TextBoxFechaCobro2" runat="server"></asp:TextBox>
                EstadoFactura: <asp:TextBox ID="TextBoxEstadoFactura2" runat="server"></asp:TextBox>
                TelefonoCliente: <asp:TextBox ID="TextBoxTelefonoCliente2" runat="server"></asp:TextBox>
                addressCliente: <asp:TextBox ID="TextBoxaddressCliente2" runat="server"></asp:TextBox><br />
                CPCliente: <asp:TextBox ID="TextBoxCPCliente2" runat="server"></asp:TextBox>
                PaisCliente: <asp:TextBox ID="TextBoxPaisCliente2" runat="server"></asp:TextBox>
                CiudadCliente: <asp:TextBox ID="TextBoxCiudadCliente2" runat="server"></asp:TextBox>
                correoCliente: <asp:TextBox ID="TextBoxcorreoCliente2" runat="server"></asp:TextBox><br />
                Producto: <asp:TextBox ID="TextBoxProducto2" runat="server"></asp:TextBox>
                ImporteBruto: <asp:TextBox ID="TextBoxImporteBruto2" runat="server"></asp:TextBox>
                Moneda: <asp:TextBox ID="TextBoxMoneda2" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonAddRegistro2" runat="server" Text="Añadir" OnClick="ButtonAddRegistro2_Click" />
                <asp:Button ID="ButtonCancelar2" runat="server" Text="Cancelar" OnClick="ButtonCancelar2_Click" /><br />
            </div>
        </div>
    </form>
</body>
</html>
