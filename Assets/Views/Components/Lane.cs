using System.Collections.Generic;
using Models.Data;
using UnityEngine;

namespace Views.Components {
    public class Lane : MonoBehaviour {
        public List<Note> Notes = new List<Note>();
        public SettingsData Settings;
        public GameObject NotePrefab;
        public GameObject SpawnPoint;

        private void Awake() {
            SpawnPoint = transform.Find("SpawnPoint").gameObject;
        }
        public void RenderNote(NoteData noteData) {
            GameObject note = Instantiate(NotePrefab, SpawnPoint.transform);
            Note data = note.GetComponent<Note>();
            data.HitTime = noteData.HitTime;
            data.NoteSpeed = noteData.NoteSpeed * Settings.Speed / 100;
            Notes.Add(data);
        }
        public void BpmChange(float bpm) {
            foreach (Note note in Notes) {
                note.UpdateNote(bpm);
            }
        }
    }
}