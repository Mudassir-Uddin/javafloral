using System.Collections.Generic;

namespace Java_Floral.Models
{
    public class checkoutVM
    {
        //_________ List ____________
        public Order order_tbl { get; set; }

        public Payement_Method payment_tbl { get; set; }
        public CheckOut checkOut_tbl{ get; set; }
        public Order_Information oInformation_tbl{ get; set; }

        public CartVM cartV{ get; set; }

        public IEnumerable<Order> order_List { get; set; }
        public IEnumerable<Payement_Method> payment_List { get; set; }
        public IEnumerable<CheckOut> checkOut_list { get; set; }
        public IEnumerable<Order_Information> order_inf_list { get; set; }
    }
}
