using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTest1
{
    [HubName("monitor")]
    public class MonitorHub : Hub
    {
        public void SendEventData(string eventType, string connectionId)
        {
            var msg = string.Format("{0}: {1}", eventType, connectionId);
            Clients.Caller.newMessage(msg);
        }
    }
}