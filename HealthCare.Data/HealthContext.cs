﻿using HealthCare.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data
{

    public class HealthContext : IdentityDbContext<Account>
    {
        public HealthContext(DbContextOptions<HealthContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(x=>x.Account)
                .WithOne(x=>x.Patient)
                .HasForeignKey<Patient>(x=>x.AccountId);

            modelBuilder.Entity<Staff>()
                .HasOne(x=>x.Account)
                .WithOne(x=>x.Staff)
                .HasForeignKey<Staff>(x=>x.AccountId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

}
