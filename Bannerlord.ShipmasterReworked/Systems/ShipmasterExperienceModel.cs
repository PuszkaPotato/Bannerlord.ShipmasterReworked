using Bannerlord.ShipmasterReworked.Settings;
using Bannerlord.ShipmasterReworked.Systems.Environment;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Bannerlord.ShipmasterReworked.Systems
{
    public static class ShipmasterExperienceModel
    {
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
                DisplayRammingDebugMessage(
                    xp,
                    damagePercent,
                    ramQuality,
                    baseXp,
                    qualityFactor,
                    rawXp
                );
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
            float baseXp = damage * ConfigCache.BallistaDamageFactor;

            float distanceMultiplier = CalculateBallistaDistanceMultiplier(distanceMeters);

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

        public static void OnBallistaHitAgent(Hero hero, int engineeringXp)
        {
            if (!IsValidHero(hero))
                return;

            if (engineeringXp <= 0)
                return;

            // Apply a factor to the engineering XP for Shipmaster
            float baseXp = engineeringXp * ConfigCache.BallistaAgentDamageFactor;

            int xp = MBRandom.RoundRandomized(baseXp);
            if (xp <= 0)
                return;

            hero.AddSkillXp(NavalSkills.Shipmaster, xp);

            if (ConfigCache.BallistaXpDebug && hero.IsHumanPlayerCharacter)
            {
                DisplayBallistaAgentDebugMessage(
                    xp,
                    engineeringXp,
                    ConfigCache.BallistaAgentDamageFactor,
                    baseXp
                );
            }
        }

        // ======================
        // Helpers
        // ======================

        private static int GetOptimalShipCount(MobileParty party)
        {
            const int BaseMaxShips = 3;
            int optimalShipCount = BaseMaxShips;
            if (party.HasPerk(NavalPerks.Shipmaster.ShoreMaster))
                optimalShipCount++;
            if (party.HasPerk(NavalPerks.Shipmaster.FleetCommander))
                optimalShipCount++;
            return optimalShipCount;
        }

        private static bool IsOptimalShipCount(MobileParty party)
        {
            int shipCount = party.Ships?.Count ?? 0;
            return shipCount == GetOptimalShipCount(party);
        }

        private static float CalculateTravelMultiplier(
            MobileParty party,
            int shipCount,
            bool isInStorm)
        {
            float multiplier = 1f;

            // Exact match by design
            if (IsOptimalShipCount(party))
                multiplier *= ConfigCache.TravelXpMultiplier;

            // Extra multiplier for travelling in storm
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
            int optimalShipCount = GetOptimalShipCount(MobileParty.MainParty);

            string stormText = string.Empty;
            if (isInStorm)
            {
                TextObject stormTextObj = new TextObject("{=shipmaster_reworked_debug_storm_text} Storm Multiplier: {STORM_MULTIPLIER}.");
                stormTextObj.SetTextVariable("STORM_MULTIPLIER", ConfigCache.StormTravelXpMultiplier.ToString("F2"));
                stormText = stormTextObj.ToString();
            }

            string optimalShipCountText;
            if (IsOptimalShipCount(MobileParty.MainParty))
            {
                TextObject optimalYesObj = new TextObject("{=shipmaster_reworked_debug_optimal_yes} Optimal ship count bonus applied.");
                optimalShipCountText = optimalYesObj.ToString();
            }
            else
            {
                TextObject optimalNoObj = new TextObject("{=shipmaster_reworked_debug_optimal_no} Non-optimal number of ships, bonus not applied. Optimal ship count is {OPTIMAL_COUNT}.");
                optimalNoObj.SetTextVariable("OPTIMAL_COUNT", optimalShipCount);
                optimalShipCountText = optimalNoObj.ToString();
            }

            TextObject message = new TextObject("{=shipmaster_reworked_debug_travel}[Shipmaster Reworked] Granted {XP} Shipmaster XP for travel. Speed: {SPEED}, Base XP: {BASE_XP}, Multiplier: {MULTIPLIER}, Final XP: {FINAL_XP}.{STORM_TEXT}{OPTIMAL_TEXT}");
            message.SetTextVariable("XP", xp);
            message.SetTextVariable("SPEED", speed.ToString("F2"));
            message.SetTextVariable("BASE_XP", baseXp.ToString("F2"));
            message.SetTextVariable("MULTIPLIER", multiplier.ToString("F2"));
            message.SetTextVariable("FINAL_XP", finalXp.ToString("F2"));
            message.SetTextVariable("STORM_TEXT", stormText);
            message.SetTextVariable("OPTIMAL_TEXT", optimalShipCountText);

            InformationManager.DisplayMessage(new InformationMessage(message.ToString()));
        }

        private static void DisplayRammingDebugMessage(
            int xp,
            float damagePercent,
            int ramQuality,
            int baseXp,
            float qualityFactor,
            float rawXp)
        {
            TextObject message = new TextObject("{=shipmaster_reworked_debug_ramming}[Shipmaster Reworked] Granted {XP} Shipmaster XP for ramming. Damage: {DAMAGE_PERCENT}, Ram Quality: {RAM_QUALITY}, Base XP: {BASE_XP}, Quality Factor: {QUALITY_FACTOR}, Raw XP: {RAW_XP}.");
            message.SetTextVariable("XP", xp);
            message.SetTextVariable("DAMAGE_PERCENT", damagePercent.ToString("P2"));
            message.SetTextVariable("RAM_QUALITY", ramQuality);
            message.SetTextVariable("BASE_XP", baseXp);
            message.SetTextVariable("QUALITY_FACTOR", qualityFactor.ToString("F2"));
            message.SetTextVariable("RAW_XP", rawXp.ToString("F2"));

            InformationManager.DisplayMessage(new InformationMessage(message.ToString()));
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
            TextObject message = new TextObject("{=shipmaster_reworked_debug_ballista}[Shipmaster Reworked] Granted {XP} Shipmaster XP for ballista hit. Distance: {DISTANCE}m, Damage: {DAMAGE}, Base XP: {BASE_XP}, Distance Multiplier: {MULTIPLIER}, Damage Factor: {DAMAGE_FACTOR}, Final XP: {FINAL_XP}.");
            message.SetTextVariable("XP", xp);
            message.SetTextVariable("DISTANCE", distance.ToString("F1"));
            message.SetTextVariable("DAMAGE", damage);
            message.SetTextVariable("BASE_XP", baseXp.ToString("F2"));
            message.SetTextVariable("MULTIPLIER", multiplier.ToString("F2"));
            message.SetTextVariable("DAMAGE_FACTOR", damageFactor.ToString("F3"));
            message.SetTextVariable("FINAL_XP", finalXp.ToString("F2"));

            InformationManager.DisplayMessage(new InformationMessage(message.ToString()));
        }

        private static void DisplayBallistaAgentDebugMessage(
            int xp,
            int engineeringXp,
            float damageFactor,
            float baseXp)
        {
            TextObject message = new TextObject("{=shipmaster_reworked_debug_ballista_agent}[Shipmaster Reworked] Granted {XP} Shipmaster XP for ballista hit on agent. Engineering XP: {ENGINEERING_XP}, Damage Factor: {DAMAGE_FACTOR}, Base XP: {BASE_XP}.");
            message.SetTextVariable("XP", xp);
            message.SetTextVariable("ENGINEERING_XP", engineeringXp);
            message.SetTextVariable("DAMAGE_FACTOR", damageFactor.ToString("F2"));
            message.SetTextVariable("BASE_XP", baseXp.ToString("F2"));

            InformationManager.DisplayMessage(new InformationMessage(message.ToString()));
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