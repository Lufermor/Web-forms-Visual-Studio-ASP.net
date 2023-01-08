<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario3.aspx.cs" Inherits="Formulario3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:AccessDataSource ID="selAlumnos" runat="server"
                 SelectCommand="select * from alunos"
                 DataSourceMode="DataReader" DataFile="~/App_Data/escuela_empresa.mdb" />
            <asp:GridView ID="GridView1" runat="server" DataSourceID="selAlumnos"
                 AlternatingRowStyle-BackColor="Gainsboro"
                 AutoGenerateColumns="false" HorizontalAlign="Center">
                    <Columns>
                        <asp:BoundField DataField="nome" HeaderText="Nombre" />
                        <asp:ImageField DataImageUrlField="foto" DataImageUrlFormatString="fotos/{0}"
                             HeaderText="Foto" />
                    </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
