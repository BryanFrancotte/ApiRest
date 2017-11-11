using System;
using System.Collections.Generic;

namespace ApiRest.Models
{
    public partial class Parcel
    {
        public long ParcelId { get; set; }
        public string ParcelType { get; set; }
        public long OrderNumberParcel { get; set; }

        public Order OrderNumberParcelNavigation { get; set; }
    }
}
