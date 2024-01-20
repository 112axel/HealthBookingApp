using HealthCare.Data;
using HealthCare.Domain.Models;

public class DoctorAvailabilityService
{
    private readonly HealthContext _dbContext;

    public DoctorAvailabilityService(HealthContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<string> GetAvailability(string doctorId, DayOfWeek dayOfWeek)
    {
        var availability = _dbContext.DoctorAvailabilities
            .SingleOrDefault(da => da.DoctorId == doctorId && da.DayOfWeek == dayOfWeek);

        return availability?.TimeSlots ?? new List<string>();
    }

    public void SaveAvailability(string doctorId, DayOfWeek dayOfWeek, List<string> timeSlots)
    {
        var availability = _dbContext.DoctorAvailabilities
            .SingleOrDefault(da => da.DoctorId == doctorId && da.DayOfWeek == dayOfWeek);

        if (availability == null)
        {
            availability = new DoctorAvailability
            {
                DoctorId = doctorId,
                DayOfWeek = dayOfWeek,
                TimeSlots = timeSlots
            };

            _dbContext.DoctorAvailabilities.Add(availability);
        }
        else
        {
            availability.TimeSlots = timeSlots;
        }

        _dbContext.SaveChanges();
    }
}
