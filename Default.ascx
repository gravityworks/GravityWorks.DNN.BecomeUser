<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="GravityWorks.BecomeUser.Default" %>
<div>
    <div id="divStatus">
        <asp:Literal ID="litStatus" runat="server" />
    </div>
    
    <div id="CurrentUser">
        <div class="greybox"><span><strong>Current Username:</strong> </span> <asp:Label id="lblCurrentUserName" runat="server" /><br/>
            <span><strong>New Username:</strong></span> <asp:TextBox ID="txtUserName" runat="server" /> 
            <asp:Button ID="btnBecomeUser" Text="Become User" runat="server" onclick="btnBecomeUser_Click" />
        </div>
    </div>
<hr/>
<script type="text/javascript">
    jQuery(document).ready(function() {
        FadeOutStatusMessage();
    });

        function FadeOutStatusMessage() {
            jQuery("#divStatus").fadeOut(10000);
        }
</script>