using UnityEngine;
using System.Collections.Generic;

using Models;
using Models.Data;

using Views;

using Models.Interface;
using Services.Factory;
using Services.Interface;
using Services.Utils;
using System.IO;
using UnityEngine.InputSystem;
using System;
using Views.Components;
using UnityEngine.InputSystem.Composites;

namespace Controllers {
    public class GamePlayController : MonoBehaviour {

        [Header("System Configuration")]
        [SerializeField] private SettingsData _settingsData;
        [SerializeField] private Location _serviceLocation;
        [SerializeField] private int PerfectThreshold;
        [SerializeField] private int GreatThreshold;
        [SerializeField] private int MissThreshold;
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private MetaData metaData;

        // Models
        private ChartModel _chartModel;

        [Header("View Configuration")]
        [SerializeField] private ChartView _chartView;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TMPro.TextMeshProUGUI _comboText;
        [SerializeField] private TMPro.TextMeshProUGUI _perfectText;
        [SerializeField] private TMPro.TextMeshProUGUI _greatText;
        [SerializeField] private TMPro.TextMeshProUGUI _missText;

        private int _gameTime = -5000;
        private float _currentBpm = 0;
        private int _combo = 0;
        private int _perfect = 0;
        private int _great = 0;
        private int _miss = 0;
        private Dictionary<int, List<NoteData>> _noteData = new Dictionary<int, List<NoteData>>() {
            {0, new List<NoteData>()},
            {1, new List<NoteData>()},
            {2, new List<NoteData>()},
            {3, new List<NoteData>()},
        };

        private IDataService _dataService;

        private void Awake() {
            _dataService = DataServiceFactory.GetDataService(_serviceLocation);
            _chartModel = new ChartModel(_dataService);

            _chartModel.OnNoteDataRemoved += (notes) => {
                _chartView.CreateNote(notes);
            };
            _enableInputActions();
        }

        private void Start() {
            _chartModel.LoadData(metaData.ChartPath, _settingsData.Speed);
            _audioSource.clip = metaData.Music;
        }
        
        private void FixedUpdate() {
            if (_gameTime == _settingsData.Offset) {
               _audioSource.Play(); 
            }

            foreach (List<NoteData> notes in _noteData.Values) {
                if (notes.Count == 0) continue;
                NoteData note = notes[0];
                if (note.HitTime - _gameTime < -MissThreshold) {
                    Debug.Log("Miss");
                    GameObject.Find(string.Format("Lane {0}/Miss", note.Lane+1)).GetComponent<Animator>().SetTrigger("Trigger");
                    notes.RemoveAt(0);
                    _combo = 0;
                    _miss++;
                }
            }

            _updateNoteData();
            _audioSource.volume = _settingsData.MusicVolume;
            _chartView.Render(_currentBpm);
            _comboText.text = _combo.ToString();
            _perfectText.text = "Perfect: " + _perfect.ToString();
            _greatText.text = "Great: " + _great.ToString();
            _missText.text = "Miss: " + _miss.ToString();

            _gameTime++;
        }

        private void _updateNoteData() {
            List<NoteData> notes = _chartModel.GetAndRemoveNoteData(_gameTime);
            if (notes == null) return;
            foreach (NoteData note in notes) {
                switch (note.Type) {
                    case NoteType.Single:
                        _noteData[note.Lane].Add(note);
                        break;
                    case NoteType.Bpm:
                        _currentBpm = note.NoteSpeed;
                        break;
                    default:
                        break;
                }
            }
        }

        private void _enableInputActions() {
            inputActions.FindAction("Gameplay/A Lane").performed += ctx => _checkNoteHit(0);
            inputActions.FindAction("Gameplay/B Lane").performed += ctx => _checkNoteHit(1);
            inputActions.FindAction("Gameplay/C Lane").performed += ctx => _checkNoteHit(2);
            inputActions.FindAction("Gameplay/D Lane").performed += ctx => _checkNoteHit(3);
            inputActions.FindAction("Gameplay/A Lane").canceled += ctx => _chartView.TriggerLane(0);
            inputActions.FindAction("Gameplay/B Lane").canceled += ctx => _chartView.TriggerLane(1);
            inputActions.FindAction("Gameplay/C Lane").canceled += ctx => _chartView.TriggerLane(2);
            inputActions.FindAction("Gameplay/D Lane").canceled += ctx => _chartView.TriggerLane(3);
            inputActions.FindActionMap("Gameplay").Enable();
        }

        private void _checkNoteHit(int lane) {
            _chartView.TriggerLane(lane);
            List<NoteData> notes = _noteData[lane];
            if (notes.Count == 0) return;
            NoteData note = notes[0];
            int hitOffset = note.HitTime - _gameTime;
            Debug.Log(String.Format("hit on {0} supposed to hit on {1} diff {2}", _gameTime, note.HitTime, hitOffset));
            if (hitOffset <= PerfectThreshold && hitOffset >= -PerfectThreshold) {
                Debug.Log("Perfect");
                GameObject.Find(string.Format("Lane {0}/Perfect", lane+1)).GetComponent<Animator>().SetTrigger("Trigger");
                notes.RemoveAt(0);
                _chartView.RemoveNotes(note);
                _combo++;
                _perfect++;
            } else if (hitOffset <= GreatThreshold && hitOffset >= -GreatThreshold) {
                Debug.Log("Great");
                GameObject.Find(string.Format("Lane {0}/Great", lane+1)).GetComponent<Animator>().SetTrigger("Trigger");
                notes.RemoveAt(0);
                _chartView.RemoveNotes(note);
                _combo++;
                _great++;
            } else if (hitOffset <= MissThreshold && hitOffset >= -MissThreshold) {
                Debug.Log("Miss");
                GameObject.Find(string.Format("Lane {0}/Miss", lane+1)).GetComponent<Animator>().SetTrigger("Trigger");
                notes.RemoveAt(0);
                _chartView.RemoveNotes(note);
                _combo = 0;
                _miss++;
            }
        }
    }
}