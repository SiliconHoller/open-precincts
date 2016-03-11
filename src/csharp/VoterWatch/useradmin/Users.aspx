<%@ Page Title="User Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="useradmin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:GridView ID="ugrid" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="userid" DataSourceID="UserDS" PageSize="100">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="userid" HeaderText="User ID" ReadOnly="True" 
                SortExpression="userid" />
            <asp:BoundField DataField="firstname" HeaderText="First Name" 
                SortExpression="firstname" />
            <asp:BoundField DataField="lastname" HeaderText="Last Name" 
                SortExpression="lastname" />
            <asp:BoundField DataField="emailaddress" HeaderText="Login" 
                SortExpression="emailaddress" />
            <asp:BoundField DataField="descr" HeaderText="Description" 
                SortExpression="descr" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="rbutton" runat="server" CommandArgument='<% Eval("userid") %>' OnClick="Send_Password_Reset" Text="Reset Password" ToolTip="Reset the user's password and send a link."/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    
        <EmptyDataTemplate>
            No users founds
        </EmptyDataTemplate>
    
    </asp:GridView>
    <asp:EntityDataSource ID="UserDS" runat="server" 
        ConnectionString="name=voterwatchEntities" 
        DefaultContainerName="voterwatchEntities" EnableDelete="True" 
        EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
        EntitySetName="users" >
    </asp:EntityDataSource>

    <div>
        <table>
            <tr>
                <th colspan="2">Add User</th>
            </tr>
            <tr>
                <td>Email/Login:</td>
                <td><asp:TextBox ID="nusername" runat="server" Columns="40" /></td>
            </tr>
            <tr>
                <td>First Name:</td>
                <td><asp:TextBox ID="nfirst" runat="server" Columns="40" /></td>
            </tr>
            <tr>
                <td>Last Name:</td>
                <td><asp:TextBox ID="nlast" runat="server" Columns="40" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <p>Description:</p>
                    <asp:TextBox ID="ndescr" runat="server" TextMode="MultiLine" Rows="10" Columns="40" />
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="cnxbutton" runat="server" OnClick="CancelAdd" Text="Cancel" /></td>
                <td><asp:Button ID="addbutton" runat="server" OnClick="AddNewUser" Text="Add User" /></td>
                
            </tr>
        </table>
    </div>
</asp:Content>
