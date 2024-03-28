using System;
using Models.Interface;

namespace Models.Data {
    [Serializable]
    public struct NoteData {
        public NoteType Type;
        public int HitTime;
        public float NoteSpeed;
        public int Duration;
        public int Lane;
    }
}