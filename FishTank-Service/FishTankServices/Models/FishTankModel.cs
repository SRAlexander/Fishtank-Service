using System;
using System.Collections.Generic;
using System.Linq;

namespace FishtankServices.Models
{
    public class Fishtank
    {
        // List of abstract fish so we can add new fish types without changing the tank class
        private readonly List<Fish> _shoalingFish;

        // Constructor
        public Fishtank()
        {
            _shoalingFish = new List<Fish>();
        }

        public List<Fish> GetShoalingFish()
        {
            List<Fish> resFish = new List<Fish>(_shoalingFish);
            return resFish;
        }

        /// <summary>
        /// Add a fish to the tank
        /// </summary>
        /// <param name="fish"></param>
        public void AddFish(Fish fish)
        {
            _shoalingFish.Add(fish);
        }

        /// <summary>
        /// Remove a fish by its type from the tank
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        public bool RemoveFish(Fish fish)
        {
            if (_shoalingFish.IndexOf(fish) == -1) return false;
            _shoalingFish.Remove(fish);
            return true;
        }

        /// <summary>
        /// Remove the first fish of a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveFishByType(string type)
        {
            var removingFish = _shoalingFish.FirstOrDefault(fish => fish.GetFishType() == type);
            if (removingFish == null) return false;
            _shoalingFish.Remove(removingFish);
            return true;
        }

        /// <summary>
        /// Remove the first fish of a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveFishByName(string name)
        {
            var removingFish = _shoalingFish.FirstOrDefault(fish => string.Equals(name, fish.GetFishName(), StringComparison.CurrentCultureIgnoreCase));
            if (removingFish == null) return false;
            _shoalingFish.Remove(removingFish);
            return true;
        }
        
        /// <summary>
        /// Get a list of all fish names
        /// </summary>
        /// <returns></returns>
        public List<string> GetFishNames()
        {
            return _shoalingFish.Select(fish => fish.GetFishName()).ToList();
        }

        /// <summary>
        /// Return a list of info strings based on each fish type
        /// </summary>
        /// <returns></returns>
        public List<string> Details()
        {
            var groupedFish = _shoalingFish.GroupBy(fish => fish.GetFishType());
            var res = (from @group in groupedFish let msg = @group.Count() == 1 ? "There is " : "There are " select msg + @group.Count() + " " + @group.Key + " in the tank").ToList();

            if (res.Any() == false)
            {
                res.Add("No fish are living in the tank");
            }
            return res;
        }

        /// <summary>
        /// Get the total food requirments based on the number of fish
        /// </summary>
        /// <returns></returns>
        public double Feed()
        {
            double totalFood = _shoalingFish.Sum(fish => fish.GetFoodRequirements());
            return totalFood;
        }

    }
}
