using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.API.Settings
{
    public interface IDeviceDatabaseSettings
    {
         string ConnectionString { get; set; }
         string DatabaseName { get; set; }
         string CollectionName { get; set; }
    }
}
