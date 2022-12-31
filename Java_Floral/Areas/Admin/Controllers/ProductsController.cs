using Java_Floral.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Java_Floral.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ProductContext  _context;
        private readonly IWebHostEnvironment environment;
        //private readonly UserManager<IdentityUser> UserManager;
        //private readonly SignInManager<IdentityUser> SignInManager;

        public ProductsController(ProductContext context , IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }


        //public IActionResult Index(string value = "")
        //{
        //    //var list = _context.Products.ToList();
        //    ViewBag.category = new SelectList(_context.Categories.ToList(), "id", "Name");

        //    var list = _context.Products.Include(e => e.PCategory).Where(e => e.PCategory.id == 2).ToList();


        //    if (value != "")
        //    {
        //        list = _context.Products.Include(x => x.PCategory).Where(x => x.PCategory.Name.Contains(value)).ToList();

        //    }
        //    return View(list);
        //    //return View(await _context.Products.ToListAsync());

        //}



        //_______________________ index _______________________
        public IActionResult Index(string value = "")
        {
            //var productContext = _context.Products.Include(p => p.PCategory);
            //var list = _context.Products.ToList();
            ViewBag.category = new SelectList(_context.Categories.ToList(), "id", "Name");
            
            ViewModelMultipleImages vm = new ViewModelMultipleImages();
            vm.products = new Products();
            vm.multipeImagePro = new ProductMultiImages();

            vm.product_List = _context.Products.Include(x => x.PCategory).Where(x => x.PCategory.Name== "Mothers Day").ToList();
            vm.multipeImagePro_List = _context.PMultiImages.ToList();

            if (value != "")
            {
                vm.product_List = _context.Products.Include(x => x.PCategory).Where(x => x.PCategory.Name.Contains(value)).ToList();
            }

            return View(vm);
        }

       


        //_______________________ Deatail _______________________
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.PCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }


        //_______________________ Create _______________________
        #region Create
        public IActionResult Create()
        {
            ViewData["Categoryid"] = new SelectList(_context.Categories, "id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ViewModelMultipleImages model)
        {
            //____________ 1. Single_Main_Image _________
            var folder = "";
            if (model.products.myimg != null)
            {
                folder = "Images/";
                folder += Guid.NewGuid().ToString() + "_" + model.products.myimg.FileName;
                var serverFolder = Path.Combine(environment.WebRootPath, folder);
                //model.BrowsImage.CopyTo(new FileStream(serverFolder, FileMode.Create));
                using (var fileMode = new FileStream(serverFolder, FileMode.Create))
                {
                    model.products.myimg.CopyTo(fileMode/*new FileStream(serverFolder, FileMode.Create)*/);
                }
            }
            model.products.PImage = folder;

            //____________ 2. Multiple Image _________
            if (model.Images != null)
            {
                //_________ 2 Add Main Image _______
                foreach (var item in model.Images)
                {
                    string fileName = upload(item); //file Image 1 by 1
                    var facilityMultipleImage = new ProductMultiImages
                    {
                        ImageUrl = fileName,
                        Product = model.products, //facility ka Saraa Record
                        id = model.products.id
                    };
                    _context.PMultiImages.Add(facilityMultipleImage); //file Image 1 by 1
                }
            }
            
            _context.Products.Add(model.products);
            _context.SaveChanges();
            //TempData["success"] = "Faciality Added Successfuly!";
            return RedirectToAction("Index");
        }
        #endregion


        //_______________________ Globle Upload Image _______________________
        public string upload(IFormFile File)
        {
            var folderLocation = "";
            if (File != null)
            {
                folderLocation = "images/";
                folderLocation += Guid.NewGuid().ToString() + "_" + File.FileName;
                string serverfolder = Path.Combine(environment.WebRootPath, folderLocation);
                using (var fileStream = new FileStream(serverfolder, FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }
                //await model.myimg.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            };
            return folderLocation;
        }


        //_______________________ Update _______________________
        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewModelMultipleImages vm = new ViewModelMultipleImages();
            vm.products = _context.Products.Where(x => x.id == id).FirstOrDefault();
            vm.multipeImagePro = new ProductMultiImages();

            vm.product_List = _context.Products.ToList();
            vm.multipeImagePro_List = _context.PMultiImages.Include(x=>x.Product).Where(x=>x.multi_img_id == id).ToList();

            if (vm.products == null)
            {
                return NotFound();
            }

            //return View(vm);    
            ViewData["Categoryid"] = new SelectList(_context.Categories, "id", "Name", vm.products.Categoryid);

            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ViewModelMultipleImages model)
        {
            //__________________ Multiple Images ______________
            var folder = "";
            if (model.products.myimg != null)
            {
                if (model.products.PImage != null)
                {
                    var oldDirectory = Path.Combine(environment.WebRootPath, model.products.PImage);
                    if (System.IO.File.Exists(oldDirectory))
                    {
                        System.IO.File.Delete(oldDirectory);
                    }
                }

                folder = "images/";
                folder += Guid.NewGuid().ToString() + "_" + model.products.myimg.FileName;
                var serverFolder = Path.Combine(environment.WebRootPath, folder);
                //model.BrowsImage.CopyTo(new FileStream(serverFolder, FileMode.Create));
                using (var fileMode = new FileStream(serverFolder, FileMode.Create))
                {
                    model.products.myimg.CopyTo(fileMode/*new FileStream(serverFolder, FileMode.Create)*/);
                }
                model.products.PImage = folder;
            }

            //__ delete Multiple Image __
            if (model.Images != null)
            {
                //_________ 1 Delete Multiple Images _____
                var record = _context.PMultiImages.Where(x => x.multi_img_id == model.products.id);
                if (record.Count() > 0)
                {

                    foreach (var item in record.ToList())
                    {
                        var serverFolder2 = Path.Combine(environment.WebRootPath, item.ImageUrl);
                        if (System.IO.File.Exists(serverFolder2))
                        {
                            System.IO.File.Delete(serverFolder2);
                        }
                    }
                   _context.PMultiImages.RemoveRange(record.ToList());
                }


                //_________ 2 Add Main Image _______
                foreach (var item in model.Images)
                {
                    string fileName = upload(item); //file Image 1 by 1
                    var facilityMultipleImage = new ProductMultiImages
                    {
                        ImageUrl = fileName,
                        Product = model.products, //facility ka Saraa Record
                        multi_img_id = model.products.id
                    };

                    _context.PMultiImages.Add(facilityMultipleImage); //file Image 1 by 1
                }
            }
            _context.Products.Update(model.products);
            _context.SaveChanges();
            //ViewData["Categoryid"] = new SelectList(_context.Categories, "id", "Name", model.products.Categoryid);
            return RedirectToAction(nameof(Index));
        }

        #endregion


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("id,Ptitle,Categoryid,Pshortdis,Plongdis,Price,PImage")] Products products)
        //{
        //    if (id != products.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(products);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductsExists(products.id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Categoryid"] = new SelectList(_context.Categories, "id", "Name", products.Categoryid);
        //    return View(products);
        //}


        public IActionResult delete(int id)
        {
            ViewModelMultipleImages vm = new ViewModelMultipleImages();
            vm.products = _context.Products.Include(x => x.PCategory).Where(x => x.id == id).FirstOrDefault(); //Single Record for Delete
            vm.multipeImagePro = new ProductMultiImages();
            vm.product_List = _context.Products.Include(x => x.PCategory).ToList();
            vm.multipeImagePro_List = _context.PMultiImages.Include(x => x.Product).Where(x => x.multi_img_id /*proId*/ == id).ToList();


            //___ Main Image 
            var imagepath1 = vm.products.PImage;
            if (System.IO.File.Exists(imagepath1))
            {
                System.IO.File.Delete(imagepath1);
            }

            //___ Image Delete all if  exist ______
            if (vm.multipeImagePro_List.ToList() != null)
            {
                foreach (var item in vm.multipeImagePro_List.ToList())
                {
                    var imagepath = item.ImageUrl;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    _context.PMultiImages.Remove(item);
                }
            }



            _context.Products.Remove(vm.products);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var products = await _context.Products.FindAsync(id);
        //    _context.Products.Remove(products);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}



        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }

    }
}
