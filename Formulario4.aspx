<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario4.aspx.cs" Inherits="Formulario4" %>

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
            <asp:DropDownList ID="desAlumnos" runat="server"
                 DataSourceID="selAlumnos"
                 DataValueField="cod_aluno" DataTextField="nome"/>
        </div>
    </form>
</body>
</html>
