using System;
using UnityEngine;

public class ScoreHolder : MonoBehaviour { 
    private float _currentScore = 0;
    private float _maxScore;
    private ScoreData _scoreData;

    public float Score { 
        get => _currentScore;
        protected set {
            if (value >= 0) {
                _currentScore = value;
                if (_maxScore < _currentScore) {
                    _maxScore = _currentScore;
                    SaveScoreData(_currentScore);
                }
            }
        } 
    }
    
    public int GetIntScore() => (int)_currentScore;
    public int GetIntMaxScore() => (int)_maxScore;

    private void SaveScoreData(float value) {
        Debug.Log("Save Score");
        PlayerPrefs.SetFloat(ScoreData.maxScoreName, value);
    }

    public void Add(float amount) {
        Score += amount;
    }

    // If we need to restart level
    public void Reset() {
        Score = 0;
    }

    // Set the scoreData from the PlayerPrefs to save playerMaxScore
    private float GetScoreData() {
        _scoreData = new ScoreData();
        if (PlayerPrefs.HasKey(ScoreData.maxScoreName)) {
            return _scoreData.maxScore = PlayerPrefs.GetFloat(ScoreData.maxScoreName);
        }
        return 0f;
    }

    // Subscribe to changers of health component
    private void Awake() {
        // Get the serialized data
        _maxScore = GetScoreData();

        PlayerHealthComponent.AmountOfDicreasedHealth += AddScoreListner;
    }

    // Unsubscribe to changers of health component
    private void OnDestroy() {
        PlayerHealthComponent.AmountOfDicreasedHealth -= AddScoreListner;
    }

    private void AddScoreListner(object sender, float amountOfChangedHealth) {
        Add(amountOfChangedHealth);
    }
}
