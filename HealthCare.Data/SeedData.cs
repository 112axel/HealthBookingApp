using HealthCare.Domain.Models;
using HealthCare.Domain.Enums;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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

        private void SaveSeedStaff(HealthContext context)
        {
            List<Staff> seedStaff = new()
            {
                CreateStaff("Kalle", "Anka", Role.Allmänläkare),
                CreateStaff("Anna", "Karlson", Role.Distriktssköterska),
            };

            foreach(var staff in seedStaff){
                context.Add(staff);
            }
        }


        private Staff CreateStaff(string firstName, string lastName, Role role)
        {
            return new Staff
            {
                Account = CreateAccount(firstName, lastName),
                Role = role,
                Schedule = new Schedule {
                    Days = Days.WeekDay,
                    WeekDate = GetNextMonday()
                },
            };
        }

        private Account CreateAccount(string firstName, string lastName)
        {
            var account = new Account { FirstName = firstName, LastName = lastName };

            string userName = $"{firstName.ToLower()}.{lastName.ToLower()}";

            userStore.SetUserNameAsync(account, userName, CancellationToken.None);

            userManager.CreateAsync(account,"P@ssword1");
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
