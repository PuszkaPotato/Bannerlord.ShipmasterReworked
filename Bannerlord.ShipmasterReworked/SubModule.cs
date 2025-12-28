using TaleWorlds.MountAndBlade;
using HarmonyLib;
using TaleWorlds.Library;


namespace Bannerlord.ShipmasterReworked
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            var harmony = new Harmony("bannerlord.shipmasterreworked");
            harmony.PatchAll();
            InformationManager.DisplayMessage(new InformationMessage("[Shipmaster Reworked] Harmony patches applied."));
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

        }
    }
}