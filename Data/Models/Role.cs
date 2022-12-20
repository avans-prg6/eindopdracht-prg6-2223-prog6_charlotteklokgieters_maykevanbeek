using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SantasWishlist_Data.Models
{
    public class Role : IdentityRole<string>
    {
        public Role(string role) : base(role) { }

        public Role(string id, string name, string normalizedName)
        {
            base.Id = id;
            base.Name = name;
            base.NormalizedName = normalizedName;
        }
    }
}
