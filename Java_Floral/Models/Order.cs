using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Java_Floral.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public int Status { get; set; }


        public Order_Information Order_info { get; set; }
        public int? Order_Informationid { get; set; }

        public Payement_Method Payement_info { get; set; }
        public int? Payement_Methodid { get; set; }


        public string? idenityUserId { get; set; }
        [ForeignKey(nameof(idenityUserId))]
        [InverseProperty(nameof(ApplicationUser.orders))]
        public virtual ApplicationUser appUser{ get; set; }

    }
}
