using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Java_Floral.Models
{
    public class ViewModelMultipleImages
    {
        
        //______________ Table for Single Record ______________
        public Products products { get; set; }
        public ProductMultiImages multipeImagePro { get; set; }

        //______________ List Record _____________
        public IEnumerable<Products> product_List { get; set; }

        public IEnumerable<ProductMultiImages> multipeImagePro_List { get; set; }


        //_____________ Multiple Images _____________
        public List<IFormFile> Images { get; set; }
    }
}
