using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Damager : MonoBehaviour
{
    // The summary of damaging objects with any health
    [SerializeField] [Range(1, 10)] private int _hitSummaryTime = 1;

    [SerializeField] private float _minDelay = 0.04f;

    // Add dictionary if we has multiple objects in trigger
    private Dictionary<int, IEnumerator<WaitForSeconds>> _coroutines;
    // The hit objects for this value != 1 because used in the math log
    private const int _defaultDamage = 1;

    private void Awake() {
        _coroutines = new Dictionary<int, IEnumerator<WaitForSeconds>>();

        if (!gameObject.GetComponent<HealthComponent>()) {
            throw new ArgumentException("This object doesn't has the HealthComponent");
        }
    }

    // Stop damage the both objects
    private void OnTriggerExit(Collider collider) {
        var colliderHash = collider.GetHashCode();
        if (_coroutines.ContainsKey(colliderHash)) {
            StopCoroutine(_coroutines[colliderHash]);
            _coroutines.Remove(colliderHash);
        }        
    }

    // Start damage the both objects
    private void OnTriggerEnter(Collider collider) {
        int colliderHash = collider.GetHashCode();
        if (!_coroutines.ContainsKey(colliderHash)) {
            HealthComponent colliderHealth = collider.gameObject.GetComponent<HealthComponent>();
            HealthComponent gameObjectHealth = gameObject.GetComponent<HealthComponent>();

            if (colliderHealth && colliderHealth.IsDamageOther) {
                int minHealth = (int)Mathf.Min(colliderHealth.Health, gameObjectHealth.Health);
                int damage = CalculateDamage(minHealth);
                float delay = CalculateDelay(damage, minHealth);
                IEnumerator<WaitForSeconds> coroutine = DamageObjects(colliderHealth, gameObjectHealth, damage, delay);
                StartCoroutine(coroutine);
                _coroutines.Add(colliderHash, coroutine);
            }
        }            
    }

    private int CalculateDamage(int health) {
        if (health < _defaultDamage) {
            return health;
        }
        else {
            if (health / _defaultDamage > 10) {
                return (int)(Mathf.Pow(10, Mathf.RoundToInt(Mathf.Log10(health)) - 1));
            }
            return _defaultDamage;
        }
    }

    private float CalculateDelay(int damage, int health) {
        float delay = damage * _hitSummaryTime / (float)health;
        if (float.IsNaN(delay) || delay < _minDelay) {
            delay = _minDelay;
        }
        return delay;
    }

    IEnumerator<WaitForSeconds> DamageObjects(HealthComponent enemyHealth, HealthComponent playerHealth
            , float damage, float delay) {
        while (enemyHealth && playerHealth) {
            if (enemyHealth.Health < damage) {
                damage = enemyHealth.Health;
            }
            enemyHealth.Hit(damage);
            playerHealth.Hit(damage);
            yield return new WaitForSeconds(delay);
        }
    }
}
