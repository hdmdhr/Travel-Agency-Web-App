using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        [Required,DisplayName("Province"),RegularExpression(@"^[A-Za-z]{2}$")]
        public string CustProv { get; set; }
        [Required,DisplayName("Postal Code"),RegularExpression(@"^[A-Za-z]\d[A-Za-z][-\ ]?\d[A-Za-z]\d$")]
        public string CustPostal { get; set; }
        [DisplayName("Country")]
        public string CustCountry { get; set; }
        [DisplayName("Home Phone"),RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string CustHomePhone { get; set; }
        [Required, DisplayName("Business Phone"), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string CustBusPhone { get; set; }
        [Required,DisplayName("Email")]
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }
        [Required, MinLength(5), MaxLength(50)]
        public string Username { get; set; }
        [DataType(DataType.Password), MinLength(5), MaxLength(50)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password)),DisplayName("Password Confirm")]
        public string PasswordConfirm { get; set; }
    }
}
