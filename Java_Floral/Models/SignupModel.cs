using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class SignupModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}
