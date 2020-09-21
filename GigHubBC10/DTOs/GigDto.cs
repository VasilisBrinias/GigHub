using GigHubBC10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHubBC10.DTOs
{
    public class GigDto
    {
        public int Id { get; set; }

        public bool IsCanceled { get;  set; }//About Delete Logical Delete
        public UserDto Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }

        public GenreDto Genre { get; set; }
        
    }
}