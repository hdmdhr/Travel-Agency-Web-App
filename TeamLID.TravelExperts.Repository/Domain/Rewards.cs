using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Rewards
    {
        public Rewards()
        {
            CustomersRewards = new HashSet<CustomersRewards>();
        }

        public int RewardId { get; set; }
        public string RwdName { get; set; }
        public string RwdDesc { get; set; }

        public virtual ICollection<CustomersRewards> CustomersRewards { get; set; }
    }
}
