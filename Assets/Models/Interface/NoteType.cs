using System;

namespace Models.Interface {
    [Serializable]
    public enum NoteType {
        Single = 0,
        Hold = 1,
        Flick = 2,
        Bpm = 3,
    }
}