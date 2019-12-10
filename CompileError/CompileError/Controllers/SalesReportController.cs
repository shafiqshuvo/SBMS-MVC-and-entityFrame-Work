using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Models;
using CompileError.Manager.Manager;

namespace CompileError.Controllers
{
    public class SalesReportController : Controller
    {
        SaleManager _saleManager = new SaleManager();
        SalesDetailManager _salesDetailManager = new SalesDetailManager();
        PurchasedProductManager _purchaseDetailManager = new PurchasedProductManager();

        [HttpGet]
        public ActionResult Show()
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            salesReportViewModel.Sales = _saleManager.GetAll();
            return View();
        }
        public ActionResult GetSalesReportByDate(string startDate, string endDate)
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            var sales = _saleManager.GetAll().ToList();
            var saledetails = _salesDetailManager.GetAll().ToList();
            var purchase = _purchaseDetailManager.GetAll().ToList();

            // purchase = purchase.Where(c => c.Date == StartDate).ToList();
            var sale = (from p in sales where p.Date.CompareTo(startDate) >= 0 && p.Date.CompareTo(endDate) < 0 select p).ToList();

            var count = (from sd in saledetails
                         join s in sale on sd.SaleId equals s.Id
                         join pu in purchase on sd.ProductCode equals pu.Product.Code
                         orderby sd.Id
                         select new SalesReport
                         {

                             Code = sd.ProductCode,
                             Quantity = sd.Quantity,
                             UnitPrice = sd.Quantity * (int) pu.UnitPrice,
                             MRP = sd.MRP * sd.Quantity,
                             Profit = sd.Quantity * (sd.MRP - pu.UnitPrice)
                         }).ToList();


            var Sum = (from c in count
                       group c by c.Code into egroup
                       select new SalesReportShow
                       {
                           Code = egroup.First().Code,
                           Quantity = egroup.Sum(s => s.Quantity),
                           UnitPrice = egroup.Sum(s => s.UnitPrice),
                           MRP = egroup.Sum(s => s.MRP),
                           Profit = egroup.Sum(s => s.Profit)

                       }).ToList();


            ViewBag.salesDetails = Sum;

            return PartialView("Report/_SalesReport");
        }

        public class SalesReport
        {

            public string Code { get; set; }

            public string Category { get; set; }
            public int UnitPrice { get; set; }
            public int Quantity { get; set; }
            public double MRP { get; set; }
            public double TotalMRP { get; set; }

            public double Profit { get; set; }



        }
        public class SalesReportShow
        {

            public string Code { get; set; }
            public int UnitPrice { get; set; }
            public int Quantity { get; set; }
            public double MRP { get; set; }
            public double TotalMRP { get; set; }

            public double Profit { get; set; }



        }

    }
}