using HarmonyLib;
using NavalDLC.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using Bannerlord.ShipmasterReworked.Systems;
using TaleWorlds.Library;

namespace Bannerlord.ShipmasterReworked.Patches.SkillsExperience.Shipmaster
{
    [HarmonyPatch(typeof(NavalSkillLevellingManager), nameof(NavalSkillLevellingManager.OnTravelOnWater))]
    public static class TravelPatch
    {
        public static bool Prefix(object[] __args)
        {
            Hero? hero;
            float speed;

            // Old version before WS v1.3.0 & BL v1.5.0:
            // OnTravelOnWater(Hero hero, float speed)
            if (__args.Length >= 2 &&
                __args[0] is Hero oldHero &&
                __args[1] is float oldSpeed)
            {
                hero = oldHero;
                speed = oldSpeed;
            }

            // New version after WS v1.3.0 & BL v1.5.0:
            // OnTravelOnWater(MobileParty party)
            else if (__args.Length >= 1 &&
                     __args[0] is MobileParty party)
            {
                hero = party.LeaderHero;
                speed = party.Speed;
            }

            // Unknown signature
            else
            {
                InformationManager.DisplayMessage(
                    new InformationMessage(
                        "[ShipmasterReworked] Unsupported Bannerlord or War Sails version detected. " +
                        "Naval travel XP patch could not be applied. Please update the mod or contact developer"
                        )
                );

                return true; //Let the original method run, since we don't know the signature
            }

            if (hero == null)
                return false;

            if (hero.PartyBelongedTo?.LeaderHero != hero)
                return false;

            ShipmasterExperienceModel.OnTravel(hero, speed);

            // Skip vanilla XP
            return false;
        }
    }
}