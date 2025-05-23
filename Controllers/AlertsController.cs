using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationServer.Hubs;

namespace NotificationServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public AlertsController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] AlertDto dto)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", dto.Title, dto.Message);
            return Ok("Notification sent");
        }
    }

    public record AlertDto(string Title, string Message);
}
