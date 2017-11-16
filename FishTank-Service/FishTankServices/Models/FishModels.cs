namespace FishtankServices.Models
{

    /// generic abstract fish class so we can build true fish from it
    public abstract class Fish
    {
        protected double FoodRequirement;
        protected string Type;
        protected string Name;

        protected Fish()
        {
            Type = "Fish";
            FoodRequirement = 0.0;
            Name = "Bruce";
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

        public string GetFishName()
        {
            return Name;
        }
    }

    /// <summary>
    /// Goldfish class inheriting from Fish
    /// </summary>
    public class GoldFish : Fish
    {
        public GoldFish(string name)
        {
            this.Type = "Goldfish";
            this.FoodRequirement = 0.1;
            this.Name = !string.IsNullOrEmpty(name) ? name : "Goldie";
        }

    }

    /// <summary>
    /// Angelfish class inheriting from Fish
    /// </summary>
    public class AngelFish : Fish
    {
        public AngelFish(string name)
        {
            this.Type = "Angelfish";
            this.FoodRequirement = 0.2;
            this.Name = !string.IsNullOrEmpty(name) ? name : "Gabriel";
        }
    }

    /// <summary>
    /// Babelfish class inheriting from Fish
    /// </summary>
    public class BabelFish : Fish
    {
        public BabelFish(string name)
        {
            this.Type = "Babelfish";
            this.FoodRequirement = 0.3;
            this.Name = !string.IsNullOrEmpty(name) ? name : "Bable";
        }
    }
}
