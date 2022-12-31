using Java_Floral.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Java_Floral.Controllers
{
    public class OccasionsController : Controller
    {
        private readonly ProductContext _context;

        public OccasionsController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult index(string value = "" , int currentPage = 1)
        {
            //var list = _context.Products.ToList();
            ViewBag.category = "All Products";

            var list = _context.Products.Include(e => e.PCategory).ToList();


            if (value != "")
            {
                list = _context.Products.Include(x=>x.PCategory).Where(x => x.PCategory.Name.Contains(value)).ToList();
                var singleProduct = _context.Products.Include(x=>x.PCategory).Where(x=>x.PCategory.Name == value).FirstOrDefault();
                ViewBag.category = singleProduct.PCategory.Name;
            }
            else
            {
                list =  _context.Products.Include(p => p.PCategory).ToList();
                ViewBag.totalPages = Math.Ceiling(list.Count() / 16.0); // 20/8  ==> 2.9 ==> 3  
                ViewBag.cPage = currentPage;
                list = list.Skip((currentPage - 1) * 16).Take(16).ToList();
            }
            return View(list);
            //return View(await _context.Products.ToListAsync());

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewModelMultipleImages vm = new ViewModelMultipleImages();
            vm.products = _context.Products.Include(x => x.PCategory).Where(x=>x.id == id).FirstOrDefault(); //catgory .. id  == category  ()
            vm.multipeImagePro = new ProductMultiImages();

            vm.product_List = _context.Products.Include(x => x.PCategory).ToList();
            vm.multipeImagePro_List = _context.PMultiImages.ToList();


            //vm.products = new Products();
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        
       
    }
}
