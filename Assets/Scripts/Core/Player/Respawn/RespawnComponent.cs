using UnityEngine;

namespace Core.Player.Respawn {
	public class RespawnComponent : MonoBehaviour {
		[SerializeField] private bool isSpawnPosition;
		[SerializeField] private Vector3 spawnPosition;

		private void Awake() {
			if (isSpawnPosition) {
				spawnPosition = gameObject.transform.position;
			}
		}

		public void Respawn() {
			gameObject.transform.position = spawnPosition;
		}
	}
}