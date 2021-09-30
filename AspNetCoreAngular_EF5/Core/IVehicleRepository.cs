using AspNetCoreAngular_EF5.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreAngular_EF5.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool isIncludeRelated = true);
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetVehicles();
    }
}