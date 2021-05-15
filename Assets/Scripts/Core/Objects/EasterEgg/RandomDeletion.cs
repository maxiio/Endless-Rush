using UnityEngine;

namespace Core.Objects.EasterEgg {
	internal class RandomDeletion : MonoBehaviour {
		[SerializeField] private float chanceToDestroy = 0.9f;
		private readonly int _maxValue = 1000;

		private void Start() {
			var rand = new System.Random();
			var isDestroyed = rand.Next(_maxValue) < chanceToDestroy * _maxValue;
			if (isDestroyed) {
				Destroy(gameObject);
			}
		}
	}
}