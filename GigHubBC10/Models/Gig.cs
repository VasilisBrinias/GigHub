using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHubBC10.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get;private set; }//About Delete Logical Delete
        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }//fernontas mono to Id mporw na exw prosvasi se ola ta paidia
        public ICollection<Attendance> Attendances { get; private set; }




        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }



        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }


        public void Modify(DateTime dateTime, string venue, byte genreId)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);
            //notification.OriginalDateTime = DateTime;
            //notification.OriginalVenue = Venue;

            Venue = venue;
            DateTime = dateTime;
            GenreId = genreId;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}