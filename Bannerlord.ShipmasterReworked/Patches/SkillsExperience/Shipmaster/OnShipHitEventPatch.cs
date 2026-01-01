using Bannerlord.ShipmasterReworked.Systems;
using HarmonyLib;
using NavalDLC.Missions.MissionLogics;
using NavalDLC.Missions.Objects;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(NavalShipsLogic), "OnShipHit")]
    public static class OnShipHitEventPatch
    {
        public static void Postfix(
            MissionShip ship,
            Agent attackerAgent,
            int damage,
            Vec3 impactPosition,
            Vec3 impactDirection,
            ref MissionWeapon weapon,
            int affectorWeaponSlotOrMissileIndex)
        {

            // --- Basic sanity ---
            if (ship == null || attackerAgent == null)
                return;

            // Only player-controlled hits
            if (!attackerAgent.IsMainAgent)
                return;

            // Weapon data must exist
            if (weapon.CurrentUsageItem == null)
                return;

            // Only ballista weapons
            WeaponClass weaponClass = weapon.CurrentUsageItem.WeaponClass;
            if (weaponClass != WeaponClass.BallistaBoulder &&
                weaponClass != WeaponClass.BallistaStone)
                return;

            // Resolve ship captain -> hero
            //var captainCharacter = ship.Captain?.Character;
            //if (captainCharacter is not CharacterObject characterObject || !characterObject.IsHero)
            //    return;

            //Hero hero = characterObject.HeroObject;
            //if (hero == null)
            //    return;

            Hero hero = Hero.MainHero;

            // Distance from attacker to impact point
            float distance = attackerAgent.Position.Distance(impactPosition);

            // Forward to system model
            ShipmasterExperienceModel.OnBallistaHit(
                hero,
                distance,
                damage);
        }
    }
}
