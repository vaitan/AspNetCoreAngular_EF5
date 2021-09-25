using AspNetCoreAngular_EF5.Core;
using AspNetCoreAngular_EF5.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreAngular_EF5.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly NewDbContext context;

        public VehicleRepository(NewDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool isIncludeRelated = true)
        {
            if (!isIncludeRelated)
                return await this.context.Vehicles.FindAsync(id);

            return await this.context.Vehicles
                .Include(v => v.VehicleFeatures)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.context.Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            this.context.Vehicles.Remove(vehicle);
        }
    }
}
