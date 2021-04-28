using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreHolder _scoreHolder;
    [SerializeField] private string tagOfCurrentScoreText = "CurrentScoreText";
    [SerializeField] private string tagOfMaxScoreText = "MaxScoreText";
    private Text[] _textsOfCurrentScore;
    private Text[] _textsOfMaxScore;

    // Initialize the _texts array which contain the ScoreUIText and MaxScoreUIText objects
    private void Awake() {
        if (!_scoreHolder) {
            throw new Exception("Set the ScoreHolder to UpdateScore");
        }

        _textsOfCurrentScore = FindTextsByTag(gameObject, tagOfCurrentScoreText);
        _textsOfMaxScore = FindTextsByTag(gameObject, tagOfMaxScoreText);

        if (_textsOfCurrentScore.Length == 0) {
            Debug.LogWarning("_textsOfCurrentScore is empty");
        }

        if (_textsOfCurrentScore.Length == 0) {
            Debug.LogWarning("_textsOfMaxScore is empty");
        }
    }    

    private void Start() {
        foreach (var text in _textsOfCurrentScore) {
            SetScoreToText(text, _scoreHolder.GetIntScore());
        }

        foreach (var text in _textsOfMaxScore) {
            SetScoreToText(text, _scoreHolder.GetIntMaxScore());
        }
    }

    private Text[] FindTextsByTag(GameObject currentGameObject, string tag) {
        return (from text in currentGameObject.GetComponentsInChildren<Text>()
                where text.CompareTag(tag)
                select text).ToArray();
    }

    private void SetScoreToText(Text text, int score) {
        text.text = ReplaceNumberForNewNumber(text.text, score.ToString());
    }

    private string ReplaceNumberForNewNumber(string defaultString, string newNumber) {
        string newString = Regex.Replace(defaultString, "[0-9]{2,}", newNumber);
        return newString;
    }
}
