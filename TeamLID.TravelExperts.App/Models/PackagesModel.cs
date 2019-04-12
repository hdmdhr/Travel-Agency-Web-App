/*
Author: Ibraheem
Purpose: Get packages
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamLID.TravelExperts.App.Models
{
    /// <summary>
    /// Packages model.
    /// </summary>
    public class PackagesModel
    {
        public int PackageId { get; set; }

        [DisplayName("Title")]
        public string PkgName { get; set; }

        [DataType(DataType.Date), DisplayName("Start Date")]
        public DateTime? PkgStartDate { get; set; }

        [DataType(DataType.Date), DisplayName("End Date")]
        public DateTime? PkgEndDate { get; set; }

        [DisplayName("Description")]
        public string PkgDesc { get; set; }

        [DisplayName("Price (CAD$)")]
        public string PkgBasePrice { get; set; }
    }
}
