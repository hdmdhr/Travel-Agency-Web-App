using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models
{
    public class UserViewModel
    {
        public int CustomerId { get; set; }
        [Required, DisplayName("First Name")]
        public string CustFirstName { get; set; }
        [Required, DisplayName("Last Name")]
        public string CustLastName { get; set; }
        [Required, DisplayName("Address")]
        public string CustAddress { get; set; }
        [Required, DisplayName("City")]
        public string CustCity { get; set; }
        [Required,DisplayName("Province")]
        public string CustProv { get; set; }
        [Required,DisplayName("Postal Code")]
        public string CustPostal { get; set; }
        [DisplayName("Country")]
        public string CustCountry { get; set; }
        [DisplayName("Home Phone")]
        public string CustHomePhone { get; set; }
        [Required, DisplayName("Business Phone")]
        public string CustBusPhone { get; set; }
        [Required,DisplayName("Email")]
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password)),DisplayName("Password Confirm")]
        public string PasswordConfirm { get; set; }
    }
}
