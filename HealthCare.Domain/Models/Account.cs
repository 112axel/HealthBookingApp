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
        public int? Age { get; set; }
        public Patient? Patient { get; set; }
        public Staff? Staff { get; set; }

        public Type AccountType()
        {
            if (Patient is not null)
            {
                return typeof(Patient);
            }
            else if (Staff is not null)
            {
                return typeof(Staff);
            }
            throw new Exception("The account is not dose not have Patient or Staff");
        }

    }
}
