using Acme.API.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetDevices();
        Task AddDevice(IEnumerable<Device> devices);
        Task UpdateDevice(IEnumerable<Device> devices);
    }
}
