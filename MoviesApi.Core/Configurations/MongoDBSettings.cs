using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Configurations
{
    public class MongoDBSettings
    {
        public string MongoUrl { get; set; }

        public string DatabaseName { get; set; }
    }
}
