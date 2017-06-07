using System.Threading.Tasks;
using NittietFirstTest.Models;
using NittietFirstTest.Models.View;

namespace NittietFirstTest.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Put(SignUpUser userModel);
        User GetUserByCredentials(string username, string password);
    }
}