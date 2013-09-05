using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace DEmo2AutoHostedWeb.Services
{
    public class RemoteEventReceiver1 : IRemoteEventService
    {
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();

            //using (ClientContext clientContext = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            //{
            //    if (clientContext != null)
            //    {
            //        clientContext.Load(clientContext.Web);
            //        clientContext.ExecuteQuery();
            //    }
            //}

            result.ChangedItemProperties["Title2"] = 
                properties.ItemEventProperties.AfterProperties["Title"];

            return result;
        }

        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
        }
    }
}
