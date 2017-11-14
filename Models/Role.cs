using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<User> User { get; set; }
    }
}
