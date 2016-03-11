<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserApps.aspx.cs" Inherits="useradmin.UserApps" %>
<%@ Register TagPrefix="userapp" TagName="appline" Src="~/UserAppLine.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="uappgrid" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="userappid" 
        DataSourceID="UAppDS">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:TemplateField HeaderText="User and Application" SortExpression="userappid">
                <ItemTemplate>
                    <userapp:appline ID="appline" runat="server" UserAppId='<%# Eval("userappid") %>'></userapp:appline>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
    <asp:EntityDataSource ID="UAppDS" runat="server" 
        ConnectionString="name=voterwatchEntities" 
        DefaultContainerName="voterwatchEntities" EnableDelete="True" 
        EnableFlattening="False" EntitySetName="user_apps">
    </asp:EntityDataSource>

    <div>
        <table>
            <tr>
                <td>Username:</td>
                <td><asp:DropDownList ID="adduname" runat="server" DataSourceID="UserDS" 
                        DataTextField="emailaddress" DataValueField="userid"></asp:DropDownList>
                    <asp:EntityDataSource ID="UserDS" runat="server" 
                        ConnectionString="name=voterwatchEntities" 
                        DefaultContainerName="voterwatchEntities" EnableFlattening="False" 
                        EntitySetName="users">
                    </asp:EntityDataSource>
                </td>
            </tr>
            <tr>
                <td>Application:</td>
                <td><asp:DropDownList ID="addappname" runat="server" DataSourceID="AppDS" 
                        DataTextField="appname" DataValueField="appid"></asp:DropDownList>
                    <asp:EntityDataSource ID="AppDS" runat="server" 
                        ConnectionString="name=voterwatchEntities" 
                        DefaultContainerName="voterwatchEntities" EnableFlattening="False" 
                        EntitySetName="applications">
                    </asp:EntityDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="addbutton" runat="server" Text="Add Combo" OnClick="CheckAdd" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
