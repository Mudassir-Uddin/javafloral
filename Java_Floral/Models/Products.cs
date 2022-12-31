using Microsoft.AspNetCore.Http;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Java_Floral.Models
{
    public class Products
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Ptitle { get; set; }
        public Category PCategory { get; set; }
        public int Categoryid { get; set; }
        [Required]
        public string Pshortdis { get; set; }
        [Required]
        public string Plongdis { get; set; }
        [Required]
        public int Price { get; set; }

        public int Quantity { get; set; }

        public string PImage { get; set; }


        //_______________ Main_Images _________________
        [Required]
        [NotMapped]
        public IFormFile myimg { get; set; }

        //[Required]
        //[NotMapped]
        //public ICollection myimg { get; set; }

    }
}
