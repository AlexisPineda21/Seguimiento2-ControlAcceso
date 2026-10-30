using AccessControl.Domain.Entities.Employees.ValueObjects;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Tests.Employees
{
    [TestClass]
    public sealed class EmailTests
    {
        [TestMethod]
        public void Constructor_WithValidEmail_NormalizesToLowerCase()
        {
            Email email = new("  Laura.Gomez@Empresa.COM ");

            Assert.AreEqual("laura.gomez@empresa.com", email.Value);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("laura.gomez")]
        [DataRow("laura@empresa")]
        [DataRow("laura gomez@empresa.com")]
        public void Constructor_WithInvalidEmail_ThrowsBusinessRuleException(string value)
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new Email(value));
        }

        [TestMethod]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            Email first = new("laura.gomez@empresa.com");
            Email second = new("LAURA.GOMEZ@empresa.com");

            Assert.AreEqual(first, second);
        }
    }
}
