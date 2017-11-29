using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ApiRest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? BirthDate { get; set; }
        public long? AddressIdUser { get; set; }
        public byte[] VerCol { get; set; }
        public Address AddressIdUserNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> OrderCoursierIdOrderNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> OrderUserIdOrderNavigation { get; set; }
    }
}