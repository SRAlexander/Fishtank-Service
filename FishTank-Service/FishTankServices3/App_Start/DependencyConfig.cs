using FishtankServices.Services;
using FishtankServices.Services.Models;
using SimpleInjector;

// register dependecy injected classes
namespace FishTankServices
{
    public static class DependencyConfig
    {
        public static void Setup(Container container)
        {
            container.Register<IFishtankService, FishtankService>(Lifestyle.Singleton);
        }
    }
}
