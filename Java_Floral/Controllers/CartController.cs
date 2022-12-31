using Java_Floral.Infra;
using Java_Floral.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Java_Floral.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public CartController(ProductContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;

        }

        //_________________ 6. View ______________
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                //___ Get Session ___
                List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart") /*key*/ ?? new List<CartItem>();

                //___ Add VM ___ (for grandTotal)
                CartVM vm = new CartVM
                {
                    CartItems = cart,
                    GrandTotal = cart.Sum(x=>x.price * x.Quantity)
                };

            return View(vm);
            }
            else
            {
                return RedirectToAction("Signin", "Accounts", new { area = "Security" });
            }
        }


        //_________________ 7. AddToCart Hit ----> AddToCart Button ______________
        public async Task<IActionResult> AddToCart(int id)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                //____ Get Session ______
                List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart") ?? new List<CartItem>();

            //____ cartModel ______
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            //____ Product Model ______
            Products product = context.Products.Find(id);


            if (cartItem == null) //cartItem Model is null ===> Then Add  ---> Product to  Session
            {
                cart.Add(new CartItem(product));
            }
            else   //else  cart is  Exist  ---> then  --> increase The ====> cartitem Model  Values
            {
                cartItem.Quantity += 1;
            }

            //________ set The Session _______
            HttpContext.Session.SetSession("Cart", cart);

            //return RedirectToAction("Index");
            return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                return RedirectToAction("Signin", "Accounts", new { area = "Security" });
            }
        }

        
        //_________________ 8. Decrease AddToCart ______________
        public IActionResult Remove(int id)
        {
            //____ Get Session ______ when decrease it mean data exist in Cart item
            List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart");// ?? new List<CartItem>(); 

            //____ cartModel ______
            //CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();
             


            //if (cartItem.Quantity > 1) //ager Quentity ha to decreaews kr daa
            //{
            //    --cartItem.Quantity;
            //}
            //else  //ager  1 haa  --> Remove kr daa Product koo
            //{
                cart.RemoveAll(x=>x.ProductId == id);
            //}

            //________ set The Session _______
            //HttpContext.Session.SetSession("Cart", cart);


            //________ Remove Session _______ ager Cart  0 ho to
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetSession("Cart", cart);
            }

            //return RedirectToAction("Index");
            return Redirect(Request.Headers["Referer"].ToString());
        }
        
        
        //_________________ 9. Decrease AddToCart ______________
        public IActionResult Decrease(int id)
        {
            //____ Get Session ______ when decrease it mean data exist in Cart item
            List<CartItem> cart = HttpContext.Session.GetSession<List<CartItem>>("Cart");// ?? new List<CartItem>(); 

            //____ cartModel ______
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();
             


            if (cartItem.Quantity > 1) //ager Quentity ha to decreaews kr daa
            {
                --cartItem.Quantity;
            }
            else  //ager  1 haa  --> Remove kr daa Product koo
            {
                cart.RemoveAll(x=>x.ProductId == id);
            }

            //________ set The Session _______
            //HttpContext.Session.SetSession("Cart", cart);


            //________ Remove Session _______ ager Cart  0 ho to
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetSession("Cart", cart);
            }

            //return RedirectToAction("Index");
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            //return Redirect("/"); //send me to  Home Page
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
