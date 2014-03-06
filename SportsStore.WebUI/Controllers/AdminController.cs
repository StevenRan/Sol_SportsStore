using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repo;

        public AdminController(IProductRepository productRepo)
        {
            this.repo = productRepo;
        }

        public ViewResult Index()
        {
            return View(this.repo.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product pro = this.repo.Products
                .FirstOrDefault(p => p.ProductId == productId);
            return View(pro);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");

            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repo.DeleteProduct(productId);
            if (deletedProduct != null) 
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}
