<%@ Page Title="Role Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="useradmin.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="rgrid" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="roleid" DataSourceID="RoleDS">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="roleid" HeaderText="Role #" ReadOnly="True" 
                SortExpression="roleid">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="rolename" HeaderText="Name" 
                SortExpression="rolename">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="descr" HeaderText="Description" 
                SortExpression="descr">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
            </asp:BoundField>
            <asp:BoundField DataField="seq" HeaderText="Sequence #" SortExpression="seq">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            No Roles in system.&nbsp; Maybe create a few?
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:EntityDataSource ID="RoleDS" runat="server" 
        ConnectionString="name=voterwatchEntities" 
        DefaultContainerName="voterwatchEntities" EnableDelete="True" 
        EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
        EntitySetName="roles">
    </asp:EntityDataSource>
        <div>
        <table>
            <tr>
                <th colspan="2">Add Role</th>
            </tr>
            <tr>
                <td>Role Name:</td>
                <td><asp:TextBox ID="nrolename" runat="server" Columns="40" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="adderror" runat="server" ForeColor="Red" Visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <p>Description:</p>
                    <asp:TextBox ID="ndescr" runat="server" TextMode="MultiLine" Rows="10" Columns="40" />
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="cnxbutton" runat="server" OnClick="CancelAdd" Text="Cancel" /></td>
                <td><asp:Button ID="addbutton" runat="server" OnClick="AddNewRole" Text="Add Role" /></td>
                
            </tr>
        </table>
    </div>
</asp:Content>
