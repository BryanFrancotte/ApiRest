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
        public TimeSpan DepositStartTime { get; set; }
        public TimeSpan DepositEndTime { get; set; }
        public string DeliveryType { get; set; }
        public string UserIdOrder { get; set; }
        public string CoursierIdOrder { get; set; }
        public long PickUpAddress { get; set; }
        public long DepositAddress { get; set; }
        public long BillingAddress { get; set; }

        public Address BillingAddressNavigation { get; set; }
        public ApplicationUser CoursierIdOrderNavigation { get; set; }
        public Address DepositAddressNavigation { get; set; }
        public Address PickUpAddressNavigation { get; set; }
        public ApplicationUser UserIdOrderNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Letter> Letter { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Parcel> Parcel { get; set; }
    }
}
