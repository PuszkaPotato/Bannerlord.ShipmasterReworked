using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

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

            // Display Debug Message if debug mode is enabled (Todo later)
            //if(hero.IsHumanPlayerCharacter)
            //{
            //    InformationManager.DisplayMessage(
            //        new InformationMessage(
            //            $"[Shipmaster Reworked] Gained {roundedXp} Shipmaster XP for traveling."));
            //}
        }

        public static void OnRamming(Hero hero, float damagePercent, int ramQuality)
        {
            if (hero == null || damagePercent <= 0f)
                return;

            const float baseXp = 30f;
            const float qualityFactor = 0.25f;

            float rawXp = baseXp * damagePercent * (1f + ramQuality * qualityFactor);
            if (rawXp <= 0f)
                return;

            int xp = MBRandom.RoundRandomized(rawXp);
            hero.AddSkillXp(NavalSkills.Shipmaster, xp);

            // Display Debug Message if debug mode is enabled (Todo later)
            //InformationManager.DisplayMessage(
            //    new InformationMessage(
            //        $"[Shipmaster Reworked] {hero.Name} gained {xp} Shipmaster XP for ramming."));
        }


        // Future:
        // public static void OnNavalBattle(Hero hero, int enemyShips) { }
    }
}
