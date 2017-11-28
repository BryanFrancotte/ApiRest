using System;
using System.Collections.Generic;

namespace ApiRest.Models
{
    public partial class Locality
    {
        public Locality()
        {
            Address = new HashSet<Address>();
        }

        public long LocalityId { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }

        public ICollection<Address> Address { get; set; }
    }
}
