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

        [Fact]
        public void ReturnsAllFeedbackFromDatabase()
        {
            // Arrange
            using (var context = new HealthContext(InMemoryDatabaseContext()))
            {
                var feedbackService = new FeedbackService(context);
                var comment1 = "Test Comment 1";
                var rating1 = Rating.Satisfied;

                var comment2 = "Test Comment 2";
                var rating2 = Rating.Perfect;

                feedbackService.SaveFeedback(comment1, rating1);
                feedbackService.SaveFeedback(comment2, rating2);

                // Act
                var allFeedback = feedbackService.GetAllFeedback();

                // Assert
                Assert.Equal(2, allFeedback.Count());

                var feedback1 = allFeedback.ElementAt(0);
                var feedback2 = allFeedback.ElementAt(1);

                Assert.Equal(comment1, feedback1.Comment);
                Assert.Equal(rating1, feedback1.Rating);

                Assert.Equal(comment2, feedback2.Comment);
                Assert.Equal(rating2, feedback2.Rating);

                //Clear
                ClearDatabase(InMemoryDatabaseContext());
            }
        }
    }
}