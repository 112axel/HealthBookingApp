using HealthCare.Core;
using HealthCare.Data;
using HealthCare.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Tests
{
    public class HealthCareUnitTests
    {
        //Create database for testing.
        public DbContextOptions<HealthContext> InMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<HealthContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return options;
        }
        //Clear test database. method used at end of tests.
        private void ClearDatabase(DbContextOptions<HealthContext> options)
        {
            using (var context = new HealthContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        //Unit tests
        [Fact]
        public void AddFeedbackTest()
        {
            // Arrange
            using (var context = new HealthContext(InMemoryDatabaseContext()))
            {
                var feedbackService = new FeedbackService(context);
                var comment = "Test Comment";
                var rating = Rating.Satisfied;

                // Act
                feedbackService.SaveFeedback(comment, rating);

                // Assert
                var savedFeedback = context.Reviews.FirstOrDefault();
                Assert.NotNull(savedFeedback);
                Assert.Equal(comment, savedFeedback.Comment);
                Assert.Equal(rating, savedFeedback.Rating);

                //Clear
                ClearDatabase(InMemoryDatabaseContext());
            }
        }
    }
}