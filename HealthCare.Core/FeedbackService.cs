using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace HealthCare.Core
{
    public class FeedbackService
    {
        private readonly HealthContext _context;

        public FeedbackService(HealthContext context)
        {
            _context = context;
        }

        public void SaveFeedback(Patient currentUser, string comment, Rating rating)
        {
            var review = new Review { Patient = currentUser, Comment = comment, Rating = rating };
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public IEnumerable<Review> GetAllFeedback()
        {
            return _context.Reviews.Include(f => f.Patient).ThenInclude(p => p.Account).OrderByDescending(r => r.Rating).ToList();
        }
    }
}