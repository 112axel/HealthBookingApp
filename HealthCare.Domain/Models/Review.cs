using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public string Comment { get; set; }
        public Rating Rating { get; set; }
    }
}
