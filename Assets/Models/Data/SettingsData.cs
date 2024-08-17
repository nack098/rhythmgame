using UnityEngine;

namespace Models.Data {
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Data/SettingsData")]
    public class SettingsData : ScriptableObject {
        public float MusicVolume;
        public float SFXVolume;
        public float Speed;
        public int Offset;
    }
}