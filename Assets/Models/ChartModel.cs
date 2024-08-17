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
        /// <param name="location">the location of a file</param>
        public void LoadData(string location, float SettingSpeed = 0) {
            string fileData = _dataService.GetData<string>(Path.Combine(Application.dataPath, location));
            string[] lines = fileData.Split('\n');
            float bpm = 0;
            foreach (string line in lines) {
                string[] data = line.Split(',');
                int lane = int.Parse(data[4]);
                int hitTime = int.Parse(data[0]);
                int endTime = int.Parse(data[3]);
                float noteSpeed = float.Parse(data[2]);
                NoteType type = (NoteType) Enum.Parse(typeof(NoteType), data[1]);

                if (type == NoteType.Bpm) {
                    bpm = noteSpeed;
                }
                if (bpm == 0 || SettingSpeed == 0) {
                    AddNoteData(hitTime, new NoteData {
                        Lane = lane,
                        HitTime = hitTime,
                        EndTime = endTime,
                        NoteSpeed = noteSpeed,
                        Type = type 
                    });
                }else {
                    float speed = bpm * noteSpeed * SettingSpeed / 100;
                    AddNoteData(hitTime - (int) (34000 / speed), new NoteData {
                        Lane = lane,
                        HitTime = hitTime,
                        EndTime = endTime,
                        NoteSpeed = noteSpeed,
                        Type = type 
                    });
                }
            }
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