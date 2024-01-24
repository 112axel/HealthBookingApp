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
}
