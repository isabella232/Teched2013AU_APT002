<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DEmo2AutoHostedWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

         <script 
        src="//ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js" 
        type="text/javascript">
    </script>
    <script 
        type="text/javascript" 
        src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js">
    </script>      
    <script 
        type="text/javascript"
        src="ChromeLoader.js">
    </script>


</head>
<body>
    <form id="form1" runat="server">

                    <!-- Chrome control placeholder -->
    <div id="chrome_ctrl_placeholder"></div>

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
