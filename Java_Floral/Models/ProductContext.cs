using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Java_Floral.Models;

namespace Java_Floral.Models
{
    public class ProductContext: IdentityDbContext<ApplicationUser>
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payement_Method> Payement_Methods{ get; set; }
        public DbSet<Order_Information> Order_Informations { get; set; }
        public DbSet<CheckOut> checkOuts { get; set; }

        //public DbSet<Java_Floral.Models.SignupModel> SignupModel { get; set; }

        public DbSet<ProductMultiImages> PMultiImages { get; set; }
    }
}
