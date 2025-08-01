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
            Assert.That(movieString, Is.Not.Null);
            Assert.That(movieString, Is.Not.Empty);
        }

        [Test]
        public void Deserialize_ShouldCreateMovie()
        {
            // Arrange
            var movieString = "AAEAAAD/////AQAAAAAAAAAMAgAAAD9XcGZNb3ZpZSwgVmVyc2lvbj0xLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGwFAQAAABVXcGZNb3ZpZS5Nb2RlbHMuTW92aWUCAAAABXRpdGxlC2Rlc2NyaXB0aW9uAQECAAAABgMAAAAKVGVzdCBUaXRsZQYEAAAAEFRlc3QgRGVzY3JpcHRpb24L";
            
            // Act
            var newMovie = stateManager.Deserialize(movieString);
            
            // Assert
            Assert.That(newMovie, Is.Not.Null);
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
            Assert.That(newMovie, Is.Not.Null);
            Assert.That(newMovie.Title, Is.EqualTo(aMovie.Title));
            Assert.That(newMovie.Description, Is.EqualTo(aMovie.Description));
        }
    }
}
