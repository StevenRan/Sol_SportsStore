using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using Moq;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private static IKernel kernal = new StandardKernel();

        public NinjectControllerFactory()
        {
            AddBings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return (controllerType==null)? null: (IController)kernal.TryGet(controllerType);
        } 


        public void AddBings()
        {
            kernal.Bind<IProductRepository>().To<EFProductRepository>();
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup( m => m.Products).Returns(new List<Product>{
            //new Product {Name = "Football", Price = 25},
            //new Product {Name = "Surf board", Price =179}
            //}.AsQueryable());

            //kernal.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}