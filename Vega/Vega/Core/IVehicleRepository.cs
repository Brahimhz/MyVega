using System.Threading.Tasks;
using Vega.Core.Models;
namespace Vega.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool eagerLoading = true);

        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);

        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}
