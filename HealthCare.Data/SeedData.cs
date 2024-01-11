using HealthCare.Domain.Models;
using HealthCare.Domain.Enums;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using HealthCare.Data;

namespace HealthCare.WebApp.Data
{
    public class SeedData
    {
        private UserManager<Account> userManager { get; set; }
        IUserStore<Account> userStore { get; set; }

        public SeedData(UserManager<Account> userManager, IUserStore<Account> userStore)
        {
            this.userManager = userManager;
            this.userStore = userStore;

        }

        public async Task SeedDataToDB(HealthContext context)
        {
            List<Staff> seedStaff = new()
            {
                CreateStaff("Kalle", "Anka", Role.Allmänläkare),
                CreateStaff("Anna", "Karlson", Role.Distriktssköterska),
            };

            foreach (var staff in seedStaff)
            {
                context.Staff.Add(staff);
                context.SaveChanges();
                await userManager.AddToRoleAsync(staff.Account, "Doctor");
            }

            List<Patient> seedPatients = new()
            {
                CreatePatinet("Farbror","Anka"),
                CreatePatinet("Kalle","Lind")
            };

            //every patient will have these 5 meetings per day for a whole week with one staff
            //Assumed apointment length of 30 min
            List<DateTime> apointmetTimes = new()
            {
                GetNextMonday().AddHours(8),
                GetNextMonday().AddHours(8).AddMinutes(30),

                GetNextMonday().AddHours(9),
                GetNextMonday().AddHours(9).AddMinutes(30),

                GetNextMonday().AddHours(12),
            };



            for (int i = 0; i < seedPatients.Count; i++)
            {
                context.Patients.Add(seedPatients[i]);
                context.SaveChanges();
                await userManager.AddToRoleAsync(seedPatients[i].Account, "Patient");
                //generate appointments for patient
                for (int day = 0; day < 7; day++)
                {
                    foreach (var time in apointmetTimes)
                    {
                        context.Appointments.Add(CreateAppointment(time.AddDays(day), seedStaff[i], seedPatients[i]));
                    }
                    context.SaveChanges();
                }
            }



            context.SaveChanges();
        }

        private Appointment CreateAppointment(DateTime dateTime, Staff doctor, Patient patient)
        {
            return new Appointment
            {
                DateTime = dateTime,
                Patient = patient,
                Staff = doctor,
                Service = "Test Checkup"
            };
        }


        private Staff CreateStaff(string firstName, string lastName, Role role)
        {
            return new Staff
            {
                Account = CreateAccount(firstName, lastName),
                Role = role,
                Schedule = new Schedule
                {
                    Days = Days.WeekDay,
                    WeekDate = GetNextMonday()
                },
            };
        }

        private Patient CreatePatinet(string firstName, string lastName)
        {
            return new Patient
            {
                Account = CreateAccount(firstName, lastName),
            };
        }

        private Account CreateAccount(string firstName, string lastName)
        {
            var account = new Account { FirstName = firstName, LastName = lastName };

            string userName = $"{firstName.ToLower()}.{lastName.ToLower()}@test.com";

            userStore.SetUserNameAsync(account, userName, CancellationToken.None);

            userManager.CreateAsync(account, "P@ssword1");
            return account;
        }

        private DateTime GetNextMonday()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextMonday = today.AddDays(daysUntilMonday);

            return nextMonday;
        }
    }
}
