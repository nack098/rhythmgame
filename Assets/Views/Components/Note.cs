using UnityEngine;

namespace Views.Components {
    public class Note : MonoBehaviour {
        public float NoteSpeed;
        public int HitTime;

        public void UpdateNote(float bpm) {
            this.transform.Translate(this._currentSpeed(bpm) * Time.deltaTime * Vector3.down);
        }
        private float _currentSpeed(float bpm) {
            return (1/bpm) * 60 * 1000 * NoteSpeed;
        }
    }
}