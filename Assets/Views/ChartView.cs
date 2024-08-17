using System.Collections.Generic;
using System.Linq;
using Models.Data;
using Models.Interface;
using UnityEngine;
using Views.Components;

namespace Views {
    public class ChartView: MonoBehaviour {

        [SerializeField] private GameObject NotePrefab;
        [SerializeField] private SettingsData Settings;

        private Lane[] lanes;
        private List<Note> notes = new List<Note>();
        private List<Note> toBeRemoved = new List<Note>();

        private void Awake() {
            lanes = GetComponentsInChildren<Lane>();
        }

        public void Render(float bpm) {
            foreach (Note note in toBeRemoved) {
                notes.Remove(note);
                note.RemoveNote();
            }
            toBeRemoved.Clear();

            foreach (Note note in notes) {
                if (note.transform.position.y < -15) {
                    toBeRemoved.Add(note);
                }
            }

            foreach (Note note in notes) {
                note.UpdateNote(bpm);
            }
        }

        public void CreateNote(List<NoteData> notesToShow) {
            foreach (NoteData note in notesToShow) {
                if (note.Type == NoteType.Bpm || note.Type == NoteType.Hold) {
                    continue;
                }
                GameObject noteView = Instantiate(NotePrefab, lanes[note.Lane].SpawnPoint.transform);
                Note noteData = noteView.GetComponent<Note>();
                noteData.ActualSpeed = note.NoteSpeed * Settings.Speed / 100;
                noteData.Data = note;
                this.notes.Add(noteData);
            }
        }

        public void RemoveNotes(NoteData noteToRemove) {
            toBeRemoved.Add(notes.First(note => note.Data.Equals(noteToRemove)));
        }

        public void TriggerLane(int lane) {
            lanes[lane].Trigger();
        }
    }
}