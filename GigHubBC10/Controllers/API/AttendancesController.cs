using GigHubBC10.DTOs;
using GigHubBC10.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHubBC10.Controllers.API
{
    [Authorize]///---->mono gia tou xristes pou einai log in
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext context;

        public AttendancesController()
        {
            context = new ApplicationDbContext();
        }

        [HttpPost]  
                                                              
        public IHttpActionResult Attend(AttendanceDto dto) //([FromBody] int gigId)--->from body einai to request--->?id=9
        {
            var userId = User.Identity.GetUserId();
            //edge case for APi
            var exist = context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);//-->elenxw an exei ginei to post mia fora 
            //kai an exei ginei na min to ksanakanei p.x. O Thanasis phge sto Rouva. Ara tha mou pei true or false auto

            if (exist)
                return BadRequest("The attendance already exists");
          
            
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()
            };

            context.Attendances.Add(attendance);
            context.SaveChanges();

            return Ok();
        }
    }
}
