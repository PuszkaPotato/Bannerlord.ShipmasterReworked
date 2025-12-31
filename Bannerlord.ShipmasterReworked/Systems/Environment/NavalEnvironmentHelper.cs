using NavalDLC;
using NavalDLC.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Party;

namespace Bannerlord.ShipmasterReworked.Systems.Environment
{
    internal static class NavalEnvironmentHelper
    {
        public static bool IsPartyInStorm(MobileParty party)
        {
            if (party == null || !party.IsCurrentlyAtSea)
                return false;

            var stormManager = NavalDLCManager.Instance?.StormManager;
            if (stormManager == null)
                return false;

            foreach (Storm storm in stormManager.SpawnedStorms)
            {
                if (party.Position.DistanceSquared(storm.CurrentPosition) <=
                    storm.EffectRadius * storm.EffectRadius)
                {
                    return true;
                }
            }

            return false;
        }
}
