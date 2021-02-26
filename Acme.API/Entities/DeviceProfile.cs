using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Entities
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceModel>().ReverseMap();
            CreateMap<State, StateModel>().ReverseMap();
        }
    }
}
