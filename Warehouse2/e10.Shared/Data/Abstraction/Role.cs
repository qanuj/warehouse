using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e10.Shared.Data.Abstraction
{
    public class Role : IdentityRole<string, UserRole>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public Role(string roleName)
            : this()
        {
            Name = roleName;
        }
    }
}