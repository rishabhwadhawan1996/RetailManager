using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using RMDesktopUILibrary.Helpers;
using RMDesktopUILibrary.Models;

namespace RMDesktopUILibrary.API
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private IAPIHelper apiHelper;

        public SaleEndpoint(IAPIHelper helper)
        {
            apiHelper = helper;
        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await apiHelper.APIClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        //public async Task<List<ProductModel>> GetAll()
        //{
        //    using (HttpResponseMessage response = await apiHelper.APIClient.GetAsync("/api/Product"))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<List<ProductModel>>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}
    }
}
