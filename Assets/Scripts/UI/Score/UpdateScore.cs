using System;
using System.Linq;
using Core.Managers.ScoreSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Score {
    public class UpdateScore : MonoBehaviour
    {
        [SerializeField] private ScoreHolder scoreHolder;
        [SerializeField] private string tagOfScoreText = "ScoreText";
        private Text[] _texts;

        // Initialize the _texts array which contain the ScoreUIText objects
        private void Awake() {
            if (!scoreHolder) {
                throw new Exception("Set the ScoreHolder to UpdateScore");
            }

            _texts = (from text in gameObject.GetComponentsInChildren<Text>()
                where text.CompareTag(tagOfScoreText) 
                select text).ToArray();
        }

        private void FixedUpdate() {
            foreach (var text in _texts) {
                text.text = scoreHolder.GetIntScore().ToString();
            }
        }
    }
}
