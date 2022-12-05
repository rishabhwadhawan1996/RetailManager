using System.Collections.Generic;

using RMDataAccessService.Internal.DataAccess;
using RMDataAccessService.Internal.Model;

namespace RMDataAccessService.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { id };
            List<UserModel> output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMData");
            return output;
        }


    }
}
