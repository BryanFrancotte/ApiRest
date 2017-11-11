using System;
using System.Collections.Generic;

namespace ApiRest.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public long CodeRole { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }

        public ICollection<User> User { get; set; }
    }
}
