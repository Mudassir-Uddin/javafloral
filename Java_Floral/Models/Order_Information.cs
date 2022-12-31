using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class Order_Information
    {
        [Key]
        public int  id{ get; set; }
        public string Country { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Company_Name { get; set; }
        public string Address { get; set; }
        public string Town_City { get; set; }
        public string Postal_Code { get; set; }
        public string Email_Address { get; set; }
        public int Phone { get; set; }
    }
}
