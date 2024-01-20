using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class DoctorAvailability
    {
        public int Id { get; set; } // Primary key for DoctorAvailability
        public string DoctorId { get; set; } // Assuming a string identifier for the doctor
        public Staff Doctor { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<string> TimeSlots { get; set; }
    }

    public class TimeSlot
    {
        public int Id { get; set; } // Primary key for TimeSlot
        public string Slot { get; set; }
        public int DoctorAvailabilityId { get; set; } // Foreign key to DoctorAvailability
        public DoctorAvailability DoctorAvailability { get; set; }
    }

}
