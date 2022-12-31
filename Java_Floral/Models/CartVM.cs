using Java_Floral.Models;
using System.Collections.Generic;

namespace Java_Floral.Models
{
    public class CartVM
    {
        public List<CartItem> CartItems{ get; set; }
        public decimal GrandTotal{ get; set; }
    }
}
