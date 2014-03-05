using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;
using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder: IModelBinder
    {
        private const string SESSIONKEY = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = (Cart)controllerContext.HttpContext.Session[SESSIONKEY];
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SESSIONKEY] = cart;
            }

            return cart;
        }
    }
}