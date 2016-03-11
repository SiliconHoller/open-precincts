<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VoterDetail.aspx.cs" Inherits="precinctcaptain.myvoters.VoterDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .gridrow 
        {
            background-color:#EEEEFF;
            color:#000000;
        }

        .altrow
        {
            background-color: #99CCFF;
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal ID="namelit" runat="server" />
    </h2>
    <asp:Table runat="server" >
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="2">Voter Information</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>State Voter ID</asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="stateid" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>County Voter ID</asp:TableCell><asp:TableCell>
                <asp:Label ID="countyid" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>Year of Birth</asp:TableCell><asp:TableCell>
                <asp:Label ID="yob" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>Registration Date</asp:TableCell><asp:TableCell>
                <asp:Label ID="regdate" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>Party Affiliation</asp:TableCell><asp:TableCell>
                <asp:Label ID="party" runat="server" /></asp:TableCell></asp:TableRow></asp:Table><p>Address Information:</p><asp:GridView ID="addgrid" runat="server" AutoGenerateColumns="False">
            <RowStyle CssClass="gridrow" />
            <AlternatingRowStyle CssClass="altrow" />
            <Columns>
                <asp:TemplateField HeaderText="Address Record">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("address_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Addr. Line 1">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("address1") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Addr. Line 2">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("address2") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("city") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("state") %>'/>
                    </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Zip">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("zip") %>'/>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Zip+4">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("zip_plusfour") %>'/>
                    </ItemTemplate>
               </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p>No address data found.</p></EmptyDataTemplate></asp:GridView><p>Registered Voting Distict</p><asp:GridView ID="distgrid" runat="server" AutoGenerateColumns="false">
            <RowStyle CssClass="gridrow" />
            <AlternatingRowStyle CssClass="altrow" />
                        <Columns>
                <asp:TemplateField HeaderText="District Record">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("districtid") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="District Type">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("typename") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Identifier">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("identifier") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("name") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("descr") %>'/>
                    </ItemTemplate>
               </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p>No district data found.</p></EmptyDataTemplate></asp:GridView></asp:Content>