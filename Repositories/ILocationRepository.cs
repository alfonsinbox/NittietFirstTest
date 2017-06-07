using System.Threading.Tasks;
using NittietFirstTest.Models;
using NittietFirstTest.Models.View;

namespace NittietFirstTest.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> Put(CreateLocation locationModel, User creator);
    }
}