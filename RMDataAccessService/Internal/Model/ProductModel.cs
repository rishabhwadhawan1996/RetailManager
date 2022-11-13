using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataAccessService.Internal
{
    public class ProductModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal RetailPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int QuantityInStock { get; set; }
    }
}
