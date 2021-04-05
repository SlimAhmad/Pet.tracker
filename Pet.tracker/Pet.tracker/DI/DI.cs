using Dna;
using Pet.Tracker.Core;

namespace Pet.Tracker
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class DI
    {

        /// <summary>
        /// A shortcut to access the <see cref="IClientDataStore"/> service
        /// </summary>
        public static IClientDataStore ClientDataStore => Framework.Service<IClientDataStore>();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="SettingsViewModel"/>
        /// </summary>
        public static SettingsViewModel ViewModelSettings => Framework.Service<SettingsViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="HomeViewModel"/>
        /// </summary>
        public static HomeViewModel ViewModelHome => Framework.Service<HomeViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="MissingPetsViewModel"/>
        /// </summary>
        public static MissingPetsViewModel ViewModelNotification => Framework.Service<MissingPetsViewModel>();
    }
}
