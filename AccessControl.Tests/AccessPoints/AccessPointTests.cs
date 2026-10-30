using AccessControl.Domain.Entities.AccessPoints;
using AccessControl.Domain.Exceptions;

namespace AccessControl.Tests.AccessPoints
{
    [TestClass]
    public sealed class AccessPointTests
    {
        [TestMethod]
        public void Constructor_WithValidData_CreatesEnabledAccessPoint()
        {
            AccessPoint accessPoint = new(" Recepción principal ", AccessPointType.Reception, "Entrada del edificio");

            Assert.AreEqual("Recepción principal", accessPoint.Name);
            Assert.AreEqual(AccessPointType.Reception, accessPoint.Type);
            Assert.IsTrue(accessPoint.IsEnabled);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("AB")]
        public void Constructor_WithInvalidName_ThrowsBusinessRuleException(string name)
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new AccessPoint(name, AccessPointType.Warehouse));
        }

        [TestMethod]
        public void Constructor_WithInvalidType_ThrowsBusinessRuleException()
        {
            Assert.ThrowsExactly<BussinesRuleException>(() => new AccessPoint("Bodega 3", (AccessPointType)99));
        }

        [TestMethod]
        public void Disable_WhenAccessPointIsEnabled_DisablesIt()
        {
            AccessPoint accessPoint = new("Bodega 1", AccessPointType.Warehouse);

            accessPoint.Disable();

            Assert.IsFalse(accessPoint.IsEnabled);
        }

        [TestMethod]
        public void Disable_WhenAccessPointIsAlreadyDisabled_ThrowsBusinessRuleException()
        {
            AccessPoint accessPoint = new("Bodega 1", AccessPointType.Warehouse);
            accessPoint.Disable();

            Assert.ThrowsExactly<BussinesRuleException>(() => accessPoint.Disable());
        }

        [TestMethod]
        public void Enable_WhenAccessPointIsDisabled_EnablesIt()
        {
            AccessPoint accessPoint = new("Bodega 1", AccessPointType.Warehouse);
            accessPoint.Disable();

            accessPoint.Enable();

            Assert.IsTrue(accessPoint.IsEnabled);
        }

        [TestMethod]
        public void Enable_WhenAccessPointIsAlreadyEnabled_ThrowsBusinessRuleException()
        {
            AccessPoint accessPoint = new("Bodega 1", AccessPointType.Warehouse);

            Assert.ThrowsExactly<BussinesRuleException>(() => accessPoint.Enable());
        }
    }
}
