using Core.Player.Health;
using UnityEngine;

namespace Core.Managers.ScoreSystem {
	public class ScoreManager : MonoBehaviour {
		[SerializeField] private bool resetScore;
		public float CurrentScore { get; private set; }
		public float MAXScore { get; private set; }
		private const float DefaultMaxScore = 0;
		private ScoreSaver _scoreSaver;

		private void Awake() {
			_scoreSaver = GetComponent<ScoreSaver>();

			// Reset local max score
			if (resetScore) {
				ResetScore();
			}

			SetMaxScore(_scoreSaver.Score);
			Subscribe();
		}

		private void ResetScore() {
			SetMaxScore(DefaultMaxScore);
			SaveScore(DefaultMaxScore);
			Debug.Log("Score is reset!");
		}

		private void SetMaxScore(float score) {
			MAXScore = score;
		}

		private void Subscribe() {
			PlayerHealthComponent.AmountOfHealthToDecrease += AddScoreListener;
		}

		// Unsubscribe to changers of health component
		private void OnDestroy() {
			Unsubscribe();
		}

		private void Unsubscribe() {
			PlayerHealthComponent.AmountOfHealthToDecrease -= AddScoreListener;
		}

		// Invoke the Score function - Add
		private void AddScoreListener(object sender, float amountOfChangedHealth) {
			Add(amountOfChangedHealth);
		}

		private void Add(float amount) {
			CurrentScore += amount;
			if (CurrentScore > MAXScore) {
				SetMaxScore(CurrentScore);
				SaveScore(CurrentScore);
			}
		}

		private void SaveScore(float scoreToSave) {
			_scoreSaver.Score = scoreToSave;
		}
	}
}