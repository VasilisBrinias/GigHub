using AutoMapper;
using GigHubBC10.DTOs;
using GigHubBC10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHubBC10.App_Start
{
    //ftiaxnw mia klasi pou klirwnwmei auta poukatevasa
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();//kane create new mapp kai tha to metatrepeis se dto
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDTO>();
        }
    }
}