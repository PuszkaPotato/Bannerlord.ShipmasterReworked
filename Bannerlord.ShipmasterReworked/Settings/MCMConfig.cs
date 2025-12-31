using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Attributes;

namespace Bannerlord.ShipmasterReworked.Settings
{
    public sealed class MCMConfig : AttributeGlobalSettings<MCMConfig>
    {
        private float _travelXpMultiplier = 3.0f;
        private bool _travelXpDebug = false;

        private bool _enableStormTravelXp = true;
        private float _stormTravelXpMultiplier = 2f;

        private int _rammingXpBase = 80;    
        private float _rammingXpQualityFactor = 0.15f;
        private bool _rammingXpDebug = false;
        
        public override string Id => ModuleInfo.Id;
        public override string DisplayName => ModuleInfo.DisplayName;
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
    }
}
