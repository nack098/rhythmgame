using System;
using System.Collections.Generic;
using System.IO;
using Models.Data;
using Models.Interface;
using Services.Interface;
using UnityEngine;

namespace Models {
    public class ChartModel {
        readonly private Dictionary<int, List<NoteData>> _noteData = new Dictionary<int, List<NoteData>>();
        readonly private IDataService _dataService;
        
        public Action<int, List<NoteData>> OnNoteDataAdded;
        public Action<List<NoteData>> OnNoteDataRemoved;

        public ChartModel(IDataService dataService) {
            _dataService = dataService;
        }

        public void AddNoteData(int showTime, NoteData noteData) {
            if (!_noteData.ContainsKey(showTime)) {
                _noteData[showTime] = new List<NoteData>();
            }
            _noteData[showTime].Add(noteData);
            OnNoteDataAdded?.Invoke(showTime, _noteData[showTime]);
        }

        /// <summary>
        /// Load Data From Location
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="difficulty"></param>
        public void LoadData(string location) {
            string fileData = _dataService.GetData<string>(Path.Combine(Application.dataPath, location));
            AddNoteData(0, new NoteData(NoteType.Bpm, 0, 160, 0, 0));
            AddNoteData(0, new NoteData(NoteType.Single, 0, 1, 0, 3));
            AddNoteData(0, new NoteData(NoteType.Single, 10, 1, 0, 3));
            AddNoteData(0, new NoteData(NoteType.Single, 15, 1, 0, 3));
            AddNoteData(0, new NoteData(NoteType.Single, 20, 1, 0, 3));
            AddNoteData(0, new NoteData(NoteType.Single, 25, 1, 0, 3));
        }

        /// <summary>
        /// Get Note Data At Specific Time
        /// </summary>
        /// <param name="timeInMiliseconds"></param>
        /// <returns></returns>
        public List<NoteData> GetNoteData(int timeInMiliseconds) {
            if (_noteData.ContainsKey(timeInMiliseconds)) {
                return _noteData[timeInMiliseconds];
            }
            return null; 
        }

        /// <summary>
        /// Get And Remove Note Data At Specific Time
        /// </summary>
        /// <param name="timeInMiliseconds"></param>
        /// <returns></returns>
        public List<NoteData> GetAndRemoveNoteData(int timeInMiliseconds) {
            if (_noteData.ContainsKey(timeInMiliseconds)) {
                List<NoteData> noteData = _noteData[timeInMiliseconds];
                _noteData.Remove(timeInMiliseconds);

                OnNoteDataRemoved?.Invoke(noteData);
                return noteData;
            }
            return null; 
        }
    }
}