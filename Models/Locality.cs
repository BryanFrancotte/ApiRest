using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Address> Address { get; set; }
    }
}
