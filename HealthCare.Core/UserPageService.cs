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

            public Patient GetPatientById(string accountId)
            {
                // Use Entity Framework Core to retrieve the patient by ID from the database
                return healthContext.Patients
                    .Include(p => p.Account)
                    .Include(p => p.Appointments)
                    .FirstOrDefault(p => p.Account.Id == accountId);
            }
    }
}
