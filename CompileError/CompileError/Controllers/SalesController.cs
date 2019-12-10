using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Model.Model;
using CompileError.Models;
using CompileError.Manager.Manager;
using AutoMapper;

namespace CompileError.Controllers
{
    public class SalesController : Controller
    {
        SalesViewModel salesViewModel = new SalesViewModel();
        CustomerManager _customerManager = new CustomerManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        PurchasedProductManager _purchaseDetailManager = new PurchasedProductManager();
        SalesDetailManager _salesDetailManager = new SalesDetailManager();
        SaleManager _saleManager = new SaleManager();
        SalesDetail salesDetail = new SalesDetail();




        [HttpGet]
        public ActionResult Sales()
        {

            var customers = _customerManager.GetAll();
            //  var customer = from c in customers select (new { c.Id, c.Name });
            salesViewModel.CustomersSelectListItem = customers

                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();


            ViewBag.Customer = salesViewModel.CustomersSelectListItem;

            var category = _categoryManager.GetAll();
            salesViewModel.CategorySelectListItem = category

                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();


            ViewBag.Category = salesViewModel.CategorySelectListItem;
            return View(salesViewModel);

        }


        public JsonResult GetLoyalityPointByCustomer(int customerid)
        {
            var customerList = _customerManager.GetAll().Where(c => c.Id == customerid).ToList();
            var customers = from s in customerList select (s.LoyalityPoint);
            return Json(customers, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetProductByCategory(int categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from s in productList select (new { s.Code, s.Name });
            return Json(products, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult GetQuantityByProduct(string productcode)
        //{
        //    var productList = _purchaseDetailManager.GetAll().Where(c => c.Product.Code == productcode).ToList();
        //    //var products = from s in productList select (new { s.Quantity, s.MRP });
        //    //return Json(products, JsonRequestBehavior.AllowGet);

        //}
        //public JsonResult AddSalesDetails(string ProductCode, int Quantity, int MRP, int TotalMRP)
        public JsonResult AddSales(int CustomerID, string Date, int Loyalitypoint)
        {

            Sale sale = new Sale();
            sale.CustomerId = CustomerID;
            sale.Date = Date;
            string value = "2030";
            sale.Code = value;

            _saleManager.Add(sale);


            var productList = _saleManager.GetAll().Where(c => c.Code == value).ToList();
            var salesId = from s in productList select (s.Id);


            return Json(salesId, JsonRequestBehavior.AllowGet);


        }
        public JsonResult AddSalesDetails(string ProductCode, int Quantity, double MRP, double TotalMRP, int SalesID)
        {

            salesDetail.ProductCode = ProductCode;
            salesDetail.Quantity = Quantity;
            salesDetail.MRP = MRP;
            salesDetail.TotalMRP = TotalMRP;
            salesDetail.SaleId = SalesID;


            _salesDetailManager.Add(salesDetail);

            string mess = "success";

            return Json(mess, JsonRequestBehavior.AllowGet);

        }
    }
}