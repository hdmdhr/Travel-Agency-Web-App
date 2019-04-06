using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TeamLID.TravelExperts.App.Models
{
    public class PackagesModel
    {
        public int PackageId { get; set; }

        [DisplayName("Title")]
        public string PkgName { get; set; }

        [DisplayName("Start Date")]
        public DateTime? PkgStartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? PkgEndDate { get; set; }

        [DisplayName("Description")]
        public string PkgDesc { get; set; }

        [DisplayName("Base Price")]
        public string PkgBasePrice { get; set; }
    }
}
