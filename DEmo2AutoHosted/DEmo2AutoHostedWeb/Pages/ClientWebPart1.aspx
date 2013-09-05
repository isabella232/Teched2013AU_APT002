<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientWebPart1.aspx.cs" Inherits="DEmo2AutoHostedWeb.Pages.ClientWebPart1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        // Set the style of the client web part page to be consistent with the host web.
        function setStyleSheet() {
            var hostUrl = ""
            if (document.URL.indexOf("?") != -1) {
                var params = document.URL.split("?")[1].split("&");
                for (var i = 0; i < params.length; i++) {
                    p = decodeURIComponent(params[i]);
                    if (/^SPHostUrl=/i.test(p)) {
                        hostUrl = p.split("=")[1];
                        document.write("<link rel=\"stylesheet\" href=\"" + hostUrl + "/_layouts/15/defaultcss.ashx\" />");
                        break;
                    }
                }
            }
            if (hostUrl == "") {
                document.write("<link rel=\"stylesheet\" href=\"/_layouts/15/1033/styles/themable/corev15.css\" />");
            }
        }
        setStyleSheet();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        
                        <table border="1" cellpadding="10">

         <tr><td>
        <h2>SharePoint Site</h2>
        <asp:Label runat="server" ID="WebTitleLabel"/>
        <h2>AppWebSite</h2>
        <asp:Label runat="server" ID="AppWebLabel"/>
        <h2>Current User:</h2>
        <asp:Label runat="server" ID="CurrentUserLabel" />
        <h2>Site Users</h2>
        <asp:ListView ID="UserList" runat="server">     
            <ItemTemplate ><asp:Label ID="UserItem" runat="server" Text="<%# Container.DataItem.ToString()  %>"></asp:Label><br /></ItemTemplate>
        </asp:ListView>
        <h2>Site Lists</h2>
        <asp:ListView ID="ListList" runat="server">
            <ItemTemplate ><asp:Label ID="ListItem" runat="server" Text="<%# Container.DataItem.ToString()  %>"></asp:Label><br /></ItemTemplate>
        </asp:ListView>
        </td>
              
        </tr>
        </table>


    </div>
    </form>
</body>
</html>