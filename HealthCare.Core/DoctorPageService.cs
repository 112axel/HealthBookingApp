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
    public class DoctorPageService
    {
        private readonly HealthContext healthContext;

        public DoctorPageService(HealthContext healthContext)
        {
            this.healthContext = healthContext;
        }

        //public Doctor GetDoctorById(string accountId)
        //{
        //    // Retrieve the doctor by ID from the database
        //    return healthContext.Doctors
        //        .Include(p => p.Account)
        //        .Include(p => p.Schedule)
        //        .FirstOrDefault(p => p.Account.Id == accountId);
        //}
    }
}
