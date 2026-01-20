using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Attributes;
using TaleWorlds.Localization;

namespace Bannerlord.ShipmasterReworked.Settings
{
    public sealed class MCMConfig : AttributeGlobalSettings<MCMConfig>
    {
        private float _travelXpMultiplier = 3.0f;
        private bool _enableStormTravelXp = true;
        private float _stormTravelXpMultiplier = 2f;

        private int _rammingXpBase = 80;
        private float _rammingXpQualityFactor = 0.15f;

        private float _ballistaDamageFactor = 0.05f;
        private float _ballistaAgentDamageFactor = 0.5f;
        private float _ballistaDamageXpMin = 5f;
        private float _ballistaDamageXpMax = 125f;

        private float _ballistaTier1Distance = 10f;
        private float _ballistaTier2Distance = 180f;
        private float _ballistaTier3Distance = 250f;
        private float _ballistaTier4Distance = 500f;
        private float _ballistaTier1Multiplier = 1.0f;
        private float _ballistaTier2Multiplier = 3.0f;
        private float _ballistaTier3Multiplier = 5.0f;
        private float _ballistaTier4Multiplier = 7.0f;

        private bool _travelXpDebug = false;
        private bool _rammingXpDebug = false;
        private bool _ballistaXpDebug = false;

        public override string Id => ModuleInfo.Id;
        public override string DisplayName => ModuleInfo.DisplayName + " " + ModuleInfo.Version;
        public override string FolderName => ModuleInfo.FolderName;
        public override string FormatType => ModuleInfo.FormatType;

