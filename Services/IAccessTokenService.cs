using System.Security.Claims;
using NittietFirstTest.Models;
using NittietFirstTest.Models.View;

namespace NittietFirstTest.Services
{
    public interface IAccessTokenService
    {
        ViewAccessToken GetToken(User user);
        string GetIdFromToken(ClaimsPrincipal claimsPrincipal);
    }
}