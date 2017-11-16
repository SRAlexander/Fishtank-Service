using System.Threading.Tasks;
using FishtankServices.Services;
using FishtankServices.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Models;

namespace UnitTests.Tests.Controllers
{
    [TestClass]
    public class FishtankServiceTests
    {
        private readonly FishtankService _fishtankService;

        public FishtankServiceTests()
        {
            this._fishtankService = new FishtankService();
        }

        /// <summary>
        /// Testing to see if we can create a fishtank and to make sure that we can't create a new one while one exists
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankCreationTest()
        {
            // Create the first fishtank
            var initialCreateRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(initialCreateRes);

            // Try to create a second, it should prevent you and return false
            var secondaryCreateRes = await _fishtankService.CreateFishtank();
            Assert.IsFalse(secondaryCreateRes); 
        }

        /// <summary>
        /// Testing to see if we can remove a tank that does not exist
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankRemoveTest()
        {
            // Try to remove a tank that does not exist
            var removeRes = await _fishtankService.RemoveFishtank();
            Assert.IsFalse(removeRes);
        }

        /// <summary>
        /// Testing to see if we can create a fishtank and remove it
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankRemoveAfterCreationTest()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            // Try to create a second, it should prevent you and return false
            var removeRes = await _fishtankService.RemoveFishtank();
            Assert.IsTrue(removeRes);
        }

        /// <summary>
        /// Make sure we get a null response if a fishtank has not been created
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetUnCreatedFishtank()
        {
            // Create the first fishtank
            var getTankRes = await _fishtankService.GetFishTankContents();
            Assert.IsNull(getTankRes);
        }

        /// <summary>
        /// Make sure we get a response if a tank has been created even if its an empty list
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetFishtankContents()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            var getTankRes = await _fishtankService.GetFishTankContents();
            Assert.IsNotNull(getTankRes);
        }

        /// <summary>
        /// Add a fish
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddFishToTank()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            var addFishRes= await _fishtankService.AddFish(FishType.Goldfish);
            Assert.IsNotNull(addFishRes);
        }

        /// <summary>
        /// Add a fish to a null tank
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddFishToNullTank()
        {
            var addFishRes = await _fishtankService.AddFish(FishType.Goldfish);
            Assert.IsNull(addFishRes);
        }

        /// <summary>
        /// Add a three fish and check to see if they have been added
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddThreeFishToTank()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Goldfish);
            await _fishtankService.AddFish(FishType.Angelfish);
            await _fishtankService.AddFish(FishType.Babelfish);
            var rescount = (await _fishtankService.GetFishTankContents()).Count;
            Assert.AreEqual(rescount, 3);
        }

        /// <summary>
        /// Add three fish and feed // Brief Specification Test
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddThreeFishAndFeed()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Goldfish);
            await _fishtankService.AddFish(FishType.Angelfish);
            await _fishtankService.AddFish(FishType.Babelfish);
            var res = await _fishtankService.Feed();

            Assert.AreEqual(res, (double) 0.6);
        }

        /// <summary>
        /// Add 10 fish and feed check
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Add10FishAndFeed()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Goldfish);
            await _fishtankService.AddFish(FishType.Angelfish);
            await _fishtankService.AddFish(FishType.Babelfish);
            await _fishtankService.AddFish(FishType.Goldfish);
            await _fishtankService.AddFish(FishType.Angelfish);
            await _fishtankService.AddFish(FishType.Babelfish);
            await _fishtankService.AddFish(FishType.Goldfish);
            await _fishtankService.AddFish(FishType.Angelfish);
            await _fishtankService.AddFish(FishType.Babelfish);
            await _fishtankService.AddFish(FishType.Babelfish);

            var res = await _fishtankService.Feed();

            Assert.AreEqual(res, (double)2.1);
        }

        /// <summary>
        /// Testing to see if checked details come back with fish information
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankDetailsTest()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Goldfish);
            await _fishtankService.AddFish(FishType.Angelfish);
            await _fishtankService.AddFish(FishType.Babelfish);
            var res = await _fishtankService.Feed();

            Assert.AreEqual(res, (double) 0.6);
            var details = await _fishtankService.GetTankDetails();

            Assert.AreEqual(details.Count, 3);
        }

        /// <summary>
        /// Testing to see if checked details come back null if a fishtank has not been created
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FishTankDetailsNoTankTest()
        {
            var details = await _fishtankService.GetTankDetails();
            Assert.IsNull(details);
        }





    }
}
