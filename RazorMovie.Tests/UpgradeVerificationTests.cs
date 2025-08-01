using System.Reflection;

namespace RazorMovie.Tests
{
    [TestClass]
    public sealed class UpgradeVerificationTests
    {
        [TestMethod]
        public void Verify_DotNet8_TargetFramework()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof(Program));
            
            // Act
            var targetFrameworkAttribute = assembly?.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>();
            
            // Assert
            Assert.IsNotNull(targetFrameworkAttribute);
            Assert.IsTrue(targetFrameworkAttribute.FrameworkName.StartsWith(".NETCoreApp,Version=v8.0"));
        }
        
        [TestMethod]
        public void Verify_HtmlSanitizer8_Namespace()
        {
            // Arrange & Act
            var htmlSanitizer = new Ganss.Xss.HtmlSanitizer();
            
            // Assert - No exception should be thrown for the correct namespace
            Assert.IsNotNull(htmlSanitizer);
        }
    }
}