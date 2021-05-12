using GameCore.Components.Health;
using UnityEngine;

public class HealerHealth : HealthComponent
{
    [SerializeField] private string _playerTag = "Player";

    // No hits to healer
    public override void Hit(float damage) {}

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == _playerTag) {
            HealthComponent playerHealth = collider.gameObject.GetComponent<HealthComponent>();
            playerHealth.Heal(Health);

            DestroyObject(gameObject);
        }
    }
}
