using System.Threading.Tasks;
using FishtankServices.Services.Models;
using FishTankServices.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Tests.Controllers
{
    [TestClass]
    public class FishtankServiceTests
    {
        private readonly FishtankService _fishtankService;

        public FishtankServiceTests()
        {
            _fishtankService = new FishtankService();
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

            var addFishRes= await _fishtankService.AddFish(FishType.Goldfish, "");
            Assert.IsNotNull(addFishRes);
        }

        /// <summary>
        /// Add a fish to a null tank
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddFishToNullTank()
        {
            var addFishRes = await _fishtankService.AddFish(FishType.Goldfish, "");
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

            await _fishtankService.AddFish(FishType.Goldfish, "");
            await _fishtankService.AddFish(FishType.Angelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");
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

            await _fishtankService.AddFish(FishType.Goldfish, "");
            await _fishtankService.AddFish(FishType.Angelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");
            var res = await _fishtankService.Feed();

            Assert.AreEqual(res, 0.6);
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

            await _fishtankService.AddFish(FishType.Goldfish, "");
            await _fishtankService.AddFish(FishType.Angelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");
            await _fishtankService.AddFish(FishType.Goldfish, "");
            await _fishtankService.AddFish(FishType.Angelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");
            await _fishtankService.AddFish(FishType.Goldfish, "");
            await _fishtankService.AddFish(FishType.Angelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");

            var res = await _fishtankService.Feed();

            Assert.AreEqual(res, 2.1);
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

            await _fishtankService.AddFish(FishType.Goldfish, "");
            await _fishtankService.AddFish(FishType.Angelfish, "");
            await _fishtankService.AddFish(FishType.Babelfish, "");
            var res = await _fishtankService.Feed();

            Assert.AreEqual(res, 0.6);
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

        /// <summary>
        /// Check that we can add anmes and that the names exist after
        /// </summary>
        [TestMethod]
        public async Task AddFishWithNames()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Goldfish, "Scott");
            await _fishtankService.AddFish(FishType.Angelfish, "Robert");
            await _fishtankService.AddFish(FishType.Babelfish, "Alexander");

            var names = await _fishtankService.GetFishNames();
            var nameOneExist = names.Contains("Scott");
            var nameTwoExists = names.Contains("Robert");
            var nameThreeExists = names.Contains("Alexander");

            Assert.IsTrue(nameOneExist);
            Assert.IsTrue(nameTwoExists);
            Assert.IsTrue(nameThreeExists);
        }

        // Check that we can remove a fish by type
        [TestMethod]
        public async Task RemoveFishByType()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Babelfish, "Alexander");
            var msgString = await _fishtankService.RemoveFishByType(FishType.Babelfish);
            var res = string.IsNullOrEmpty(msgString);
            Assert.IsTrue(res);
        }

        // Check that we get an error when we tried to remove a fish by type that does not exist
        [TestMethod]
        public async Task RemoveFishByTypeDoesNotExist()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Babelfish, "Alexander");
            var msgString = await _fishtankService.RemoveFishByType(FishType.Angelfish);
            var res = string.IsNullOrEmpty(msgString);
            Assert.IsFalse(res);
        }

        // Check that we can remove a fish by type
        [TestMethod]
        public async Task RemoveFishByName()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Babelfish, "Alexander");
            var msgString = await _fishtankService.RemoveFishByName("Alexander");
            var res = string.IsNullOrEmpty(msgString);
            Assert.IsTrue(res);
        }

        // Check that we get an error when we tried to remove a fish by type that does not exist
        [TestMethod]
        public async Task RemoveFishByNameDoesNotExist()
        {
            // Create the first fishtank
            var createRes = await _fishtankService.CreateFishtank();
            Assert.IsTrue(createRes);

            await _fishtankService.AddFish(FishType.Babelfish, "Robert");
            var msgString = await _fishtankService.RemoveFishByName("Alexander");
            var res = string.IsNullOrEmpty(msgString);
            Assert.IsFalse(res);
        }





    }
}
