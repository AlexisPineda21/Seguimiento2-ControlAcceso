using AccessControl.Domain.Entities.AccessLogs;
using AccessControl.Domain.Entities.Credentials;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Tests.AccessLogs
{
    [TestClass]
    public sealed class AccessLogTests
    {
        [TestMethod]
        public void Authorize_WithCredential_CreatesAuthorizedLog()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());
            Guid accessPointId = Guid.NewGuid();

            AccessLog accessLog = AccessLog.Authorize(credential, accessPointId, MovementType.Entry);

            Assert.AreEqual(AccessResult.Authorized, accessLog.Result);
            Assert.AreEqual("CRD-1001", accessLog.CredentialCode);
            Assert.AreEqual(credential.Id, accessLog.CredentialId);
            Assert.AreEqual(credential.EmployeeId, accessLog.EmployeeId);
            Assert.AreEqual(accessPointId, accessLog.AccessPointId);
            Assert.AreEqual(MovementType.Entry, accessLog.MovementType);
            Assert.IsNull(accessLog.RejectionReason);
        }

        [TestMethod]
        public void Reject_WithUnknownCredential_CreatesRejectedLogWithoutEmployee()
        {
            AccessLog accessLog = AccessLog.Reject("crd-9999", Guid.NewGuid(), MovementType.Entry, "La credencial no existe.");

            Assert.AreEqual(AccessResult.Rejected, accessLog.Result);
            Assert.AreEqual("CRD-9999", accessLog.CredentialCode);
            Assert.AreEqual("La credencial no existe.", accessLog.RejectionReason);
            Assert.IsNull(accessLog.CredentialId);
            Assert.IsNull(accessLog.EmployeeId);
        }

        [TestMethod]
        public void Reject_WithKnownCredential_KeepsCredentialAndEmployee()
        {
            Credential credential = new("CRD-1003", Guid.NewGuid());
            credential.Block("Pérdida");

            AccessLog accessLog = AccessLog.Reject(credential.Code, Guid.NewGuid(), MovementType.Entry, "La credencial está bloqueada.", credential);

            Assert.AreEqual(AccessResult.Rejected, accessLog.Result);
            Assert.AreEqual(credential.Id, accessLog.CredentialId);
            Assert.AreEqual(credential.EmployeeId, accessLog.EmployeeId);
        }

        [TestMethod]
        public void Reject_WithoutReason_ThrowsBusinessRuleException()
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => AccessLog.Reject("CRD-1001", Guid.NewGuid(), MovementType.Entry, ""));
        }

        [TestMethod]
        public void Reject_WithEmptyAccessPointId_ThrowsBusinessRuleException()
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => AccessLog.Reject("CRD-1001", Guid.Empty, MovementType.Exit, "Motivo"));
        }

        [TestMethod]
        public void Authorize_WithInvalidMovementType_ThrowsBusinessRuleException()
        {
            Credential credential = new("CRD-1001", Guid.NewGuid());

            Assert.ThrowsExactly<BussinesRuleException>(() => AccessLog.Authorize(credential, Guid.NewGuid(), (MovementType)5));
        }
    }
}
