using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FishtankServices.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTests.Controllers;

// Futher example of testing the the rest layers using moq to insert Mock services
namespace UnitTests.Tests.Controllers
{
    [TestClass]
    public class FishtankControllerTests
    {

        /// <summary>
        /// Check that no content is returned is the creation of a fishtank was successful
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankCreationTrueTest()
        {
            var fishtankService = new Mock<IFishtankService>();
            fishtankService.Setup(s => s.CreateFishtank()).ReturnsAsync(true);
            var controller = new FishtankController(fishtankService.Object);

            var response = await controller.Create();
            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));
            Assert.AreEqual((int)((StatusCodeResult)response).StatusCode, (int) HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Check that a bad request is returned if the creation failed due to already existing
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankCreationFalseTest()
        {
            var fishtankService = new Mock<IFishtankService>();
            fishtankService.Setup(s => s.CreateFishtank()).ReturnsAsync(false);
            var controller = new FishtankController(fishtankService.Object);

            var response = await controller.Create();
            var responseType = response.GetType();
            Assert.AreEqual(responseType.Name, "BadRequestErrorMessageResult");
        }

        /// <summary>
        /// Check that no content is returned is the creation of a fishtank was successful
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankRemovalTrueTest()
        {
            var fishtankService = new Mock<IFishtankService>();
            fishtankService.Setup(s => s.RemoveFishtank()).ReturnsAsync(true);
            var controller = new FishtankController(fishtankService.Object);

            var response = await controller.RemoveTank();
            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));
            Assert.AreEqual((int)((StatusCodeResult)response).StatusCode, (int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Check that a bad request is returned if the creation failed due to already existing
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankRemovalFalseTest()
        {
            var fishtankService = new Mock<IFishtankService>();
            fishtankService.Setup(s => s.RemoveFishtank()).ReturnsAsync(false);
            var controller = new FishtankController(fishtankService.Object);

            var response = await controller.RemoveTank();
            var responseType = response.GetType();
            Assert.AreEqual(responseType.Name, "BadRequestErrorMessageResult");
        }






    }
}
