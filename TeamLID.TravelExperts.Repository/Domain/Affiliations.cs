using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Affiliations
    {
        public Affiliations()
        {
            SupplierContacts = new HashSet<SupplierContacts>();
        }

        public string AffilitationId { get; set; }
        public string AffName { get; set; }
        public string AffDesc { get; set; }

        public virtual ICollection<SupplierContacts> SupplierContacts { get; set; }
    }
}
