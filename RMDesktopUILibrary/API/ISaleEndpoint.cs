using System.Threading.Tasks;

using RMDesktopUILibrary.Models;

namespace RMDesktopUILibrary.API
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}