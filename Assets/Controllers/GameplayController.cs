using UnityEngine;
using System.Collections.Generic;

using Models;
using Models.Data;

using Views;

using Models.Interface;
using Services.Factory;
using Services.Interface;

namespace Controllers {
    public class GamePlayController : MonoBehaviour {

        [Header("System Configuration")]
        [SerializeField] private SettingsData _settingsData;
        [SerializeField] private Location _serviceLocation;

        // Models
        private ChartModel _chartModel;
        private SettingsModel _settingsModel;

        [Header("View Configuration")]
        [SerializeField] private ChartView _chartView;

        private int _gameTime = 0;
        private float _currentBpm = 0;
        private float _bpmInterval = 0;
        private int _bpmChangeEnd = 0;

        private void Awake() {
            IDataService dataService = DataServiceFactory.GetDataService(_serviceLocation);
            _chartModel = new ChartModel(dataService);
            _settingsModel = new SettingsModel(_settingsData);

            _chartModel.OnNoteDataRemoved += (notes) => {
                _chartView.CreateNote(notes);
            };
        }

        private void Start() {
            _chartModel.LoadData("Charts/Lemuria/easy.chart");
        }
        
        private void FixedUpdate() {
            _updateNoteData();
            _updateBpm();
            _gameTime++;
        }

        private void Update() {
            _chartView.Render(_currentBpm);
        }

        private void _updateNoteData() {
            List<NoteData> notes = _chartModel.GetAndRemoveNoteData(_gameTime);
            if (notes == null) return;
            foreach (NoteData note in notes) {
                if (note.Type == NoteType.Bpm) {
                    _bpmChangeEnd = note.EndTime;
                    if (note.EndTime - note.HitTime == 0) {
                        this._currentBpm = note.NoteSpeed;
                    }else {
                        this._bpmInterval = note.NoteSpeed / (note.EndTime - note.HitTime);
                        this._bpmChangeEnd = note.EndTime;
                    }
                    continue;
                }
            }
        }

        private void _updateBpm() {
            if (this._gameTime == this._bpmChangeEnd)  {
                this._bpmChangeEnd = 0;
                this._bpmInterval = 0;
            }
            
            if (this._bpmInterval != 0) {
                this._currentBpm += this._bpmInterval;
            }
        }
    }
}