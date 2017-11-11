using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApiRest.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public long CodeRoleUser { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public long? AddressIdUser { get; set; }

        public Address AddressIdUserNavigation { get; set; }
        public Role CodeRoleUserNavigation { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
