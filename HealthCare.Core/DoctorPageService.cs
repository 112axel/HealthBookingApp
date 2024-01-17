﻿using HealthCare.Data;
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

        public Staff GetDoctorByUserId(string accountId)
        {
            // Retrieve the doctor by ID from the database
            return healthContext.Staff
                .Include(s => s.Account)
                .Include(s => s.Appointments)
                .FirstOrDefault(s => s.Account.Id == accountId);
        }

        public void SetDoctorAvailability(string doctorAccountId, List<Schedule> weeklySchedule)
        {
            // Logic to update the doctor's availability in the database
            // You might want to store the availability in a separate table or extend the Staff model
        }
    }
}
