using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using NUnit.Framework;

namespace MvcMovie.IntegrationTests
{
    public class MvcMovieBrowserTests
    {
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory?.Dispose();
        }

        [Test]
        public async Task HomePageLoads()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("Movie App"));
        }

        [Test]
        public async Task HelloWorldControllerResponds()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/HelloWorld");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("Hello from our View Template!"));
        }

        [Test]
        public async Task HelloWorldWelcomeWithParametersResponds()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/HelloWorld/Welcome?name=Test&id=1");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("Hello Test"));
            Assert.That(content, Does.Contain("ID: 1"));
        }

        [Test]
        public async Task MoviesPageLoads()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Movies");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("Movies") | Does.Contain("Index"));
        }
    }
}