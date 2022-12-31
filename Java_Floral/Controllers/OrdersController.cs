using Java_Floral.Infra;
using Java_Floral.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Java_Floral.Controllers
{
    public class OrdersController : Controller
    {

        private readonly ProductContext context;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;

        public OrdersController(ProductContext context, UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this.context = context;
        }

        public IActionResult OrderCheckOut()
        {
            //___ Get Session ___
            List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart") /*key*/ ?? new List<CartItem>();

            //___ Add VM ___ (for grandTotal)
            CartVM vm = new CartVM
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.price * x.Quantity)
            };

            checkoutVM vm1 = new checkoutVM();
            vm1.cartV = vm;
            vm1.checkOut_tbl = new CheckOut();
            vm1.order_tbl = new Order();
            vm1.oInformation_tbl = new Order_Information();
            vm1.payment_tbl = new Payement_Method();
                
            return View(vm1);
        }


        [HttpPost]
        public IActionResult OrderCheckOut(checkoutVM model)
        {
            //______ humara data sassion me haa ----------
            //___ Get Session ___
            List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart") /*key*/ ?? new List<CartItem>();

            //___ Add VM ___ (for grandTotal)
            CartVM vm = new CartVM
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.price * x.Quantity)
            };


            if(cart.Count > 0)  // yaa 0 haa  session mee  ----> goood going
            {
                //_________________ 1st _______________________  
                //model.payment_tbl.Name = ;     ----> name   haa   is lyaa     yaa  step  zrori nhin ???  
                var l = cart.ToList();

                model.payment_tbl.Sub_Total =(int)vm.GrandTotal; //ya decimal me the value
                model.payment_tbl.Grand_Total =(int)(vm.GrandTotal) + (model.payment_tbl.Shipping_Chargers);

                context.Payement_Methods.Add(model.payment_tbl);
                context.SaveChanges();


                //_________________ 2nd _______________________
                if (model.oInformation_tbl.First_Name != null && model.oInformation_tbl.Address != null && model.oInformation_tbl.Email_Address != null && model.oInformation_tbl.Country != null)
                {
                    context.Order_Informations.Add(model.oInformation_tbl);
                    context.SaveChanges();


                    //_________________ 3rd __________________//ya comit app hta dain aa  
                    //model.order_tbl.idenityUserId = "1baaf107-caa3-4fb4-a1e8-181bc23a5857";// HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    int lastId1 = context.Order_Informations.Max(item=>item.id);
                    int lastId2 = context.Payement_Methods.Max(item=>item.id);
                    model.order_tbl.Order_Informationid = lastId1;
                    model.order_tbl.Payement_Methodid = lastId2;
                    model.order_tbl.Status = 1; //order panding 

                    context.Orders.Add(model.order_tbl);


                    //__________________ last ______________________
                    var listproducts = cart.ToList();
                    foreach (var item in listproducts)
                    {
                        model.checkOut_tbl.Productsid = item.ProductId;
                        //___product ka  name ----. forign key sa ay gaa
                        model.checkOut_tbl.Total_Quantity = item.Quantity;
                        model.checkOut_tbl.Total_Price = (int) (item.Quantity * item.price);
                        model.checkOut_tbl.Orderid = model.order_tbl.id;

                        context.checkOuts.Add(model.checkOut_tbl);
                    }
                }


            }


            return RedirectToAction("OrderCheckOut");
        }



    }
}
