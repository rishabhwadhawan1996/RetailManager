using System.Net.Http;
using System.Threading.Tasks;

using RMDesktopUILibrary.Models;

namespace RMDesktopUILibrary.Helpers
{
    public interface IAPIHelper
    {
        void LogOffUser();

        Task<AuthenticatedUser> Authenticate(string username, string password);

        Task GetLoggedInUserInfo(string token);

        HttpClient APIClient { get;}
    }
}