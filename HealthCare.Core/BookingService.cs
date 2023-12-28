using System;
using HealthCare.Domain.Models;
namespace HealthCare.Core
{
    public class BookingService
    {
        private List<Appointment> appointments = new();

        public BookingService()
        {
            // mock data
            appointments.Add(new() { Id = 1, DateTime = DateTime.Now.AddHours(2), Service = "General Checkup" });
            appointments.Add(new() { Id = 2, DateTime = DateTime.Now.AddHours(4), Service = "Vaccination" });

        }

        public IEnumerable<Appointment> GetBookings()
        {
            return appointments;
        }

        public void AddBooking(Appointment booking)
        {
            appointments.Add(booking);
        }
    }
}

