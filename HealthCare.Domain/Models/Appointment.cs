using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        //Patient and staff is optional becose of cascading delete
        public Patient? Patient { get; set; }
        public Staff? Staff { get; set; } 
        public DateTime DateTime { get; set; }
        public string Service { get; set; }
    }
}
