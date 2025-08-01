using System.Reflection;

namespace MvcMovie.Tests
{
    public class UpgradeVerificationTests
    {
        [Test]
        public void Verify_DotNet8_TargetFramework()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof(Program));
            
            // Act
            var targetFrameworkAttribute = assembly?.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>();
            
            // Assert
            Assert.That(targetFrameworkAttribute, Is.Not.Null);
            Assert.That(targetFrameworkAttribute.FrameworkName, Does.StartWith(".NETCoreApp,Version=v8.0"));
        }
        
        [Test]
        public void Verify_EntityFrameworkCore8_IsLoaded()
        {
            // Arrange & Act
            var efAssembly = Assembly.LoadFrom("Microsoft.EntityFrameworkCore.dll");
            var version = efAssembly.GetName().Version;
            
            // Assert
            Assert.That(version, Is.Not.Null);
            Assert.That(version.Major, Is.EqualTo(8));
        }
    }
}