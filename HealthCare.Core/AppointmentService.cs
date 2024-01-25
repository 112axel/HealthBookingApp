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

        public void UpdateAppointment(Appointment updatedAppointment)
        {
            var existingAppointment = _context.Appointments
                .Include(a => a.Staff)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == updatedAppointment.Id);

            if (existingAppointment != null)
            {
                existingAppointment.DateTime = updatedAppointment.DateTime;
                existingAppointment.Service = updatedAppointment.Service;

                _context.SaveChanges();
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            var appointmentToDelete = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);

            if (appointmentToDelete != null)
            {
                _context.Appointments.Remove(appointmentToDelete);
                _context.SaveChanges();
            }
        }
    }
}

