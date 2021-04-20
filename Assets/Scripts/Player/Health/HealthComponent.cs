using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public static event EventHandler IsDie;

	[SerializeField] private float _health;
	[SerializeField] private bool _isDestroyed = false;

	public float Health {
		get => _health;
		private set {
			_health = value;
			if (_health <= 0) {
				IsDie?.Invoke(this, EventArgs.Empty);
				if (_isDestroyed) {
					Destroy(gameObject);
                }
			}
		}
	}

	public void Hit(float damage) {
		Health -= damage;
	}

	public void Heal(float health) {
		Health += health;
    }
}
