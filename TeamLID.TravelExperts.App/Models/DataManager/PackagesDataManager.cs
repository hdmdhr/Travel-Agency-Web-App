using System;
using System.Collections.Generic;
using System.Linq;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models.DataManager
{
    public class PackagesDataManager
    {
        public static List<Packages> GetAll()
        {
            var context = new TravelExpertsContext();

            var packages = context.Packages.ToList();

            return packages;
        }

    }
}
