using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class DoctorPageService
    {
        private readonly HealthContext healthContext;

        public DoctorPageService(HealthContext healthContext)
        {
            this.healthContext = healthContext;
        }

        public void UpdateStaff(Staff currentUser, string firstName, string lastName, int? age, string email, string phoneNumber)
        {
            if (currentUser != null)
            {
                // Update properties of the existing patient with the new values
                currentUser.Account.FirstName = firstName;
                currentUser.Account.LastName = lastName;
                currentUser.Account.Age = age;
                currentUser.Account.Email = email;
                currentUser.Account.PhoneNumber = phoneNumber;

                // Mark the entity as modified
                healthContext.Entry(currentUser).State = EntityState.Modified;

                // Save changes to the database
                healthContext.SaveChanges();
            }
            else
            {
                // Handle if the patient is not found
                throw new ArgumentException("Staff not found");
            }
        }
    }
}
