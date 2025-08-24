using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using NUnit.Framework;

namespace RazorMovie.IntegrationTests
{
    public class RazorMovieBrowserTests
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
            Assert.That(content, Does.Contain("html") | Does.Contain("HTML"));
        }

        [Test]
        public async Task PrivacyPageLoads()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Privacy");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("Privacy") | Does.Contain("privacy"));
        }

        [Test]
        public async Task HtmlSanitizerWorksInPageRequests()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act - This tests that the application starts and can handle basic requests
            // which validates that the HtmlSanitizer dependency injection works
            var response = await client.GetAsync("/");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}