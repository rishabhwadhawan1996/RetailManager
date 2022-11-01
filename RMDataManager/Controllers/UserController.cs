using System.Collections.Generic;
using System.Web;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using RMDataAccessService.DataAccess;
using RMDataAccessService.Internal.Model;

namespace RMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public List<UserModel> GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId);
        }
    }
}