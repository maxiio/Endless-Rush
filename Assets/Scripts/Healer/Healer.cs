using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float _healthToHeal;
    [SerializeField] private string _playerTag = "Player";

    public float HealthToHeal { get => _healthToHeal; private set { } }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == _playerTag) {
            HealthComponent playerHealth = collider.gameObject.GetComponent<HealthComponent>();
            playerHealth.Heal(_healthToHeal);

            Destroy(gameObject);
        }
    }
}
