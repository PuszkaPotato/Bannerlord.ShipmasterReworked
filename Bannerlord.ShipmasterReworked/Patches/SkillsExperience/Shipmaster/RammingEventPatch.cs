using Bannerlord.ShipmasterReworked.Systems;
using HarmonyLib;
using NavalDLC.Missions.MissionLogics;
using NavalDLC.Missions.Objects;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(NavalShipsLogic), nameof(NavalShipsLogic.AfterStart))]
    public static class RammingEventPatch
    {
        public static void Postfix(NavalShipsLogic __instance)
        {
            __instance.ShipRammingEvent += OnShipRamming;
        }

        private static void OnShipRamming(
            MissionShip rammingShip,
            MissionShip rammedShip,
            float damagePercent,
            bool isFirstImpact,
            CapsuleData capsuleData,
            int ramQuality)
        {
            if (!isFirstImpact)
                return;

            var captain = rammingShip?.Captain?.Character;

            // Fix: Check if captain is a Hero using the CharacterObject property
            if (captain is null || captain is not CharacterObject characterObject || !characterObject.IsHero)
                return;

            var hero = characterObject.HeroObject;
            if (hero is null)
                return;

            ShipmasterExperienceModel.OnRamming(hero, damagePercent, ramQuality);
        }
    }
}
