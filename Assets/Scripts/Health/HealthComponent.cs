using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public static event EventHandler IsDie;
	public event EventHandler<float> CurrentHealth;

	[SerializeField] protected float _health;
	[SerializeField] protected bool _isDestroyed = false;

	private float _defaultHealth;

	public virtual float Health {
		get => _health;
		protected set {
			_health = value;
			CurrentHealth?.Invoke(this, value);
			if (_health < 1) {
				IsDie?.Invoke(this, EventArgs.Empty);
				if (_isDestroyed) {
					DestroyObject(gameObject);
                }
			}
		}
	}

    private void Awake() {
		_defaultHealth = _health;
	}

    public virtual void Hit(float damage) {
		Health -= damage;
	}

	public void Heal(float health) {
		Health += health;
    }

	private void StroyObject(GameObject gameObject) {
		gameObject.GetComponentInChildren<Collider>().enabled = true;
		gameObject.GetComponentInChildren<Renderer>().enabled = true;
	}

	private void DestroyObject(GameObject gameObject) {
		gameObject.GetComponentInChildren<Collider>().enabled = false;
		gameObject.GetComponentInChildren<Renderer>().enabled = false;
	}

    public void SetDefaultHealth(GameObject gameObject) {
		StroyObject(gameObject);
		Health = _defaultHealth;
	}
}
