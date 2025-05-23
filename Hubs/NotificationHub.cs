using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NotificationServer.Hubs
{
    public class NotificationHub : Hub
    {
        public Task SendNotification(string title, string message)
        {
            return Clients.All.SendAsync("ReceiveNotification", title, message);
        }

        public Task SubscribeToGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
