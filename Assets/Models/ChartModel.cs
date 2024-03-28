using System;
using System.Collections.Generic;
using System.IO;
using Models.Data;
using Services.Interface;
using UnityEngine;

namespace Models {
    public class ChartModel {
        readonly private Dictionary<int, List<NoteData>> _noteData = new Dictionary<int, List<NoteData>>();
        readonly private IDataService _dataService;
        
        private Action<int, List<NoteData>> _onNoteDataAdded;
        private Action<List<NoteData>> _onNoteDataRemoved;

        public ChartModel(IDataService dataService) {
            _dataService = dataService;
        }

        public void AddNoteData(int showTime, NoteData noteData) {
            if (!_noteData.ContainsKey(showTime)) {
                _noteData[showTime] = new List<NoteData>();
            }
            _noteData[showTime].Add(noteData);

            _onNoteDataAdded?.Invoke(showTime, _noteData[showTime]);
        }

        /// <summary>
        /// Load Data From Location
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="difficulty"></param>
        public void LoadData(string location) {
            string fileData = _dataService.GetData<string>(Path.Combine(Application.dataPath, location));
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

                _onNoteDataRemoved?.Invoke(noteData);
                return noteData;
            }
            return null; 
        }

        public void BindOnNoteDataAdded(Action<int, List<NoteData>> action) {
            _onNoteDataAdded += action;
        }

        public void BindOnNoteDataRemoved(Action<List<NoteData>> action) {
            _onNoteDataRemoved += action;
        }
    }
}