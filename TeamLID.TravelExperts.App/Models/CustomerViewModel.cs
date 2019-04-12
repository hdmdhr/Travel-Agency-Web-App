/*
Authors: Ibraheem & Dong Ming
Purpose: Get customer
*/
using System;
using System.ComponentModel;

namespace TeamLID.TravelExperts.App.Models
{
    /// <summary>
    /// Customer view model.
    /// </summary>
    public class CustomerViewModel
    {
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

        [DisplayName("Postal Code")]
        public string CustPostal { get; set; }

        [DisplayName("Country")]
        public string CustCountry { get; set; }

        [DisplayName("Home Phone")]
        public string CustHomePhone { get; set; }

        [DisplayName("Business Phone")]
        public string CustBusPhone { get; set; }

        [DisplayName("Email")]
        public string CustEmail { get; set; }

        [DisplayName("Agent")]
        public string AgentId { get; set; }

        [DisplayName("Username")]
        public string Username { get; set; }
    }
}
