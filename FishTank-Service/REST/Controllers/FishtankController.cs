using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FishtankServices.Models;
using FishtankServices.Services;
using UnitTests.Models;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Fishtank controller using dependency injection
    /// </summary>
    [RoutePrefix("v1/fishtank")]
    public class FishtankController : ApiController
    {

        // injected interface
        private readonly IFishtankService _fishtankService;

        public FishtankController(IFishtankService fishtankService)
        {
            _fishtankService = fishtankService;
        }

        /// <summary>
        /// Get the fishtank contents
        /// Returns a bad request if it does not exist
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<Fish>))]
        public async Task<IHttpActionResult> Get()
        {
            var fish = await _fishtankService.GetFishTankContents();

            if (fish == null)
            {
                return BadRequest("No fishtank exists, create one first.");
            }

            return Ok(fish);
        }

        /// <summary>
        /// Get a user friendly breakdown list of strings to display
        /// Returns a bad request if it does not exist
        /// </summary>
        /// <returns></returns>
        [Route("details")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetails()
        {
            var tank = await _fishtankService.GetTankDetails();

            if (tank == null)
            {
                return BadRequest("No fishtank exists, create one first.");
            }

            return Ok(tank);
        }

        /// <summary>
        /// Create a fishtank if it's not been created already
        /// Return a no content if successful
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpGet]
        public async Task<IHttpActionResult> Create()
        {
            var created = await _fishtankService.CreateFishtank();

            if (!created)
            {
                return BadRequest("A fishtank already exists, remove the existing one first.");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Destroy the fishtank 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveTank()
        {
            var removed = await _fishtankService.RemoveFishtank();

            if (removed == false)
            {
                return BadRequest("No fishtank exist to delete.");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get the amount of food required to feed all the fish
        /// </summary>
        /// <returns></returns>
        [Route("feed")]
        [HttpGet]
        public async Task<IHttpActionResult> Feed()
        {
            var foodRequired = await _fishtankService.Feed();
            return Ok(foodRequired);
        }

        /// <summary>
        /// Add a fish by given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("fish")]
        [HttpPost]
        public async Task<IHttpActionResult> AddFish(FishType type)
        {
            var fish = await _fishtankService.AddFish(type);
            if (fish == null)
            {
                return BadRequest("Unable to add fish");
            }

            return Ok(fish);

        }

        /// <summary>
        /// Remove a fish by its given type
        /// No content is returned if successful
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("fish")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveFish(FishType type)
        {
            var removedMsg = await _fishtankService.RemoveFishByType(type);
            if (string.IsNullOrEmpty(removedMsg))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return BadRequest(removedMsg);
        }


    }
}
