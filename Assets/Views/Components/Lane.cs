using System;
using System.Collections.Generic;
using Models.Data;
using UnityEngine;

namespace Views.Components {
    public class Lane : MonoBehaviour {
        public GameObject SpawnPoint;
        public GameObject Bg;
        public Color DefaultColor;
        public Color TriggeredColor;
        public float Duarion;

        private bool _triggered = false;

        private void Update() {
            if (this._triggered) {
                Bg.GetComponent<Renderer>().material.color = Color.Lerp(Bg.GetComponent<Renderer>().material.color, TriggeredColor, Duarion);
            } else {
                Bg.GetComponent<Renderer>().material.color = Color.Lerp(Bg.GetComponent<Renderer>().material.color, DefaultColor, Duarion);
            }
        }

        public void Trigger() {
            this._triggered = !this._triggered;
        }
    }
}