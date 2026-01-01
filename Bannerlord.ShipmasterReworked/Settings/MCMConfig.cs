using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Attributes;

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

        private bool _ballistaXpDebug = false;
        private bool _travelXpDebug = false;
        private bool _rammingXpDebug = false;

        public override string Id => ModuleInfo.Id;
        public override string DisplayName => ModuleInfo.DisplayName + " " + ModuleInfo.Version;
        public override string FolderName => ModuleInfo.FolderName;
        public override string FormatType => ModuleInfo.FormatType;

        [SettingPropertyFloatingInteger(
            "Travel XP Multiplier",
            0.1f,
            10f,
            "#0.00",
            Order = 1,
            RequireRestart = false,
            HintText = "Multiplier for Shipmaster XP gained from traveling.")]
        [SettingPropertyGroup("Travel Multiplier Settings")]
        public float TravelXpMultiplier
        {
            get => _travelXpMultiplier;
            set
            {
                if (_travelXpMultiplier != value)
                {
                    _travelXpMultiplier = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        // Do we apply bonus from travelling in storm?
        [SettingPropertyBool(
            "Enable Storm Travel Bonus",
            Order = 2,
            RequireRestart = false,
            HintText = "If enabled, traveling in stormy weather will provide additional Shipmaster XP.")]
        [SettingPropertyGroup("Travel Multiplier Settings")]
        public bool EnableStormTravelXp
        {
            get => _enableStormTravelXp;
            set
            {
                if (_enableStormTravelXp != value)
                {
                    _enableStormTravelXp = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Storm Travel XP Bonus Multiplier",
            0.1f,
            10.0f,
            "#0.00",
            Order = 3,
            RequireRestart = false,
            HintText = "Additional multiplier for Shipmaster XP when traveling in stormy weather.")]
        [SettingPropertyGroup("Travel Multiplier Settings")]
        public float StormTravelXpMultiplier
        {
            get => _stormTravelXpMultiplier;
            set
            {
                if (_stormTravelXpMultiplier != value)
                {
                    _stormTravelXpMultiplier = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyBool(
            "Enable Travel XP Debug Messages",
            Order = 9,
            RequireRestart = false,
            HintText = "If enabled, debug messages will be displayed when gaining Shipmaster XP from traveling.")]
        [SettingPropertyGroup("Travel Multiplier Settings")]
        public bool TravelXpDebug
        {
            get => _travelXpDebug;
            set
            {
                if (_travelXpDebug != value)
                {
                    _travelXpDebug = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyInteger(
            "Ramming XP Base",
            0,
            100,
            Order = 1,
            RequireRestart = false,
            HintText = "Base XP gained from ramming before multipliers.")]
        [SettingPropertyGroup("Ramming XP Settings")]
        public int RammingXpBase
        {
            get => _rammingXpBase;
            set
            {
                if (_rammingXpBase != value)
                {
                    _rammingXpBase = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ramming XP Quality Factor",
            0.0f,
            1.0f,
            "#0.00",
            Order = 2,
            RequireRestart = false,
            HintText = "Multiplier factor for ramming quality affecting XP gained.")]
        [SettingPropertyGroup("Ramming XP Settings")]
        public float RammingXpQualityFactor
        {
            get => _rammingXpQualityFactor;
            set
            {
                if (_rammingXpQualityFactor != value)
                {
                    _rammingXpQualityFactor = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyBool(
            "Enable Ramming XP Debug Messages",
            Order = 9,
            RequireRestart = false,
            HintText = "If enabled, debug messages will be displayed when gaining Shipmaster XP from ramming.")]
        [SettingPropertyGroup("Ramming XP Settings")]
        public bool RammingXpDebug
        {
            get => _rammingXpDebug;
            set
            {
                if (_rammingXpDebug != value)
                {
                    _rammingXpDebug = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Damage XP Factor",
            0.0f,
            0.2f,
            "#0.000",
            Order = 1,
            RequireRestart = false,
            HintText = "Factor to calculate Shipmaster XP from ballista damage dealt.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaDamageFactor
        {
            get => _ballistaDamageFactor;
            set
            {
                if (_ballistaDamageFactor != value)
                {
                    _ballistaDamageFactor = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Damage XP Minimum",
            1f,
            400f,
            "#0.00",
            Order = 2,
            RequireRestart = false,
            HintText = "Minimum Shipmaster XP gained from ballista damage.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaDamageXpMin
        {
            get => _ballistaDamageXpMin;
            set
            {
                if (_ballistaDamageXpMin != value)
                {
                    _ballistaDamageXpMin = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Damage XP Maximum",
            10f,
            800f,
            "#0.00",
            Order = 3,
            RequireRestart = false,
            HintText = "Maximum Shipmaster XP gained from ballista damage.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaDamageXpMax
        {
            get => _ballistaDamageXpMax;
            set
            {
                if (_ballistaDamageXpMax != value)
                {
                    _ballistaDamageXpMax = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        // ===============================
        // Ballista Distance XP Tiers
        // ===============================

        [SettingPropertyFloatingInteger(
            "Ballista Tier 1 Distance",
            10f,
            500f,
            "#0",
            Order = 4,
            RequireRestart = false,
            HintText = "Minimum distance (in meters) for Ballista XP Tier 1.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier1Distance
        {
            get => _ballistaTier1Distance;
            set
            {
                if (_ballistaTier1Distance != value)
                {
                    _ballistaTier1Distance = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 1 Multiplier",
            1f,
            20f,
            "#0.00",
            Order = 5,
            RequireRestart = false,
            HintText = "XP multiplier applied when Ballista Tier 1 distance is reached.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier1Multiplier
        {
            get => _ballistaTier1Multiplier;
            set
            {
                if (_ballistaTier1Multiplier != value)
                {
                    _ballistaTier1Multiplier = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 2 Distance",
            10f,
            800f,
            "#0",
            Order = 6,
            RequireRestart = false,
            HintText = "Minimum distance (in meters) for Ballista XP Tier 2.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier2Distance
        {
            get => _ballistaTier2Distance;
            set
            {
                if (_ballistaTier2Distance != value)
                {
                    _ballistaTier2Distance = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 2 Multiplier",
            1f,
            20f,
            "#0.00",
            Order = 7,
            RequireRestart = false,
            HintText = "XP multiplier applied when Ballista Tier 2 distance is reached.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier2Multiplier
        {
            get => _ballistaTier2Multiplier;
            set
            {
                if (_ballistaTier2Multiplier != value)
                {
                    _ballistaTier2Multiplier = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 3 Distance",
            10f,
            1200f,
            "#0",
            Order = 8,
            RequireRestart = false,
            HintText = "Minimum distance (in meters) for Ballista XP Tier 3.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier3Distance
        {
            get => _ballistaTier3Distance;
            set
            {
                if (_ballistaTier3Distance != value)
                {
                    _ballistaTier3Distance = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 3 Multiplier",
            1f,
            20f,
            "#0.00",
            Order = 9,
            RequireRestart = false,
            HintText = "XP multiplier applied when Ballista Tier 3 distance is reached.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier3Multiplier
        {
            get => _ballistaTier3Multiplier;
            set
            {
                if (_ballistaTier3Multiplier != value)
                {
                    _ballistaTier3Multiplier = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 4 Distance",
            10f,
            2000f,
            "#0",
            Order = 10,
            RequireRestart = false,
            HintText = "Minimum distance (in meters) for Ballista XP Tier 4 (exceptional shots).")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier4Distance
        {
            get => _ballistaTier4Distance;
            set
            {
                if (_ballistaTier4Distance != value)
                {
                    _ballistaTier4Distance = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyFloatingInteger(
            "Ballista Tier 4 Multiplier",
            1f,
            20f,
            "#0.00",
            Order = 11,
            RequireRestart = false,
            HintText = "XP multiplier applied when Ballista Tier 4 distance is reached.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public float BallistaTier4Multiplier
        {
            get => _ballistaTier4Multiplier;
            set
            {
                if (_ballistaTier4Multiplier != value)
                {
                    _ballistaTier4Multiplier = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyBool(
            "Enable Ballista XP Debug Messages",
            Order = 99,
            RequireRestart = false,
            HintText = "If enabled, debug messages will be displayed when gaining Shipmaster XP from ballista damage.")]
        [SettingPropertyGroup("Ballista XP Settings")]
        public bool BallistaXpDebug
        {
            get => _ballistaXpDebug;
            set
            {
                if (_ballistaXpDebug != value)
                {
                    _ballistaXpDebug = value;
                    ConfigCache.Refresh(this);
                    OnPropertyChanged();
                }
            }
        }
    }
}
