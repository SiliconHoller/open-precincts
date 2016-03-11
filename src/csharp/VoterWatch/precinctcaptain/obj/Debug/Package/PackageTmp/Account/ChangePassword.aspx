<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="precinctcaptain.Account.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="userid" runat="server" />
    <asp:HiddenField ID="resetval" runat="server" />
    <div  ID="resettable" runat="server">
        <h2>
            Change Password
        </h2>
        <p>
            Use the form below to change your password.  New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
        </p>
        <div style="display:table">
            <div style="display:table-row">
                <div style="display:table-cell">
                    UserName:
                </div>
                <div style="display:table-cell">
                    <asp:Label ID="usernamelabel" runat="server" />
                </div>
            </div>
            <div style="display:table-row">
                <div style="display:table-cell">
                    New Password:
                </div>
                <div style="display:table-cell">
                    <asp:TextBox ID="npassword1" runat="server" Columns="30" TextMode="Password"/><br />
                </div>
            </div>
            <div style="display:table-row">
                <div style="display:table-cell">
                    Re-enter to confirm:&nbsp;&nbsp;
                </div>
                <div style="display:table-cell">
                    <asp:TextBox ID="npassword2" runat="server" Columns="30" TextMode="Password"/>
                </div>
            </div>
            <asp:CompareValidator ID="passwordcomparevalidator" runat="server" 
                ErrorMessage="The passwords are not the same." ControlToValidate="npassword1" ControlToCompare="npassword2"></asp:CompareValidator>
            <div style="display:table-row">
                <div style="display:table-cell">
                    <asp:Button ID="resetbutton" runat="server" Text="Reset Password" OnClick="Check_And_Reset"/><br />
                </div>
                <div style="display:table-cell">
                    <asp:Button ID="cnxbutton" runat="server" Text="Cancel" OnClick="CancelReset"/>
                </div>
            </div>
        </div>
    </div>
    <div id="errordisplay" runat="server">
        <asp:Label ID="errorval" runat="server" />
    </div>
</asp:Content>
