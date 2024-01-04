using HealthCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class UserPageService
    {
        public List<Patient> GetPatients() 
        {
            var patients = new List<Patient>
            {
                new Patient
            {
                Id = 1,
                Account = new Account { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "hashedPassword", Salt = "salt" },
                Appointments = new List<Appointment>
                {
                    new Appointment { Id = 1, DateTime = DateTime.Now, Service = "Checkup" },
                }
            },
            new Patient
            {
                Id = 2,
                Account = new Account { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", Password = "hashedPassword", Salt = "salt" },
                Appointments = new List<Appointment>
                {
                    new Appointment { Id = 2, DateTime = DateTime.Now.AddDays(7), Service = "Cleaning" },
                }
            },
            };

            return patients;
        }
    }
}
