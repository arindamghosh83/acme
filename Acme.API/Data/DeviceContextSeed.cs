using Acme.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Data
{
    public class DeviceContextSeed
    {
        //public static void SeedData(IMongoCollection<BsonDocument> deviceCollection)
        public static void SeedData(IMongoCollection<Device> deviceCollection)
        {
            bool existDevice = deviceCollection.Find(d => true).Any();
            if(!existDevice)
            {
                //deviceCollection.InsertManyAsync(GetPreConfiguredDevices());
                deviceCollection.InsertManyAsync(GetAllPreConfiguredDevices());
            }
        }

        private static IEnumerable<BsonDocument> GetPreConfiguredDevices()
        {
            return new List<BsonDocument>()
            {
                new BsonDocument { { "type", "car" }, {
                "state",
                new BsonDocument {{"fluid_levels", "low"}, { "engine_temperature", "normal" }, { "tire_pressure", "normal" }, { "location", "abcd" } }
                },
        },
                new BsonDocument { { "type", "fridge" }, {
                "state",
                new BsonDocument {{"ice_level", "low"}, { "water_leaks", "no" }, { "current_temperature", "abcd" } }
                },

        },
        };

        }

        private static IEnumerable<Device> GetAllPreConfiguredDevices()
        {
            return new List<Device>()
            {
                new Device {
                    Type = "car", State = new State { 
                    FluidLevels = "low",
                    EngineTemperature= "normal",
                    TirePressure = "normal",
                    Location = "xyz"
                } 
                },
                new Device {Type = "fridge", State = new State {
                    IceLevel = "low",
                    WaterLeaks= "no",
                    CurrentTemperature = "normal"
                } }
        };

        }
    }
}
