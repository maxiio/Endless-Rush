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

    private void Awake() {
        if (!gameObject.GetComponent<HealthComponent>()) {
            throw new Exception("This object doesn't has the HealthComponent");
        }
    }

    // Stop damage the both objects
    private void OnTriggerExit(Collider collider) {
        StopCoroutine("DamageObjects");
    }

    // Start damage the both objects
    private void OnTriggerEnter(Collider collider) {
        HealthComponent colliderHealth = collider.gameObject.GetComponent<HealthComponent>();
        HealthComponent gameObjectHealth = gameObject.GetComponent<HealthComponent>();

        if (colliderHealth) {
            float damage = CalculateDamage(colliderHealth.Health);
            float delay = CalculateDelay();
            StartCoroutine(DamageObjects(colliderHealth, gameObjectHealth, damage, delay));
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
