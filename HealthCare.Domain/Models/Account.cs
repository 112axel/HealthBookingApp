using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class Account:IdentityUser
    {
        //Id is implemented by IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
