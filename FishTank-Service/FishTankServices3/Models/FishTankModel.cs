using System.Collections.Generic;
using System.Linq;

namespace FishtankServices.Models
{
    public class Fishtank
    {
        // List of abstract fish so we can add new fish types without changing the tank class
        public readonly List<Fish> _shoalingFish;

        // Constructor
        public Fishtank()
        {
            this._shoalingFish = new List<Fish>();
        }

        /// <summary>
        /// Add a fish to the tank
        /// </summary>
        /// <param name="fish"></param>
        public void AddFish(Fish fish)
        {
            _shoalingFish.Add(fish);
            return;
        }

        public List<Fish> GetShoalingFish()
        {
            List<Fish> resFish = new List<Fish>(_shoalingFish);
            return resFish;
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
        /// Get the total food requirments based on the number of fish
        /// </summary>
        /// <returns></returns>
        public double Feed()
        {
            double totalFood = _shoalingFish.Sum(fish => fish.GetFoodRequirements());
            return totalFood;
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
    }
}
