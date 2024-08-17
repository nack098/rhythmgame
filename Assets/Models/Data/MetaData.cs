using System;
using UnityEngine;

namespace Models.Data {
    [CreateAssetMenu(fileName = "MetaData", menuName = "Data/MetaData")]
    public class MetaData: ScriptableObject {
        public string BPM;
        public string Title;
        public string Artist;
        public string Background;
        public AudioClip Music;
        public string Difficulty;
        public string ChartPath;
    }
}