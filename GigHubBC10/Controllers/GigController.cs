using GigHubBC10.Models;
using GigHubBC10.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHubBC10.Controllers
{
    public class GigController : Controller
    {
        
        private ApplicationDbContext context;
        
       
        public GigController()
        {
            context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = context.Gigs
                .Where(a => a.ArtistId == userId && a.DateTime > DateTime.Now && !a.IsCanceled)
                .Include(a => a.Genre)
                .ToList();

            return View(gigs);
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I Am Attenting"
            };

            return View("Gigs",viewModel);//ok!
        }

        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
          
            var viewModel = new GigFormViewModel
            {
                Genres = context.Genres.ToList(),
                Heading = "Add a Gig"
            };

            return View("GigForm",viewModel);
        }

        //EDIT

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = context.Gigs
                .Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Genres = context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                GenreId = gig.GenreId,
                Venue = gig.Venue
            };


            return View("GigForm",viewModel);
        }

        //POST
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = context.Genres.ToList();

                return View("Create", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);
            //gig.Venue = viewModel.Venue;
            //gig.DateTime = viewModel.GetDateTime();
            //gig.GenreId = viewModel.GenreId;
  
            context.SaveChanges();

            return RedirectToAction("Mine", "Gig");
        }


        //POST
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)//<--to gemizw to object check it!

        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = context.Genres.ToList();

                return View("Create", viewModel);
            }

                

            //var artistId = User.Identity.GetUserId();
            /* var artist = context.Users.Single(u => u.Id == artistId);*///einai o user pou einai syndedemenos
                                                                          //var genre = context.Genres.Single(g => g.Id == viewModel.GenreId);
                                                                          //Add Gig context
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                Venue = viewModel.Venue
            };

            context.Gigs.Add(gig);
            context.SaveChanges();

            return RedirectToAction("Mine", "Gig");//kanei redirect eipidei einai post kai prepei kapou na se gyrizei
        }
    }
}