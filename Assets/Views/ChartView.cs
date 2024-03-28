using System.Collections.Generic;
using Models.Data;
using UnityEngine;
using Views.Components;

namespace Views {
    public class ChartView: MonoBehaviour {
        private Lane[] lanes;

        private void Awake() {
            lanes = GetComponentsInChildren<Lane>();
        }

        public void Render(List<NoteData> notes) {
        }

        public void SetLaneBpm(float bpm) {
        }

        public void Hit(int laneIndex) {
        }

        public void Remove(int laneIndex) {
        }
    }
}