        // ===============================
        // Travel XP Settings
        // ===============================

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_travel_xp_multiplier}Travel XP Multiplier", 0.1f, 10f, "#0.00", Order = 1, RequireRestart = false,
            HintText = "{=shipmaster_reworked_travel_xp_multiplier_hint}Multiplier for Shipmaster XP gained from traveling.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_travel}Travel XP Settings", GroupOrder = 1)]
        public float TravelXpMultiplier
        {
            get => _travelXpMultiplier;
            set => SetAndRefresh(ref _travelXpMultiplier, value);
        }

        [SettingPropertyBool("{=shipmaster_reworked_enable_storm_bonus}Enable Storm Travel Bonus", Order = 2, RequireRestart = false,
            HintText = "{=shipmaster_reworked_enable_storm_bonus_hint}If enabled, traveling in stormy weather will provide additional Shipmaster XP.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_travel}Travel XP Settings", GroupOrder = 1)]
        public bool EnableStormTravelXp
        {
            get => _enableStormTravelXp;
            set => SetAndRefresh(ref _enableStormTravelXp, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_storm_travel_xp_multiplier}Storm Travel XP Bonus Multiplier", 0.1f, 10.0f, "#0.00", Order = 3, RequireRestart = false,
            HintText = "{=shipmaster_reworked_storm_travel_xp_multiplier_hint}Additional multiplier for Shipmaster XP when traveling in stormy weather.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_travel}Travel XP Settings", GroupOrder = 1)]
        public float StormTravelXpMultiplier
        {
            get => _stormTravelXpMultiplier;
            set => SetAndRefresh(ref _stormTravelXpMultiplier, value);
        }

        // ===============================
        // Ramming XP Settings
        // ===============================

        [SettingPropertyInteger("{=shipmaster_reworked_ramming_xp_base}Ramming XP Base", 0, 100, Order = 1, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ramming_xp_base_hint}Base XP gained from ramming before multipliers.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ramming}Ramming XP Settings", GroupOrder = 2)]
        public int RammingXpBase
        {
            get => _rammingXpBase;
            set => SetAndRefresh(ref _rammingXpBase, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ramming_xp_quality_factor}Ramming XP Quality Factor", 0.0f, 1.0f, "#0.00", Order = 2, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ramming_xp_quality_factor_hint}Multiplier factor for ramming quality affecting XP gained.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ramming}Ramming XP Settings", GroupOrder = 2)]
        public float RammingXpQualityFactor
        {
            get => _rammingXpQualityFactor;
            set => SetAndRefresh(ref _rammingXpQualityFactor, value);
        }

        // ===============================
        // Ballista XP Settings
        // ===============================

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_damage_factor}Ballista Damage XP Factor", 0.0f, 0.2f, "#0.000", Order = 1, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_damage_factor_hint}Factor to calculate Shipmaster XP from ballista damage dealt.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings", GroupOrder = 3)]
        public float BallistaDamageFactor
        {
            get => _ballistaDamageFactor;
            set => SetAndRefresh(ref _ballistaDamageFactor, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_agent_damage_factor}Ballista Agent Damage Factor", 0.0f, 5.0f, "#0.00", Order = 2, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_agent_damage_factor_hint}Factor to calculate Shipmaster XP from ballista damage dealt to agents (crew/captains).")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings", GroupOrder = 3)]
        public float BallistaAgentDamageFactor
        {
            get => _ballistaAgentDamageFactor;
            set => SetAndRefresh(ref _ballistaAgentDamageFactor, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_damage_xp_min}Ballista Damage XP Minimum", 1f, 125, "#0.00", Order = 3, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_damage_xp_min_hint}Minimum Shipmaster XP gained from ballista damage.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings", GroupOrder = 3)]
        public float BallistaDamageXpMin
        {
            get => _ballistaDamageXpMin;
            set => SetAndRefresh(ref _ballistaDamageXpMin, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_damage_xp_max}Ballista Damage XP Maximum", 125, 800f, "#0.00", Order = 4, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_damage_xp_max_hint}Maximum Shipmaster XP gained from ballista damage.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings", GroupOrder = 3)]
        public float BallistaDamageXpMax
        {
            get => _ballistaDamageXpMax;
            set => SetAndRefresh(ref _ballistaDamageXpMax, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier1_distance}Ballista Tier 1 Distance", 10f, 180f, "#0", Order = 1, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier1_distance_hint}Minimum distance (in meters) for Ballista XP Tier 1.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier1Distance
        {
            get => _ballistaTier1Distance;
            set => SetAndRefresh(ref _ballistaTier1Distance, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier1_multiplier}Ballista Tier 1 Multiplier", 1f, 20f, "#0.00", Order = 2, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier1_multiplier_hint}XP multiplier applied when Ballista Tier 1 distance is reached.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier1Multiplier
        {
            get => _ballistaTier1Multiplier;
            set => SetAndRefresh(ref _ballistaTier1Multiplier, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier2_distance}Ballista Tier 2 Distance", 180f, 250f, "#0", Order = 3, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier2_distance_hint}Minimum distance (in meters) for Ballista XP Tier 2.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier2Distance
        {
            get => _ballistaTier2Distance;
            set => SetAndRefresh(ref _ballistaTier2Distance, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier2_multiplier}Ballista Tier 2 Multiplier", 1f, 20f, "#0.00", Order = 4, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier2_multiplier_hint}XP multiplier applied when Ballista Tier 2 distance is reached.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier2Multiplier
        {
            get => _ballistaTier2Multiplier;
            set => SetAndRefresh(ref _ballistaTier2Multiplier, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier3_distance}Ballista Tier 3 Distance", 250f, 500f, "#0", Order = 5, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier3_distance_hint}Minimum distance (in meters) for Ballista XP Tier 3.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier3Distance
        {
            get => _ballistaTier3Distance;
            set => SetAndRefresh(ref _ballistaTier3Distance, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier3_multiplier}Ballista Tier 3 Multiplier", 1f, 20f, "#0.00", Order = 6, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier3_multiplier_hint}XP multiplier applied when Ballista Tier 3 distance is reached.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier3Multiplier
        {
            get => _ballistaTier3Multiplier;
            set => SetAndRefresh(ref _ballistaTier3Multiplier, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier4_distance}Ballista Tier 4 Distance", 500f, 2000f, "#0", Order = 7, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier4_distance_hint}Minimum distance (in meters) for Ballista XP Tier 4 (exceptional shots).")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier4Distance
        {
            get => _ballistaTier4Distance;
            set => SetAndRefresh(ref _ballistaTier4Distance, value);
        }

        [SettingPropertyFloatingInteger("{=shipmaster_reworked_ballista_tier4_multiplier}Ballista Tier 4 Multiplier", 1f, 20f, "#0.00", Order = 8, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_tier4_multiplier_hint}XP multiplier applied when Ballista Tier 4 distance is reached.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_ballista}Ballista XP Settings/{=shipmaster_reworked_group_ballista_advanced}Advanced Distance Tiers")]
        public float BallistaTier4Multiplier
        {
            get => _ballistaTier4Multiplier;
            set => SetAndRefresh(ref _ballistaTier4Multiplier, value);
        }

        // ===============================
        // Debug Settings
        // ===============================

        [SettingPropertyBool("{=shipmaster_reworked_travel_xp_debug}Enable Travel XP Debug Messages", Order = 1, RequireRestart = false,
            HintText = "{=shipmaster_reworked_travel_xp_debug_hint}If enabled, debug messages will be displayed when gaining Shipmaster XP from traveling.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_debug}Debug Settings", GroupOrder = 4)]
        public bool TravelXpDebug
        {
            get => _travelXpDebug;
            set => SetAndRefresh(ref _travelXpDebug, value);
        }

        [SettingPropertyBool("{=shipmaster_reworked_ramming_xp_debug}Enable Ramming XP Debug Messages", Order = 2, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ramming_xp_debug_hint}If enabled, debug messages will be displayed when gaining Shipmaster XP from ramming.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_debug}Debug Settings", GroupOrder = 4)]
        public bool RammingXpDebug
        {
            get => _rammingXpDebug;
            set => SetAndRefresh(ref _rammingXpDebug, value);
        }

        [SettingPropertyBool("{=shipmaster_reworked_ballista_xp_debug}Enable Ballista XP Debug Messages", Order = 3, RequireRestart = false,
            HintText = "{=shipmaster_reworked_ballista_xp_debug_hint}If enabled, debug messages will be displayed when gaining Shipmaster XP from ballista damage.")]
        [SettingPropertyGroup("{=shipmaster_reworked_group_debug}Debug Settings", GroupOrder = 4)]
        public bool BallistaXpDebug
        {
            get => _ballistaXpDebug;
            set => SetAndRefresh(ref _ballistaXpDebug, value);
        }

        // ===============================
        // Helper Method
        // ===============================

        private void SetAndRefresh<T>(ref T field, T value)
        {
            if (!Equals(field, value))
            {
                field = value;
                ConfigCache.Refresh(this);
                OnPropertyChanged();
            }
        }
    }
}