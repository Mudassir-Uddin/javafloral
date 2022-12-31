using Java_Floral.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Java_Floral.Infra
{
    public class SmallCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //____ Get Session Data ____
            List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart");
            SmallCartVM smallCartVM;
            if (cart == null || cart.Count == 0)
            {
                smallCartVM = null;
            }
            else
            {
                smallCartVM = new SmallCartVM
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.price)
                };
            }
            return View(smallCartVM);
        }
    }
}
