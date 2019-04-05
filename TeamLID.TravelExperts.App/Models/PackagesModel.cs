using System;
using System.ComponentModel;

namespace TeamLID.TravelExperts.App.Models
{
    public class PackagesModel
    {
        public int PackageId { get; set; }

        [DisplayName("Package Title")]
        public string PkgName { get; set; }

        public DateTime? PkgStartDate { get; set; }

        public DateTime? PkgEndDate { get; set; }

        public string PkgDesc { get; set; }

        [DisplayName("Base Price")]
        public string PkgBasePrice { get; set; }
    }
}
