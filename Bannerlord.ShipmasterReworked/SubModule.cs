using HarmonyLib;
using Bannerlord.ShipmasterReworked.Settings;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;


namespace Bannerlord.ShipmasterReworked
{
    public class SubModule : MBSubModuleBase
    {
        public string moduleId => ModuleInfo.Id;
        public string moduleDisplayName => ModuleInfo.DisplayName;
        public string moduleVersion => ModuleInfo.Version;
        public string settingsFolderName => ModuleInfo.FolderName;
        public string settingsFormatType => ModuleInfo.FormatType;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            var harmony = new Harmony(ModuleInfo.Id);

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                try
                {
                    new PatchClassProcessor(harmony, type).Patch();
                }
                catch (System.Exception ex)
                {
                    InformationManager.DisplayMessage(new InformationMessage($"[{ModuleInfo.DisplayName}] Skipped patch {type.Name}: {ex.Message}", Colors.Red));
                }
            }

            InformationManager.DisplayMessage(new InformationMessage($"[{ModuleInfo.DisplayName}] Harmony patches applied.", Colors.Green));
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            var settings = MCMConfig.Instance;
            if (settings != null)
            {
                ConfigCache.Refresh(settings);
            }

            InformationManager.DisplayMessage(new InformationMessage($"[{ModuleInfo.DisplayName}] Version {ModuleInfo.Version} loaded.", Colors.Green));
        }
    }

    public static class ModuleInfo
    {
        public const string Id = "bannerlord.shipmasterreworked";
        public const string DisplayName = "Shipmaster Reworked";
        public const string FolderName = "ShipmasterReworked";
        public const string FormatType = "json";

        public static string Version =>
            Assembly.GetExecutingAssembly()
                    .GetName()
                    .Version?
                    .ToString() ?? "Unknown";
    }
}