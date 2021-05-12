using Core.Components.Health;
using UnityEngine;

namespace Core.Objects.Healer {
	public class HealerHealth : HealthComponent {
		[SerializeField] private string playerTag = "Player";

		// No hits to healer
		public override void Hit(float damage) {
		}

		private void OnTriggerEnter(Collider otherCollider) {
			if (otherCollider.gameObject.CompareTag(playerTag)) {
				var playerHealth = otherCollider.gameObject.GetComponent<HealthComponent>();
				playerHealth.Heal(Health);

				Destroy(gameObject);
			}
		}
	}
}