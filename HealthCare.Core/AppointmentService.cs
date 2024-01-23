﻿using HealthCare.Data;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace HealthCare.Core
{
	public class AppointmentService
	{
        private readonly HealthContext _context;

        public AppointmentService(HealthContext context)
        {
            _context = context;
        }

        public Patient GetPatientById(string accountId)
        {
            // Retrieve the patient by ID from the database
            return _context.Patients
                .Include(p => p.Account)
                .Include(p => p.Appointments.OrderBy(a => a.DateTime))
                .ThenInclude(p => p.Staff)
                .ThenInclude(p => p.Account)
                .FirstOrDefault(p => p.Account.Id == accountId);
        }
        public Staff GetDoctorById(string accountId)
        {
            // Retrieve the doctor by ID from the database
            return _context.Staff
                .Include(p => p.Account)
                .Include(p => p.Appointments.OrderBy(a => a.DateTime))
                .ThenInclude(p => p.Patient)
                .ThenInclude(p => p.Account)
                .FirstOrDefault(p => p.Account.Id == accountId);
        }
    }
}

