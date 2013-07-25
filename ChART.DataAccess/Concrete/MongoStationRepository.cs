using ChART.DataAccess.Abstract;
using ChART.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChART.DataAccess.Concrete
{
    public class MongoStationRepository:IStationRepository
    {
        MongoDatabase database;
        public MongoStationRepository(String connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            database = server.GetDatabase(url.DatabaseName);
        }
        public IQueryable<Station> Stations 
        { 
          get {
              return database.GetCollection<Station>("stations").AsQueryable<Station>(); 
          } 
        }
    }
}
