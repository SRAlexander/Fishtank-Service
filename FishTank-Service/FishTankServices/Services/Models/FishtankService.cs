using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishtankServices.Models;
using FishTankServices.Dto;
using FishTankServices.Models;

namespace FishtankServices.Services.Models
{
    /// <summary>
    /// Singleton designed injection class so we can build a rest layer on top
    /// </summary>
    public class FishtankService : IFishtankService
    {
        private Fishtank _fishTank;

        /// <summary>
        /// Create a fishtank and return it only if no fishtank exists
        /// otherwise return null
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateFishtank()
        {
            if (_fishTank == null)
            {
                _fishTank = new Fishtank();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Destroy the fish tank, return true if successful
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveFishtank()
        {
            if (_fishTank == null) return false;
            _fishTank = null;
            return true;
        }

        /// <summary>
        /// Will only return a fishtank or null
        /// </summary>
        /// <returns></returns>
        public async Task<List<FishDto>> GetFishTankContents()
        {
            //TODO : future ammendment:- implement automapper

            return _fishTank?.GetShoalingFish()
                .Select(fish => new FishDto()
                {
                    Type = fish.GetFishType(),
                    FoodRequirement = fish.GetFoodRequirements()
                })
                .ToList();
        }

        /// <summary>
        /// Get the details of the tanks contents
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetTankDetails()
        {
            return _fishTank?.Details();
        }

        /// <summary>
        /// Get the amount of food required by the fish in the tank
        /// </summary>
        /// <returns></returns>
        public async Task<double> Feed()
        {
            return _fishTank == null ? 0 : Math.Round(_fishTank.Feed(), 1);
        }

        /// <summary>
        /// Add a fish to the tank
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Fish> AddFish(FishType type, string name)
        {
            if (_fishTank == null) return null;

            Fish newFish = CreateFishFromEnum(type, name);
            if (newFish == null)
            {
                return null;
            }

            _fishTank.AddFish(newFish);
            return newFish;
        }

        public async Task<List<string>> GetFishNames()
        {
            return _fishTank?.GetFishNames();
        }

        /// <summary>
        /// Remove a fish of a given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<string> RemoveFishByType(FishType type)
        {
            if (_fishTank == null) return null;

            var removed = _fishTank.RemoveFishByType(type.ToString());

            if (removed)
            {
                return "";
            }

            return "No " + type + " are in the tank to remove";
        }

        /// <summary>
        /// Remove a fish of a given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<string> RemoveFishByName(string name)
        {
            var removed = _fishTank.RemoveFishByName(name);

            if (removed)
            {
                return "";
            }

            return "Could not find " + name + " in the tank to remove";
        }

        /// <summary>
        /// Create a fish based on a given enum type value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected Fish CreateFishFromEnum(FishType type, string name)
        {
            Fish newFish = null;
            switch (type)
            {
                case FishType.Goldfish:
                {
                    newFish = new GoldFish(name);
                    break;
                }
                case FishType.Angelfish:
                {
                    newFish = new AngelFish(name);
                    break;
                }
                case FishType.Babelfish:
                {
                    newFish = new BabelFish(name);
                    break;
                }
            }

            return newFish;
        }
    }
}
