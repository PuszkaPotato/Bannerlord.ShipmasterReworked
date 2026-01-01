namespace Bannerlord.ShipmasterReworked.Settings
{
    public static class ConfigCache
    {
        public static float TravelXpMultiplier { get; private set; }
        public static bool EnableStormTravelXp { get; private set; }
        public static float StormTravelXpMultiplier { get; private set; }
        public static int RammingXpBase { get; private set; }
        public static float RammingXpQualityFactor { get; private set; }
        public static float BallistaDamageFactor { get; private set; }
        public static float BallistaDamageXpMin { get; private set; }
        public static float BallistaDamageXpMax { get; private set; }
        public static float BallistaTier1Distance { get; private set; }
        public static float BallistaTier2Distance { get; private set; }
        public static float BallistaTier3Distance { get; private set; }
        public static float BallistaTier4Distance { get; private set; }
        public static float BallistaTier1Multiplier { get; private set; }
        public static float BallistaTier2Multiplier { get; private set; }
        public static float BallistaTier3Multiplier { get; private set; }
        public static float BallistaTier4Multiplier { get; private set; }
        public static bool BallistaXpDebug { get; private set; }
        public static bool TravelXpDebug { get; private set; }
        public static bool RammingXpDebug { get; private set; }


        public static void Refresh(MCMConfig settings)
        {
            TravelXpMultiplier = settings.TravelXpMultiplier;
            EnableStormTravelXp = settings.EnableStormTravelXp;
            StormTravelXpMultiplier = settings.StormTravelXpMultiplier;

            RammingXpBase = settings.RammingXpBase;
            RammingXpQualityFactor = settings.RammingXpQualityFactor;

            BallistaDamageFactor = settings.BallistaDamageFactor;
            BallistaDamageXpMin = settings.BallistaDamageXpMin;
            BallistaDamageXpMax = settings.BallistaDamageXpMax;

            BallistaTier1Distance = settings.BallistaTier1Distance;
            BallistaTier2Distance = settings.BallistaTier2Distance;
            BallistaTier3Distance = settings.BallistaTier3Distance;
            BallistaTier4Distance = settings.BallistaTier4Distance;
            BallistaTier1Multiplier = settings.BallistaTier1Multiplier;
            BallistaTier2Multiplier = settings.BallistaTier2Multiplier;
            BallistaTier3Multiplier = settings.BallistaTier3Multiplier;
            BallistaTier4Multiplier = settings.BallistaTier4Multiplier;

            BallistaXpDebug = settings.BallistaXpDebug;
            TravelXpDebug = settings.TravelXpDebug;
            RammingXpDebug = settings.RammingXpDebug;

        }
    }
}
