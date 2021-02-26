using Acme.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Acme.API.Entities;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace Acme.API.Controllers
{
    [ApiController]
    //[Route("api/v1/[controller]")]
    [Route("api/[controller]")]
    //[EnableCors("AllowOrigin")]
    public class AcmeController : ControllerBase
    {
        private readonly IDeviceRepository _repository;
        private readonly IMapper _mapper;
        public AcmeController(IDeviceRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _repository.GetDevices();
            return Ok(devices);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDevices(IFormFile file)
        {
            byte[] buffer = new byte[1024];
            int bytesread = await file.OpenReadStream().ReadAsync(buffer);
            var devices = JsonConvert.DeserializeObject<List<DeviceModel>>(Encoding.UTF8.GetString(buffer, 0, bytesread));
            var devicestoAdd = new List<Device>();
            var devicestoUpdate = new List<Device>();
            //var data = await System.Text.Json.JsonSerializer.DeserializeAsync<List<DeviceModel>>(file.OpenReadStream());
            //var data = JsonConvert.DeserializeObject<List<DeviceModel>>(System.utf)
            //using (FileStream s = (FileStream)file.OpenReadStream())
            //using (StreamReader sr = new StreamReader(s))
            //{
            //    var data = System.Text.Json.JsonSerializer.DeserializeAsync<Device>(sr);
            //}
            //using (JsonReader reader = new JsonTextReader(sr))
            //{

            //}
            //using var stream = file.OpenReadStream();
            //using JsonTextReader reader = new JsonTextReader(stream);
            //stream.
            //return Ok(data);
            foreach (var device in devices)
            {
                if(String.IsNullOrEmpty(device.Id))
                {
                    var newdevice = _mapper.Map<Device>(device);
                    devicestoAdd.Add(newdevice);
                } else
                {
                    if(device.State != null)
                    {
                        var updateddevice = _mapper.Map<Device>(device);
                        devicestoUpdate.Add(updateddevice);
                    } else
                    {

                    }
                }
            }
            if(devicestoAdd.Count > 0)
            {
                await _repository.AddDevice(devicestoAdd);
            }
            if (devicestoUpdate.Count > 0)
            {
                await _repository.UpdateDevice(devicestoUpdate);
            }

            return Ok(devices);
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateDeviceState(State deviceState)
        //{
   
        //}
    }
}
