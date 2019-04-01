using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Customers
    {
        public Customers()
        {
            Bookings = new HashSet<Bookings>();
            CreditCards = new HashSet<CreditCards>();
            CustomersRewards = new HashSet<CustomersRewards>();
        }

        public int CustomerId { get; set; }
        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustProv { get; set; }
        public string CustPostal { get; set; }
        public string CustCountry { get; set; }
        public string CustHomePhone { get; set; }
        public string CustBusPhone { get; set; }
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Agents Agent { get; set; }
        public virtual ICollection<Bookings> Bookings { get; set; }
        public virtual ICollection<CreditCards> CreditCards { get; set; }
        public virtual ICollection<CustomersRewards> CustomersRewards { get; set; }
    }
}
