using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Entities
{
    [BsonIgnoreExtraElements]
    public class Device
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("state")]
        public State State { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Car : Device
    {
        [BsonElement("fluid_levels")]
        public string FluidLevels { get; set; }
        [BsonElement("engine_temperature")]
        public string EngineTemperature { get; set; }
        [BsonElement("tire_pressure")]
        public string TirePressure { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Fridge : Device
    {
        [BsonElement("ice_level")]
        public string IceLevel { get; set; }
        [BsonElement("water_leaks")]
        public string WaterLeaks { get; set; }
        [BsonElement("tire_pressure")]
        public string TirePressure { get; set; }
        [BsonElement("current_temperature")]
        public string CurrentTemperature { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class State
    {
        [BsonElement("fluid_levels")]
        public string FluidLevels { get; set; }
        [BsonElement("engine_temperature")]
        public string EngineTemperature { get; set; }
        [BsonElement("tire_pressure")]
        public string TirePressure { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("ice_level")]
        public string IceLevel { get; set; }
        [BsonElement("water_leaks")]
        public string WaterLeaks { get; set; }
        [BsonElement("current_temperature")]
        public string CurrentTemperature { get; set; }
    }
}
