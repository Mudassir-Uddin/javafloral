using System.Collections.Generic;

namespace Java_Floral.Models
{
    public class VmModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Products Product { get; set; }
        public IEnumerable<Products> Products { get; set; }

    }
}
