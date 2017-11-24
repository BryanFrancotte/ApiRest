using System;
using System.Collections.Generic;

namespace ApiRest
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
        public string UserIdOrder { get; set; }
        public string CoursierIdOrder { get; set; }
        public long PickUpAddress { get; set; }
        public long DepositAddress { get; set; }
        public long BillingAddress { get; set; }

        public Address BillingAddressNavigation { get; set; }
        public AspNetUsers CoursierIdOrderNavigation { get; set; }
        public Address DepositAddressNavigation { get; set; }
        public Address PickUpAddressNavigation { get; set; }
        public AspNetUsers UserIdOrderNavigation { get; set; }
        public ICollection<Letter> Letter { get; set; }
        public ICollection<Parcel> Parcel { get; set; }
    }
}
