<%@ Page Title="About The User" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="precinctcaptain.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About me.
    </h2>
    <p>
        <asp:Table ID="udatatable" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">First Name:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="fname" runat="server" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Last Name:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="lname" runat="server" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Login:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="loginname" runat="server" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" ColumnSpan="2">
                    <p>User Description: <asp:Label ID="description" runat="server" /></p>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </p>
    <p>
        Applications and Roles
    </p>
    <asp:Table ID="approletable" runat="server">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server">Application</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Role</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        
    </asp:Table>
</asp:Content>
