using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompileError.Manager.Manager;
using CompileError.Models;

namespace CompileError.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly CategoryManager _categoryManager = new CategoryManager();
        private readonly ProductManager _productManager = new ProductManager();

        [HttpGet]
        public ActionResult Add()
        {
            PurchaseModelView purchaseModelView = new PurchaseModelView();
            
            fillComboBox(purchaseModelView);
            return View(purchaseModelView);
        }

        private void fillComboBox(PurchaseModelView purchaseModelView)
        {
            purchaseModelView.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            purchaseModelView.ProductSelectListItems = _productManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
        }
    }
}