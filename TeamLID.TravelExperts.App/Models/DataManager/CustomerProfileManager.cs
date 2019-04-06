using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models.DataManager
{
    public class CustomerProfileManager
    {
        //public static List<> GetAllBookingsByCustomer(int )

        //public static List<Asset> GetAllByAssetType(int assetTypeId)
        //{
        //    var context = new AssetsContext();
        //    var assets = context.Assets.
        //        Include(asset => asset.AssetType).
        //        Where(ast => ast.Id == assetTypeId).ToList();

        //    return assets;
        //}

        public static Customers Find(int id)
        {
            var context = new TravelExpertsContext();

            // find the domain entity with this context that has the same id as 
            // the entity passed
            var customer = context.Customers.
                Include(agt => agt.Agent).
                SingleOrDefault(ast => ast.CustomerId == id);

            return customer;

        }

    }
}
