using UnityEngine;

namespace Core.Managers.ScoreSystem {
	public class ScoreSaver : MonoBehaviour {
		public float Score {
			get => GetScoreData();
			set => SaveScoreData(value);
		}

		private float GetScoreData() {
			if (PlayerPrefs.HasKey(PrefNames.MAXScoreName)) {
				return PlayerPrefs.GetFloat(PrefNames.MAXScoreName);
			}

			return 0f;
		}

		private void SaveScoreData(float value) {
			PlayerPrefs.SetFloat(PrefNames.MAXScoreName, value);
		}
	}
}