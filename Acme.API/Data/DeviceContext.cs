using Acme.API.Entities;
using Acme.API.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Data
{
    public class DeviceContext : IDeviceContext
    {
        public DeviceContext(IDeviceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Devices = database.GetCollection<BsonDocument>(settings.CollectionName);
            Devices2 = database.GetCollection<Device>(settings.CollectionName);
            //DeviceContextSeed.SeedData(Devices);
            DeviceContextSeed.SeedData(Devices2);
        }
        public IMongoCollection<BsonDocument> Devices { get; }
        public IMongoCollection<Device> Devices2 { get; }
    }
}
