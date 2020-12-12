using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MoviesApi.Core.Configurations;
using MoviesApi.Database.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Database.Services
{
    public class ManageConnection : IManageConnection
    {
        public MongoClient MongoClient { get; set; }

        public ManageConnection(IOptions<MongoDBSettings> mongoSettings)
        {
            var settings = MongoClientSettings.FromUrl(
                new MongoUrl(mongoSettings.Value.MongoUrl));
            
            MongoClient = new MongoClient(settings);
        }
    }
}
