using Acme.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Data
{
    public interface IDeviceContext
    {
        IMongoCollection<BsonDocument> Devices { get; }
        IMongoCollection<Device> Devices2 { get; }
    }
}
