﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using RMDataAccessService.DataAccess;
using RMDataAccessService.Internal.Model;

using RMDataManager.Models;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData saleData = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            saleData.SaveSales(sale,userId);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}