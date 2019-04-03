using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models
{
    public class SimplifiedUser
    {
        public Customers Customer { get; set; }
        [DisplayName("Password Confirm")]
        public string PasswordConfirm { get; set; }
    }
}
