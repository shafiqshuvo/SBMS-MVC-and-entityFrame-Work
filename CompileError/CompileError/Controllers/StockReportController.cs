using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Manager.Manager;
using CompileError.Models;
using System.Linq;

namespace CompileError.Controllers
{
    public class StockReportController : Controller
    {
        SaleManager _saleManager = new SaleManager();
        SalesDetailManager _salesDetailManager = new SalesDetailManager();
        PurchasedProductManager _purchasedProductManager = new PurchasedProductManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();


        [HttpGet]
        public ActionResult Search()
        {
            StockViewModel stockOnSaleViewModel = new StockViewModel();
            stockOnSaleViewModel.Sales = _saleManager.GetAll();

            StockViewModel stockOnPurchaseViewModel = new StockViewModel();
            stockOnPurchaseViewModel.Purchases = _purchaseManager.GetAll();

            return View();
        }

        [HttpPost]
        public ActionResult Search(string startDate, string endDate)
        {
            var products = _productManager.GetAll().ToList();
            var categories = _categoryManager.GetAll().ToList();
            //StockViewModel stockOnSaleViewModel = new StockViewModel();
            var sales = _saleManager.GetAll().ToList();
            var salesProducts =  _salesDetailManager.GetAll().ToList();

            //StockViewModel stockOnPurchaseViewModel = new StockViewModel();
            var purchases = _purchaseManager.GetAll().ToList();
            var purchaseProducts = _purchasedProductManager.GetAll().ToList();

            var productCategory = (from pro in products
                                   join ca in categories on pro.CategoryId equals ca.Id
                                   select new ProductCategory
                                   {
                                       ProductId = pro.Id,
                                       ProductCode = pro.Code,
                                       ProductName = pro.Name,
                                       CategoryName = ca.Name,
                                       ReorderLevel = pro.ReorderLevel

                                   }).ToList();

            var purchaseList = (from pu in purchaseProducts
                                join pro in purchases on pu.ProductId equals pro.Id
                                select new PurchasesList
                                {
                                    
                                    ProductId = pu.ProductId,
                                    PurchaseQuantity = pu.Quantity,
                                    PurchaseDate = pro.Date,
                                    ExpireDate = pu.ExpireDate,
                                    ManufactureDate = pu.ManufactureDate

                                 }).ToList();

            var purchaseProductList = (from pu in purchaseList
                                       join po in productCategory on pu.ProductId equals po.ProductId
                                       select new PurchaseProductList
                                       {
                                           ProductId = pu.ProductId,
                                           ProductCode = po.ProductCode,
                                           ProductName = po.ProductName,
                                           CategoryName = po.CategoryName,
                                           PurchaseDate = pu.PurchaseDate,
                                           PurchaseQuantity = pu.PurchaseQuantity,
                                           ReorderLevel = po.ReorderLevel,
                                           ManufactureDate = pu.ManufactureDate,
                                           ExpireDate = pu.ExpireDate

                                       }).ToList();


            var salesList = (from sd in salesProducts
                             join sa in sales on sd.SaleId equals sa.Id
                             select new SalesList
                              {
                                  ProductCode = sd.ProductCode,
                                  SalesQuantity = sd.Quantity,
                                  SalesDate = sa.Date
                                 
                              }).ToList();

            var stockReport = (from ppl in purchaseProductList
                               join sl in salesList on ppl.ProductCode equals sl.ProductCode
                               select new StockReport
                               {
                                   ProductId = ppl.ProductId,
                                   ProductCode = ppl.ProductCode,
                                   ProductName = ppl.ProductName,
                                   CategoryName = ppl.CategoryName,
                                   ReorderLevel = ppl.ReorderLevel,
                                   ExpireDate = ppl.ExpireDate,
                                   ManufactureDate = ppl.ManufactureDate,
                                   PurchaseQuantity = ppl.PurchaseQuantity,
                                   PurchaseDate = ppl.PurchaseDate,
                                   SalesDate = sl.SalesDate,
                                   SalesQuantity = sl.SalesQuantity,


                               }).ToList();

            var stockReportView = (from product in stockReport
                                   where ((product.PurchaseDate.CompareTo(startDate) >= 0 && product.SalesDate.CompareTo(startDate) >= 0
                                         && product.PurchaseDate.CompareTo(endDate) <= 0 && product.SalesDate.CompareTo(endDate) <= 0))                                 
                                   group product by product.ProductCode into pGroup
                                   select new StockReportView
                                   {
                                       ProductCode = pGroup.First().ProductCode,
                                       ProductName = pGroup.First().ProductName,
                                       CategoryName = pGroup.First().CategoryName,
                                       ReorderLevel = pGroup.First().ReorderLevel,
                                       ExpireDate = pGroup.First().ExpireDate,
                                       In = pGroup.Sum(s => s.PurchaseQuantity),
                                       Out = pGroup.Sum(s => s.SalesQuantity),
                                       OpeningBalance = (pGroup.Sum(s => s.PurchaseQuantity) - pGroup.Sum(s => s.SalesQuantity)),
                                       closingBalance = ((pGroup.Sum(s => s.PurchaseQuantity) - pGroup.Sum(s => s.SalesQuantity)) 
                                                        + pGroup.Sum(s => s.PurchaseQuantity)) - pGroup.Sum(s => s.SalesQuantity)
                                   }).ToList();


            //var stockIn = (from product in stockReportView
            //               where (product.PurchaseDate.CompareTo(startDate) >= 0 && product.PurchaseDate.CompareTo(endDate) <= 0)
            //               select product).ToList();


            //var stockOut = (from product in salesProducts
            //                where (product.Sale.Date.CompareTo(startDate) >= 0 && product.Sale.Date.CompareTo(endDate) <= 0)
            //                select product).ToList();

           
           

            return View(stockReportView);
        }

        public class ProductCategory
        {
            public int ProductId { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public int ReorderLevel { get; set; }

        }
        public class PurchasesList
        {
            public int ProductId { get; set; }
            public double PurchaseQuantity { get; set; }
            public string ExpireDate { get; set;}
            public string ManufactureDate { get; set; }
            public string PurchaseDate { get; set; }

        }

        public class PurchaseProductList
        {
            public int ProductId { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public int ReorderLevel { get; set; }
            public double PurchaseQuantity { get; set; }
            public string ExpireDate { get; set; }
            public string ManufactureDate { get; set; }
            public string PurchaseDate { get; set; }
        }



        public class SalesList
        {

            public string ProductCode { set; get; }
            public double SalesQuantity { set; get; }
            public string SalesDate { get; set; }           
           
        }

        public class StockReport
        {
            public int ProductId { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public int ReorderLevel { get; set; }
            public double PurchaseQuantity { get; set; }
            public string ExpireDate { get; set; }
            public string ManufactureDate { get; set; }
            public string PurchaseDate { get; set; }
           
            public double SalesQuantity { set; get; }
            public string SalesDate { get; set; }

        }

        public class StockReportView
        {
            
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public int ReorderLevel { get; set; }            
            public string ExpireDate { get; set; }          
            public string PurchaseDate { get; set;}
            public double In { set; get; }
            public double Out { set; get; }
            public double OpeningBalance { get; set; }
            public double closingBalance { get; set; }
            public string SalesDate { get; set; }

        }
    }
}