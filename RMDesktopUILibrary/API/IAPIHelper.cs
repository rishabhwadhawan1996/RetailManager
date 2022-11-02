using System.Threading.Tasks;

using RMDesktopUILibrary.Models;

namespace RMDesktopUILibrary.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);

        Task GetLoggedInUserInfo(string token);
    }
}