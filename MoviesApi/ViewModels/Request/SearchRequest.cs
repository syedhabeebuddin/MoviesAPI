using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.ViewModels.Request
{
    public class SearchRequest
    {
        public string fieldName { get; set; }
        public string fieldValue { get; set; }
    }
}
