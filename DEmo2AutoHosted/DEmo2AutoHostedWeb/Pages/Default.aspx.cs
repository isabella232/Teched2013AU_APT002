using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Client;

namespace DEmo2AutoHostedWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // The following code gets the client context and Title property by using TokenHelper.
            // To access other properties, you may need to request permissions on the host web.

            var contextToken = TokenHelper.GetContextTokenFromRequest(Page.Request);
            var hostWeb = Page.Request["SPHostUrl"];

            using (var clientContext = TokenHelper.GetClientContextWithContextToken(hostWeb, contextToken, Request.Url.Authority))
            {
                //Load the properties for the web object.
                Web web = clientContext.Web;
                clientContext.Load(web);
                clientContext.ExecuteQuery();

                //Get the site name.
                var siteName = web.Title;

                //Get the current user.
                clientContext.Load(web.CurrentUser);
                clientContext.ExecuteQuery();
                var currentUser = clientContext.Web.CurrentUser.LoginName;

                //Load the lists from the Web object.
                ListCollection lists = web.Lists;
                clientContext.Load<ListCollection>(lists);
                clientContext.ExecuteQuery();

                //Load the current users from the Web object.
                UserCollection users = web.SiteUsers;
                clientContext.Load<UserCollection>(users);
                clientContext.ExecuteQuery();

                List<string> listOfUsers = new List<string>();
                List<string> listOfLists = new List<string>();


                foreach (User siteUser in users)
                {
                    listOfUsers.Add(siteUser.LoginName);
                }


                foreach (List list in lists)
                {
                    listOfLists.Add(list.Title);
                }

                WebTitleLabel.Text = siteName + " " + web.Url;

                AppWebLabel.Text = Page.Request["SPAppWebUrl"];

                CurrentUserLabel.Text = currentUser;
                UserList.DataSource = listOfUsers;
                UserList.DataBind();
                ListList.DataSource = listOfLists;
                ListList.DataBind();




            }
        }
    }
}