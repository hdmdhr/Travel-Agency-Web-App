using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Agents
    {
        public Agents()
        {
            Customers = new HashSet<Customers>();
        }

        public int AgentId { get; set; }
        public string AgtFirstName { get; set; }
        public string AgtMiddleInitial { get; set; }
        public string AgtLastName { get; set; }
        public string AgtBusPhone { get; set; }
        public string AgtEmail { get; set; }
        public string AgtPosition { get; set; }
        public int? AgencyId { get; set; }

        public virtual Agencies Agency { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
