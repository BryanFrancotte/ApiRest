using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApiRest.Models
{
    public partial class Address
    {
        public Address()
        {
            AspNetUsers = new HashSet<ApplicationUser>();
            OrderBillingAddressNavigation = new HashSet<Order>();
            OrderDepositAddressNavigation = new HashSet<Order>();
            OrderPickUpAddressNavigation = new HashSet<Order>();
        }

        public long AddressId { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BoxNumber { get; set; }
        public long LocalityIdAddress { get; set; }

        public Locality LocalityIdAddressNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ApplicationUser> AspNetUsers { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> OrderBillingAddressNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> OrderDepositAddressNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Order> OrderPickUpAddressNavigation { get; set; }
    }
}
