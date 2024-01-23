using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class DoctorAvailabilityService
{
    private readonly HealthContext _dbContext;

    public DoctorAvailabilityService(HealthContext dbContext)
    {
        _dbContext = dbContext;
    }
    private DateTime GetNextMonday()
    {
        DateTime today = DateTime.Today;
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
        DateTime nextMonday = today.AddDays(daysUntilMonday);

        return nextMonday;
    }
    public Schedule GetAvailability(int doctorId)
    {
        Schedule availability = _dbContext.Staff
             .Include(x => x.Schedule)
             .Where(x => x.Id == doctorId)
             .SelectMany(x => x.Schedule).First(x => x.WeekDate == GetNextMonday());

        return availability;
    }

    public void SaveAvailability(int doctorId, Days days, bool state)
    {
        var availability = GetAvailability(doctorId);
        if (state == true)
        {
            availability.Days |= days;
        }
        else
        {
            availability.Days &= ~days;
        }

        _dbContext.SaveChanges();
    }
}
