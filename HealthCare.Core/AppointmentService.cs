using HealthCare.Data;
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
                .Include(p => p.Appointments)
                .ThenInclude(p => p.Staff)
                .ThenInclude(p => p.Account)
                .FirstOrDefault(p => p.Account.Id == accountId);
        }
        public Staff GetDoctorById(string accountId)
        {
            // Retrieve the doctor by ID from the database
            return _context.Staff
                .Include(p => p.Account)
                .Include(p => p.Appointments)
                .ThenInclude(p => p.Patient)
                .ThenInclude(p => p.Account)
                .FirstOrDefault(p => p.Account.Id == accountId);
        }

        public bool TimeSlotAvailableForDoctor(string doctorAccountId, DateTime requestedDateTime)
        {
            var doctor = GetDoctorById(doctorAccountId);

            var existingAppointments = _context.Appointments
                .Include(a => a.Staff)
                .Where(a => a.Staff.Id == doctor.Id && a.DateTime.Date == requestedDateTime.Date)
                .ToList();

            // Check for overlapping appointments
            var TimeSlotAvailable = !existingAppointments.Any(a =>
                requestedDateTime >= a.DateTime && requestedDateTime < a.DateTime.AddMinutes(30));

            return TimeSlotAvailable;
        }

    }
}

