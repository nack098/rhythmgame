using System;
using Models.Interface;

namespace Models.Data {
    [Serializable]
    public struct NoteData {
        public NoteType Type;
        public int HitTime;
        public float NoteSpeed;
        public int EndTime;
        public int Lane;
        public NoteData(NoteType type, int hitTime, float noteSpeed, int endTime, int lane) {
            this.Type = type;
            this.HitTime = hitTime;
            this.NoteSpeed = noteSpeed;
            this.EndTime = endTime;
            this.Lane = lane;
        } 
    }
}