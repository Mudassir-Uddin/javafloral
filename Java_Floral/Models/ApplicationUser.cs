using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Java_Floral.Models
{

    public class ApplicationUser:IdentityUser
    {

        public ApplicationUser()
        {
            orders = new HashSet<Order>();
        }

        [InverseProperty("appUser")]
        public virtual ICollection<Order> orders { get; set; }

    }
}
