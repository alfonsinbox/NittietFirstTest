using System.Threading.Tasks;
using NittietFirstTest.Models;

namespace NittietFirstTest.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> CreateTokenForUser(User user, string device);
        User GetUserFromToken(string token);
    }
}