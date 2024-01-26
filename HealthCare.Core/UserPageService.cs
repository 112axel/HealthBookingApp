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

        public void UpdateUser(Account currentUser, string firstName, string lastName, int? age, string email, string phoneNumber)
        {
            if (currentUser != null)
            {
                // Update properties of the existing user with the new values
                currentUser.FirstName = firstName;
                currentUser.LastName = lastName;
                currentUser.Age = age;
                currentUser.Email = email;
                currentUser.PhoneNumber = phoneNumber;

                // Mark the entity as modified
                healthContext.Entry(currentUser).State = EntityState.Modified;

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