using HarmonyLib;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(NavalSkillLevellingManager), nameof(NavalSkillLevellingManager.OnTravelOnWater))]
    public static class TravelPatch
    {
        public static bool Prefix(Hero hero, float speed)
        {
            if (hero?.PartyBelongedTo == null)
                return false;

            MobileParty mobileParty = hero.PartyBelongedTo;

            int numOfShips = mobileParty.Ships?.Count ?? 0;
            if (numOfShips <= 0)
                return false;

            int maxNumOfShips = 3;

            if (mobileParty.HasPerk(NavalPerks.Shipmaster.ShoreMaster, checkSecondaryRole: true))
                maxNumOfShips += 1;

            if (mobileParty.HasPerk(NavalPerks.Shipmaster.FleetCommander))
                maxNumOfShips += 2;

            int multiplier = 1;
            if (numOfShips == maxNumOfShips)
                multiplier = 3;

            float baseXp = 1.4f * speed;
            float finalXp = baseXp * multiplier;
            int roundedXp = MBRandom.RoundRandomized(finalXp);

            hero.AddSkillXp(NavalSkills.Shipmaster, roundedXp);

            InformationManager.DisplayMessage(
                new InformationMessage(
                    $"[Shipmaster Reworked] {hero.Name} gained {roundedXp} Shipmaster XP for traveling with {numOfShips}/{maxNumOfShips} ships."));

            // Skip vanilla XP
            return false;
        }
    }
}
