using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class Payement_Method
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }

        //
        public int Shipping_Chargers { get; set; } = 70;
        public int Sub_Total { get; set; }
        public int Grand_Total { get; set; }

    }
}
