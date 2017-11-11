using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApiRest.Models
{
    public partial class Order
    {
        public Order()
        {
            Letter = new HashSet<Letter>();
            Parcel = new HashSet<Parcel>();
        }

        public long OrderNumber { get; set; }
        public string State { get; set; }
        public DateTime PickUpDate { get; set; }
        public TimeSpan PickUpStartTime { get; set; }
        public TimeSpan PickUpEndTime { get; set; }
        public DateTime DepositDate { get; set; }
        public TimeSpan DepositeStartTime { get; set; }
        public TimeSpan DepositeEndTime { get; set; }
        public string DeliveryType { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public long UserIdOrder { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public long PickUpAddress { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public long DepositAddress { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public long BillingAddress { get; set; }

        public Address BillingAddressNavigation { get; set; }
        public Address DepositAddressNavigation { get; set; }
        public Address PickUpAddressNavigation { get; set; }
        public User UserIdOrderNavigation { get; set; }
        public ICollection<Letter> Letter { get; set; }
        public ICollection<Parcel> Parcel { get; set; }
    }
}
