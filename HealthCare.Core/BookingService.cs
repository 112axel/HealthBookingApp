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

        public List<Staff> GetStaff(Role role)
        {
            return context.Staff
                .Include(x=>x.Appointments)
                .Include(x=>x.Schedule)
                .Where(x=>x.Role == role).ToList();
        }

        public List<Appointment>[] GetAvailableTimeSlots(Staff staff,DateTime weekStart)
        {
            List<Appointment> allTimeSlots = new();

            return AllApointmentsInWeek(staff.Appointments,staff.Schedule.First(x=>x.WeekDate == weekStart));

        }

        private List<Appointment>[] AllApointmentsInWeek(List<Appointment> bookedAppointment, Schedule weekSchedule)
        {
            List<Appointment>[] all = new List<Appointment>[7];
            //Set time to 8
            var dateTime = weekSchedule.WeekDate.AddHours(8);


            foreach (var day in Enumerable.Range(0, 7))
            {
                var flagValue = day == 0 ? 0 : Math.Pow(day, 2);
                if (weekSchedule.Days.HasFlag((Days)flagValue))
                {
                    all[day] = AllApointmentsInDay(bookedAppointment, dateTime.AddDays(day));
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
                    currentTime = currentTime.AddMinutes(30);
                }
            }
            return appointments;
        }
    }
}

