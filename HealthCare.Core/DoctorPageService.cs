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

        public Staff GetDoctorByUserId(string accountId)
        {
            // Retrieve the doctor by ID from the database
            return healthContext.Staff
                .Include(s => s.Account)
                .Include(s => s.Appointments)
                .FirstOrDefault(s => s.Account.Id == accountId);
        }

    //    public void SetDoctorWeeklySchedule(string doctorAccountId, List<Schedule> weeklySchedule)
    //    {
    //        // Logic to update the doctors weekly schedule in the database
    //        // You might want to store the schedule in a separate table or extend the Staff model
    //    }

    //    public List<Schedule> GetDoctorWeeklySchedule(string doctorAccountId)
    //    {
    //        // Logic to retrieve the doctors weekly schedule from the database
    //        // This should return a list of the doctors availability for the upcoming week
    //        return new List<Schedule>(); // Placeholder
    //    }
    
    //public bool IsDoctorAvailable(string doctorAccountId, DayOfWeek dayOfWeek)
    //    {
    //        var doctorSchedule = GetDoctorWeeklySchedule(doctorAccountId);

    //        // Check if the doctor is available on the specified day
    //        return doctorSchedule.Any(schedule => schedule.Days.HasFlag(dayOfWeek));
    //    }

        public List<Schedule> GetDoctorAvailability(string doctorAccountId)
        {
            // Placeholder: return a list of sample availability for testing
            var sampleAvailability = new List<Schedule>
        {
            new Schedule { Id = 1, WeekDate = DateTime.Today, Days = Days.Monday },
            new Schedule { Id = 2, WeekDate = DateTime.Today, Days = Days.Wednesday },
            // Add more sample data as needed
        };

            return sampleAvailability;
        }

        public void SetDoctorAvailability(string doctorAccountId, List<Schedule> weeklySchedule)
        {
            // Logic to update the doctor's availability in the database
            // You might want to store the availability in a separate table or extend the Staff model
        }
    }
}
