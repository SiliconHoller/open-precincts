<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDistricts.aspx.cs" Inherits="useradmin.UserDistricts1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        User:&nbsp;&nbsp;<asp:DropDownList ID="userselect" runat="server" ClientIDMode="Static" 
            AutoPostBack="true" OnSelectedIndexChanged="ShowUserDistricts"/>        
    </p>

    <p>
        Add District:&nbsp;&nbsp;<asp:DropDownList ID="districtselect" runat="server" 
            ClientIDMode="Static" />
        &nbsp;&nbsp;
        <asp:Button ID="addbutton" runat="server" Text="Add District" OnClick="AddUserDistrict" />
    </p>

    <asp:GridView ID="udgrid" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="districtid" >
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                         CommandArgument='<%# Eval("districtid") %>' Text="Delete" OnClick="DeleteUserDistrict"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="typename">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("typename") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unique ID" SortExpression="identifier">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("identifier") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="name">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="descr" ControlStyle-Width="300px">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("descr") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <p>No Districts assigned to this user.</p>
        </EmptyDataTemplate>    
    </asp:GridView>



</asp:Content>
