using System;

public class PlayerHealthComponent : HealthComponent
{
	public static event EventHandler<float> AmountOfDicreasedHealth;

	public override void Hit(float damage) {
		if (!IsDie) {
			base.Hit(damage);
			AmountOfDicreasedHealth?.Invoke(this, damage);
        }        
	}
}
