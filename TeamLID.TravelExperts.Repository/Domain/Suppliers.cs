using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            ProductsSuppliers = new HashSet<ProductsSuppliers>();
            SupplierContacts = new HashSet<SupplierContacts>();
        }

        public int SupplierId { get; set; }
        public string SupName { get; set; }

        public virtual ICollection<ProductsSuppliers> ProductsSuppliers { get; set; }
        public virtual ICollection<SupplierContacts> SupplierContacts { get; set; }
    }
}
