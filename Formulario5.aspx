<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario5.aspx.cs" Inherits="Formulario5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:AccessDataSource ID="selCiudades" runat="server"
                 SelectCommand="select * from locals"
                 DataFile="~/App_Data/escuela_empresa.mdb" />
            <asp:AccessDataSource ID="selAlumnos" runat="server"
                 SelectCommand="select * from alunos"
                 FilterExpression ="cod_cidade='{0}'"
                 DataFile="~/App_Data/escuela_empresa.mdb">
                    <FilterParameters>
                        <asp:ControlParameter ControlID="desCiudades" PropertyName="SelectedValue" />
                    </FilterParameters>
            </asp:AccessDataSource>
            <asp:DropDownList ID="desCiudades" runat="server"
                 DataSourceID="selCiudades"
                 DataValueField="cod_local" DataTextField="nom_local"
                 AutoPostBack="true"/>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="selAlumnos"
                 AlternatingRowStyle-BackColor="Gainsboro"
                 AutoGenerateColumns="false" HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="nome" HeaderText="Nombre" />
                    <asp:ImageField DataImageUrlField="foto" DataAlternateTextFormatString="fotos/{0}"
                         ControlStyle-Height="100" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
