using AccessControl.Domain.Entities.Employees.ValueObjects;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Tests.Employees
{
    [TestClass]
    public sealed class DocumentNumberTests
    {
        [TestMethod]
        public void Constructor_WithValidValue_TrimsAndUppercases()
        {
            DocumentNumber documentNumber = new(" ab12345 ");

            Assert.AreEqual("AB12345", documentNumber.Value);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("1234")]
        [DataRow("1.036.625.478")]
        [DataRow("103 662 5478")]
        [DataRow("123456789012345678901")]
        public void Constructor_WithInvalidValue_ThrowsBusinessRuleException(string value)
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new DocumentNumber(value));
        }
    }
}
