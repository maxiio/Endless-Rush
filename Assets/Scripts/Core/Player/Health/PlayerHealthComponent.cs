using System;
using Core.Components.Health;

namespace Core.Player.Health {
	public class PlayerHealthComponent : HealthComponent {
		public static event EventHandler<float> AmountOfHealthToDecrease;

		public override void Hit(float damage) {
			if (!IsDie) {
				base.Hit(damage);
				AmountOfHealthToDecrease?.Invoke(this, damage);
			}
		}
	}
}