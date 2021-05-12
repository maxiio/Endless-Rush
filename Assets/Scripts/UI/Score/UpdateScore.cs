using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private ScoreHolder _scoreHolder;
    [SerializeField] private string tagOfScoreText = "ScoreText";
    private Text[] _texts;

    // Initialize the _texts array which contain the ScoreUIText objects
    private void Awake() {
        if (!_scoreHolder) {
            throw new Exception("Set the ScoreHolder to UpdateScore");
        }

        _texts = (from text in gameObject.GetComponentsInChildren<Text>()
                  where text.CompareTag(tagOfScoreText) 
                  select text).ToArray();
    }

    private void FixedUpdate() {
        foreach (var text in _texts) {
            text.text = _scoreHolder.GetIntScore().ToString();
        }
    }
}
