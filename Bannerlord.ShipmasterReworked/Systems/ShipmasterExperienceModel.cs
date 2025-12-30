using Bannerlord.ShipmasterReworked.Settings;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Bannerlord.ShipmasterReworked.Systems
{
    public static class ShipmasterExperienceModel
    {
        public static void OnTravel(Hero hero, float speed)
        {
            if (hero == null)
                return;

            if (float.IsNaN(speed) || float.IsInfinity(speed) || speed <= 0f)
                return;

            float travelXpMultiplier = ConfigCache.TravelXpMultiplier;
            float multiplier = 1f;
            const int baseMaxShips = 3;

            var mobileParty = hero.PartyBelongedTo;
            if (mobileParty == null)
                return;

            int numOfShips = mobileParty.Ships?.Count ?? 0;
            if (numOfShips <= 0)
                return;

            int maxNumOfShips = baseMaxShips;

            if (mobileParty.HasPerk(NavalPerks.Shipmaster.ShoreMaster, checkSecondaryRole: true))
                maxNumOfShips += 1;

            if (mobileParty.HasPerk(NavalPerks.Shipmaster.FleetCommander))
                maxNumOfShips += 1;

            if (numOfShips == maxNumOfShips)
                multiplier *= travelXpMultiplier;

            float baseXp = 1.4f * speed;
            float finalXp = baseXp * multiplier;
            int roundedXp = MBRandom.RoundRandomized(finalXp);

            hero.AddSkillXp(NavalSkills.Shipmaster, roundedXp);

            if (ConfigCache.TravelXpDebug && hero.IsHumanPlayerCharacter)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage(
                        $"[Shipmaster Reworked] Granted {roundedXp} Shipmaster XP for travel with {numOfShips} ships (max {maxNumOfShips}) at speed {speed:F2}. Base XP: {baseXp:F2}, Multiplier: {multiplier}, Final XP before rounding: {finalXp:F2}."));
            }
        }

        public static void OnRamming(Hero hero, float damagePercent, int ramQuality)
        {
            if (hero == null)
                return;

            if (float.IsNaN(damagePercent) || float.IsInfinity(damagePercent))
                return;

            // Clamp to sane range: 0% – 150%
            damagePercent = MathF.Clamp(damagePercent, 0f, 1.5f);

            if (damagePercent <= 0f)
                return;


            int baseXp = ConfigCache.RammingXpBase;
            float qualityFactor = ConfigCache.RammingXpQualityFactor;

            float rawXp = baseXp * damagePercent * (1f + ramQuality * qualityFactor);
            if (rawXp <= 0f)
                return;

            int xp = MBRandom.RoundRandomized(rawXp);
            hero.AddSkillXp(NavalSkills.Shipmaster, xp);

            if (ConfigCache.RammingXpDebug && hero.IsHumanPlayerCharacter)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage(
                        $"[Shipmaster Reworked] Granted {xp} Shipmaster XP for ramming with damage percent {damagePercent:P2} and ram quality {ramQuality}. Base XP: {baseXp}, Quality Factor: {qualityFactor}, Raw XP before rounding: {rawXp:F2}."));
            }
        }
    }
}
