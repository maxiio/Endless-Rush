using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public event EventHandler IsDieEvent;
	public event EventHandler<float> CurrentHealth;

	public bool IsDamageOther;
	[SerializeField] protected float _health;
	[SerializeField] protected bool _isDestroyed;

	private float _defaultHealth;
    private bool _isDie = false;

    public virtual float Health {
		get => _health;
		set {
			_health = value;
			CurrentHealth?.Invoke(this, value);
			if (_health < 1) {
				IsDie = true;
				if (_isDestroyed) {
					DestroyObject(gameObject);
                }
			}
		}
	}

    protected bool IsDie { 
		get => _isDie;
		set {
			_isDie = value;
			if (value) {
				IsDieEvent?.Invoke(this, EventArgs.Empty);
			}
		} 
	}

    private void Awake() {
		_defaultHealth = _health;
	}

    public virtual void Hit(float damage) {
		if (!_isDie) {
			if (damage <= Health) {
				Health -= damage;
            }
			else {
				Health -= Health;
			}
        }
	}

	public void Heal(float health) {
		if (!_isDie) {
			Health += health;
        }
    }

	protected void StroyObject(GameObject gameObject) {
		gameObject.GetComponentInChildren<Collider>().enabled = true;
		gameObject.GetComponentInChildren<Renderer>().enabled = true;
	}

	protected void DestroyObject(GameObject gameObject) {
		//gameObject.GetComponentInChildren<Collider>().enabled = false;
		//gameObject.GetComponentInChildren<Renderer>().enabled = false;
		Destroy(gameObject);
	}

    public void SetDefaultHealth(GameObject gameObject) {
		StroyObject(gameObject);
		Health = _defaultHealth;
	}
}
