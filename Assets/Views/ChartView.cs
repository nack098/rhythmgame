using System.Collections.Generic;
using Models.Data;
using Models.Interface;
using UnityEngine;
using Views.Components;

namespace Views {
    public class ChartView: MonoBehaviour {
        private Lane[] lanes;

        private void Awake() {
            lanes = GetComponentsInChildren<Lane>();
        }

        public void Render(float bpm) {
            foreach (Lane lane in lanes) {
                lane.BpmChange(bpm);
            }
        }

        public void CreateNote(List<NoteData> notes) {
            foreach (NoteData note in notes) {
                if (note.Type == NoteType.Bpm) {
                    continue;
                }
                lanes[note.Lane].RenderNote(note);
            }
        }
    }
}