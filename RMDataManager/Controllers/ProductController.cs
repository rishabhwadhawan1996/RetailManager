using System.Collections.Generic;
using System.Web.Http;

using RMDataAccessService.DataAccess;
using RMDataAccessService.Internal;

namespace RMDataManager.Controllers
{
    [Authorize(Roles = "Cashier")]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get()
        {
            ProductData prodData = new ProductData();
            return prodData.GetProducts();
        }
    }
}
