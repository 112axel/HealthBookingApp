using System;
using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace HealthCare.Core
{
    public class BookingService
    {
        HealthContext context { get; set; }
        public BookingService(HealthContext context)
        {
            this.context = context;
        }

        public void BookApointment(Staff staff, Patient patient, Appointment appointment,string subject)
        {
            appointment.Staff = staff;
            appointment.Patient = patient;
            appointment.Service = subject;
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }

        public List<Staff> GetStaff(Role? role)
        {
            if(role is null)
            {
                return new();
            }

            return context.Staff
                .Include(x=>x.Appointments)
                .Include(x=>x.Schedule)
                .Include(x=>x.Account)
                .Where(x=>x.Role == role).ToList();
        }


        public Dictionary<DateTime, List<Appointment>> AllApointmentsInWeek(Staff staff, Schedule weekSchedule)
        {
            var bookedAppointment = staff.Appointments;
            Dictionary<DateTime, List<Appointment>> all = new();
            //Set time to 8
            var weekStart = weekSchedule.WeekDate.AddHours(8);


            foreach (var day in Enumerable.Range(0, 7))
            {
                var flagValue = Math.Pow(2, day);
                var dayStart = weekStart.AddDays(day);

                if (weekSchedule.Days.HasFlag((Days)flagValue))
                {
                    all.Add(dayStart,AllApointmentsInDay(bookedAppointment, dayStart));
                }
                else
                {
                    all.Add(dayStart, new());
                }
            }

            return all;
        }

        private List<Appointment> AllApointmentsInDay(List<Appointment> bookedAppointment, DateTime startTime)
        {
            List<Appointment> appointments = new();
            var currentTime = startTime;
            while (currentTime <= startTime.AddHours(8))
            {
                if(!bookedAppointment.Exists(x=>x.DateTime == currentTime))
                {
                    appointments.Add(new Appointment { DateTime = currentTime, Service = "" });
                }
                currentTime = currentTime.AddMinutes(30);
            }
            return appointments;
        }
    }
}

