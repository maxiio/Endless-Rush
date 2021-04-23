using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class DisplayHealth : MonoBehaviour
{
    [Tooltip("Set to the child text this tag if you need to display the current health")]
    [SerializeField] private string _tagName = "HealthText";

    private Text[] _texts;
    private HealthComponent _healthComponent;

    private void Awake() {
        _texts = gameObject.GetComponentsInChildren<Text>();
        _healthComponent = gameObject.GetComponentInParent<HealthComponent>();
        UpdateHealth(this, _healthComponent.Health);
        _healthComponent.CurrentHealth += UpdateHealth;
    }

    private void OnDestroy() {
        _healthComponent.CurrentHealth -= UpdateHealth;
    }

    // Set to each text the current sender health
    private void UpdateHealth(object sender, float currentHealth) {
        foreach (var textComponent in _texts) {
            if (textComponent.tag == _tagName) {
                textComponent.text = ((int)currentHealth).ToString();
            }
        }
    }
}
