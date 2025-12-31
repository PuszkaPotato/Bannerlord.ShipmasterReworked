namespace Bannerlord.ShipmasterReworked.Settings
{
    public static class ConfigCache
    {
        public static float TravelXpMultiplier { get; private set; }
        public static bool TravelXpDebug { get; private set; }
        public static int RammingXpBase { get; private set; }
        public static float RammingXpQualityFactor { get; private set; }
        public static bool RammingXpDebug { get; private set; }
        public static bool EnableStormTravelXp { get; private set; }
        public static float StormTravelXpMultiplier { get; private set; }

        public static void Refresh(MCMConfig settings)
        {
            TravelXpMultiplier = settings.TravelXpMultiplier;
            TravelXpDebug = settings.TravelXpDebug;
            
            RammingXpBase = settings.RammingXpBase;
            RammingXpQualityFactor = settings.RammingXpQualityFactor;
            RammingXpDebug = settings.RammingXpDebug;

            EnableStormTravelXp = settings.EnableStormTravelXp;
            StormTravelXpMultiplier = settings.StormTravelXpMultiplier;
        }
    }
}
