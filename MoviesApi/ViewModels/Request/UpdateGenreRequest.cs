using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.ViewModels.Request
{
    public class UpdateGenreRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
