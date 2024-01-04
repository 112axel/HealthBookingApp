using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.WebApp.Data
{

    public class HealthContext : DbContext
    {
        public HealthContext(DbContextOptions<HealthContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Staff> Staff { get; set; }

    }
}
