using Bannerlord.ShipmasterReworked.Systems;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(DefaultSkillLevelingManager), nameof(DefaultSkillLevelingManager.OnCombatHit))]
    public static class BallistaAgentHitPatch
    {
        public static void Postfix(
            CharacterObject affectorCharacter,
            CharacterObject affectedCharacter,
            float shotDifficulty,
            WeaponComponentData affectorWeapon,
            CombatXpModel.MissionTypeEnum missionType,
            bool isTeamKill,
            float damageAmount,
            bool isFatal,
            bool isSiegeEngineHit)
        {
            if (isTeamKill || !isSiegeEngineHit)
                return;

            if (affectorWeapon == null)
                return;

            // Check if ballista
            WeaponClass weaponClass = affectorWeapon.WeaponClass;
            if (weaponClass != WeaponClass.BallistaBoulder &&
                weaponClass != WeaponClass.BallistaStone)
                return;

            if (!affectorCharacter.IsHero)
                return;

            Hero hero = affectorCharacter.HeroObject;
            if (hero == null)
                return;

            // Replicate the same XP calculation the game does for Engineering
            ExplainedNumber baseXp = new ExplainedNumber(
                Campaign.Current.Models.CombatXpModel.GetXpFromHit(
                    hero.CharacterObject,
                    null,
                    affectedCharacter,
                    hero.PartyBelongedTo?.Party,
                    (int)damageAmount,
                    isFatal,
                    missionType).ResultNumber);

            if (shotDifficulty > 0f)
            {
                baseXp.AddFactor(Campaign.Current.Models.CombatXpModel.GetXpMultiplierFromShotDifficulty(shotDifficulty));
            }

            int engineeringXp = MBRandom.RoundRandomized(baseXp.ResultNumber);

            // Award Shipmaster XP
            ShipmasterExperienceModel.OnBallistaHitAgent(hero, engineeringXp);
        }
    }
}