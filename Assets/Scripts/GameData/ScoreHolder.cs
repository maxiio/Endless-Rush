﻿using System;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{
    private float _currentScore = 0;
    private float _maxScore;

    public float Score { 
        get => _currentScore;
        protected set {
            if (value >= 0) {
                _currentScore = value;
                if (_maxScore < _currentScore) {
                    _maxScore = _currentScore;
                }
            }
        } 
    }

    public int GetIntScore() => (int)_currentScore;

    public void Add(float amount) {
        Score += amount;
    }

    // If we need to restart level
    public void Reset() {
        Score = 0;
    }

    // Subscribe to changers of health component
    private void Awake() {
        HealthComponent.AmountOfChangedHealth += AddScoreListner;
    }

    // Unsubscribe to changers of health component
    private void OnDestroy() {
        HealthComponent.AmountOfChangedHealth -= AddScoreListner;
    }

    private void AddScoreListner(object sender, float amountOfChangedHealth) {
        Add(amountOfChangedHealth);
    }
}
