using Bannerlord.ShipmasterReworked.Settings;
using Bannerlord.ShipmasterReworked.Systems.Environment;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Bannerlord.ShipmasterReworked.Systems
{
    public static class ShipmasterExperienceModel
    {
        private const int BaseMaxShips = 3;

        public static void OnTravel(Hero hero, float speed)
        {
            if (!IsValidHero(hero))
                return;

            if (!IsValidPositiveFloat(speed))
                return;

            MobileParty party = hero.PartyBelongedTo;
            if (party == null)
                return;

            int shipCount = party.Ships?.Count ?? 0;
            if (shipCount <= 0)
                return;

            bool isInStorm = ConfigCache.EnableStormTravelXp
                && NavalEnvironmentHelper.IsPartyInStorm(party);

            float multiplier = CalculateTravelMultiplier(party, shipCount, isInStorm);
            float baseXp = 1.4f * speed;
            float finalXp = baseXp * multiplier;

            int xp = MBRandom.RoundRandomized(finalXp);
            if (xp <= 0)
                return;

            hero.AddSkillXp(NavalSkills.Shipmaster, xp);

            if (ConfigCache.TravelXpDebug && hero.IsHumanPlayerCharacter)
            {
                DisplayTravelDebugMessage(
                    xp,
                    speed,
                    baseXp,
                    multiplier,
                    finalXp,
                    isInStorm
                );
            }
        }

        public static void OnRamming(Hero hero, float damagePercent, int ramQuality)
        {
            if (!IsValidHero(hero))
                return;

            if (!IsValidFloat(damagePercent))
                return;

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
                InformationManager.DisplayMessage(new InformationMessage(
                    $"[Shipmaster Reworked] Granted {xp} Shipmaster XP for ramming. " +
                    $"Damage: {damagePercent:P2}, Ram Quality: {ramQuality}, " +
                    $"Base XP: {baseXp}, Quality Factor: {qualityFactor}, Raw XP: {rawXp:F2}"
                ));
            }
        }

        public static void OnBallistaHit(
            Hero hero,
            float distanceMeters,
            int damage)
        {
            if (!IsValidHero(hero))
                return;

            if (!IsValidPositiveFloat(distanceMeters))
                return;

            if (damage <= 0)
                return;

            // Convert damage to XP contribution
            float baseXp =
                damage * ConfigCache.BallistaDamageFactor;

            float distanceMultiplier = 
                CalculateBallistaDistanceMultiplier(distanceMeters);

            // Safety clamp
            baseXp = MathF.Clamp(
                baseXp,
                ConfigCache.BallistaDamageXpMin,
                ConfigCache.BallistaDamageXpMax);

            float finalXp = baseXp * distanceMultiplier;

            int xp = MBRandom.RoundRandomized(finalXp);
            if (xp <= 0)
                return;

            hero.AddSkillXp(NavalSkills.Shipmaster, xp);

            if (ConfigCache.BallistaXpDebug && hero.IsHumanPlayerCharacter)
            {
                DisplayBallistaDebugMessage(
                    xp,
                    distanceMeters,
                    damage,
                    baseXp,
                    distanceMultiplier,
                    ConfigCache.BallistaDamageFactor,
                    finalXp
                );
            }
        }


        // ======================
        // Helpers
        // ======================

        private static float CalculateTravelMultiplier(
            MobileParty party,
            int shipCount,
            bool isInStorm)
        {
            int optimalShipCount = BaseMaxShips;

            if (party.HasPerk(NavalPerks.Shipmaster.ShoreMaster))
                optimalShipCount++;

            if (party.HasPerk(NavalPerks.Shipmaster.FleetCommander))
                optimalShipCount++;

            float multiplier = 1f;

            // Exact match by design
            if (shipCount == optimalShipCount)
                multiplier *= ConfigCache.TravelXpMultiplier;

            if (isInStorm)
                multiplier *= ConfigCache.StormTravelXpMultiplier;

            return multiplier;
        }

        private static void DisplayTravelDebugMessage(
            int xp,
            float speed,
            float baseXp,
            float multiplier,
            float finalXp,
            bool isInStorm)
        {
            string stormText = isInStorm
                ? $" Storm Multiplier: {ConfigCache.StormTravelXpMultiplier}."
                : string.Empty;

            InformationManager.DisplayMessage(new InformationMessage(
                $"[Shipmaster Reworked] Granted {xp} Shipmaster XP for travel. " +
                $"Speed: {speed:F2}, Base XP: {baseXp:F2}, " +
                $"Multiplier: {multiplier:F2}, Final XP: {finalXp:F2}.{stormText}"
            ));
        }

        private static void DisplayBallistaDebugMessage(
            int xp,
            float distance,
            int damage,
            float baseXp,
            float multiplier,
            float damageFactor,
            float finalXp)
        {
            InformationManager.DisplayMessage(new InformationMessage(
                $"[Shipmaster Reworked] Granted {xp} Shipmaster XP for ballista hit. " +
                $"Distance: {distance:F1}m, Damage: {damage}, " +
                $"Base XP: {baseXp}, Distance Multiplier: {multiplier}, " +
                $"Damage Factor: {damageFactor:F2}, Final XP: {finalXp:F2}"
            ));
        }

        private static float CalculateBallistaDistanceMultiplier(float distanceMeters)
        {
            float multiplier = 1f;

            if (distanceMeters >= ConfigCache.BallistaTier1Distance)
                multiplier = ConfigCache.BallistaTier1Multiplier;

            if (distanceMeters >= ConfigCache.BallistaTier2Distance)
                multiplier = ConfigCache.BallistaTier2Multiplier;

            if (distanceMeters >= ConfigCache.BallistaTier3Distance)
                multiplier = ConfigCache.BallistaTier3Multiplier;

            if (distanceMeters >= ConfigCache.BallistaTier4Distance)
                multiplier = ConfigCache.BallistaTier4Multiplier;

            return multiplier;
        }

        private static bool IsValidHero(Hero hero)
        {
            return hero != null;
        }

        private static bool IsValidFloat(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }

        private static bool IsValidPositiveFloat(float value)
        {
            return IsValidFloat(value) && value > 0f;
        }
    }
}
