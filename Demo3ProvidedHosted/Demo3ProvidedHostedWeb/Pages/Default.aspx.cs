using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo3ProvidedHostedWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                // The following code gets the client context and Title property by using TokenHelper.
                // To access other properties, you may need to request permissions on the host web.

                var contextToken = (string) this.Session["Token"] ??
                                   TokenHelper.GetContextTokenFromRequest(Page.Request);
                var hostWeb = Page.Request["SPHostUrl"];
                this.Session["Token"] = contextToken;

                using (
                    var clientContext = TokenHelper.GetClientContextWithContextToken(hostWeb, contextToken,
                        Request.Url.Authority))
                {
                    clientContext.Load(clientContext.Web, web => web.Title);
                    clientContext.ExecuteQuery();
                    Response.Write(clientContext.Web.Title);
                }
            }
            catch (Exception ex)
            {
                {
                    Response.Write(ex.ToString());
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var contextToken = (string)this.Session["Token"];
            var hostWeb = Page.Request["SPHostUrl"];
            var listId = Page.Request["SPListId"];

            using (var clientContext = TokenHelper.GetClientContextWithContextToken(hostWeb, contextToken, Request.Url.Authority))
            {
                var list = clientContext.Web.Lists.GetById(new Guid(listId));
                var item = list.AddItem(new Microsoft.SharePoint.Client.ListItemCreationInformation());
                item["Title"] = Guid.NewGuid().ToString("N");
                item.Update();
                clientContext.ExecuteQuery();
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var contextToken = (string)this.Session["Token"];
            var hostWeb = Page.Request["SPHostUrl"];
            var listId = Page.Request["SPListId"];

            var contextTokenObject = TokenHelper.ReadAndValidateContextToken(contextToken, Request.Url.Authority);
            var appOnlyAccessToken = TokenHelper.GetAppOnlyAccessToken(contextTokenObject.TargetPrincipalName,
                new Uri(hostWeb).Authority, contextTokenObject.Realm);

            using (var clientContext = TokenHelper.GetClientContextWithAccessToken(hostWeb, appOnlyAccessToken.AccessToken))
            {
                var list = clientContext.Web.Lists.GetById(new Guid(listId));
                var item = list.AddItem(new Microsoft.SharePoint.Client.ListItemCreationInformation());
                item["Title"] = Guid.NewGuid().ToString("N");
                item.Update();
                clientContext.ExecuteQuery();
            }

        }
    }
}