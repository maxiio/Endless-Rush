using System;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Managers.ScoreSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOver {
	public class GameOverScoreDisplay : MonoBehaviour {
		[SerializeField] private ScoreManager scoreManager;
		[SerializeField] private string tagOfCurrentScoreText = "CurrentScoreText";
		[SerializeField] private string tagOfMaxScoreText = "MaxScoreText";
		private Text[] _textsOfCurrentScore;
		private Text[] _textsOfMaxScore;

		// Initialize the _texts array which contain the ScoreUIText and MaxScoreUIText objects
		private void Awake() {
			if (!scoreManager) {
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

		private void OnEnable() {
			foreach (var text in _textsOfCurrentScore) {
				SetScoreToText(text, (int)scoreManager.CurrentScore);
			}

			foreach (var text in _textsOfMaxScore) {
				SetScoreToText(text, (int)scoreManager.MAXScore);
			}
		}

		private Text[] FindTextsByTag(GameObject currentGameObject, string currentTag) {
			return (from text in currentGameObject.GetComponentsInChildren<Text>()
				where text.CompareTag(currentTag)
				select text).ToArray();
		}

		private void SetScoreToText(Text text, int score) {
			text.text = ReplaceNumberForNewNumber(text.text, score.ToString());
		}

		public static string ReplaceNumberForNewNumber(string defaultString, string newNumber) {
			var newString = Regex.Replace(defaultString, "[0-9]{1,}", newNumber);
			return newString;
		}
	}
}