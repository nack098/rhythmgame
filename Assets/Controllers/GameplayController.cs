using UnityEngine;

using Models;
using Models.Data;
using Services.Factory;
using Services.Interface;
using System.Collections.Generic;
using System.Linq;
using Views;
using Models.Interface;

namespace Controller {
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

        private void Awake() {
            IDataService dataService = DataServiceFactory.GetDataService(_serviceLocation);
            _chartModel = new ChartModel(dataService);
            _settingsModel = new SettingsModel(_settingsData);

            _chartModel.BindOnNoteDataRemoved(_chartView.Render);
        }

        private void Start() {
            _chartModel.LoadData("Charts/Lemuria/easy.chart");
        }
        
        private void FixedUpdate() {
            _gameTime++;
            foreach (NoteData note in _chartModel.GetAndRemoveNoteData(_gameTime)) {
                if (note.Type == NoteType.Bpm) {
                    _chartView.SetLaneBpm(note.NoteSpeed);
                }
                if (note.HitTime == _gameTime) {
                    _chartView.Hit(note.Lane);
                }
            }
        }
    }
}