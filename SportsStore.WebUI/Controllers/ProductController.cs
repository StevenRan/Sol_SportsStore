using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        private IProductRepository repo;
        private int pageSize = 2;

        public ProductController(IProductRepository productRepository)
        {
            this.repo = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repo.Products
                .Where(p=> category == null? true : p.Category == category)
                .OrderBy(p=>p.ProductId)
                .Skip((page-1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null? repo.Products.Count(): 
                            repo.Products.Where(e=> e.Category == category).Count()
                },
                CurrentCategory = category

            };

            return View(model);         
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
