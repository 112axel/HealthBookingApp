using HealthCare.Data;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class UserPageService
    {
        private readonly HealthContext healthContext;

        public UserPageService(HealthContext healthContext)
        {
            this.healthContext = healthContext;
        }

        public void UpdateUser(Staff? currentDoctor, Patient? currentPatient, string firstName, string lastName, int? age, string email, string phoneNumber)
        {
            if (currentDoctor != null)
            {
                // Update properties of the existing patient with the new values
                currentDoctor.Account.FirstName = firstName;
                currentDoctor.Account.LastName = lastName;
                currentDoctor.Account.Age = age;
                currentDoctor.Account.Email = email;
                currentDoctor.Account.PhoneNumber = phoneNumber;

                // Mark the entity as modified
                healthContext.Entry(currentDoctor).State = EntityState.Modified;

                // Save changes to the database
                healthContext.SaveChanges();
            }
            else if (currentPatient != null)
            {
                // Update properties of the existing patient with the new values
                currentPatient.Account.FirstName = firstName;
                currentPatient.Account.LastName = lastName;
                currentPatient.Account.Age = age;
                currentPatient.Account.Email = email;
                currentPatient.Account.PhoneNumber = phoneNumber;

                // Mark the entity as modified
                healthContext.Entry(currentPatient).State = EntityState.Modified;

                // Save changes to the database
                healthContext.SaveChanges();
            }
            else
            {
                // Handle if the user is not found
                throw new ArgumentException("User not found");
            }
        }
    }
}