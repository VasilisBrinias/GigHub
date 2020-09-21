using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHubBC10.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get;private set; } //kleidwnw ta paidia pou xeireizomai kartw
        public NotificationType Type { get;private set; }
        public DateTime? OriginalDateTime { get;private set; }
        public string OriginalVenue { get;private set; }

        [Required]
        public Gig Gig { get; set; }

        protected Notification()
        {}

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentException("gig");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }
        //Factory methods working with private methods
        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigUpdated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDatetime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDatetime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }
    }
}