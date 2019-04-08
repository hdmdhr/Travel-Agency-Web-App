using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        [DisplayName("First Name")]
        public string CustFirstName { get; set; }
        [DisplayName("Last Name")]
        public string CustLastName { get; set; }
        [DisplayName("Address")]
        public string CustAddress { get; set; }
        [DisplayName("City")]
        public string CustCity { get; set; }
        [DisplayName("Province")]
        public string CustProv { get; set; }
        [DisplayName("Zip Code")]
        public string CustPostal { get; set; }
        [DisplayName("Country")]
        public string CustCountry { get; set; }
        [DisplayName("Home Phone")]
        public string CustHomePhone { get; set; }
        [DisplayName("Business Phone")]
        public string CustBusPhone { get; set; }
        [DisplayName("Email")]
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
