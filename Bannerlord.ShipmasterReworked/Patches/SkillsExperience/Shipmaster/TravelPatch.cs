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
        public static bool Prefix(object[] __args)
        {
            Hero? hero = __args[0] switch
            {
                Hero h => h,
                MobileParty party => party.LeaderHero,
                _ => null
            };

            if (hero == null)
                return false;

            if (hero.PartyBelongedTo?.LeaderHero != hero)
                return false;

            float speed = (float)__args[1];
            ShipmasterExperienceModel.OnTravel(hero, speed);

            // Skip vanilla XP
            return false;
        }
    }
}