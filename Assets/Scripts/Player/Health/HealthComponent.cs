using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public static event EventHandler IsDie;
	public event EventHandler<float> CurrentHealth;

	[SerializeField] protected float _health;
	[SerializeField] protected bool _isDestroyed = false;

	public virtual float Health {
		get => _health;
		protected set {
			_health = value;
			CurrentHealth?.Invoke(this, value);
			if (_health < 1) {
				IsDie?.Invoke(this, EventArgs.Empty);
				if (_isDestroyed) {
					Destroy(gameObject);
                }
			}
		}
	}

	public virtual void Hit(float damage) {
		Health -= damage;
	}

	public void Heal(float health) {
		Health += health;
    }
}
