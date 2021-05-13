using UnityEngine;

namespace SaveData {
	public class PrefDataSaver : MonoBehaviour, IDataSaver {
		public float GetData(string nameOfScore) {
			return PlayerPrefs.HasKey(nameOfScore) ? PlayerPrefs.GetFloat(nameOfScore) : 0f;
		}

		public void SaveData(string nameOfScore, float value) {
			PlayerPrefs.SetFloat(nameOfScore, value);
		}
	}
}