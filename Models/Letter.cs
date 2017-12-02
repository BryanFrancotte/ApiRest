using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApiRest.Models
{
    public partial class Letter
    {
        public long LetterId { get; set; }
        public bool IsImportant { get; set; }
        public long OrderNumberLetter { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Order OrderNumberLetterNavigation { get; set; }
    }
}
