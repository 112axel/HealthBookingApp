using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class SheduleService
    {
        private HealthContext context { get; set; }
        private static int amountOfFutureWeeks = 4;
        private static Days defualtDays = Days.WeekDay;
        public SheduleService(HealthContext context)
        {
            this.context = context;
        }


        /// <summary>
        /// returns a list of sheules for next 4 weeks
        /// </summary>
        /// <returns></returns>
        public List<Schedule> GetSchedules(Staff staff)
        {
            List<Schedule> shedules = staff.Schedule;

            if (shedules.Count() < amountOfFutureWeeks)
            {
                foreach (var num in Enumerable.Range(0, amountOfFutureWeeks - shedules.Count()))
                {
                    var lastDay = shedules.LastOrDefault();
                    var date = lastDay == null? GetCurerrentMonday():lastDay.WeekDate.AddDays(7);
                    shedules.Add(new Schedule { WeekDate = date, Days = defualtDays });
                }
                context.SaveChanges();
            }
            return shedules;
        }

        private DateTime GetCurerrentMonday()
        {
            var today = DateTime.Now;
            return new GregorianCalendar().AddDays(today, -((int)today.DayOfWeek) + 1);
        }

    }
}
