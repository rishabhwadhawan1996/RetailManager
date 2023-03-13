using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktopUILibrary.API
{
    public interface IUserEndpoint
    {
        Task AddUserToRole(string userId, string role);
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task RemoveUserFromRole(string userId, string role);
    }
}