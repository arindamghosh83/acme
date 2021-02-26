using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Acme.API.Entities
{
    [DataContract]
    public class DeviceModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name ="type")]
        public string Type { get; set; }
        [DataMember(Name = "state")]
        public StateModel State { get; set; }
    }

    [DataContract]
    public class StateModel
    {
        [DataMember(Name = "fluid_levels")]
        public string FluidLevels { get; set; }
        [DataMember(Name = "engine_temperature")]
        public string EngineTemperature { get; set; }
        [DataMember(Name = "tire_pressure")]
        public string TirePressure { get; set; }
        [DataMember(Name = "location")]
        public string Location { get; set; }
        [DataMember(Name = "ice_level")]
        public string IceLevel { get; set; }
        [DataMember(Name = "water_leaks")]
        public string WaterLeaks { get; set; }
        [DataMember(Name = "current_temperature")]
        public string CurrentTemperature { get; set; }
    }
}
