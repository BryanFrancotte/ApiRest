using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApiRest.Models
{
    public partial class Parcel
    {
        public long ParcelId { get; set; }
        public int ParcelType { get; set; }
        public long OrderNumberParcel { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Order OrderNumberParcelNavigation { get; set; }
    }
}
