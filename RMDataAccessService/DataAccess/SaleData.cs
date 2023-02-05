using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RMDataAccessService.Internal.DataAccess;
using RMDataAccessService.Internal.Model;

namespace RMDataAccessService.DataAccess
{
    public class SaleData
    {
        public void SaveSales(SaleModel saleInfo,string cashierId)
        {
            List<SaleDetailDbModel> details = new List<SaleDetailDbModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate()/100;
            foreach(var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDbModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                var productInfo = products.GetProductById(item.ProductId);
                if (productInfo == null)
                {
                    throw new Exception("product info is null");
                }
                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;
                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * taxRate;
                }
                details.Add(detail);   
            }
            SaleDbModel sale = new SaleDbModel()
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId= cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;

            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("RMData");
                    sql.SaveDataUsingTransaction<SaleDbModel>("dbo.spSale_Insert", sale);
                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("dbo.spSale_Lookup",
                        new { CashierId = sale.CashierId, SaleDate = sale.SaleDate }).FirstOrDefault();
                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        sql.SaveDataUsingTransaction("dbo.spSaleDetail_Insert", item);
                    }
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
            
        }
    }
}
