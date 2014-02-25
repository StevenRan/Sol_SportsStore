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

        public ViewResult List(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repo.Products
                .OrderBy(p=>p.ProductId)
                .Skip((page-1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repo.Products.Count()
                }
            };

            return View(model);         
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
