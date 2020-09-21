using GigHubBC10.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using System.Data.Entity;

namespace GigHubBC10.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        

        private ApplicationDbContext context;

        public GigsController()
        {
            context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();
            

            //var notification = new Notification(NotificationType.GigCanceled, gig);
           
            //foreach(var attendee in gig.Attendances.Select(a => a.Attendee))
            //{
            //    attendee.Notify(notification);
            //}
            //var attendees = context.Attendances
            //    .Where(a => a.GigId == gig.Id)
            //    .Select(a => a.Attendee)
            //    .ToList();


            //foreach (var attendee in attendees)
            //{
            //    attendee.Notify(notification);
                
            //}

            context.SaveChanges();

            return Ok();
        }
    }
}
