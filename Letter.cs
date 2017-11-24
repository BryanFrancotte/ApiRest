using System;
using System.Collections.Generic;

namespace ApiRest
{
    public partial class Letter
    {
        public long LetterId { get; set; }
        public bool IsImportant { get; set; }
        public long OrderNumberLetter { get; set; }

        public Order OrderNumberLetterNavigation { get; set; }
    }
}
