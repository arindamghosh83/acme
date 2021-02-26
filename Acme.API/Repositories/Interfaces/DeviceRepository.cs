using Acme.API.Data;
using Acme.API.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Repositories.Interfaces
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IDeviceContext _context;

        public DeviceRepository(IDeviceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddDevice(IEnumerable<Device> devices)
        {
            var documents = new List<BsonDocument>();
            foreach(var device in devices)
            {
                if(device.Type == "car")
                {
                 documents.Add(new BsonDocument { { "type", device.Type }, {
                "state",
                new BsonDocument {{"fluid_levels", device.State.FluidLevels}, { "engine_temperature", device.State.EngineTemperature }, { "tire_pressure", device.State.TirePressure }, { "location", device.State.Location } }
                },
                });
                } else if(device.Type == "fridge")
                {
                 documents.Add(new BsonDocument { { "type", device.Type }, {
                "state",
                new BsonDocument {{"ice_level", device.State.IceLevel }, { "water_leaks", device.State.WaterLeaks }, { "current_temperature", device.State.CurrentTemperature } }
                },
                });
                }

            }
            await _context.Devices.InsertManyAsync(documents);
        }

        public async Task UpdateDevice(IEnumerable<Device> devices)
        {
            var deviceIds = devices.Select(d => d.Id);
            //var documents = new List<BsonDocument>();
            var filters = new List<FilterDefinition<Device>>();
            var updates = new List<UpdateDefinition<Device>>();
            //var filter = Builders<BsonDocument>.Filter.Or
            var devicestoUpdateCursor = _context.Devices2.Find(d => deviceIds.Contains(d.Id));
            var devicestoUpdate = await devicestoUpdateCursor.ToListAsync();
            foreach (var devicetoUpdate in devicestoUpdate)
            {
                var updateDeviceState = devices.First(d => d.Id == devicetoUpdate.Id).State;
                var filter = Builders<Device>.Filter.Eq(d => d.Id, devicetoUpdate.Id);
                var update = Builders<Device>.Update.Set(d => d.State, updateDeviceState);
                var result = await _context.Devices2.UpdateOneAsync(filter, update);
                //filters.Add(filter);
                //updates.Add(update);
                //devicetoUpdate.State = devices.First(d => d.Id == devicetoUpdate.Id).State;

            }
            //var combinedFilter = Builders<Device>.Filter.Or(filters);
        }

        public async Task<bool> UpdateDeviceState(Device device)
        {
            var updatedResult = await _context.Devices2.ReplaceOneAsync(filter: d => d.Id == device.Id, replacement: device);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteDevice(string id)
        {
            FilterDefinition<Device> filter = Builders<Device>.Filter.Eq(d => d.Id, id);
            DeleteResult deleteResult = await _context.Devices2.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task CreateDevice(Device device)
        {
            await _context.Devices2.InsertOneAsync(device);
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var devices = await _context.Devices2.Find(d => true).ToListAsync();
            // var documents = await _context.Devices.Find(d => true).ToListAsync();
            //var devices = new List<Device>(); 
            // foreach(BsonDocument doc in documents)
            //{
            //    if(doc["type"] == "car")
            //    {
            //        var device = BsonSerializer.Deserialize<Device>(doc);
            //        devices.Add(device);
            //    }
            //    else if(doc["type"] == "fridge")
            //    {
            //        var device = BsonSerializer.Deserialize<Device>(doc);
            //        devices.Add(device);
            //    }
            //}
            return devices;
        }


    }
}
