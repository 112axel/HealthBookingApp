using HealthCare.Core;
using HealthCare.Data;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Tests
{
    public class FeedbackServiceTests
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
                Patient user = new Patient()
                {
                    Account = new Account()
                    {
                        FirstName = "first",
                        LastName = "last",
                        UserName = "user",
                    }
                };
                var comment = "Test Comment";
                var rating = Rating.Satisfied;

                // Act
                feedbackService.SaveFeedback(user, comment, rating);

                // Assert
                var savedFeedback = context.Reviews.FirstOrDefault();
                Assert.NotNull(savedFeedback);
                Assert.Equal(user.Account.UserName, savedFeedback.Patient.Account.UserName);
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
                Patient user = new Patient()
                {
                    Account = new Account()
                    {
                        FirstName = "first",
                        LastName = "last",
                        UserName = "user",
                    }
                };
                var comment1 = "Test Comment 1";
                var rating1 = Rating.Satisfied;

                var comment2 = "Test Comment 2";
                var rating2 = Rating.Perfect;

                feedbackService.SaveFeedback(user, comment1, rating1);
                feedbackService.SaveFeedback(user, comment2, rating2);

                // Act
                var allFeedback = feedbackService.GetAllFeedback();

                // Assert
                Assert.Equal(2, allFeedback.Count());

                var feedback1 = allFeedback.ElementAt(1);
                var feedback2 = allFeedback.ElementAt(0);

                Assert.Equal(comment1, feedback1.Comment);
                Assert.Equal(rating1, feedback1.Rating);
                Assert.Equal(user.Account.UserName, feedback1.Patient.Account.UserName);

                Assert.Equal(comment2, feedback2.Comment);
                Assert.Equal(rating2, feedback2.Rating);
                Assert.Equal(user.Account.UserName, feedback2.Patient.Account.UserName);

                //Clear
                ClearDatabase(InMemoryDatabaseContext());
            }
        }
    }
}