using Core.Player.Health;
using UnityEngine;

namespace Core.Managers.ScoreSystem {
	public class ScoreManager : MonoBehaviour {
		[SerializeField] private bool resetScore;

		private float _currentScore;
		private float _maxScore;
		private ScoreData _scoreData;

		private float Score {
			get => _currentScore;
			set {
				if (value >= 0) {
					_currentScore = value;
					if (_maxScore < _currentScore) {
						_maxScore = _currentScore;
						SaveScoreData(_currentScore);
					}
				}
			}
		}

		public int GetIntScore() {
			return (int) _currentScore;
		}

		public int GetIntMaxScore() {
			return (int) _maxScore;
		}

		// Save local Score to playerPrefs
		private void SaveScoreData(float value) {
			Debug.Log("Save Score");
			PlayerPrefs.SetFloat(ScoreData.MAXScoreName, value);
		}

		// Increment the local Score on value
		private void Add(float amount) {
			Score += amount;
		}

		// If we need to restart level
		public void Reset() {
			Score = 0;
		}

		// Set the scoreData from the PlayerPrefs to save playerMaxScore
		private float GetScoreData() {
			_scoreData = new ScoreData();
			if (PlayerPrefs.HasKey(ScoreData.MAXScoreName)) {
				return _scoreData.maxScore = PlayerPrefs.GetFloat(ScoreData.MAXScoreName);
			}

			return 0f;
		}

		// Set local max score to zero
		private void ResetScore() {
			_maxScore = 0;
			Debug.Log("Score is reset!");
		}

		// Subscribe to changes of health component
		private void Awake() {
			// Get the serialized data
			_maxScore = GetScoreData();

			// Reset local max score
			if (resetScore) {
				ResetScore();
			}

			// Subscribe
			PlayerHealthComponent.AmountOfHealthToDecrease += AddScoreListener;
		}

		// Unsubscribe to changers of health component
		private void OnDestroy() {
			// Unsubscribe
			PlayerHealthComponent.AmountOfHealthToDecrease -= AddScoreListener;
		}

		// Invoke the Score function - Add
		private void AddScoreListener(object sender, float amountOfChangedHealth) {
			Add(amountOfChangedHealth);
		}
	}
}
