using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Products
    {
        public Products()
        {
            ProductsSuppliers = new HashSet<ProductsSuppliers>();
        }

        public int ProductId { get; set; }
        public string ProdName { get; set; }

        public virtual ICollection<ProductsSuppliers> ProductsSuppliers { get; set; }
    }
}
