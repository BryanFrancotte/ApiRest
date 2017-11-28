using System;
using System.Collections.Generic;

namespace ApiRest.Models
{
    public partial class Address
    {
        public Address()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
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
        public ICollection<ApplicationUser> AspNetUsers { get; set; }
        public ICollection<Order> OrderBillingAddressNavigation { get; set; }
        public ICollection<Order> OrderDepositAddressNavigation { get; set; }
        public ICollection<Order> OrderPickUpAddressNavigation { get; set; }
    }
}
