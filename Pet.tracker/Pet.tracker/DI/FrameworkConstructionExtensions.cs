using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace Pet.Tracker
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for Fasetto Word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddPetTrackerViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of Settings view model
            construction.Services.AddSingleton<SettingsViewModel>();

            // Bind to a single instance of Home view model
            construction.Services.AddSingleton<HomeViewModel>();

            // Bind to a single instance of Missing pet view model
            construction.Services.AddSingleton<MissingPetsViewModel>();

            // Return the construction for chaining
            return construction;
        }
    }
}
