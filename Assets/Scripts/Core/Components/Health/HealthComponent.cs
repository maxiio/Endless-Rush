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
						DestroyObject(gameObject);
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

		private static void ImplementObject(GameObject gameObjectToImplement) {
			gameObjectToImplement.GetComponentInChildren<Collider>().enabled = true;
			gameObjectToImplement.GetComponentInChildren<Renderer>().enabled = true;
		}

		private static void DestroyObject(GameObject gameObjectToDestroy) {
			//gameObject.GetComponentInChildren<Collider>().enabled = false;
			//gameObject.GetComponentInChildren<Renderer>().enabled = false;
			Destroy(gameObjectToDestroy);
		}

		public void SetDefaultHealth(GameObject gameObjectToChange) {
			ImplementObject(gameObjectToChange);
			Health = _defaultHealth;
		}
	}
}