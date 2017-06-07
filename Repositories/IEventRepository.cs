using System.Threading.Tasks;
using NittietFirstTest.Models;
using NittietFirstTest.Models.View;

namespace NittietFirstTest.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event> Put(CreateEvent model, User creator, Location location);
    }
}