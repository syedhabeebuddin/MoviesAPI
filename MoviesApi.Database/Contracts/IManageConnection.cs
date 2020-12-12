﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Database.Contracts
{
    public interface IManageConnection
    {
        public MongoClient MongoClient { get; set; }
    }
}
