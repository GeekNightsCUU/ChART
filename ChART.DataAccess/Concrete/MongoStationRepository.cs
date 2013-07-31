using ChART.DataAccess.Abstract;
using ChART.Domain.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System;
using System.Linq;

namespace ChART.DataAccess.Concrete
{
    public class MongoStationRepository:IStationRepository
    {
        MongoDatabase database;
        static MongoStationRepository()
        {
            BsonClassMap.RegisterClassMap<Station>(cm =>
            {
                cm.AutoMap();
                cm.IdMemberMap.SetRepresentation(BsonType.ObjectId);
            });
        }
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

        public Station Station(String stationId)
        {
            return database.GetCollection<Station>("stations").AsQueryable<Station>().First(s => s.Id == stationId); 
        }
    }
}
