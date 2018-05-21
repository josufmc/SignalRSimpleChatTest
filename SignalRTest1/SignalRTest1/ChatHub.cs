using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;

namespace SignalRTest1
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);
            Clients.All.newMessage(msg);
        }

        public void JoinRoom(string room)
        {
            // This is not persisted
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageForRoom(string room, string message)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);
            Clients.Group(room).newMessage(msg);
        }

        public void SendMessageData(SendData data)
        {
            // Process incoming data
            // Transform data
            // Craft new data
            Clients.All.newData(data);
        }

        public override Task OnConnected() {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            SendMonitoringData("Disconnected", Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected() {
            SendMonitoringData("Reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void SendMonitoringData(string eventType, string connectionId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.newEvent(eventType, connectionId);
            //var msg = string.Format("{0}: {1}", eventType, connectionId);
            //Clients.Caller.newMessage(msg);
        }

        //public Task<int> SendDataAsync()
        //{
        //    // Async work
        //}
    }
}