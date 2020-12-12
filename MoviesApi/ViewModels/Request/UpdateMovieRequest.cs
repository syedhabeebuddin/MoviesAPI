﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.ViewModels.Request
{
    public class UpdateMovieRequest
    {   
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }        
        public int Duration { get; set; }
        public int Rating { get; set; }
    }
}
