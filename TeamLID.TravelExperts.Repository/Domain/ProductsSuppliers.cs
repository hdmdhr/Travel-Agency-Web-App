using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class ProductsSuppliers
    {
        public ProductsSuppliers()
        {
            BookingDetails = new HashSet<BookingDetails>();
            PackagesProductsSuppliers = new HashSet<PackagesProductsSuppliers>();
        }

        public int ProductSupplierId { get; set; }
        public int? ProductId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<BookingDetails> BookingDetails { get; set; }
        public virtual ICollection<PackagesProductsSuppliers> PackagesProductsSuppliers { get; set; }
    }
}
