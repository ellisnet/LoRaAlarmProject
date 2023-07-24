using Microsoft.AspNetCore.Mvc;
using NotificationServiceEmulator.Models;
using System;
using System.Text;

#pragma warning disable CS1591

namespace NotificationServiceEmulator.Controllers
{
    public class SendNotificationController : Controller
    {
        private readonly ISimpleMessaging _messaging;
        public SendNotificationController(ISimpleMessaging messaging)
        {
            _messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
        }

        [HttpGet("send")]
        public IActionResult SendNotification(string id, string title, string message, string type)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(title)) { sb.AppendLine(title.Trim());}
            if (!string.IsNullOrWhiteSpace(type)) { sb.AppendLine(type.Trim()); }
            if (!string.IsNullOrWhiteSpace(message)) { sb.AppendLine(message.Trim()); }
            var notification = sb.ToString();

            _messaging.Send(this, nameof(NotificationMessage), new NotificationMessage
            {
                Message = (string.IsNullOrWhiteSpace(notification)) ? "(No message text)" : notification.Trim()
            });

            return Ok();
        }
    }
}
