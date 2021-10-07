using AspNetCoreAngular_EF5.Core;
using AspNetCoreAngular_EF5.Extensions;
using AspNetCoreAngular_EF5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery filter)
        {
            var query = this.context.Vehicles
                .Include(v => v.VehicleFeatures)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .AsQueryable();

            if (filter.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == filter.MakeId.Value);

            if (filter.ModelId.HasValue)
                query = query.Where(v => v.Model.Id == filter.ModelId.Value);

            //Sort
            var sortQuery = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["contactEmail"] = v => v.ContactEmail,
                ["contactPhone"] = v => v.ContactPhone,
                ["id"] = v => v.Id
            };

            query.ApplyOrdering(filter, sortQuery);

            return await query.ToListAsync();
        }
    }
}