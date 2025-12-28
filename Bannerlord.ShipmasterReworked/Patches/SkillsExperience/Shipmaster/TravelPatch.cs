using HarmonyLib;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using Bannerlord.ShipmasterReworked.Systems;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(NavalSkillLevellingManager), nameof(NavalSkillLevellingManager.OnTravelOnWater))]
    public static class TravelPatch
    {
        public static bool Prefix(Hero hero, float speed)
        {
            if (hero?.PartyBelongedTo == null)
                return false;

            ShipmasterExperienceModel.OnTravel(hero, speed);

            // Skip vanilla XP
            return false;
        }
    }
}