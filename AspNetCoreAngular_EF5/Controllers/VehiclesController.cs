using AspNetCoreAngular_EF5.Controllers.Resources;
using AspNetCoreAngular_EF5.Core;
using AspNetCoreAngular_EF5.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreAngular_EF5.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._vehicleRepository = vehicleRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Vehicle vehicle = this._mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = System.DateTime.Now;

            this._vehicleRepository.AddVehicle(vehicle);
            await this._unitOfWork.CompleteAsync();

            var result = this._mapper.Map<Vehicle, SaveVehicleResource>(vehicle);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Vehicle vehicle = await this._vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            this._mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            if (vehicle != null)
                vehicle.LastUpdate = System.DateTime.Now;

            await this._unitOfWork.CompleteAsync();

            var result = this._mapper.Map<Vehicle, SaveVehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            Vehicle vehicle = await this._vehicleRepository.GetVehicle(id, isIncludeRelated: false);
            if (vehicle == null)
                return NotFound();

            this._vehicleRepository.RemoveVehicle(vehicle);

            await this._unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await this._vehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            var vehicleResource = this._mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles(FilterResource filterResource)
        {
            var filter = _mapper.Map<FilterResource, Filter>(filterResource);

            var vehicles = await this._vehicleRepository.GetVehicles(filter);

            return this._mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        }
    }
}
