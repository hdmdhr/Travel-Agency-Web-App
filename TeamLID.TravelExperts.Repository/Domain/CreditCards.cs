using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class CreditCards
    {
        public int CreditCardId { get; set; }
        public string Ccname { get; set; }
        public string Ccnumber { get; set; }
        public DateTime Ccexpiry { get; set; }
        public int CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
    }
}
