using AccessControl.Domain.Entities.Credentials;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Tests.Credentials
{
    [TestClass]
    public sealed class CredentialTests
    {
        [TestMethod]
        public void Constructor_WithValidData_CreatesActiveCredential()
        {
            Guid employeeId = Guid.NewGuid();

            Credential credential = new(" crd-1001 ", employeeId);

            Assert.AreEqual("CRD-1001", credential.Code);
            Assert.AreEqual(employeeId, credential.EmployeeId);
            Assert.AreEqual(CredentialStatus.Active, credential.Status);
            Assert.IsTrue(credential.CanBeUsed);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("CR1")]
        [DataRow("CRD_1001")]
        [DataRow("CRD 1001")]
        [DataRow("CRD-100000000000000001")]
        public void Constructor_WithInvalidCode_ThrowsBusinessRuleException(string code)
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new Credential(code, Guid.NewGuid()));
        }

        [TestMethod]
        public void Constructor_WithEmptyEmployeeId_ThrowsBusinessRuleException()
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new Credential("CRD-1001", Guid.Empty));
        }

        [TestMethod]
        public void Block_WhenCredentialIsActive_SetsBlockedStatus()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());

            credential.Block("Reporte de pérdida del carné");

            Assert.AreEqual(CredentialStatus.Blocked, credential.Status);
            Assert.AreEqual("Reporte de pérdida del carné", credential.StatusReason);
            Assert.IsNotNull(credential.BlockedAt);
            Assert.IsFalse(credential.CanBeUsed);
        }

        [TestMethod]
        public void Block_WithoutReason_ThrowsBusinessRuleException()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());

            Assert.ThrowsExactly<BussinesRuleException>(() => credential.Block(" "));
        }

        [TestMethod]
        public void Block_WhenCredentialIsAlreadyBlocked_ThrowsBusinessRuleException()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());
            credential.Block("Pérdida");

            Assert.ThrowsExactly<BussinesRuleException>(() => credential.Block("Pérdida"));
        }

        [TestMethod]
        public void Unblock_WhenCredentialIsBlocked_SetsActiveStatus()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());
            credential.Block("Pérdida");

            credential.Unblock();

            Assert.AreEqual(CredentialStatus.Active, credential.Status);
            Assert.IsNull(credential.StatusReason);
            Assert.IsNull(credential.BlockedAt);
        }

        [TestMethod]
        public void Unblock_WhenCredentialIsActive_ThrowsBusinessRuleException()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());

            Assert.ThrowsExactly<BussinesRuleException>(() => credential.Unblock());
        }

        [TestMethod]
        public void Revoke_WhenCredentialIsBlocked_SetsRevokedStatus()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());
            credential.Block("Pérdida");

            credential.Revoke("Retiro del empleado");

            Assert.AreEqual(CredentialStatus.Revoked, credential.Status);
            Assert.AreEqual("Retiro del empleado", credential.StatusReason);
            Assert.IsNotNull(credential.RevokedAt);
        }

        [TestMethod]
        public void Revoke_WhenCredentialIsAlreadyRevoked_ThrowsBusinessRuleException()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());
            credential.Revoke("Retiro del empleado");

            Assert.ThrowsExactly<BussinesRuleException>(() => credential.Revoke("Retiro del empleado"));
        }

        [TestMethod]
        public void Unblock_WhenCredentialIsRevoked_ThrowsBusinessRuleException()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());
            credential.Revoke("Retiro del empleado");

            Assert.ThrowsExactly<BussinesRuleException>(() => credential.Unblock());
        }
    }
}
