using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class Patient 
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public string AccountId { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
