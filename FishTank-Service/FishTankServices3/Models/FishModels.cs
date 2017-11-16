namespace FishtankServices.Models
{

    /// generic abstract fish class so we can build true fish from it
    public abstract class Fish
    {
        protected double FoodRequirement;
        protected string Type;

        protected Fish()
        {
            Type = "Fish";
            FoodRequirement = 0.0;
        }

        public virtual string GetDescription()
        {
            return "I'm a + " + Type;
        }

        public virtual double GetFoodRequirements()
        {
            return FoodRequirement;
        }

        public virtual string GetFishType()
        {
            return Type;
        }
    }

    /// <summary>
    /// Goldfish class inheriting from Fish
    /// </summary>
    public class GoldFish : Fish
    {
        public GoldFish()
        {
            this.Type = "Goldfish";
            this.FoodRequirement = 0.1;
        }

    }

    /// <summary>
    /// Angelfish class inheriting from Fish
    /// </summary>
    public class AngelFish : Fish
    {
        public AngelFish()
        {
            this.Type = "Angelfish";
            this.FoodRequirement = 0.2;
        }
    }

    /// <summary>
    /// Babelfish class inheriting from Fish
    /// </summary>
    public class BabelFish : Fish
    {
        public BabelFish()
        {
            this.Type = "Babelfish";
            this.FoodRequirement = 0.3;
        }
    }
}
