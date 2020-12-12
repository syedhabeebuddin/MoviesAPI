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
    public class ManageDatabase : IManageDatabase
    {
        private readonly IManageConnection _mongoConnection;

        public ManageDatabase(IManageConnection mongoConnection, IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mongoConnection = mongoConnection;
            ApplicationDB = _mongoConnection.MongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
        }

        public IMongoDatabase ApplicationDB { get; set; }
    }
}
