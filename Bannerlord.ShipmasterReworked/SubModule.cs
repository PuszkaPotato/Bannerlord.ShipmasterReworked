using TaleWorlds.MountAndBlade;
using HarmonyLib;
using TaleWorlds.Library;


namespace Bannerlord.ShipmasterReworked
{
    public class SubModule : MBSubModuleBase
    {
        public string moduleId => ModuleInfo.Id;
        public string moduleDisplayName => ModuleInfo.DisplayName;
        public string settingsFolderName => ModuleInfo.FolderName;
        public string settingsFormatType => ModuleInfo.FormatType;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            var harmony = new Harmony(ModuleInfo.Id);
            harmony.PatchAll();
            InformationManager.DisplayMessage(new InformationMessage($"[{ModuleInfo.DisplayName}] Harmony patches applied.", Colors.Green));
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

    public static class ModuleInfo
    {
        public const string Id = "bannerlord.shipmasterreworked";
        public const string DisplayName = "Shipmaster Reworked";
        public const string FolderName = "ShipmasterReworked";
        public const string FormatType = "json";
    }
}