using HealthCare.Data;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class AccountService
    {
        HealthContext context { get; set; }
        public AccountService(HealthContext context )
        {
            this.context = context;
        }

        public Account GetAccountByName(string accountName) 
        {
            var account = context.Accounts
                .Include(x => x.Patient)
                .Include(x => x.Staff)
                .First(x => x.UserName == accountName);
            if(account.AccountType() == typeof(Staff))
            {
                account.Staff.Appointments = context.Appointments
                    .Include(x=>x.Patient)
                    .ThenInclude(x => x.Account)
                    .Where(x=>x.Staff.Id ==account.Staff.Id)
                    .OrderBy(x=>x.DateTime)
                    .ToList();
            }
            else if(account.AccountType() == typeof(Patient))
            {
                account.Patient.Appointments = context.Appointments
                    .Include(x=>x.Staff)
                    .ThenInclude(x => x.Account)
                    .Where(x=>x.Patient.Id ==account.Patient.Id)
                    .OrderBy(x=>x.DateTime)
                    .ToList();
            }
            return account;

        }
    }
}
