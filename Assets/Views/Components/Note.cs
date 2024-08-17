using Models.Data;
using UnityEngine;

namespace Views.Components {
    public class Note : MonoBehaviour {
        public float ActualSpeed;
        public NoteData Data;

        private float _currentSpeed(float bpm) {
            return bpm * ActualSpeed;
        }

        public void UpdateNote(float bpm) {
            this.transform.Translate(this._currentSpeed(bpm) * Time.deltaTime * Vector3.down);
        }
        public void RemoveNote() {
            Destroy(this.gameObject);
        }
    }
}