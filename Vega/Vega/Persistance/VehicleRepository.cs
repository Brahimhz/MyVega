using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vega.Core;
using Vega.Core.Models;
using Vega.Extensions;

namespace Vega.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id, bool eagerLoading = true)
        {
            if (!eagerLoading)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
             .Include(v => v.Features)
                 .ThenInclude(vf => vf.Feature)
             .Include(v => v.Model)
                 .ThenInclude(vm => vm.Make)
             .SingleOrDefaultAsync(v => v.Id == id);

        }

        public async Task<Vehicle> GetVehicleWithMake(int id)
        {

            return await context.Vehicles
             .Include(v => v.Features)
                 .ThenInclude(vf => vf.Feature)
             .Include(v => v.Model)
                 .ThenInclude(vm => vm.Make)
             .SingleOrDefaultAsync(v => v.Id == id);

        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }


        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {

            var result = new QueryResult<Vehicle>();

            var query = context.Vehicles
              .Include(v => v.Model)
                .ThenInclude(m => m.Make)
              .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
              .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId);

            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queryObj.ModelId);


            //if (queryObj.SortBy == "make")
            //    query = (queryObj.IsAsc) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);

            //if (queryObj.SortBy == "make")
            //    query = (queryObj.IsAsc) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);

            var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {

                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id

            };


            query = query.ApplyOrdering(queryObj, columnMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;


        }

    }
}
