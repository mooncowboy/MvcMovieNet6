using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMovie.Models;
using WpfMovie.Services;

namespace WpfMovie.Tests
{
    internal class MovieStateManagerTests
    {
        private MovieStateManager stateManager;

        [SetUp]
        public void SetUp()
        {
            stateManager = new MovieStateManager();
        }

        [Test]
        public void Serialize_ShouldCreateString()
        {
            // Arrange
            string title = "Test Title";
            string description = "Test Description";
            var aMovie = new Movie
            {
                Title = title,
                Description = description
            };

            // Act
            var movieString = stateManager.Serialize(aMovie);

            // Assert
            Assert.NotNull(movieString);
            Assert.IsNotEmpty(movieString);
        }

        [Test]
        public void Deserialize_ShouldCreateMovie()
        {
            // Arrange
            // Create a JSON-based serialized string (base64 encoded)
            var testJson = "{\"Title\":\"Test Title\",\"Description\":\"Test Description\"}";
            var testBytes = System.Text.Encoding.UTF8.GetBytes(testJson);
            var movieString = Convert.ToBase64String(testBytes);
            
            // Act
            var newMovie = stateManager.Deserialize(movieString);
            
            // Assert
            Assert.IsNotNull(newMovie);
            Assert.That(newMovie.Title, Is.EqualTo("Test Title"));
            Assert.That(newMovie.Description, Is.EqualTo("Test Description"));
        }

        [Test]
        public void Serialize_IsIdempotent()
        {
            // Arrange
            string title = "Test Title";
            string description = "Test Description";
            var aMovie = new Movie
            {
                Title = title,
                Description = description
            };

            // Act
            var movieString = stateManager.Serialize(aMovie);
            var newMovie = stateManager.Deserialize(movieString);

            // Assert
            Assert.IsNotNull(newMovie);
            Assert.That(aMovie.Title, Is.EqualTo(newMovie.Title));
            Assert.That(aMovie.Description, Is.EqualTo(newMovie.Description));
        }
    }
}
