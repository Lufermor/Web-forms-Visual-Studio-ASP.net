<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Proyecto1_Facturas.aspx.cs" Inherits="Proyecto1_Facturas" %>

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
            </asp:DropDownList>
            <%-- El siguiente Label1 tiene la función de enseñarle al usuario
                 un mensaje cuando la selección realizada no obtiene resultados,
                 por defecto no se muestra por pantalla y sólo se ve 
                 cuando no hay items en la selección, cambiando su valor 
                 de Visible desde el evento--%>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false">
                <br /><br />No extiste ningún elemento que coincida con la búsqueda
            </asp:Label>
            <%-- El siguiente botón llama a un evento que se encarga de exportar a xls
                 los datos del GridView que aparece en ese momento por pantalla--%>
            <asp:Button ID="Button1" runat="server" Text="Exportar a XLS" OnClick="ExportXML" />
            <%-- El siguiente GridView es el item central del web form, es donde se muestran 
                 los datos del xml leído, los muestra en forma de tabla.--%>
            <asp:GridView ID="GridView1" runat="server" 
                AlternatingRowStyle-BackColor="Gainsboro"
                AutoGenerateColumns="true" HorizontalAlign="Center"
                HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"></asp:GridView>
        </div>
    </form>
</body>
</html>
