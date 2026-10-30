using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Tests.Employees
{
    [TestClass]
    public sealed class EmployeeTests
    {
        private static Employee CreateEmployee()
        {
            return new Employee(new DocumentNumber("1036625478"),
                                "Laura",
                                "Gómez",
                                new Email("laura.gomez@empresa.com"),
                                "Coordinadora de bodega",
                                Guid.NewGuid());
        }

        [TestMethod]
        public void Constructor_WithValidData_CreatesActiveEmployee()
        {
            Employee employee = CreateEmployee();

            Assert.AreNotEqual(Guid.Empty, employee.Id);
            Assert.AreEqual(EmploymentStatus.Active, employee.Status);
            Assert.IsTrue(employee.IsActive);
            Assert.AreEqual("Laura Gómez", employee.FullName);
            Assert.IsNull(employee.DeactivatedAt);
        }

        [TestMethod]
        public void Constructor_TrimsNamesAndPosition()
        {
            Employee employee = new(new DocumentNumber("71589632"),
                                    "  Andrés ",
                                    " Zapata  ",
                                    new Email("andres.zapata@empresa.com"),
                                    "  Supervisor de seguridad ",
                                    Guid.NewGuid());

            Assert.AreEqual("Andrés", employee.FirstName);
            Assert.AreEqual("Zapata", employee.LastName);
            Assert.AreEqual("Supervisor de seguridad", employee.Position);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("A")]
        public void Constructor_WithInvalidFirstName_ThrowsBusinessRuleException(string firstName)
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new Employee(new DocumentNumber("1036625478"),
                                                                            firstName,
                                                                            "Gómez",
                                                                            new Email("laura.gomez@empresa.com"),
                                                                            "Coordinadora",
                                                                            Guid.NewGuid()));
        }

        [TestMethod]
        public void Constructor_WithEmptyAreaId_ThrowsBusinessRuleException()
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new Employee(new DocumentNumber("1036625478"),
                                                                            "Laura",
                                                                            "Gómez",
                                                                            new Email("laura.gomez@empresa.com"),
                                                                            "Coordinadora",
                                                                            Guid.Empty));
        }

        [TestMethod]
        public void Update_WithValidData_ChangesEmployeeData()
        {
            Employee employee = CreateEmployee();
            Guid newAreaId = Guid.NewGuid();

            employee.Update("Laura María", "Gómez Ríos", new Email("lmgomez@empresa.com"), "Jefe de bodega", newAreaId);

            Assert.AreEqual("Laura María", employee.FirstName);
            Assert.AreEqual("Gómez Ríos", employee.LastName);
            Assert.AreEqual("lmgomez@empresa.com", employee.Email.Value);
            Assert.AreEqual("Jefe de bodega", employee.Position);
            Assert.AreEqual(newAreaId, employee.AreaId);
        }

        [TestMethod]
        public void Update_WhenEmployeeIsInactive_ThrowsBusinessRuleException()
        {
            Employee employee = CreateEmployee();
            employee.Deactivate();

            Assert.ThrowsExactly<BussinesRuleException>(() => employee.Update("Laura",
                                                                              "Gómez",
                                                                              new Email("laura.gomez@empresa.com"),
                                                                              "Coordinadora",
                                                                              Guid.NewGuid()));
        }

        [TestMethod]
        public void Deactivate_WhenEmployeeIsActive_SetsInactiveStatus()
        {
            Employee employee = CreateEmployee();

            employee.Deactivate();

            Assert.AreEqual(EmploymentStatus.Inactive, employee.Status);
            Assert.IsFalse(employee.IsActive);
            Assert.IsNotNull(employee.DeactivatedAt);
        }

        [TestMethod]
        public void Deactivate_WhenEmployeeIsAlreadyInactive_ThrowsBusinessRuleException()
        {
            Employee employee = CreateEmployee();
            employee.Deactivate();

            Assert.ThrowsExactly<BussinesRuleException>(() => employee.Deactivate());
        }
    }
}
