﻿using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public static event EventHandler IsDie;
	public static event EventHandler<float> AmountOfChangedHealth;

	[SerializeField] protected float _health;
	[SerializeField] protected bool _isDestroyed = false;
	[SerializeField] protected bool _isPlayer = false;

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
		if (_isPlayer) {
			AmountOfChangedHealth?.Invoke(this, damage);
        }
	}

	public void Heal(float health) {
		Health += health;
    }
}
