using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product pro = context.Products.Find(product.ProductId);
                if (pro != null)
                {
                    pro.Name = product.Name;
                    pro.Description = product.Description;
                    pro.Price = product.Price;
                    pro.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product pro = context.Products.Find(productId);
            if (pro != null)
            {
                context.Products.Remove(pro);
                context.SaveChanges();
            }

            return pro;
        }
    }
}
