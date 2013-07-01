<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GravityWorks.BecomeUser.Settings" %>
<div>
    Role that become user should not allow, meaning if a user is in the role below, they can not become them. Only one role supported
    <asp:TextBox ID="txtRoles" runat="server" />
    <br />
    Session state to clear
    <asp:TextBox ID="txtSessionStateToClear" runat="server" />
</div>