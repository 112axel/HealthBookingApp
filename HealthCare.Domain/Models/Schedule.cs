using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime WeekDate { get; set; } //date of the first day of the week
        public Days Days { get; set; }
    }
}
