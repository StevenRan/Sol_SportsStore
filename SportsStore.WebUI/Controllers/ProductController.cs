using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            return View(repo.Products
                .OrderBy(p=>p.ProductId)
                .Skip((page-1) * pageSize)
                .Take(pageSize));
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
