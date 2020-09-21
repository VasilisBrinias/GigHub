using GigHubBC10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHubBC10.DTOs
{
    public class NotificationDTO
    {
        public DateTime DateTime  { get; set; }
        public NotificationType Type  { get; set; }
        public DateTime? OriginalDateTime  { get; set; }
        public string OriginalVenue  { get; set; }
        public GigDto Gig  { get; set; }
    }
}