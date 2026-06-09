using HarmonyLib;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using Bannerlord.ShipmasterReworked.Systems;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(NavalSkillLevellingManager), nameof(NavalSkillLevellingManager.OnTravelOnWater))]
    public static class TravelPatch
    {
        public static bool Prefix(MobileParty party, float speed)
        {
            Hero hero = party.LeaderHero;
            if (hero == null)
                return false;

            ShipmasterExperienceModel.OnTravel(hero, speed);

            // Skip vanilla XP
            return false;
        }
    }
}