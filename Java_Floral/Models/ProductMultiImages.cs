using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Java_Floral.Models
{
    public class ProductMultiImages
    {

        [Key]
        public int id { get; set; }
        
        public string ImageUrl { get; set; }




        public int multi_img_id { get; set; }


        [ForeignKey(nameof(multi_img_id))]
        public virtual  Products Product { get; set; }

    }
}
