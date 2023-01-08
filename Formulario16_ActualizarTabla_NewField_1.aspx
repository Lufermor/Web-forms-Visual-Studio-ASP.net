<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Formulario16_ActualizarTabla_NewField_1.aspx.cs" Inherits="Formulario16_ActualizarTabla_NewField_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="ID" DataSourceID="SqlDataSource1" GridLines="Horizontal" ShowFooter="True">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="MenInsertar" runat="server">Insertar</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DNI" SortExpression="DNI">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DNI") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("DNI") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TxtDni" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="sexo" SortExpression="sexo">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server">  <%--SelectedValue='<%# Bind("sexo") %>'--%>
                                <asp:ListItem>Seleccionar sexo</asp:ListItem>
                                <asp:ListItem>Hombre</asp:ListItem>
                                <asp:ListItem>Mujer</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("sexo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSexo" runat="server">
                                <asp:ListItem>Seleccionar sexo</asp:ListItem>
                                <asp:ListItem>Hombre</asp:ListItem>
                                <asp:ListItem>Mujer</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CONEXION02 %>" DeleteCommand="DELETE FROM [Personas] WHERE [ID] = @ID" InsertCommand="INSERT INTO [Personas] ([DNI], [Nombre], [sexo]) VALUES (@DNI, @Nombre, @sexo)" SelectCommand="SELECT [ID], [DNI], [Nombre], [sexo] FROM [Personas]" UpdateCommand="UPDATE [Personas] SET [DNI] = @DNI, [Nombre] = @Nombre, [sexo] = @sexo WHERE [ID] = @ID">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="DNI" Type="String" />
                    <asp:Parameter Name="Nombre" Type="String" />
                    <asp:Parameter Name="sexo" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="DNI" Type="String" />
                    <asp:Parameter Name="Nombre" Type="String" />
                    <asp:Parameter Name="sexo" Type="String" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </div>
    </form>
</body>
</html>
