using System.Collections.Generic;
using System.Threading.Tasks;
using FishtankServices.Models;
using FishTankServices.Dto;
using UnitTests.Models;

namespace FishtankServices.Services
{
    public interface IFishtankService
    {
        /// <summary>
        /// Will only return a fishtank or null
        /// </summary>
        /// <returns></returns>
        Task<List<FishDto>> GetFishTankContents();

        /// <summary>
        /// Create a fishtank and return it only if no fishtank exists
        /// otherwise return null
        /// </summary>
        /// <returns></returns>
        Task<bool> CreateFishtank();

        /// <summary>
        /// Remove a fishtank by setting it to null
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveFishtank();

        /// <summary>
        /// Returns the ammount of food required for the fish
        /// </summary>
        /// <returns></returns>
        Task<double> Feed();

        /// <summary>
        /// Get Fishtank details
        /// </summary>
        Task<List<string>> GetTankDetails();

        /// <summary>
        /// Add a fish to the tank
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<Fish> AddFish(FishType type);

        /// <summary>
        /// Remove a fish of a certain type
        /// </summary>
        /// <param name="type"></param>
        Task<string> RemoveFishByType(FishType type);

    }
}
