<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" Inherits="useradmin.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="appgrid" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="appid" 
        DataSourceID="AppDS">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="appid" HeaderText="App #" ReadOnly="True" 
                SortExpression="appid" />
            <asp:TemplateField HeaderText="App Name" SortExpression="appname">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("appname") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("appname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Display Name" SortExpression="displayname">
                <EditItemTemplate>
                    <asp:TextBox ID="dispnamebox" runat="server" Text='<%# Bind("displayname") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="displabel" runat="server" Text='<%# Bind("displayname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sequence" SortExpression="seq">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("seq") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("seq") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="descr">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("descr") %>' TextMode="MultiLine" Rows="10" Columns="25" Width="250px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("descr") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:EntityDataSource ID="AppDS" runat="server" 
        ConnectionString="name=voterwatchEntities" 
        DefaultContainerName="voterwatchEntities" EnableDelete="True" 
        EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
        EntitySetName="applications">
    </asp:EntityDataSource>
    <div>
        <table>
            <tr>
                <th colspan="2">Add Application</th>
            </tr>
            <tr>
                <td>Application Name:</td>
                <td><asp:TextBox ID="nappname" runat="server" Columns="40" /></td>
            </tr>
            <tr>
                <td>Display Name:</td>
                <td><asp:TextBox ID="ndisplay" runat="server" Columns="40" /></td>
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
                <td><asp:Button ID="addbutton" runat="server" OnClick="AddNewApp" Text="Add Application" /></td>
                
            </tr>
        </table>
    </div>

</asp:Content>
