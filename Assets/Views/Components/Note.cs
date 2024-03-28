using UnityEngine;

namespace Views.Components {
    public class Note : MonoBehaviour {
        public float NoteSpeed;
        public float BPM;
        public int HitTime;

        private void Update() {
            this.transform.Translate(this._currentSpeed() * Time.deltaTime * Vector3.down);
        }
        private float _currentSpeed() {
            return (1/BPM)*(1/60)* NoteSpeed;
        }
    }
}