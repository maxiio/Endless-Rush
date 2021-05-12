using Core.Managers.ScoreSystem;
using Core.Player.Health;
using UnityEngine;

namespace Core.Managers.ScoreSystem {
	public class ScoreManager : MonoBehaviour {
		[SerializeField] private bool resetScore;

		private float _currentScore;
		private float _maxScore;
		private ScoreData _scoreData;

		private ScoreSaver _scoreSaver;

		public float Score {
			get => _currentScore;
			set {
				if (value >= 0) {
					_currentScore = value;
					if (_maxScore < _currentScore) {
						_maxScore = _currentScore;
						_scoreSaver.SaveScoreData(_currentScore);
					}
				}
			}
		}

		public ScoreData ScoreData {
			set { _scoreData = value; }
			get { return _scoreData; }
		}

		public float MAXScore {
			set { _maxScore = value; }
			get { return _maxScore; }
		}

		public int GetIntScore() {
			return (int) _currentScore;
		}

		public int GetIntMaxScore() {
			return (int) _maxScore;
		}

		// Subscribe to changes of health component
		private void Awake() {
			//_scoreSaver = GetComponent<ScoreSaver>();
			
			// Get the serialized data
			_maxScore = _scoreSaver.GetScoreData(this);

			// Reset local max score
			if (resetScore) {
				_scoreSaver.ResetScore(this);
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
			_scoreSaver.Add(amountOfChangedHealth, this);
		}
	}
}

public class ScoreSaver : MonoBehaviour {
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

	public void Add(float amount, ScoreManager scoreManager) {
		scoreManager.Score += amount;
	}

	public float GetScoreData(ScoreManager scoreManager) {
		scoreManager.ScoreData = new ScoreData();
		if (PlayerPrefs.HasKey(ScoreData.MAXScoreName)) {
			return scoreManager.ScoreData.maxScore = PlayerPrefs.GetFloat(ScoreData.MAXScoreName);
		}

		return 0f;
	}

	public void ResetScore(ScoreManager scoreManager) {
		scoreManager.MAXScore = 0;
		Debug.Log("Score is reset!");
	}

	public void SaveScoreData(float value) {
		Debug.Log("Save Score");
		PlayerPrefs.SetFloat(ScoreData.MAXScoreName, value);
	}

	public void Reset(ScoreManager scoreManager) {
		scoreManager.Score = 0;
	}
}