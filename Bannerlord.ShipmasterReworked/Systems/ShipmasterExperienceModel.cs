using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace Bannerlord.ShipmasterReworked.Systems
{
    public static class ShipmasterExperienceModel
    {
        private const float BaseTravelXpMultiplier = 1.4f;
        private const int BaseMaxShips = 3;

        public static void OnTravel(Hero hero, float speed)
        {
            var mobileParty = hero.PartyBelongedTo;
            if (mobileParty == null)
                return;

            int numOfShips = mobileParty.Ships?.Count ?? 0;
            if (numOfShips <= 0)
                return;

            int maxNumOfShips = BaseMaxShips;

            if (mobileParty.HasPerk(NavalPerks.Shipmaster.ShoreMaster, checkSecondaryRole: true))
                maxNumOfShips += 1;

            if (mobileParty.HasPerk(NavalPerks.Shipmaster.FleetCommander))
                maxNumOfShips += 1;

            int multiplier = 1;
            if (numOfShips == maxNumOfShips)
                multiplier = 3;

            float baseXp = BaseTravelXpMultiplier * speed;
            float finalXp = baseXp * multiplier;
            int roundedXp = MBRandom.RoundRandomized(finalXp);

            hero.AddSkillXp(NavalSkills.Shipmaster, roundedXp);
        }

        // Future:
        // public static void OnRamming(Hero hero, float damage) { }
        // public static void OnNavalBattle(Hero hero, int enemyShips) { }
    }
}
