using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace Notification_Api.Hubs
{
    [Route("notificationHub")]
    [EnableCors("AllowLocalhost")]
    public class NotificationHub:Hub
    {
        public async Task SendNotification(string message)
        {
            
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
