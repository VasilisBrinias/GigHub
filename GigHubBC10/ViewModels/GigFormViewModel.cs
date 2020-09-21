using GigHubBC10.Controllers;
using GigHubBC10.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace GigHubBC10.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        public string Heading { get; set; }

        public string Action 
        {
            get
            {
                //me auto ton tropo to ekana na katalavaimei pote  allazei to create se update
                //prosoxi an allaksoun ta onomata ActionResult tha saei tha exei provlima
                //return 
                Expression<Func<GigController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<GigController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create; ;
                var actionName = (action.Body as MethodCallExpression).Method.Name;
                return actionName;
            }
        }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        public DateTime GetDateTime()
        {
           return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        } 
        
        public IEnumerable<Genre> Genres { get; set; }
    }
}