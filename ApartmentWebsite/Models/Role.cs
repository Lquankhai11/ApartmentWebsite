using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class Role
    {
        public Role()
        {
            UserInfs = new HashSet<UserInf>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<UserInf> UserInfs { get; set; }
    }
}
