using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification_Api.Data;
using Notification_Api.Hubs;
using Notification_Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationsController(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        [HttpGet]
        public IActionResult GetNotifications()
        {
            try
            {
                
                List<Notification> notifications = _context.Notifications.ToList();

                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            try
            {
                notification.CreatedAt = DateTime.UtcNow;
                notification.IsRead = false;

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

               
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message);

                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
