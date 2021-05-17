namespace SaveData {
	public interface IDataSaver {
		float GetData(string nameOfScore);
		void SaveData(string nameOfScore, float value);
	}
}