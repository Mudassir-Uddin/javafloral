using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
