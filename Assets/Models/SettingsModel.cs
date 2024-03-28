using Models.Data;

namespace Models {
    public class SettingsModel {
        private SettingsData _settingsData;
        public SettingsModel(SettingsData settingsData) {
            _settingsData = settingsData;
        }

        public float MusicVolume {
            get => _settingsData.MusicVolume;
            set => _settingsData.MusicVolume = value;
        }

        public float SFXVolume {
            get => _settingsData.SFXVolume;
            set => _settingsData.SFXVolume = value;
        }

        public float Speed {
            get => _settingsData.Speed;
            set => _settingsData.Speed = value;
        }

        public float Offset {
            get => _settingsData.Offset;
            set => _settingsData.Offset = value;
        }
    }
}