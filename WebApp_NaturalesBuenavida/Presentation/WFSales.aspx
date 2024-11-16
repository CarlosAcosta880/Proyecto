<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSales.aspx.cs" Inherits="Presentation.WFSales" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Ventas</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Gestión de Ventas</h2>
            <asp:Label ID="LblMsg" runat="server" ForeColor="Red" />

            <!-- Formulario para agregar una nueva venta -->
            <fieldset>
                <legend>Agregar Nueva Venta</legend>
                <asp:Label Text="Descripción:" runat="server" />
                <asp:TextBox ID="TxtDescripcion" runat="server" /><br />

                <asp:Label Text="Cliente:" runat="server" />
                <asp:TextBox ID="TxtClienteId" runat="server" /><br />

                <asp:Label Text="Empleado:" runat="server" />
                <asp:TextBox ID="TxtEmpleadoId" runat="server" /><br />

                <asp:Label Text="Fecha:" runat="server" />
                <asp:TextBox ID="TxtFecha" runat="server" Text='<%# DateTime.Now.ToString("yyyy-MM-dd") %>' /><br />

                <asp:Label Text="Total:" runat="server" />
                <asp:TextBox ID="TxtTotal" runat="server" /><br />

                <asp:Button ID="BtnAddSale" runat="server" Text="Agregar Venta" OnClick="BtnAddSale_Click" />
            </fieldset>

            <!-- GridView para mostrar las ventas existentes -->
            <asp:GridView ID="GVSales" runat="server" AutoGenerateColumns="False"
                OnRowCommand="GVSales_RowCommand" OnRowDataBound="GVSales_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Referencia">
                        <ItemTemplate>
                            <asp:Label ID="LblReferencia" runat="server" Text='<%# Eval("Referencia") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <asp:Label ID="LblFecha" runat="server" Text='<%# Eval("Fecha") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="LblTotal" runat="server" Text='<%# Eval("Total") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CommandName="Update" 
                                CommandArgument='<%# Eval("Referencia") %>' OnClientClick="return confirm('¿Está seguro de que desea actualizar esta venta?');" />
                            &nbsp;&nbsp;
                            <asp:Button ID="BtnDelete" runat="server" Text="Eliminar" CommandName="Delete" 
                                CommandArgument='<%# Eval("Referencia") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar esta venta?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
