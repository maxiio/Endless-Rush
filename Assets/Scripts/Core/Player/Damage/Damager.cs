using System;
using System.Collections.Generic;
using Core.Components.Health;
using UnityEngine;

namespace Core.Player.Damage {
	[ExecuteInEditMode]
	public class Damager : MonoBehaviour {
		// The summary of damaging objects with any health
		[SerializeField] [Range(1, 10)] private int hitSummaryTime = 1;

		[SerializeField] private float minDelay = 0.04f;

		// Add dictionary if we has multiple objects in trigger
		private Dictionary<int, IEnumerator<WaitForSeconds>> _coroutines;

		// The hit objects for this value != 1 because used in the math log
		private const int DefaultDamage = 1;

		private void Awake() {
			_coroutines = new Dictionary<int, IEnumerator<WaitForSeconds>>();

			if (!gameObject.GetComponent<HealthComponent>()) {
				throw new ArgumentException("This object doesn't has the HealthComponent");
			}
		}

		// Stop damage the both objects
		private void OnTriggerExit(Collider otherCollider) {
			var colliderHash = otherCollider.GetHashCode();
			if (_coroutines.ContainsKey(colliderHash)) {
				StopCoroutine(_coroutines[colliderHash]);
				_coroutines.Remove(colliderHash);
			}
		}

		// Start damage the both objects
		private void OnTriggerEnter(Collider otherCollider) {
			var colliderHash = otherCollider.GetHashCode();
			if (!_coroutines.ContainsKey(colliderHash)) {
				var colliderHealth = otherCollider.gameObject.GetComponent<HealthComponent>();
				var gameObjectHealth = gameObject.GetComponent<HealthComponent>();

				if (colliderHealth && colliderHealth.isDamageOther) {
					var minHealth = (int) Mathf.Min(colliderHealth.Health, gameObjectHealth.Health);
					var damage = CalculateDamage(minHealth);
					var delay = CalculateDelay(damage, minHealth);
					var coroutine = DamageObjects(colliderHealth, gameObjectHealth, damage, delay);
					StartCoroutine(coroutine);
					_coroutines.Add(colliderHash, coroutine);
				}
			}
		}

		private int CalculateDamage(int health) {
			if (health < DefaultDamage) {
				return health;
			}

			if (health / DefaultDamage > 10) {
				return (int) Mathf.Pow(10, Mathf.RoundToInt(Mathf.Log10(health)) - 1);
			}

			return DefaultDamage;
		}

		private float CalculateDelay(int damage, int health) {
			var delay = damage * hitSummaryTime / (float) health;
			if (float.IsNaN(delay) || delay < minDelay) {
				delay = minDelay;
			}

			return delay;
		}

		private IEnumerator<WaitForSeconds> DamageObjects(HealthComponent enemyHealth, HealthComponent playerHealth
			, float damage, float delay) {
			var hasHealth = enemyHealth && playerHealth;
			// ReSharper disable once LoopVariableIsNeverChangedInsideLoop because using the non-stop coroutine
			while (hasHealth) {
				if (enemyHealth.Health < damage) {
					damage = enemyHealth.Health;
				}

				enemyHealth.Hit(damage);
				playerHealth.Hit(damage);
				yield return new WaitForSeconds(delay);
			}
		}
	}
}