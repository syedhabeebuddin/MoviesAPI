﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.ViewModels.Request
{
    public class AddMovieRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
        public List<string> Genres { get; set; }
        public int Duration { get; set; }
        public int Rating { get; set; }
    }
}
