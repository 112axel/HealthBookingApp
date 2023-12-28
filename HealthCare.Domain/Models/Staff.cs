using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public Role Role { get; set; }
        public List<Appointment> Appointments { get; set; }
        public Schedule Schedule { get; set; }
        
    }
}
