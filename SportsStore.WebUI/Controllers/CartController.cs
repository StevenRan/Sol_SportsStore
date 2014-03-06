using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string resultUrl)
        {
            Product pro = repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (pro != null)
            {
                cart.AddItem(pro, 1); 
            }

            return RedirectToAction("Index", new { resultUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string resultUrl)
        {
            Product pro = repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (pro != null)
            {
                cart.RemoveLine(pro);
            }

            return RedirectToAction("Index", new { resultUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
