using System;
using UnityEngine;

namespace Core.Components.Health {
	public class HealthComponent : MonoBehaviour {
		public event EventHandler IsDieEvent;
		public event EventHandler<float> CurrentHealth;

		public bool isDamageOther;
		[SerializeField] protected float health;
		[SerializeField] protected bool isDestroyed;

		private float _defaultHealth;
		private bool _isDie;

		public virtual float Health {
			get => health;
			set {
				health = value;
				CurrentHealth?.Invoke(this, value);
				if (health < 1) {
					IsDie = true;
					if (isDestroyed) {
						Destroy(gameObject);
					}
				}
			}
		}

		protected bool IsDie {
			get => _isDie;
			private set {
				_isDie = value;
				if (value) {
					IsDieEvent?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void Awake() {
			_defaultHealth = health;
		}

		public virtual void Hit(float damage) {
			if (_isDie) {
				return;
			}

			if (damage <= Health) {
				Health -= damage;
			}
			else {
				Health -= Health;
			}
		}

		public void Heal(float healthToHeal) {
			if (!_isDie) {
				Health += healthToHeal;
			}
		}

		public void Revive(GameObject gameObjectToChange) {
			Health = _defaultHealth;
			IsDie = false;
		}
	}
}