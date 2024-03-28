using System;

namespace Models.Interface {
    [Serializable]
    public enum NoteType {
        Single,
        Hold,
        Flick,
        Bpm,
    }
}