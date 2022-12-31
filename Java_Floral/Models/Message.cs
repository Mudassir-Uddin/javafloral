using System.ComponentModel.DataAnnotations;

namespace Java_Floral.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string userid{ get; set; }

        public string message { get; set; }
    }
}
