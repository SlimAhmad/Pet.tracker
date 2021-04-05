using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Pet.Tracker.Core
{
    public static class AppSetting
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public static string ClientDataStoreConnection
        {
            get => AppSettings.GetValueOrDefault(nameof(ClientDataStoreConnection), "PetTracker.db");
            set => AppSettings.AddOrUpdateValue(nameof(ClientDataStoreConnection), value);
        }
        public static string PetTrackerServer
        {
            get => AppSettings.GetValueOrDefault(nameof(PetTrackerServer), "https://337ab562488b.ngrok.io");
            set => AppSettings.AddOrUpdateValue(nameof(PetTrackerServer), value);
        }


        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }
}
