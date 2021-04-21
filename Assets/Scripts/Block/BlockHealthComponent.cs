using UnityEngine.UI;
using UnityEngine;

public class BlockHealthComponent : HealthComponent
{
    [Tooltip("Set to the child text this tag if you need to display the current health")]
    [SerializeField] private string _tagName = "HealthText";

    public override float Health {
        get => _health;
        protected set {
            _health = value;

            // Set each Text child at this object the new value of hp
            var texts = gameObject.GetComponentsInChildren<Text>();
            foreach (var textComponent in texts) {
                if (textComponent.tag == _tagName) {
                    textComponent.text = ((int)_health).ToString();
                }
            }

            if (_health <= 0 && _isDestroyed) {
                Destroy(gameObject);
            }
        }
    }
}
