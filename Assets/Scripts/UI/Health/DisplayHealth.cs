using Core.Components.Health;
using UnityEngine;

namespace UI.Health {
	[ExecuteInEditMode]
	public class DisplayHealth : DisplayOnText {
		private HealthComponent _health;

		// Subscribe to object with health
		protected override void Awake() {
			base.Awake();
			_health = gameObject.GetComponentInParent<HealthComponent>();
			_health.CurrentHealth += UpdateText;
			UpdateText(this, _health.Health);
		}

		// Unsubscribe
		protected void OnDestroy() {
			_health.CurrentHealth -= UpdateText;
		}
	}
}