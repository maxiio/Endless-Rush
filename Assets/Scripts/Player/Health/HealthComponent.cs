using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public static event EventHandler IsDie;

	[SerializeField] protected float _health;
	[SerializeField] protected bool _isDestroyed = false;

	public virtual float Health {
		get => _health;
		protected set {
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
