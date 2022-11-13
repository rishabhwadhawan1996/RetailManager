using System.Collections.Generic;
using System.Threading.Tasks;

using RMDesktopUILibrary.Models;

namespace RMDesktopUILibrary.API
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}