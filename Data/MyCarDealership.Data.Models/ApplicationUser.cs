namespace MyCarDealership.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }
            
        public string FullName { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}