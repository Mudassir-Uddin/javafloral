using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Image_Url { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Title { get; set; }
        public string UserId { get; set; }
    }
}
