using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Damager : MonoBehaviour
{
    // The summary of damaging objects with any health
    [SerializeField] [Range(.1f, 10)] private float _hitSummaryTime = 1f;
    // The hit call per second for updating health value
    [SerializeField] [Range(1, 60)] private int _hitCallPerSecond = 10;

    // Add dictionary if we has multiple objects in trigger
    private Dictionary<int, IEnumerator<WaitForSeconds>> _coroutines;
        

    private void Awake() {
        _coroutines = new Dictionary<int, IEnumerator<WaitForSeconds>>();

        if (!gameObject.GetComponent<HealthComponent>()) {
            throw new Exception("This object doesn't has the HealthComponent");
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

            if (colliderHealth) {
                float damage = CalculateDamage(colliderHealth.Health);
                float delay = CalculateDelay();
                IEnumerator<WaitForSeconds> coroutine = DamageObjects(colliderHealth, gameObjectHealth, damage, delay);
                StartCoroutine(coroutine);
                _coroutines.Add(colliderHash, coroutine);
            }
        }            
    }

    private float CalculateDamage(float health) {
        return health / _hitSummaryTime / _hitCallPerSecond;
    }

    private float CalculateDelay() {
        return 1 / _hitCallPerSecond;
    }

    IEnumerator<WaitForSeconds> DamageObjects(HealthComponent enemyHealth, HealthComponent playerHealth
            , float damage, float delay) {
        while (enemyHealth && playerHealth) {
            enemyHealth.Hit(damage);
            playerHealth.Hit(damage);
            yield return new WaitForSeconds(delay);
        }
    }
}
