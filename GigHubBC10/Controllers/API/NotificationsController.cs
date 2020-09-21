using AutoMapper;
using GigHubBC10.DTOs;
using GigHubBC10.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHubBC10.Controllers.API
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext context;

        public NotificationsController()
        {
            context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDTO> GetNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)//<------pou den exoun diavastei notifications
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
            //gia na  min to xrisimopoiw kanw auto mapping
            // manualMapping --->return notifications.Select(n => new NotificationDTO()
            return notifications.Select(Mapper.Map<Notification, NotificationDTO>); 

           // {
                //DateTime = n.DateTime,
                //Gig = new GigDto
                //{
                //    Artist = new UserDto
                //    {
                //        Id = n.Gig.Artist.Id,
                //        Name = n.Gig.Artist.Name
                //    },
                //    DateTime = n.Gig.DateTime,
                //    Id = n.Gig.Id,
                //    IsCanceled = n.Gig.IsCanceled,
                //    Venue = n.Gig.Venue,

                //},
                //OriginalDateTime = n.OriginalDateTime,
                //OriginalVenue = n.OriginalVenue
                
            //});
        }
    }
}
