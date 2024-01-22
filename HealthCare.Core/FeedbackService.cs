using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
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

        public void SaveFeedback(string comment, Rating rating)
        {
            var review = new Review { Comment = comment, Rating = rating };
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public IEnumerable<Review> GetAllFeedback()
        {
            return _context.Reviews.OrderByDescending(review => review.Rating).ToList();
        }
    }
}