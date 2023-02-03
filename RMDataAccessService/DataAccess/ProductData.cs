using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RMDataAccessService.Internal;
using RMDataAccessService.Internal.DataAccess;

namespace RMDataAccessService.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            List<ProductModel> output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");
            return output;
        }

        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();
            ProductModel output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId}, "RMData").FirstOrDefault();
            return output;
        }
    }
}
