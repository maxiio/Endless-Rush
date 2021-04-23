using UnityEngine.UI;
using UnityEngine;

public class DisplayOnText : MonoBehaviour
{
    [Tooltip("Set to the child text this tag if you need to display the current health")]
    [SerializeField] protected string _tagName = "HealthText";

    protected Text[] _texts;

    // Find all text objects at childrens
    protected virtual void Awake() {
        _texts = gameObject.GetComponentsInChildren<Text>();
    }

    // Set to each text the current sender health
    protected void UpdateText(object sender, float value) {
        foreach (var textComponent in _texts) {
            if (textComponent.tag == _tagName) {
                textComponent.text = ((int)value).ToString();
            }
        }
    }
}