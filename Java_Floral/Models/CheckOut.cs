using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class CheckOut
    {
        [Key]
        public int id { get; set; }
        public Order order { get; set; }
        public int? Orderid { get; set; }
        public Products products_info { get; set; }
        public int? Productsid { get; set; }
        public int Total_Quantity { get; set; }
        public int Total_Price { get; set; }

    }
}
